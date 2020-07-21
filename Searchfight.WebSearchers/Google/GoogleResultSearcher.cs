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

namespace Searchfight.WebSearchers.Google
{
    public class GoogleResultSearcher : WebSearcher, IResultSearcher
    {
        private const string _searchUriTemplate = "https://www.googleapis.com/customsearch/v1?key={0}&cx={1}&q={2}";

        private readonly string _apiKey;
        private readonly string _customSearchEngineId;
        private readonly HttpClient _httpClient;
        private readonly ILogger<GoogleResultSearcher> _logger;

        public string Name => "Google";

        public GoogleResultSearcher(IOptions<GoogleSearcherConfiguration> options, HttpClient httpClient, ILogger<GoogleResultSearcher> logger)
            : base(options.Value.MaxConcurrency)
        {
            _logger = logger;
            _httpClient = httpClient;

            _apiKey = options?.Value?.ApiKey ?? throw new ArgumentException("API key for Google searcher is not set");
            _customSearchEngineId =
                options?.Value?.CustomSearchEngineId ?? throw new ArgumentException("Custom search engine id is not set");
        }

        public async Task<Dictionary<string, Result<long>>> GetNumberOfResults(IEnumerable<string> searchedTopics)
        {
            Dictionary<string, Tuple<Dto.Google.CroppedRoot, string>> results = await RunConcurrently(
                searchedTopics,
                async topic => await GetNumberOfResults(topic));

            return results.ToDictionary(k => k.Key, ParseNumberOfResults);
        }

        private Result<long> ParseNumberOfResults(KeyValuePair<string, Tuple<Dto.Google.CroppedRoot, string>> keyValuePair)
        {
            if (!string.IsNullOrEmpty(keyValuePair.Value.Item2))
            {
                return new Result<long>(keyValuePair.Value.Item2);
            }

            if (!long.TryParse(keyValuePair.Value.Item1.SearchInformation.TotalResults, out var numberOfResults))
            {
                string error =
                    $"Returned number of results is not numeric or larger than 'long': {keyValuePair.Value.Item1.SearchInformation.TotalResults}";
                _logger.LogWarning(error);

                return new Result<long>(error);
            }

            return new Result<long>(numberOfResults);
        }

        private async Task<Tuple<Dto.Google.CroppedRoot, string>> GetNumberOfResults(string topic)
        {
            try
            {
                var response = await _httpClient.GetAsync(string.Format(_searchUriTemplate,
                    _apiKey,
                    _customSearchEngineId,
                    WebUtility.UrlEncode(topic)));
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    string error = $"{response.StatusCode} - {response.ReasonPhrase}";
                    _logger.LogWarning($"Unable to find information for '{topic}': {error}");

                    return new Tuple<Dto.Google.CroppedRoot, string>(null, error);
                }

                var result = await response.Content.ReadAsStringAsync();
                var deserializeObject = JsonConvert.DeserializeObject<Dto.Google.CroppedRoot>(result);

                return Tuple.Create<Dto.Google.CroppedRoot, string>(deserializeObject, null);
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e, $"Unable to find information for '{topic}': {e.Message}");
                return new Tuple<Dto.Google.CroppedRoot, string>(null, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error occurred while getting results");
                return new Tuple<Dto.Google.CroppedRoot, string>(null, e.Message);
            }
        }
    }
}