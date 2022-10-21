using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientDocumentation.Web.Models.ViewModels
{
    public class SearchFilterViewModel
    {
        public List<DropdownViewModel> Dropdowns{ get; set; }
        public List<DropdownViewModel> AllDropdowns { get; set; }
    }
}