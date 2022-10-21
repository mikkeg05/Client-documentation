using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Events;
using Umbraco.Core.Services.Implement;
using Umbraco.Core.Services;
using Umbraco.Core.Composing;
using Umbraco.Core;
using ClientDocumentation.Web.Business.Interfaces;
using ClientDocumentation.Web.Business.Services;

namespace ClientDocumentation.Web.Composers
{
    
    public class MemberComposer : ComponentComposer<MemberComponent>
    {

    }

    
    public class MemberComponent : IComponent
    {
        private readonly IMemberSignUpService _memberSignUpService;
        

        public MemberComponent(IMemberSignUpService memberSignUpService)
        {
            _memberSignUpService = memberSignUpService;
        }

       
        public void Initialize()
        {


            UserService.SavingUser += UserService_SavingUser;

        }

        private void UserService_SavingUser(IUserService sender, SaveEventArgs<Umbraco.Core.Models.Membership.IUser> e)
        {
            foreach(var content in e.SavedEntities) 
            { 
                _memberSignUpService.CreateMembersOnUserSave(content);
            
            }
        }

       



        // terminate: runs once when Umbraco stops
        public void Terminate()
        {

            UserService.SavingUser += UserService_SavingUser;
        }

       
    }
}