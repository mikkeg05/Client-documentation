using ClientDocumentation.Web.Business.Interfaces;
using ClientDocumentation.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Core.PropertyEditors;
using Umbraco.Core.Services;
using static Umbraco.Core.PropertyEditors.ValueListConfiguration;

namespace ClientDocumentation.Web.Business.Services
{
    public class DropDownService : IDropDownService
    {
        private readonly IEntityService _entityService;
        private readonly IDataTypeService _dataTypeService;

        public DropDownService()
        {
        }

        public DropDownService(IDataTypeService dataTypeService, IEntityService entityService)
        {
            _entityService = entityService;
            _dataTypeService = dataTypeService;
        }
        public List<DropdownListItem> GetDropdownValues(int dataTypeId, int? selectedId = null)
        {
            IDataType dataType = _dataTypeService.GetDataType(dataTypeId);
            if (dataType == null)
                return new List<DropdownListItem> { null };

            ValueListConfiguration prevalues = (ValueListConfiguration)dataType.Configuration;
            return prevalues.Items
                .Select(x => CreateDropdownListItem(x, selectedId))
                .ToList();
        }
        public List<DropdownViewModel> GetAllDropdowns()
        {
            List<DropdownViewModel> dropdowns = new List<DropdownViewModel>();
            int containerId = _dataTypeService.GetDataType(1095).ParentId;
            var container = _entityService.Get(containerId);
            if (container.HasChildren)
            {
                var dropDownIds = container.AdditionalData.Keys;
                var children = _entityService.GetChildren(container.Id, UmbracoObjectTypes.DataType);
                List<Guid> dataGuids = new List<Guid>();

                if (children.Any())
                {
                    foreach (var item in children)
                    {
                        
                        dataGuids.Add(item.Key);
                    }
                }
                if (dataGuids.Any()) 
                {
                    foreach (var item in dataGuids)
                    {
                        var dataType = _dataTypeService.GetDataType(item);
                        ValueListConfiguration prevalues = (ValueListConfiguration)dataType.Configuration;

                        dropdowns.Add(new DropdownViewModel(prevalues.Items.Select(x => CreateItem(x)).ToList(), dataType.Name));
                    }
                }
            }
            return dropdowns;
        }
        private DropdownListItem CreateItem(ValueListItem prevalueValue)
        {
            return new DropdownListItem { Value = prevalueValue.Value, Name = prevalueValue.Value };
        }
        private DropdownListItem CreateDropdownListItem(ValueListItem prevalueValue, int? selectedId)
        {
            bool isSelected = selectedId.HasValue
                ? prevalueValue.Id.Equals(selectedId.Value)
                : false;
            return new DropdownListItem { Value = prevalueValue.Value, Selected = isSelected, Name = prevalueValue.Value };
        }
    }
}