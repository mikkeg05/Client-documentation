using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientDocumentation.Web.Models.ViewModels
{
    public class DropdownViewModel
    {
        public List<DropdownListItem> DropdownItems { get; set; }
        public string InputNameAttr { get; set; }
        

        public DropdownViewModel(List<DropdownListItem> dropdownItems, string name)
        {
            DropdownItems = dropdownItems;
            InputNameAttr = name;
        }
        public bool IsValid() 
        {
            return DropdownItems.Any() && !string.IsNullOrEmpty(InputNameAttr);
        }
        public bool IsSelectedAndValid() 
        {
            var haha = DropdownItems.Select(x => x);
            return IsValid() && DropdownItems.FindAll(x => x.Selected).Any();
        }
    }
}