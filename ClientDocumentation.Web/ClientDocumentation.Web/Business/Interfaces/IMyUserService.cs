using ClientDocumentation.Web.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientDocumentation.Web.Business.Interfaces
{
    public interface IMyUserService
    {
        List<Umbraco.Core.Models.Membership.IUser> CreateUser(string json);
        List<UserViewModel> GetUserViewModels(string json);
    }
}