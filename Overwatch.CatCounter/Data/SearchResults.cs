namespace Overwatch.CatCounter
{
    /// <summary>
    /// The results of the operation, including count and exit code.
    /// </summary>
    public struct SearchResults
    {
        /// <summary>
        /// The number of occurrences.
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
