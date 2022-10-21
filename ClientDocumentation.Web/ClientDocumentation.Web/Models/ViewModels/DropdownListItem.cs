using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientDocumentation.Web.Models.ViewModels
{
    public class DropdownListItem
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public bool Selected{ get; set; }
    }
}