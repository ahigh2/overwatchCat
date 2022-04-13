namespace Overwatch.CatCounter
{
    /// <summary>
    /// The type of search to be performed
    /// </summary>
    public enum SearchMode
    {
        /// <summary>
        /// Find the word exactly, not as a prefix or any part of another word or space delimited character array.
        /// "cat" would be found in "the quick brown cat" but not "catapult" or "caterpillar".
        /// </summary>
        Strict,
        /// <summary>
        /// Find occurrences of the word as a character array, i.e. as a prefix or part of another non-space-delimited character array.
        /// "cat" would be found in "caterpillar", "catapult", and "the quick brown cat"
        /// </summary>
        Lax
    }
}
