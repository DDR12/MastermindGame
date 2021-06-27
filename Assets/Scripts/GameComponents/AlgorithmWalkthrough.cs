using Mastermind.Boards;
using Mastermind.CodeBreakers;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
namespace Mastermind
{
    /// <summary>
    /// Used to visualize the AI playing, useful only for Opponent type <see cref="Enums.OpponentType.SmartAI"/>
    /// </summary>
    public class AlgorithmWalkthrough : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI remainingCombinationsText = null, lastGuessText = null, lastGuessResultText = null, newestGuessText = null, remainingGuessCountText = null;


        private void OnEnable()
        {
            SmartAIPlayerModule.OnSmartAIThought += OnSmartAIThought;
        }


        private void OnDisable()
        {
            SmartAIPlayerModule.OnSmartAIThought -= OnSmartAIThought;
        }

        private void OnSmartAIThought(List<Combination> remainingGuesses, Combination lastGuess, Evaluation lastEvaluation, Combination newestGuess, uint turn)
        {
            lastGuessText.text = $"Prev. Guess: {lastGuess}";
            lastGuessResultText.text = $"Prev Result: {lastEvaluation}";
            newestGuessText.text = $"Current Guess: {newestGuess}";
            remainingGuessCountText.text = $"Remaining: <color=yellow>{remainingGuesses.Count}</color> Combinations";

            remainingCombinationsText.text = string.Join("\n", remainingGuesses.Select(o => o.ToString()));
        }

    }
}