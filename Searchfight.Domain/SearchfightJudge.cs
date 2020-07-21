using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Searchfight.Common;

namespace Searchfight.Domain
{
    public class SearchfightJudge : ISearchfightJudge
    {
        private readonly IEnumerable<IResultSearcher> _searchers;

        public SearchfightJudge(IEnumerable<IResultSearcher> searchers)
        {
            _searchers = searchers;
        }

        public async Task<SearchfightResult> GetResults(IEnumerable<string> topics)
        {
            var searcherResults = new Dictionary<string, Dictionary<string, Result<long>>>();
            foreach (var searcher in _searchers)
            {
                Dictionary<string, Result<long>> searcherResult = await searcher.GetNumberOfResults(topics);
                searcherResults.Add(searcher.Name, searcherResult);
            }

            var searcherWinners = GetSearcherWinners(searcherResults);

            var generalResults = searcherResults.Select(r => new SearcherResult(r.Key, r.Value));

            var totalWinners = GetTotalWinners(topics, searcherResults);

            return new SearchfightResult(generalResults, searcherWinners, totalWinners);
        }

        private static IEnumerable<(string, string, long Value)> GetSearcherWinners(Dictionary<string, Dictionary<string, Result<long>>> searcherResults)
        {
            foreach (var searcherResult in searcherResults)
            {
                var maxNumberOfResults = searcherResult.Value.Max(r => r.Value.Value);
                var searcherWinners = searcherResult.Value.Where(r => r.Value.Value == maxNumberOfResults);
                foreach (var searcherWinner in searcherWinners)
                {
                    yield return (searcherResult.Key, searcherWinner.Key, searcherWinner.Value.Value);
                }
            }
        }

        private static IEnumerable<(string Topic, long NumberOfResults)> GetTotalWinners(
            IEnumerable<string> topics,
            Dictionary<string, Dictionary<string, Result<long>>> searcherResults)
        {
            var totalWinnerResults = new Dictionary<string, long>();
            foreach (var topic in topics)
            {
                foreach (var (_, results) in searcherResults)
                {
                    if (totalWinnerResults.ContainsKey(topic))
                    {
                        totalWinnerResults[topic] += results[topic].Value;
                    }
                    else
                    {
                        totalWinnerResults[topic] = results[topic].Value;
                    }
                }
            }

            var maxNumberOfResults = totalWinnerResults.Values.Max(r => r);
            var totalWinners = totalWinnerResults.Where(r => r.Value == maxNumberOfResults);

            foreach (var winner in totalWinners)
            {
                yield return (winner.Key, winner.Value);
            }
        }
    }

    public class SearchfightResult
    {
        public SearchfightResult(
            IEnumerable<SearcherResult> results,
            IEnumerable<(string, string, long Value)> searcherWinners,
            IEnumerable<(string Topic, long NumberOfResults)> totalWinner)
        {
            GeneralResults = results;
            TotalWinners = totalWinner;
            SearcherWinners = searcherWinners;
        }

        public IEnumerable<SearcherResult> GeneralResults { get; }

        public IEnumerable<(string Topic, long NumberOfResults)> TotalWinners { get; set; }

        public IEnumerable<(string Name, string Topic, long NumberOfResults)> SearcherWinners { get; set; }
    }

    public readonly struct SearcherResult
    {
        public SearcherResult(string name, Dictionary<string, Result<long>> results)
        {
            Name = name;
            TopicResults = results;
        }

        public string Name { get; }

        public Dictionary<string, Result<long>> TopicResults { get; }
    }
}