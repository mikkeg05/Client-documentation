using ClientDocumentation.Web.Business.Interfaces;
using ClientDocumentation.Web.Models.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using Umbraco.Core;
using Umbraco.Core.Models.Membership;
using Umbraco.Core.Services;

namespace ClientDocumentation.Web.Business.Services
{
    public class MyUserService : IMyUserService
    {
        private readonly IUserService _userService;

        public MyUserService(IUserService userService)
        {
            _userService = userService;
        }
        public List<UserViewModel> GetUserViewModels(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<UserViewModel>>(json);
            }
            catch
            {
                return null;
            }
        }
        public List<IUser> CreateUser(string json)
        {
            List<UserViewModel> users = GetUserViewModels(json);
            if (!users.Any())
                return null;
            List<IUser> userList = new List<IUser>();
            foreach (var user in users)
            {
               var newUser = _userService.CreateUserWithIdentity(user.FullName, user.Email);
                _userService.Save(newUser);
                userList.Add(newUser);
            }
            return userList;
            
        }
        
    }
}