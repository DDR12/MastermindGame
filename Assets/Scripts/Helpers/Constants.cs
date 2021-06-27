namespace Mastermind
{
    /// <summary>
    /// Constants of the game live here.
    /// </summary>
    public static class Constants
    {
        public static readonly byte[] PossibleTokens = null;

        static Constants()
        {
            PossibleTokens = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        }
    }
}