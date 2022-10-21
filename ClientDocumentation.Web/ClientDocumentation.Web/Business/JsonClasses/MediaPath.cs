using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientDocumentation.Web.Business.JsonClasses
{
    public class MediaPath
    {
        [JsonProperty("src")]
        public string Path { get; set; }
    }
}