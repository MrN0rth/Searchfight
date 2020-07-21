namespace Searchfight.WebSearchers.Google
{
    public class GoogleSearcherConfiguration
    {
        public int MaxConcurrency { get; set; } = 5;

        public string ApiKey { get; set; }

        public string CustomSearchEngineId { get; set; }
    }
}