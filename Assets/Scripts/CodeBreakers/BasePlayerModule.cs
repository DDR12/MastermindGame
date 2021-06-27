using Mastermind.Boards;

namespace Mastermind.CodeBreakers
{
    /// <summary>
    /// Basic class for any code breaker module, should derive from this to introduce new types of breakers.
    /// </summary>
    public abstract class BasePlayerModule
    {
        protected abstract Combination FormulateGuessInternal(Board board);
        public abstract void Initialize();
        public Combination FormulateGuess(Board board)
        {
            Combination combo = FormulateGuessInternal(board);
            return combo;
        }
    }
}
