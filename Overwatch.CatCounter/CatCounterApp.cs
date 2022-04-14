﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace Overwatch.CatCounter
{
    internal class CatCounterApp : ICatCounterApp
    {
        private readonly ILogger<ICatCounterApp> logger;
        private readonly ITextFileReader textReader;
        private readonly IWordCounter wordCounter;
        private readonly IConfiguration configuration;
        private ICounterParameters options;

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

        public async Task<int> Run(ICounterParameters options)
        {
            this.options = options;
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
                    using var stream = textReader.StreamTextFile(options.Path);

                    // Divide the file into 2048 byte segments
                    const int chunkSize = 2048;
                    for (int i = 0; i < fileInfo.Length; i += chunkSize)
                    {
                        char[] buffer = new char[chunkSize];
                        await stream.ReadBlockAsync(buffer, 0, chunkSize);
                    }
                }
                else
                {
                    string text = textReader.ReadTextFile(options.Path);
                    count = wordCounter.CountWords(options.Mode, options.SearchTerm, text);
                }
            }
            // Fall back to a text string if that was provided and the input path doesn't exist or wasn't provided
            else if (string.IsNullOrEmpty(options.Text) == false)
            {
                count = wordCounter.CountWords(options.Mode, options.SearchTerm, options.Text);
            }

            logger.LogInformation($"Found the term '{options.SearchTerm}' {count} times.");
            return 0;
        }
    }

    internal interface ICatCounterApp
    {
        /// <summary>
        /// Runs the application to count instances of the word 'cat'.
        /// </summary>
        /// <param name="options">The options for the job.</param>
        /// <returns>An exit code.</returns>
        Task<int> Run(ICounterParameters options);
    }
}