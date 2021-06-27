using Mastermind.Boards;

namespace Mastermind.CodeBreakers
{
    /// <summary>
    /// An AI version that just randomly guesses every turn.
    /// </summary>
    public class StupidAIModule : BasePlayerModule
    {
        protected override Combination FormulateGuessInternal(Board board)
        {
            return Utils.GenerateRandomCombination(GameManager.Instance.AllowDuplication);
        }
        public override void Initialize()
        {

        }
    }
}
