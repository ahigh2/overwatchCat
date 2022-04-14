using System.IO;

namespace Overwatch.CatCounter
{
    public interface ITextFileReader
    {
        /// <summary>
        /// Opens a stream to read portions of a text file on disk.
        /// </summary>
        /// <param name="path">The path to the text file on disk.</param>
        /// <returns>a <see cref="StreamReader"/> to stream the text, which must be disposed after use.</returns>
        public StreamReader StreamTextFile(string path);
        /// <summary>
        /// Reads a given text file into memory.
        /// </summary>
        /// <param name="path">The path to the text file on disk.</param>
        /// <returns>The text file as a <see cref="string"/></returns>
        public string ReadTextFile(string path);
    }
}
