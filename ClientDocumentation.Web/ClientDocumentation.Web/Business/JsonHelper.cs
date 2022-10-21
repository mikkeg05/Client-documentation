using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientDocumentation.Web.Business
{
    public class JsonHelper
    {
        public JsonHelper()
        {

        }
        public List<T> GenericDeserializer<T>(string jsonValues)
        {
            List<T> values = new List<T>();
            try
            {
                List<T> mediaItems = JsonConvert.DeserializeObject<List<T>>(jsonValues);
                foreach (var item in mediaItems)
                {
                    values.Add(item);
                }
                return values;
            }
            catch { return null; }
        }
    }
}