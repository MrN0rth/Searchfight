using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Searchfight.Common;
using Searchfight.Configuration;
using Searchfight.Domain;

namespace Searchfight
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //args = new[] { ".net", "java", "c++", "pascal", "python", "ruby", "js" };

            var topics = args.Distinct();
            IServiceProvider serviceProvider = ConfigureProvider();

            var judge = serviceProvider.GetService<ISearchfightJudge>();
            var results = await judge.GetResults(topics);
            PrintResults(results, args);
            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
        }

        private static void PrintResults(SearchfightResult results, string[] topics)
        {
            Console.WriteLine("Results per topic");
            foreach (var topic in topics)
            {
                Console.WriteLine($"{topic}:");
                foreach (SearcherResult searcherResult in results.GeneralResults)
                {
                    Console.WriteLine($"    {searcherResult.Name}: {searcherResult.TopicResults[topic]}");
                }
            }

            Console.WriteLine("Winners by searchers:");
            foreach (var searcherWinner in results.SearcherWinners)
            {
                Console.WriteLine($"    {searcherWinner.Name}: {searcherWinner.Topic} ({searcherWinner.NumberOfResults} results)");
            }

            Console.WriteLine("Total winner:");

            if (results.TotalWinners.Count() > 1)
            {
                Console.WriteLine("    There is more than one winner!");
            }

            foreach (var winner in results.TotalWinners)
            {
                Console.WriteLine($"    {winner.Topic} ({winner.NumberOfResults})");
            }
        }

        private static IServiceProvider ConfigureProvider()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.ConfigureServiceOptions();
            var loggingConfig = serviceCollection.BuildServiceProvider().GetService<IOptions<LoggingConfiguration>>();

            serviceCollection.AddLogging(builder => builder.AddConsole())
                .Configure<LoggerFilterOptions>(options => options.MinLevel = loggingConfig?.Value?.LogLevel ?? LogLevel.Information);

            serviceCollection.ConfigureServices();

            return serviceCollection.BuildServiceProvider();
        }
    }
}
