using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientDocumentation.Web.Business.JsonClasses
{
    public class KeyMediaKey
    {
        [JsonProperty("key")]
        public Guid Key { get; set; }

        [JsonProperty("mediaKey")]
        public Guid MediaKey { get; set; }

    }
}