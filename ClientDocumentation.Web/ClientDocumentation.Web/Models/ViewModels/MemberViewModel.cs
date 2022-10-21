using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ClientDocumentation.Web.Models.ViewModels
{
    public class MemberViewModel 
    {
        //[BindProperty(Name = "userName")]
        public string userName { get; set; }
        public string email { get; set; }
        public string name { get; set; }
    }
}