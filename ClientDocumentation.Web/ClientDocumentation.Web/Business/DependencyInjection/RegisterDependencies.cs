using ClientDocumentation.Web.Business.Interfaces;
using ClientDocumentation.Web.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Core.Services;

namespace ClientDocumentation.Web.Business.DependencyInjection
{
    public class RegisterDependencies : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Register<IMemberSignUpService, MemberSignUpService>();
            composition.Register<Business.Interfaces.IMyUserService, MyUserService>();
            composition.Register<Business.Interfaces.IClientSaveComposerService, ClientSaveComposerService>();
            composition.Register<Business.Interfaces.IDropDownService, DropDownService>();
            composition.Register<Business.Interfaces.ISearchService, SearchService>();
            composition.Register<Business.Interfaces.IClientService, ClientService>();
        }
    }
}