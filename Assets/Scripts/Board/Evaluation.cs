using System;

namespace Mastermind.Boards
{
    /// <summary>
    /// The result of a guess against a secret code.
    /// </summary>
    public class Evaluation : IEquatable<Evaluation>
    {
        public static Evaluation None => new Evaluation();

        public int Bulls { get; set; }
        public int Cows { get; set; }

        private Evaluation()
        {
        }

        public Evaluation(int bulls, int cows)
        {
            Bulls = bulls;
            Cows = cows;
        }

        public override int GetHashCode() => Bulls * 17 + Cows;

        public override string ToString()
        {
            return $"Bulls: {Bulls}, Cows: {Cows}";
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            if (obj == null)
                return false;
            if (obj is Evaluation evaluation)
                return Equals(evaluation);
            return false;
        }
        public bool Equals(Evaluation other)
        {
            if (other == null)
                return false;
            return other.Bulls == Bulls && other.Cows == Cows;
        }
    }
}