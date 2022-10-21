using ImageProcessor.Imaging.Colors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace ClientDocumentation.Web.Business
{

    
    public static class NestedContentHElper
    {
        public static Dictionary<string, List<string>> ClientDataMappings()
        {

            Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();
            string appSettingsValue = System.Web.Configuration.WebConfigurationManager.AppSettings["ClientDataMapping"];
            foreach(var tabConfig in appSettingsValue.Split(';'))
            {
                var key = tabConfig.Split('|')[0];
                var value = tabConfig.Split('|')[1];
                result.Add(key, value.Split(',').ToList());
            }

            return result;
        }
        public static Dictionary<string, List<IPublishedElement>> TabAndProperties(IPublishedContent content)
        {
            var clientDataMappings = ClientDataMappings();
            var result = new Dictionary<string, List<IPublishedElement>>();

            foreach (var prop in content.Properties.Where(x => x.PropertyType.DataType.EditorAlias.Equals("Umbraco.NestedContent")))
            {
                var mapping = clientDataMappings.FirstOrDefault(x => x.Value.Contains(prop.Alias));
                if(mapping.Key == null)
                    continue;

                string key = mapping.Key;
                
                
                if(content.Value(prop.Alias) as IEnumerable<IPublishedElement> != null) 
                { 
                    foreach(var item in content.Value(prop.Alias) as IEnumerable<IPublishedElement>) 
                    {
                        if (result.ContainsKey(key))
                            result[key].Add(item);

                        else
                            result.Add(key, new List<IPublishedElement> { { item  } });
                    }
                    continue;
                }
                var value = content.Value<IPublishedElement>(prop.Alias);

                    if (result.ContainsKey(key))
                        result[key].Add(value);

                    else
                        result.Add(key, new List<IPublishedElement> { { value } });
                
            }

            return result;
        }

        public static T ConvertToUmbElement<T>(object model) where T : IPublishedElement
        {
            return (T)Convert.ChangeType(model, typeof(T));
        }
    }
}