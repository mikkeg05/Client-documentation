using ClientDocumentation.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientDocumentation.Web.Business.Interfaces
{
    public interface IMyUserService
    {
        void CreateUser(string json);
        List<UserViewModel> GetUserViewModels(string json);
    }
}