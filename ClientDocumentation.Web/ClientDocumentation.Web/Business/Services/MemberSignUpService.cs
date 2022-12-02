using ClientDocumentation.Web.Business.Interfaces;
using ClientDocumentation.Web.Models.ViewModels;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Core.Models.Membership;
using Umbraco.Core.Services;
using Umbraco.Core.Services.Implement;

namespace ClientDocumentation.Web.Business.Services
{
    public class MemberSignUpService : IMemberSignUpService
    {
        private readonly IMemberService _memberService;
        private readonly IUserService _userService;
        private readonly IMemberGroupService _memberGroupService;
        public MemberSignUpService(IMemberService memberService, IUserService userService, IMemberGroupService memberGroupService)
        {
            _memberService = memberService;
            _userService = userService;
            _memberGroupService = memberGroupService;
        }

        public void CreateMembers(HttpRequestBase Request)
        {
            MemberViewModel memberViewModel = new MemberViewModel();
            //memberViewModel.

            foreach (var key in Request.Form.AllKeys.Where(x => !x.Contains("ufprt")))
            {
                PropertyInfo prop = memberViewModel.GetType().GetProperty(key, BindingFlags.Public | BindingFlags.Instance);

                if (key == prop.Name && prop.CanWrite && prop != null && key != null)
                {
                    prop.SetValue(memberViewModel, Request[key], null);
                }
            }
            var newMember = _memberService.CreateMember(memberViewModel.UserName, memberViewModel.Email, memberViewModel.Name, "Member");
            _memberService.Save(newMember);

        }

        public IMember CreateMembersOnUserSave(IUser user)
        {
            if (user == null || string.IsNullOrEmpty(user?.Username?.Trim()))
            {
                return null;
            }

            var existingMember = _memberService.GetByEmail(user.Email);
            var existingUser = _userService.GetByUsername(user.Username);

            if (existingMember == null)
            {
                if (existingUser != null)
                {
                    string newName = existingUser.Name;

                    if (existingUser.Name.Contains(" ")) 
                    { 
                        newName = existingUser.Name.Split(' ')[0]; 
                    }

                    var newMember = _memberService.CreateMember(newName, existingUser.Email, existingUser.Name, "Member");
                    newMember.LastPasswordChangeDate = DateTime.Now;

                    _memberService.Save(newMember);
                    _memberService.SavePassword(newMember, "12345qwert");

                    if (existingUser.Groups.Any())
                    {
                        foreach (var group in existingUser.Groups)
                        {
                            _memberService.AssignRole(newMember.Id, group.Name);
                            _memberGroupService.Save(_memberGroupService.GetByName(group.Name));
                            _memberService.Save(newMember);
                        }

                    }
                    return newMember;
                }
            }

            if (existingUser != null && existingUser.Groups.Any())
            {
                foreach (var group in existingUser.Groups)
                {
                    _memberService.AssignRole(existingMember.Name, group.Name);
                    _memberGroupService.Save(_memberGroupService.GetByName(group.Name));
                }
            }
            return null;
        }
        public void CreateMembersOnUserGroupSave(IUserGroup userGroup)
        {
            if (userGroup == null)
            {
                return;
            }

            if (_userService.GetAllInGroup(userGroup.Id).Any())
            {
                foreach (var user in _userService.GetAllInGroup(userGroup.Id))
                {
                    var newMember = _memberService.CreateMember(user.Name, user.Email, user.Name, "Member");
                    _memberService.AssignRole(newMember.Name, userGroup.Name);
                    newMember.LastPasswordChangeDate = DateTime.Now;
                    _memberService.Save(newMember);
                    _memberService.SavePassword(newMember, "12345qwert");

                }
            }
        }
    }
}