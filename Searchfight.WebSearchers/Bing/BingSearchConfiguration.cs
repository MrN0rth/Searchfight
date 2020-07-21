namespace Searchfight.WebSearchers.Bing
{
    public class BingSearchConfiguration
    {
        public int MaxConcurrency { get; set; } = 5;

        public string ApiKey { get; set; }

        public int DelayBetweenRequests { get; set; }
    }
}