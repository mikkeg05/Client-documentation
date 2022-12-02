using ClientDocumentation.Web.Models.ModelsBuilder;
using ClientDocumentation.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Web;

namespace ClientDocumentation.Web.Business.Interfaces
{
    public interface ISearchService
    {
        SearchPageViewModel GetSearchPageViewModel(Search search, List<string> strings, string query, UmbracoContext context);
        SearchPageViewModel GetSearchPageViewModel(Search search, HttpRequestBase Request, UmbracoContext context);

        List<string> QueryStrings(HttpRequestBase Request);
        string GetRequestQuery(HttpRequestBase Request);
    }
}