using ClientDocumentation.Web.Business.Interfaces;
using ClientDocumentation.Web.Business.Services;
using ClientDocumentation.Web.Models.ModelsBuilder;
using ClientDocumentation.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.Cache;
using Umbraco.Core.Configuration;
using Umbraco.Core.Logging;
using Umbraco.Core.Services;
using Umbraco.Web;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;
namespace ClientDocumentation.Web.Controllers
{
    public class ClientController : RenderMvcController
    {
        public ClientController(IGlobalSettings globalSettings, IUmbracoContextAccessor umbracoContextAccessor, ServiceContext serviceContext, AppCaches appCaches, IProfilingLogger profilingLogger, UmbracoHelper umbracoHelper) : base(globalSettings, umbracoContextAccessor, serviceContext, appCaches, profilingLogger, umbracoHelper) { }


        public ActionResult Index(Client currentpage)
        {
            var mailstring = ViewToStringRenderer.RenderViewToString(ControllerContext, "~/Views/Client.cshtml", currentpage);
            //MailService.SendMail(mailstring);

            return View("~/Views/Client.cshtml", currentpage);
        }

        
    }
}