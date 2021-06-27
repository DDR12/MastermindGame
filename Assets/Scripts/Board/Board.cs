using System;
using System.Collections.Generic;

namespace Mastermind.Boards
{
    /// <summary>
    /// The game board, stores combinations of every turn and the result of these combinations.
    /// </summary>
    public class Board
    {
        public delegate void TurnChangeEventHandler(uint turn, uint maxTurns);
        public static event TurnChangeEventHandler OnTurnChanged;

        public uint CurrentTurn { get; private set; }

        private readonly Dictionary<uint, Combination> combinations;
        private readonly Dictionary<uint, Evaluation> evaluations;
        private readonly uint maxTurns;

        public Board(uint maxTurns)
        {
            combinations = new Dictionary<uint, Combination>();
            evaluations = new Dictionary<uint, Evaluation>();

            if (maxTurns <= 0)
                throw new ArgumentException($"Invalid argument Max turns = {maxTurns}, must be > 0");
            this.maxTurns = maxTurns;
            NextTurn();
        }

        public void NextTurn()
        {
            if(CurrentTurn >= maxTurns)
            {
                return;
            }
            CurrentTurn++;
            OnTurnChanged?.Invoke(CurrentTurn, maxTurns);
        }
        public bool IsMaxTurnsReached() => CurrentTurn >= maxTurns;
        public void AddNextCombination(Combination combination, Evaluation evaluation)
        {
            if (combination is null)
            {
                throw new ArgumentNullException(nameof(combination));
            }

            if (evaluation is null)
            {
                throw new ArgumentNullException(nameof(evaluation));
            }

            combinations.Add(CurrentTurn, combination);
            evaluations.Add(CurrentTurn, evaluation);
        }

        public Combination GetLastGuess()
        {
            if (combinations.TryGetValue(CurrentTurn - 1, out Combination lastGuess))
                return lastGuess;
            throw new Exception($"Can't find guess of last turn, doesn't exist.");
        }
        public Evaluation GetLastEvaluation()
        {
            if (evaluations.TryGetValue(CurrentTurn - 1, out Evaluation evaluation))
                return evaluation;
            throw new Exception($"Can't find evaluation of last turn, doesn't exist.");
        }
    }
}