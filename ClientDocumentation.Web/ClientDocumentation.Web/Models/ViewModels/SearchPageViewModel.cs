using ClientDocumentation.Web.Models.ModelsBuilder;
using Examine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models.PublishedContent;

namespace ClientDocumentation.Web.Models.ViewModels
{
    public class SearchPageViewModel : PublishedContentWrapped
    {
        

        public SearchPageViewModel(IPublishedContent content) : base(content) { }
        public long ResultCount { get; set; }
        //public ISearchResults SearchResults { get; set; }
        public IEnumerable<PublishedSearchResult> SearchResults { get; set; }
        public SearchFilterViewModel Filter { get; set; }

    }
}