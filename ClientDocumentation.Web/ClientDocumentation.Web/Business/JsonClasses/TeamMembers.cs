using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientDocumentation.Web.Business.JsonClasses
{
    public class TeamMembers
    {
        [JsonProperty("key")]
        public Guid Key { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("ncContentTypeAlias")]
        public string NcContentTypeAlias { get; set; }
        [JsonProperty("user")]
        public int User { get; set; }
    }
}