using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Searchfight.Common;
using Searchfight.Domain;

namespace Searchfight.WebSearchers.Bing
{
    public class BingResultSearcher : WebSearcher, IResultSearcher
    {
        const string _uriTemplate = "https://api.cognitive.microsoft.com/bing/v7.0/search?q={0}";

        private readonly string _apiKey;
        private readonly HttpClient _httpClient;
        private readonly ILogger<BingResultSearcher> _logger;

        public BingResultSearcher(IOptions<BingSearchConfiguration> options, HttpClient httpClient, ILogger<BingResultSearcher> logger)
            : base(options.Value.MaxConcurrency, options.Value.DelayBetweenRequests)
        {
            _httpClient = httpClient;
            _logger = logger;
            _apiKey = options?.Value?.ApiKey  ?? throw new ArgumentException("API key for Bing searcher is not set");;
        }

        public string Name => "Bing";

        public async Task<Dictionary<string, Result<long>>> GetNumberOfResults(IEnumerable<string> searchedTopics)
        {
            if (!_httpClient.DefaultRequestHeaders.Contains("Ocp-Apim-Subscription-Key"))
            {
                _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _apiKey);
            }

            Dictionary<string, Tuple<Dto.Bing.CroppedRoot, string>> results = await RunConcurrently(
                searchedTopics,
                async topic => await GetNumberOfResults(topic));

            return results.ToDictionary(k => k.Key, ParseNumberOfResults);
        }

        private Result<long> ParseNumberOfResults(KeyValuePair<string, Tuple<Dto.Bing.CroppedRoot, string>> keyValuePair)
        {
            if (!string.IsNullOrEmpty(keyValuePair.Value.Item2))
            {
                return new Result<long>(keyValuePair.Value.Item2);
            }

            return new Result<long>(keyValuePair.Value.Item1.WebPages.TotalEstimatedMatches);
        }

        private async Task<Tuple<Dto.Bing.CroppedRoot, string>> GetNumberOfResults(string topic)
        {
            try
            {
                var response = await _httpClient.GetAsync(string.Format(_uriTemplate, WebUtility.UrlEncode(topic)));
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    string error = $"{response.StatusCode} - {response.ReasonPhrase}";
                    _logger.LogWarning($"Unable to find information for '{topic}': {error}");

                    return new Tuple<Dto.Bing.CroppedRoot, string>(null, error);
                }

                var result = await response.Content.ReadAsStringAsync();
                var deserializeObject = JsonConvert.DeserializeObject<Dto.Bing.CroppedRoot>(result);

                return Tuple.Create<Dto.Bing.CroppedRoot, string>(deserializeObject, null);
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e, $"Unable to find information for '{topic}': {e.Message}");
                return new Tuple<Dto.Bing.CroppedRoot, string>(null, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error occurred while getting results");
                return new Tuple<Dto.Bing.CroppedRoot, string>(null, e.Message);
            }
        }
    }
}