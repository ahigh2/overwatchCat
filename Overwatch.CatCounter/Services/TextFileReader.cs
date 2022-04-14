using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace Overwatch.CatCounter
{
    public class TextFileReader : ITextFileReader
    {
        private readonly ILogger<ITextFileReader> logger;
        public TextFileReader(ILogger<ITextFileReader> logger)
        {
            this.logger = logger;
        }

        public string ReadTextFile(string path)
        {
            if (File.Exists(path) == false)
            {
                logger.LogError($"Unable to locate file at {path}");
                return null;
            }

            using StreamReader streamReader = new StreamReader(path);
            string text = streamReader.ReadToEnd();
            return text;
        }

        public StreamReader StreamTextFile(string path)
        {
            if (File.Exists(path) == false)
            {
                logger.LogError($"Unable to locate file at {path}");
                return null;
            }

            StreamReader streamReader = new StreamReader(path);
            return streamReader;
        }
    }
}
