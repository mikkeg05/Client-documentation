using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientDocumentation.Web.Models.ViewModels
{
    public class ClientSectionViewModel
    {
        public string Title { get; set; }
        public string Icon { get; set; }
        public string Id { get; set; }
        public List<BaseCard> Cards { get; set; }
    }
}