using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Overwatch.CatCounter
{
    public class WordCounter : IWordCounter
    {
        private readonly ILogger<IWordCounter> logger;

        public WordCounter(ILogger<IWordCounter> logger)
        {
            this.logger = logger;
        }

        public int CountWords(SearchMode searchMode, string word, string text)
        {
            // Throw out invalid values.
            if (word == null ||
                text == null ||
                string.IsNullOrWhiteSpace(word) ||
                string.IsNullOrWhiteSpace(text) ||
                text.Length < word.Length)
            {
                return 0;
            }

            switch (searchMode)
            {
                case SearchMode.Strict:
                    {
                        return GetWordCountStrict(word, text);
                    }
                case SearchMode.Lax:
                    {
                        return GetWordCountLax(word, text);
                    }
                default:
                    {
                        throw new NotImplementedException();
                    }
            }
        }

        private int GetWordCountStrict(string word, string text)
        {
            int count = 0;
            // Remove all non-alpha characters so that they don't impact a strict search (i.e. numerals, punctuation).
            string trimmedText = new string(text.Where(c => char.IsLetter(c) || char.IsWhiteSpace(c)).ToArray());

            string[] words = trimmedText.Split(' ');
            foreach (string splitWord in words)
            {
                if (word.Equals(splitWord, StringComparison.OrdinalIgnoreCase))
                {
                    count++;
                }
            }

            return count;
        }

        private int GetWordCountLax(string word, string text)
        {
            int count = 0;

            string[] words = text.Split(' ');
            foreach (string splitWord in words)
            {
                for (int i = 0; i < splitWord.Length; i += word.Length)
                {
                    // If the word is shorter than the text or the text window is shorter than the word length, skip.
                    if (splitWord.Length < word.Length ||
                        i + word.Length > splitWord.Length)
                    {
                        continue;
                    }

                    string testWord = splitWord.Substring(i, word.Length);
                    if (testWord.Equals(word, StringComparison.OrdinalIgnoreCase))
                    {
                        count++;
                    }
                }
            }

            return count;
        }
    }
}
