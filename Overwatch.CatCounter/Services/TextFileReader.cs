using System.IO;

namespace Overwatch.CatCounter
{
    public class TextFileReader : ITextFileReader
    {
        public string ReadTextFile(string path)
        {
            if (File.Exists(path) == false)
                return null;

            using StreamReader streamReader = new StreamReader(path);
            string text = streamReader.ReadToEnd();
            return text;
        }

        public StreamReader StreamTextFile(string path)
        {
            if (File.Exists(path) == false)
                return null;

            StreamReader streamReader = new StreamReader(path);
            return streamReader;
        }
    }
}
