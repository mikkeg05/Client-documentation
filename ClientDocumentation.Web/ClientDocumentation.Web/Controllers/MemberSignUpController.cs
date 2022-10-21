using ClientDocumentation.Web.Business.Interfaces;
using ClientDocumentation.Web.Business.Services;
using ClientDocumentation.Web.Models.ModelsBuilder;
using ClientDocumentation.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using IMemberSignUpService = ClientDocumentation.Web.Business.Interfaces.IMemberSignUpService;

namespace ClientDocumentation.Web.Controllers
{
    public class MemberSignUpController : SurfaceController
    {
        private readonly IMemberSignUpService _memberSignUpService;

        public MemberSignUpController(IMemberSignUpService memberSignUpService)
        {
            _memberSignUpService = memberSignUpService;
        }

        public ActionResult Index(CreateUsers currentPage) { return View(); }
        
        [HttpPost]
        public ActionResult CreateMembers()
        {
            _memberSignUpService.CreateMembers(Request);

            return CurrentUmbracoPage();
        }
    }
}