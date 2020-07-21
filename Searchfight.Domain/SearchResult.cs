using System.Collections.Generic;

namespace Searchfight.Domain
{
    public class SearchResult
    {
        private Dictionary<string, long> _results = new Dictionary<string, long>();

        public SearchResult(string error)
        {
            Error = error;
        }

        public SearchResult(Dictionary<string, long> results)
        {
            _results = results;
        }

        public bool IsSuccess => string.IsNullOrEmpty(Error);

        public string Error { get; }
    }
}