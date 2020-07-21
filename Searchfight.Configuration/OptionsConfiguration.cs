using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Searchfight.Common;
using Searchfight.WebSearchers;
using Searchfight.WebSearchers.Bing;
using Searchfight.WebSearchers.Google;

namespace Searchfight.Configuration
{
    public static class OptionsConfiguration
    {
        public static void ConfigureServiceOptions(this IServiceCollection serviceCollection)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .Build();

            serviceCollection.Configure<LoggingConfiguration>(configuration.GetSection("Logging"));
            serviceCollection.Configure<GoogleSearcherConfiguration>(configuration.GetSection("GoogleSearcher"));
            serviceCollection.Configure<BingSearchConfiguration>(configuration.GetSection("BingSearcher"));
        }
    }
}