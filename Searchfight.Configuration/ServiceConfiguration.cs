using Microsoft.Extensions.DependencyInjection;
using Searchfight.Domain;
using Searchfight.WebSearchers;
using Searchfight.WebSearchers.Bing;
using Searchfight.WebSearchers.Google;

namespace Searchfight.Configuration
{
    public static class ServiceConfiguration
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddHttpClient<IResultSearcher, GoogleResultSearcher>();
            services.AddHttpClient<IResultSearcher, BingResultSearcher>();

            services.AddTransient<ISearchfightJudge, SearchfightJudge>();
        }
    }
}