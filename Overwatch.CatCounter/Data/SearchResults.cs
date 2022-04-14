namespace Overwatch.CatCounter
{
    public struct SearchResults
    {
        /// <summary>
        /// The numbe of occurrences.
        /// </summary>
        public int Count;
        /// <summary>
        /// The terminal status of the application.
        /// </summary>
        public int ExitCode;

        public SearchResults(int count, int exitCode)
        {
            Count = count;
            ExitCode = exitCode;
        }
    }
}
