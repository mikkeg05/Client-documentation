using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientDocumentation.Web.Models.ViewModels
{
    public class UserViewModel
    {
        [JsonProperty("Firstname")]
        public string FirstName { get; set; }
        
        [JsonProperty("Lastname")]
        public string LastName { get; set; }

        [JsonProperty("EmailAddress")]
        public string Email { get; set; }
        public string FullName => FirstName +" "+ LastName;

        public UserViewModel()
        {

        }

       
    }
}