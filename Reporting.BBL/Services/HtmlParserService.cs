using System.Collections.Generic;
using System.Threading.Tasks;
using Reporting.BBL.ApiInterfaces;
using Reporting.BBL.Interfaces;

namespace Reporting.BBL.Services
{
    public class HtmlParserService : IHtmlParserService
    {
        private readonly IScientificJournalsApi _journalsApi;

        public HtmlParserService(IScientificJournalsApi journalsApi)
        {
            _journalsApi = journalsApi;
        }

        public async Task<IEnumerable<string>> GetScientificJournalsCategoryB()
        {
            var response = await _journalsApi.GetScientificJournalsAsync();



            return new string[] { };
        }
    }
}
