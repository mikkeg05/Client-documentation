using ClientDocumentation.Web.Business.Interfaces;
using ClientDocumentation.Web.Models.ModelsBuilder;
using ClientDocumentation.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.WebApi;

namespace ClientDocumentation.Web.Controllers
{
    public class ClientController : UmbracoAuthorizedApiController
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost]
        [Route("createclient")]
        public void CreateClient(string name) 
        { 
            _clientService.CreateClient(name);
        }

    }
}