using System.Threading.Tasks;
using Reporting.Common.ApiModels;
using RestEase;

namespace Reporting.BBL.ApiInterfaces
{
    public interface IIeeeXploreApi
    {
        [Query("apikey")]
        string ApiKey { get; set; }

        [Get("search/articles")]
        Task<ScopusSearchResult> GetArticlesAsync([Query("article_number")] string articleNumber,
            [Query("article_title")] string title);

        [Get("search/articles")]
        Task<ScopusSearchResult> GetAuthorArticlesAsync([Query("author")] string authorName,
            [Query("max_records")] int maxRecords = 200);
    }
}
