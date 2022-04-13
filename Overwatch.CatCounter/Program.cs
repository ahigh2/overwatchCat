using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Overwatch.CatCounter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging(opts => opts.AddConsole())
                .AddSingleton<IWordCounter, WordCounter>()
                .BuildServiceProvider();

            var wordService = serviceProvider.GetService<IWordCounter>();
        }
    }
}
