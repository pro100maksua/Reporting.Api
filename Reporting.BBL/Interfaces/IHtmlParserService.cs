using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reporting.BBL.Interfaces
{
    public interface IHtmlParserService
    {
        Task<IEnumerable<string>> GetScientificJournalsCategoryB();
    }
}
