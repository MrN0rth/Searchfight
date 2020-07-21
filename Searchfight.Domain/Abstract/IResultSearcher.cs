using System.Collections.Generic;
using System.Threading.Tasks;
using Searchfight.Common;

namespace Searchfight.Domain
{
    public interface IResultSearcher
    {
        string Name { get; }

        Task<Dictionary<string, Result<long>>> GetNumberOfResults(IEnumerable<string> searchedTopics);
    }
}