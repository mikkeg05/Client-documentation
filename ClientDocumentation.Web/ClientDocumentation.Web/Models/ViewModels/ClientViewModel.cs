using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientDocumentation.Web.Models.ViewModels
{
    public class ClientViewModel
    {
        public string ClientName { get; set; }
        public List<ClientSectionViewModel> Sections { get; set; }
    }
}