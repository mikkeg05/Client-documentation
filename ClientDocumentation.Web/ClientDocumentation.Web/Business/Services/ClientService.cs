using ClientDocumentation.Web.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Services;

namespace ClientDocumentation.Web.Business.Services
{
    public class ClientService : IClientService
    {
        private readonly IContentService _contentService;

        public ClientService(IContentService contentService)
        {
            _contentService = contentService;
        }

        public void CreateClient(string name) 
        {
            var clientNode = Umbraco.Web.Composing.Current.UmbracoHelper.ContentAtRoot().FirstOrDefault(x => x.ContentType.Alias == "clients");
            if (!string.IsNullOrEmpty(name) && clientNode != null)
            {
                var newClient = _contentService.Create(name, clientNode.Id, "client");
                _contentService.Save(newClient);
            }
        }
    }
}