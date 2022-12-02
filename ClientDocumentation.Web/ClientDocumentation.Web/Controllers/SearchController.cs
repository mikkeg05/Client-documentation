using ClientDocumentation.Web.Models.ModelsBuilder;
using ClientDocumentation.Web.Models.ViewModels;
using Examine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.Services;
using Umbraco.Web;
using Umbraco.Web.Mvc;
using ClientDocumentation.Web.Business.Services;
using System.Text;
using NPoco;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.Models;
using ClientDocumentation.Web.Business.Interfaces;
using Umbraco.Core.Configuration;
using Umbraco.Core.Cache;
using Umbraco.Core.Logging;
using Umbraco.Web.Models;

namespace ClientDocumentation.Web.Controllers
{
    public class SearchController : RenderMvcController
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService, IGlobalSettings globalSettings, IUmbracoContextAccessor umbracoContextAccessor, ServiceContext serviceContext, AppCaches appCaches, IProfilingLogger profilingLogger, UmbracoHelper umbracoHelper) : base(globalSettings, umbracoContextAccessor, serviceContext, appCaches, profilingLogger, umbracoHelper) { _searchService = searchService; }
        
        public SearchController(ISearchService searchService) 
        { 
            _searchService = searchService;
        }
        public override ActionResult Index(ContentModel currentPage)
        {
            var content = currentPage.Content;
            Search search = new Search(content);
            
            var stringList = _searchService.QueryStrings(Request);
            var requestQuery = _searchService.GetRequestQuery(Request);
            //SearchPageViewModel searchPageViewModel = _searchService.GetSearchPageViewModel(search, stringList, requestQuery, UmbracoContext);
            SearchPageViewModel searchPageViewModel = _searchService.GetSearchPageViewModel(search, Request, UmbracoContext);

            return View("~/Views/Search.cshtml", searchPageViewModel);
        }




    }
}