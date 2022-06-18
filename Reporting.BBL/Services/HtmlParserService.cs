using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
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
            var lastPage = await GetLastPage();

            var journalNames = new List<string>();

            await Parallel.ForEachAsync(
                 Enumerable.Range(1, int.Parse(lastPage)),
                 async (page, _) =>
                 {
                     var response = await _journalsApi.GetScientificJournalsAsync(page);

                     var doc = new HtmlDocument();
                     doc.LoadHtml(response);

                     var journalNodes =
                         doc.DocumentNode.SelectNodes(
                             "//div[@name='searchBlockElement']/div[@name='nameSearchMain']//a");

                     journalNames.AddRange(journalNodes.Select(e => e.InnerText));
                 });

            return journalNames;
        }

        private async Task<string> GetLastPage()
        {
            var response = await _journalsApi.GetScientificJournalsAsync();

            var doc = new HtmlDocument();
            doc.LoadHtml(response);

            var lastPageNode =
                doc.DocumentNode.SelectSingleNode("//nav//li[contains(@class, 'page-item')][last() - 1]/a");

            return lastPageNode?.InnerText;
        }
    }
}
