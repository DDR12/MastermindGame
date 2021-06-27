using Mastermind.Boards;
using Mastermind.Enums;
using Mastermind.CodeBreakers;
using Mastermind.ViewModels;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Mastermind
{
    /// <summary>
    /// Handles all game UI states, starts and restarts the game.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        #region Singleton Pattern
        public static GameManager Instance { get; private set; }
        #endregion

        [Header("Basics")]
        [SerializeField]
        private GameSettings settingsFile = null;
        [SerializeField]
        private CodeMaker codeMaker = null;
        [SerializeField]
        private AlgorithmWalkthrough algorithmVisualizer = null;

        [Header("Game State Panels")]
        [SerializeField]
        private CanvasGroup startGamePanel = null;
        [SerializeField]
        private CanvasGroup guidePanel = null, duringGamePanel = null;
        
        [Header("Buttons")]
        [SerializeField]
        private Button startGameButton = null;
        [SerializeField]
        private Button resetGameButton = null;
        [Header("View Models")]
        [SerializeField]
        private TurnResultUIViewModel turnResultPrefab = null;

        [SerializeField]
        private TextMeshProUGUI currentTurnText = null, secretCodeText = null;

        public bool AllowDuplication => settingsFile.AllowDuplication;



        private MastermindGame currentGame = null;
        private ObjectPool<TurnResultUIViewModel> turnResultsRowsPool = null;

        #region Mono-Callbacks
        private void Start()
        {
            ValidateGameRequiredFields();
            InitializeUIElements();
        }
        private void Awake()
        {
            Instance = this;
            turnResultsRowsPool = new ObjectPool<TurnResultUIViewModel>(turnResultPrefab);
        }
        private void OnEnable()
        {
            Board.OnTurnChanged += OnTurnChanged;
        }

        private void OnDisable()
        {
            Board.OnTurnChanged -= OnTurnChanged;
        }
        private void Update()
        {
            // A game is in-progress, can't start a new one by key/mouse press.
            if (currentGame != null && currentGame.IsGameOver == false)
                return;
            // Already showed the start game panel.
            if (startGamePanel.interactable)
                return;
            if (Input.anyKeyDown)
            {
                UpdateUIBasedOnGameStatus(GameStatus.SettingCode);
            }
        }
        #endregion

        private void InitializeUIElements()
        {
            startGameButton.onClick.AddListener(StartNewGame);
            resetGameButton.onClick.AddListener(() => SceneManager.LoadScene(0));
            UpdateUIBasedOnGameStatus(GameStatus.Guide);
        }
        private void SetUIPanelVisibility(CanvasGroup panel, bool visible)
        {
            if (panel == null)
                return;
            panel.alpha = visible ? 1 : 0;
            panel.interactable = panel.blocksRaycasts = visible;
        }
        private void ValidateGameRequiredFields()
        {
            if (settingsFile == null)
                throw new Exception($"Unable to start game without assigning a game settings file.");
            if(codeMaker == null)
                throw new Exception($"Unable to start game without assigning a code maker.");
        }
        private void UpdateUIBasedOnGameStatus(GameStatus gameStatus)
        {
            SetUIPanelVisibility(guidePanel, gameStatus == GameStatus.Guide);
            SetUIPanelVisibility(startGamePanel, gameStatus == GameStatus.SettingCode);
            
            // In-progress UI stays visible after game over to still see the track.
            SetUIPanelVisibility(duringGamePanel, gameStatus >= GameStatus.InProgress);
            resetGameButton.gameObject.SetActive(gameStatus == GameStatus.Over);
        }

        private IEnumerator SimulateGameRoutine()
        {
            while (true)
            {
                var result = currentGame.PlayNextTurn(out GameTurnResult turnResult);
                DisplayTurnResult(turnResult);
                if (result != NextTurnGameResult.Nothing)
                {
                    UpdateUIBasedOnGameStatus(GameStatus.Over);
                    break;
                }
                yield return new WaitForSeconds(settingsFile.CodeBreakerDelayBetweenTurns);
            }
        }


        #region Game callbacks
        private void OnTurnChanged(uint turn, uint maxTurns)
        {
            currentTurnText.text = $"Turn {turn}/{maxTurns}";
        }
        private void DisplayTurnResult(GameTurnResult turnResult)
        {
            if (turnResult == null)
                return;
            if (turnResultPrefab == null)
                return;
            var item = turnResultsRowsPool.GetNewItem();
            item.Repaint(turnResult);
        }
        #endregion

        #region Player Initiated Actions
        public void StartNewGame()
        {
            StartNewGame(settingsFile.CodeBreakerType);
        }
        public void StartNewGame(OpponentType codeBreakerType)
        {
            BasePlayerModule codeBreaker = null;
            switch (codeBreakerType)
            {
                case OpponentType.StupidAI:
                    codeBreaker = new StupidAIModule();
                    break;
                case OpponentType.SmartAI:
                    codeBreaker = new SmartAIPlayerModule();
                    break;
                default:
                    break;
            }

            if (codeBreaker == null) // In case of an unsupported type.
                return;
            var codeMakerError = codeMaker.GetCode(out Combination secretCode);
            if(codeMakerError != ProcessInputCodeMakerCodeError.None)
            {
                Debug.LogError($"Codemaker error: {codeMakerError}");
                return;
            }

            algorithmVisualizer.gameObject.SetActive(codeBreakerType == OpponentType.SmartAI && settingsFile.ShowAlgorithmVisualizer);

            secretCodeText.text = $"<color=white>Secret Code</color> {string.Join(" ", secretCode.Tokens)}";
            UpdateUIBasedOnGameStatus(GameStatus.InProgress);
            currentGame = new MastermindGame(settingsFile.MaxTurns, secretCode, codeBreaker);
            StartCoroutine(SimulateGameRoutine());
        }


        #endregion

    }

}