using Mastermind.Boards;
using Mastermind.Enums;
using Mastermind.CodeBreakers;
using UnityEngine;

namespace Mastermind
{
    /// <summary>
    /// A mastermind game, an instance of this class represents a new game.
    /// </summary>
    public class MastermindGame
    {
        private SecretCodeHandler secretCodeHandler = null;
        private BasePlayerModule codeBreaker = null;

        public Board Board { get; } = null;
        public bool IsGameOver { get; private set; }

        public MastermindGame(uint maxTurns, Combination secretCode, BasePlayerModule codeBreaker)
        {
            this.Board = new Board(maxTurns);
            this.secretCodeHandler = new SecretCodeHandler(secretCode);
            this.codeBreaker = codeBreaker;
            codeBreaker.Initialize();
        }


        public NextTurnGameResult PlayNextTurn(out GameTurnResult turnResult)
        {
            turnResult = null;
            if (IsGameOver)
                return NextTurnGameResult.GameOver;

            // Make a guess.
            Combination turnGuess = codeBreaker.FormulateGuess(Board);
            // Check the guess.
            Evaluation evaluation = secretCodeHandler.EvaluateCombination(turnGuess);
            turnResult = new GameTurnResult() { Guess = turnGuess, GuessResult = evaluation, Turn = Board.CurrentTurn };
            if (evaluation.Bulls == 4)
            {
                return NextTurnGameResult.GameWon;
            }
            Board.AddNextCombination(turnGuess, evaluation);
            if (Board.IsMaxTurnsReached())
            {
                IsGameOver = true;
                return NextTurnGameResult.GameOver;
            }
            Board.NextTurn();
            return NextTurnGameResult.Nothing;
        }
    }
}