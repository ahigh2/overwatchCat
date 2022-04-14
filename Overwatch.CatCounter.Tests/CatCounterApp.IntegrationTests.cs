using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Overwatch.CatCounter.Tests
{
    public class CatCounterApp
    {
        private const string searchTerm = "cat";
        private ICatCounterApp catApp;

        [SetUp]
        public void Setup()
        {
            var services = Program.ConfigureServices(new ServiceCollection());
            var serviceProvider = services.BuildServiceProvider();
            catApp = serviceProvider.GetService<ICatCounterApp>();
        }

        [TestCase(SearchMode.Lax, 31)]
        [TestCase(SearchMode.Strict, 16)]
        public async Task CatCounter_ShouldCountFileInput(SearchMode mode, int expectedCount)
        {
            // Arrange
            ICounterParameters counterParameters = new CounterParameters()
            {
                Mode = mode,
                SearchTerm = searchTerm,
                Path = @"c:\temp\catInTheHat.txt"
            };

            // Act
            SearchResults results = await catApp.Run(counterParameters);

            // Assert
            results.Count.Should().Be(expectedCount);
        }

        [TestCase(SearchMode.Lax, "the cat in the catcher's hat", 2)]
        [TestCase(SearchMode.Strict, "Catcher in the Rye and Cat in the Hat", 1)]
        public async Task CatCounter_ShouldCountTextInput(SearchMode mode, string textInput, int expectedCount)
        {
            // Arrange
            ICounterParameters counterParameters = new CounterParameters()
            {
                Mode = mode,
                SearchTerm = searchTerm,
                Text = textInput
            };

            // Act
            SearchResults results = await catApp.Run(counterParameters);

            // Assert
            results.Count.Should().Be(expectedCount);
        }

        [Test]
        public async Task CatCounter_ShouldPreferValidFilePath()
        {
            // Arrange
            ICounterParameters counterParameters = new CounterParameters()
            {
                Mode = SearchMode.Lax,
                SearchTerm = searchTerm,
                Text = "cat",
                Path = @"c:\temp\catInTheHat.txt"
            };

            // Act
            SearchResults results = await catApp.Run(counterParameters);

            // Assert
            results.Count.Should().BeGreaterThan(1);
        }

        [Test]
        public async Task CatCounter_ShouldFallbackToText_OnBadPath()
        {
            // Arrange
            ICounterParameters counterParameters = new CounterParameters()
            {
                Mode = SearchMode.Lax,
                SearchTerm = searchTerm,
                Text = "cat",
                Path = @"x:\fakePath\fake.txt"
            };

            // Act
            SearchResults results = await catApp.Run(counterParameters);

            // Assert
            results.Count.Should().Be(1);
        }
    }
}
