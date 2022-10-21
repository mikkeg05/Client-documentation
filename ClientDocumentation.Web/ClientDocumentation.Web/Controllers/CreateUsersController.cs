using ClientDocumentation.Web.Business.Interfaces;
using ClientDocumentation.Web.Business.Services;
using ClientDocumentation.Web.Models.ModelsBuilder;
using ClientDocumentation.Web.Models.ViewModels;
using MessagePack.Formatters;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.Services;
using Umbraco.Web.Mvc;
using IMyUserService = ClientDocumentation.Web.Business.Interfaces.IMyUserService;

namespace ClientDocumentation.Web.Controllers
{
    public class CreateUsersController : SurfaceController
    {
        private readonly IMyUserService _myUserService;
        public CreateUsersController(IMyUserService userService)
        {
            _myUserService = userService;
        }

        
        public ActionResult Index(CreateUsers currentPage) 
        {
            if (Request.InputStream != null && Request.InputStream.Length > 0)
            {
                var reader = new StreamReader(Request.InputStream);

                string json = reader.ReadToEnd();
                _myUserService.CreateUser(json);
            }
            
            return View("~/Views/CreateUsers.cshtml");
        }
       
        [HttpPost]
        public ActionResult UploadFile(CreateUsersViewModel model) 
        {
            if (Request.InputStream != null && Request.InputStream.Length > 0)
            {
                foreach (string keyName in Request.Files)
                {
                    string extension = Path.GetExtension(Request.Files[keyName].FileName);
                    if (!extension.Contains(".json"))
                        return CurrentUmbracoPage();
                }
                var reader = new StreamReader(Request.Files[0].InputStream);
              


                string json = reader.ReadToEnd();
                string newJson = json;//.Replace("\n", "").Replace("\r", "").Replace("\"", "");
                newJson = newJson.Substring(newJson.IndexOf('['));
                newJson = newJson.Substring(0, newJson.IndexOf(']')+1);
                _myUserService.CreateUser(newJson);
            }
            
            return CurrentUmbracoPage();
        }
    }
}