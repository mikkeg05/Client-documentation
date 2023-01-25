using ClientDependency.Core.Logging;
using ClientDocumentation.Web.Business.Interfaces;
using ClientDocumentation.Web.Business.Services;
using ClientDocumentation.Web.Models.ModelsBuilder;
using ImageProcessor.Imaging.Colors;
using ImageProcessor.Imaging.Filters.EdgeDetection;
using MessagePack.Formatters;
using NPoco;
using StackExchange.Profiling.Internal;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.Services;
using Umbraco.Core.Services.Implement;
using Umbraco.Web;
using Umbraco.Web.Mvc;
using MyUserService = ClientDocumentation.Web.Business.Services.MyUserService;

namespace ClientDocumentation.Web.Composers
{
    [RuntimeLevel(MinLevel = RuntimeLevel.Run)]
    public class ClientComposer : ComponentComposer<ClientSaveComponent>
    {
        
    }
    public class ClientSaveComponent : IComponent
    {
        private readonly IClientSaveComposerService _clientSaveService;
        private readonly IMediaService _mediaService;

        public ClientSaveComponent (IClientSaveComposerService clientSaveService, IMediaService mediaService) 
        {
            _mediaService = mediaService;
            _clientSaveService = clientSaveService;
        }
        

        public void Initialize()
        {
            ContentService.Published += ContentService_Published;
        }

        private void ContentService_Published(IContentService sender, Umbraco.Core.Events.ContentPublishedEventArgs e)
        {
            var helper = Umbraco.Web.Composing.Current.UmbracoHelper;
            var clientFolder = _mediaService.GetRootMedia().FirstOrDefault(x => x.Name == "Clients");
            //var hiddenClientsFolder = _mediaService.GetRootMedia().FirstOrDefault(x => x.Name == "Hidden Clients");
            if(clientFolder == null) 
            {
                var newClientFolder = _mediaService.CreateMedia("Clients", -1, "Folder");
                _mediaService.Save(newClientFolder);
                clientFolder = _mediaService.GetById(newClientFolder.Id);
            }
            foreach(var publishedItem in e.PublishedEntities) 
            {
                
                if(publishedItem.ContentTypeId == 1068)
                    _clientSaveService.OnClientPublishEvent(clientFolder, publishedItem, helper);
            }
            
        }

        public void Terminate()
        {
            ContentService.Published -= ContentService_Published;
        }
    }
}