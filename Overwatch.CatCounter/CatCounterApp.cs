﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace Overwatch.CatCounter
{
    public class CatCounterApp : ICatCounterApp
    {
        private readonly ILogger<ICatCounterApp> logger;
        private readonly ITextFileReader textReader;
        private readonly IWordCounter wordCounter;
        private readonly IConfiguration configuration;

        public CatCounterApp(
            ILogger<ICatCounterApp> logger,
            ITextFileReader textReader,
            IWordCounter wordCounter,
            IConfiguration configuration)
        {
            this.logger = logger;
            this.textReader = textReader;
            this.wordCounter = wordCounter;
            this.configuration = configuration;
        }

        public async Task<SearchResults> Run(ICounterParameters options)
        {
            int count = 0;

            // Favor a file path if provided
            if (File.Exists(options.Path))
            {
                FileInfo fileInfo = new FileInfo(options.Path);
                int maxFileSize = configuration.GetValue<int>("maximumReadFileSizeInBytes");
                // Stream the file, it's too big to read directly.
                // Note that this is a pretty arbitrarily small limit imposed here just for illustrative purposes.
                if (fileInfo.Length > maxFileSize)
                {
                    logger.LogInformation($"Dividing a {fileInfo.Length} byte file into {fileInfo.Length / maxFileSize} {maxFileSize} byte segments for streaming.");
                    using var stream = textReader.StreamTextFile(options.Path);

                    // Divide the file into the maximum file read byte number of segments
                    count = await ReadStreamInChunks(stream, options, fileInfo.Length, count, maxFileSize);
                }
                else
                {
                    logger.LogInformation($"Reading a {fileInfo.Length} byte file into memory.");
                    string text = textReader.ReadTextFile(options.Path);
                    count = wordCounter.CountWords(options.Mode, options.SearchTerm, text);
                }
            }
            // Fall back to a text string if that was provided and the input path doesn't exist or wasn't provided
            else if (string.IsNullOrEmpty(options.Text) == false)
            {
                count = wordCounter.CountWords(options.Mode, options.SearchTerm, options.Text);
            }

            logger.LogInformation($"Found the term '{options.SearchTerm}' {count} times using a {options.Mode} method.");
            return new SearchResults(count, 0);
        }

        private async Task<int> ReadStreamInChunks(
            StreamReader stream,
            ICounterParameters options,
            long fileSizeInBytes,
            int count,
            int chunkSize)
        {
            // Oversize the last buffer by the max file size to ensure we don't truncate any characters
            for (int i = 0; i <= fileSizeInBytes + chunkSize; i += chunkSize)
            {
                char[] buffer = new char[chunkSize];
                int bytesRead = await stream.ReadBlockAsync(buffer, 0, chunkSize);
                string textChunk = new string(buffer);
                count += wordCounter.CountWords(options.Mode, options.SearchTerm, textChunk);

                // EOF
                if (bytesRead == 0)
                {
                    break;
                }
            }

            return count;
        }
    }

    public interface ICatCounterApp
    {
        /// <summary>
        /// Runs the application to count instances of the word 'cat'.
        /// </summary>
        /// <param name="options">The options for the job.</param>
        /// <returns>An exit code.</returns>
        Task<SearchResults> Run(ICounterParameters options);
    }
}
