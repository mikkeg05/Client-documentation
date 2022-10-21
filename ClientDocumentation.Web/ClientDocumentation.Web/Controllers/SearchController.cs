﻿using ClientDocumentation.Web.Models.ModelsBuilder;
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

namespace ClientDocumentation.Web.Controllers
{
    public class SearchController : RenderMvcController
    {
        private readonly ISearchService _searchService;

        
        public SearchController(ISearchService searchService) 
        { 
            _searchService = searchService;
        }
        public ActionResult Index(Search currentPage)
        {
            SearchPageViewModel searchPageViewModel = _searchService.GetSearchPageViewModel(currentPage, Request, UmbracoContext);
            
            return View("~/Views/Search.cshtml", searchPageViewModel);
        }




    }
}