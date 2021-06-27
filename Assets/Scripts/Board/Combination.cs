using System;
using System.Collections.Generic;
using System.Linq;

namespace Mastermind.Boards
{
    /// <summary>
    /// A wrapper to store the array of digits used to represent a guess.
    /// </summary>
    public class Combination : IEquatable<Combination>
    {
        public static Combination None => new Combination(Array.Empty<byte>());

        public List<byte> Tokens { get; set; }

        private Combination()
        {
        }

        public Combination(IEnumerable<byte> tokens)
        {
            Tokens = new List<byte>(tokens);
        }

        private bool IsValidIndex(int index)
        {
            return Tokens != null && index >= 0 && index < Tokens.Count;
        }
        public override string ToString()
        {
            return string.Join(" ", Tokens.Select(token => token.ToString()));
        }

        public bool ContainsTokenAtIndex(byte token, int index)
        {
            if (!IsValidIndex(index))
                return false;
            return Tokens[index].Equals(token);
        }

        public bool ContainsToken(byte token) => Tokens != null && Tokens.Any(o => o.Equals(token));


        public bool Equals(Combination combination)
        {
            if (combination == null)
                return false;
            if (combination.Tokens == null && Tokens == null)
                return true;
            if (combination.Tokens.Count != Tokens.Count)
                return false;
            for(int i = 0; i < Tokens.Count;i++)
            {
                if (!Tokens[i].Equals(combination.Tokens[i]))
                    return false;
            }
            return true;
        }
    }
}