using System.Collections.Generic;

namespace Reporting.Common.ApiModels
{
    public class ScopusSearchResult
    {
        public int TotalRecords { get; set; }
        public int TotalSearched { get; set; }

        public IEnumerable<IeeeXploreArticle> Articles { get; set; }
    }
}
