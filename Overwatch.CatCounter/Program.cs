using CommandLine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Overwatch.CatCounter
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            ServiceProvider serviceProvider = ConfigureServices(new ServiceCollection()).BuildServiceProvider();
            var wordService = serviceProvider.GetService<IWordCounter>();

            await Parser.Default.ParseArguments<CounterParameters>(args).MapResult(async (CounterParameters opts) =>
            {
                var app = serviceProvider.GetRequiredService<ICatCounterApp>();
                var result = await app.Run(opts);
                return result.ExitCode;
            }, errors => Task.FromResult(-1));
        }

        public static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            services
                .AddLogging(opts =>
                {
                    LogLevel logLevel = config.GetValue<LogLevel>("Logging.LogLevel.Default");
                    opts.AddConsole()
                    .SetMinimumLevel(logLevel);
                })
                .AddSingleton<ICatCounterApp, CatCounterApp>()
                .AddSingleton<IWordCounter, WordCounter>()
                .AddSingleton<ICounterParameters, CounterParameters>()
                .AddTransient<ITextFileReader, TextFileReader>()
                .BuildServiceProvider();

            services.AddSingleton(config);
            return services;
        }
    }
}
