using System.Threading.Tasks;
using RestEase;

namespace Reporting.BBL.ApiInterfaces
{
    public interface IScientificJournalsApi
    {
        [Get("search?sortOrder=title&galuzSearch%5B0%5D=технічні&categorySearch%5B0%5D=b")]
        Task<string> GetScientificJournalsAsync([Query] int page = default);
    }
}
