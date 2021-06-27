using Mastermind.Boards;

namespace Mastermind
{
    /// <summary>
    /// A class representing the result of the latest turn for use in UI display.
    /// </summary>
    public class GameTurnResult
    {
        public uint Turn { get; set; }
        public Combination Guess { get; set; }
        public Evaluation GuessResult { get; set; }
    }
}