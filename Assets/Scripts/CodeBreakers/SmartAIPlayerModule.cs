using Mastermind.Boards;
using System.Collections.Generic;

namespace Mastermind.CodeBreakers
{
    /// <summary>
    /// An AI version that plays by the process of elimination and by studying its previous guesses and their results.
    /// </summary>
    public class SmartAIPlayerModule : BasePlayerModule
    {
        public delegate void SmartAIThinkEventHandler(List<Combination> remainingGuesses, Combination lastGuess,
            Evaluation lastEvaluation, Combination newestGuess, uint turn);

        public static event SmartAIThinkEventHandler OnSmartAIThought;

        private List<Combination> viableSecrets = null;

        public SmartAIPlayerModule() : base()
        {

        }


        private List<Combination> GetNextTokenForCombo(List<byte> tokens)
        {
            List<Combination> allCombos = new List<Combination>();
            if(tokens.Count == 4)
            {
                allCombos.Add(new Combination(tokens));
                return allCombos;
            }

            // Add all possible token for each non-duplicated new value to
            // the existing incomplete combination
            foreach (byte token in Constants.PossibleTokens)
            {
                if (tokens.Contains(token))
                    continue;
                List<byte> newList = new List<byte>();
                newList.AddRange(tokens);
                newList.Add(token);
                allCombos.AddRange(GetNextTokenForCombo(newList));
            }
            return allCombos;
        }
        private Combination DetermineNextGuess()
        {
            // Just take the first viable guess after we eliminated all bad ones based on last guess.
            if (viableSecrets.Count == 0)
                throw new System.Exception($"No more viable secret codes!");
            return viableSecrets[0];
        }
        private void ReduceGuesses(Board board)
        {
            Combination lastGuess = board.GetLastGuess();
            Evaluation lastEval = board.GetLastEvaluation();

            // Based on last guess and its result, eliminate bad future combinations.
            for (int i = viableSecrets.Count - 1; i >= 0; i--)
            {
                var code = viableSecrets[i];
                Evaluation eval = Utils.EvaluateCombination(code, lastGuess);
                if (eval.Equals(lastEval) == false)
                    viableSecrets.RemoveAt(i);
            }

            OnSmartAIThought?.Invoke(viableSecrets, lastGuess, lastEval, DetermineNextGuess(), board.CurrentTurn);
        }
        protected override Combination FormulateGuessInternal(Board board)
        {
            // Base guess at the start of the game, can be any 4 digits.
            if (board.CurrentTurn == 1)
            {
                Combination baseGuess = new Combination(new byte[] { 1, 2, 3, 4 });
                OnSmartAIThought?.Invoke(viableSecrets, Combination.None, Evaluation.None, baseGuess, board.CurrentTurn);
                return baseGuess;
            }
            ReduceGuesses(board);
            return DetermineNextGuess();
        }



        public override void Initialize()
        {
            viableSecrets = GetNextTokenForCombo(new List<byte>());
        }

    }
}
