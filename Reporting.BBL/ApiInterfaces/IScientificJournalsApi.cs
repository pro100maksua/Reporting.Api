using System.Threading.Tasks;
using RestEase;

namespace Reporting.BBL.ApiInterfaces
{
    public interface IScientificJournalsApi
    {
        [Get("search?categorySearch[]=b")]
        Task<string> GetScientificJournalsAsync([Query] int page = default);
    }
}
