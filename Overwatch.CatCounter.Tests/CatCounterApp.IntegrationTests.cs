using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
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
        public async Task CatCounter_ShouldCountTextInput(SearchMode mode, int expectedCount)
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
    }
}
