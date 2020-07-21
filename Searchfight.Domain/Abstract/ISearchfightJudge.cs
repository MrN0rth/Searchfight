using System.Collections.Generic;
using System.Threading.Tasks;

namespace Searchfight.Domain
{
    public interface ISearchfightJudge
    {
        Task<SearchfightResult> GetResults(IEnumerable<string> topics);
    }
}