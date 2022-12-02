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
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}