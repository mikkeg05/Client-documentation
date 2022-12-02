using ClientDocumentation.Web.Models.ModelsBuilder;
using Examine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.Models;

namespace ClientDocumentation.Web.Models.ViewModels
{
    public class SearchPageViewModel : ContentModel
    {
        

        public SearchPageViewModel(IPublishedContent content) : base(content) { }
        public string NoResultMessage { get; set; }
        //public ISearchResults SearchResults { get; set; }
        public IEnumerable<PublishedSearchResult> SearchResults { get; set; }
        public long? ResultCount => SearchResults?.LongCount();
        public SearchFilterViewModel Filter { get; set; }

    }
}