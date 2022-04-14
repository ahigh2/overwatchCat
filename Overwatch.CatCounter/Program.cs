using CommandLine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Overwatch.CatCounter
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ServiceProvider serviceProvider = ConfigureServices(new ServiceCollection()).BuildServiceProvider();
            var wordService = serviceProvider.GetService<IWordCounter>();

            await Parser.Default.ParseArguments<CounterParameters>(args).MapResult(async (CounterParameters opts) =>
            {
                var app = serviceProvider.GetRequiredService<ICatCounterApp>();
                return await app.Run(opts);
            }, errors => Task.FromResult(-1));
        }

        private static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            services
                .AddLogging(opts => opts.AddConsole())
                .AddSingleton<ICatCounterApp, CatCounterApp>()
                .AddSingleton<IWordCounter, WordCounter>()
                .AddSingleton<ICounterParameters, CounterParameters>()
                .AddTransient<ITextFileReader, TextFileReader>()
                .BuildServiceProvider();

            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            services.AddSingleton(config);
            return services;
        }
    }
}
