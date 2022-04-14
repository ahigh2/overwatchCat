using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;

namespace Overwatch.CatCounter.Tests
{
    public class WordCounterTests
    {
        private const string word = "cat";
        private IWordCounter wordCounter;

        [SetUp]
        public void Setup()
        {
            ILogger<IWordCounter> logger = Substitute.For<ILogger<IWordCounter>>();
            wordCounter = new WordCounter(logger);
        }

        [TestCase(word, "cat", SearchMode.Strict, 1)]
        [TestCase(word, "cat", SearchMode.Lax, 1)]
        [TestCase(word, "the quick brown cat", SearchMode.Strict, 1)]
        [TestCase(word, "the quick brown cat", SearchMode.Lax, 1)]
        [TestCase(word, "the quick brown cat found the slow green caterpillar", SearchMode.Strict, 1)]
        [TestCase(word, "the quick brown cat found the slow green caterpillar", SearchMode.Lax, 2)]
        [TestCase(word, "the quick brown cat found the slow green caterpillar and the orange cat", SearchMode.Lax, 3)]
        [TestCase(word, "the shaggy brown dog", SearchMode.Strict, 0)]
        [TestCase(word, "the shaggy brown dog", SearchMode.Lax, 0)]
        [TestCase(word, " ", SearchMode.Lax, 0)]
        [TestCase(" ", " ", SearchMode.Lax, 0)]
        public void WordCounter_ShouldCountWords_WhenGivenProperParams(string word, string text, SearchMode mode, int expectedResult)
        {
            wordCounter.CountWords(mode, word, text).Should().Be(expectedResult);
        }
    }
}