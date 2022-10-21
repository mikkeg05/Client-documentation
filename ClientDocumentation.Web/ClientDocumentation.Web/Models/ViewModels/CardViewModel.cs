using System.Collections.Generic;

namespace ClientDocumentation.Web.Models.ViewModels
{
    public class CardViewModel : BaseCard
    {
        public string[] Tags { get; set; }
        public Dictionary<string, string> AdditionalInfo { get; set; }
        public string Text { get; set; }
        public string Id { get; set; }
        public List<BaseCard> Cards { get; set; }
        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Title) 
                && !string.IsNullOrEmpty(Id);
        }
    }
}