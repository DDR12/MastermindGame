using Mastermind.Enums;
using UnityEngine;

namespace Mastermind
{
    /// <summary>
    /// A game settings provider, the project can have multiple settings provider files but only one is usable during a game.
    /// </summary>
    [CreateAssetMenu(fileName ="NewGameSettings.asset", menuName ="Mastermind/Create New Game Settings")]
    public class GameSettings : ScriptableObject
    {
        [SerializeField]
        private uint maxTurns = 10;
        [SerializeField]
        private OpponentType codeBreakerType = OpponentType.SmartAI;
        [SerializeField]
        private bool allowDuplicationInCode = false, showAlgorithmVisualizer = false;
        [SerializeField]
        private float codeBreakerDelayBetweenTurns = 2;


        public uint MaxTurns => maxTurns;
        public OpponentType CodeBreakerType => codeBreakerType;
        public bool AllowDuplication => allowDuplicationInCode;
        public bool ShowAlgorithmVisualizer => showAlgorithmVisualizer;
        public float CodeBreakerDelayBetweenTurns => codeBreakerDelayBetweenTurns;
    }

}