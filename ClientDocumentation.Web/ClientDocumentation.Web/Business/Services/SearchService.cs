using ClientDocumentation.Web.Business.Interfaces;
using ClientDocumentation.Web.Models.ModelsBuilder;
using ClientDocumentation.Web.Models.ViewModels;
using Examine;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Umbraco.Core.Services;
using Umbraco.Web;

namespace ClientDocumentation.Web.Business.Services
{
    public class SearchService : ISearchService
    {
        private readonly IExamineManager _examineManager;
        private readonly IDataTypeService _dataTypeService;
        private readonly IEntityService _entityService;
        private readonly IDropDownService _dropDownService;

        public SearchService(IExamineManager examineManager, IDataTypeService dataTypeService, IEntityService entityService, IDropDownService dropDownService)
        {
            _examineManager = examineManager;
            _dataTypeService = dataTypeService;
            _entityService = entityService;
            _dropDownService = dropDownService;
        }

        public SearchPageViewModel GetSearchPageViewModel(Search search, HttpRequestBase Request, UmbracoContext context) 
        {
            List<string> strings = new List<string>();
            for (int i = 1; i < Request.QueryString.Count; i++)
            {
                strings.Add(Request.QueryString[i]);
            }
            List<DropdownViewModel> filter = new List<DropdownViewModel>();

            var dropdowns = _dropDownService.GetAllDropdowns();
            foreach (var item in dropdowns) { filter.Add(item); }
            _examineManager.TryGetIndex("ExternalIndex", out var index);
            var searcher = index.GetSearcher();
            var searchPageViewModel = new SearchPageViewModel(search);
            var query = Request.QueryString["q"];

            StringBuilder stringBuilder = new StringBuilder();
            SearchFilterViewModel filterModel = new SearchFilterViewModel { Dropdowns = filter, AllDropdowns = filter };
            
            foreach (var queryFilter in strings)
            {
                if (!string.IsNullOrEmpty(queryFilter))
                {
                    var filteredDropdowns = filterModel.Dropdowns.Select(x => x.DropdownItems.FirstOrDefault(z => z.Name == queryFilter)).OfType<DropdownListItem>();
                    if (filteredDropdowns.Any() && filteredDropdowns != null)
                    {

                        filterModel.Dropdowns.Select(x => x.DropdownItems.FirstOrDefault(z => z.Name == queryFilter)).OfType<DropdownListItem>().FirstOrDefault().Selected = true;
                    }
                }
            }
            stringBuilder.Append(query);
            List<string> selectedItems = new List<string>();
            var filteredItems = filterModel.Dropdowns.Select(x => x.DropdownItems
                .FindAll(z => z.Selected))
                .Select(y => y
                .Select(t => t.Name));
            foreach (var item in filteredItems)
            {
                if (item.Any() && item.FirstOrDefault() != null && item.FirstOrDefault().Any())
                    selectedItems.Add(item.FirstOrDefault());
            }
            var filteredNames = filterModel.Dropdowns.Where(x => x.IsSelectedAndValid()).Select(z => z.InputNameAttr);
            List<string> selectedNames = new List<string>();
            if (filteredNames.Any() && filteredNames.First() != null && filteredNames.First().Any())
            {
                foreach (var item in filteredNames)
                {
                    string newstring = item;
                    switch (newstring)
                    {
                        case "Key Feature Sections":
                            newstring = "features";
                            break;
                        case "Environments":
                            newstring = "environments";
                            break;
                        case "Server Type":
                            newstring = "environments";
                            break;
                        case "Version Control":
                            newstring = "versionControlValue";
                            break;
                        default:
                            break;
                    }
                    selectedNames.Add(newstring);
                }
            }
            var stringParams = selectedNames.Zip(selectedItems, (n, i) => new { Name = n, Item = i });
            foreach (var ni in stringParams)
            {
                stringBuilder.Append(" AND " + ni.Name + ":" + ni.Item.ToLower());
            }
            if (!string.IsNullOrEmpty(stringBuilder.ToString()))
            {
                var searchResult = searcher.CreateQuery()
                    .NativeQuery(stringBuilder.ToString())
                    .Execute()
                    .ToPublishedSearchResults(context.PublishedSnapshot);
                long resultCount = searchResult != null && searchResult.Any() ? searchResult.Count() : 0;
                if (resultCount > 0)
                {
                    searchPageViewModel.SearchResults = searchResult.Where(x => x.Content.Url() != string.Empty).Where(x => x.Content.ContentType.ItemType.ToString() != "Media");
                    searchPageViewModel.ResultCount = resultCount;

                }
            }
            searchPageViewModel.Filter = filterModel;
            return searchPageViewModel;

        }
    }
}