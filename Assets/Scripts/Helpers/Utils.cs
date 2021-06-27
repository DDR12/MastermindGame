using Mastermind.Boards;
using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace Mastermind
{
    /// <summary>
    /// Helper functions accessible from anywhere.
    /// </summary>
    public class Utils
    {
        public static Combination GenerateRandomCombination(bool allowDuplication)
        {
            List<byte> tokens = new List<byte>(10);
            List<byte> pickables = new List<byte>(Constants.PossibleTokens);
            for(int i = 0; i < 4; i++)
            {
                int index = Random.Range(0, pickables.Count);
                tokens.Add(pickables[index]);
                if (!allowDuplication)
                    pickables.RemoveAt(index);
            }
            return new Combination(tokens);
        }


        public static Evaluation EvaluateCombination(Combination secretCode, Combination guess)
        {
            int bulls = 0, cows = 0;
            for(int i = 0; i < secretCode.Tokens.Count; i++)
            {
                byte token = secretCode.Tokens[i];
                if (guess.ContainsTokenAtIndex(token, i))
                    bulls++;
                else if (guess.ContainsToken(token))
                    cows++;
            }
            return new Evaluation(bulls, cows);
        }


        public static string WrapInSizeTag(string text, float size)
        {
            return $"<size={size}>{text}</size>";
        }

    }
}