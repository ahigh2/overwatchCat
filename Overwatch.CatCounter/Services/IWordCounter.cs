namespace Overwatch.CatCounter
{
    public interface IWordCounter
    {
        /// <summary>
        /// Counts the number of occurrences of a given <paramref name="word"/> in a  based on the <paramref name="searchMode"/>
        /// </summary>
        /// <param name="searchMode">How the search will be performed.</param>
        /// <param name="word">The word to be located.</param>
        /// <param name="text">The text to be searched.</param>
        /// <returns>A count of the number of occurrences of the word within the text.</returns>
        int CountWords(SearchMode searchMode, string word, string text);
    }
}
