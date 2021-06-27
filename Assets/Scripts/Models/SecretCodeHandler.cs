using Mastermind.Boards;

namespace Mastermind
{
    /// <summary>
    /// Handler to cache the secret code and evaluate the guesses/combinations against it.
    /// </summary>
    public class SecretCodeHandler
    {
        private readonly Combination secretCode;
        public SecretCodeHandler(Combination secretCode)
        {
            this.secretCode = secretCode;
        }

        public Evaluation EvaluateCombination(Combination guess)
        {
            return Utils.EvaluateCombination(secretCode, guess);
        }
    }
}