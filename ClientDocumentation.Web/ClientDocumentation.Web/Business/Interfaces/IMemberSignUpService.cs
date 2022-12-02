using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Core.Models.Membership;

namespace ClientDocumentation.Web.Business.Interfaces
{
    public interface IMemberSignUpService
    {
        void CreateMembers(HttpRequestBase Request);
        IMember CreateMembersOnUserSave(IUser content);
        void CreateMembersOnUserGroupSave(IUserGroup content);
    }
}