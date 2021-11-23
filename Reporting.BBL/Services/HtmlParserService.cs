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

            var parseTasks = Enumerable.Range(1, int.Parse(lastPage)).Select(async n =>
            {
                var response = await _journalsApi.GetScientificJournalsAsync(n);

                var doc = new HtmlDocument();
                doc.LoadHtml(response);

                var journalNodes =
                    doc.DocumentNode.SelectNodes("//div[@name='searchBlockElement']/div[@name='nameSearchMain']//a");

                return journalNodes.Select(e => e.InnerText);
            });

            var journalNames = (await Task.WhenAll(parseTasks)).SelectMany(e => e).ToList();

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
