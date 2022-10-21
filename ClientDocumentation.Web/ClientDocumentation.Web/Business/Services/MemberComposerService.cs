using ClientDocumentation.Web.Models.ModelsBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Core.Models.Membership;
using Umbraco.Core.Services.Implement;
using Umbraco.Web.Security;

namespace ClientDocumentation.Web.Business.Services
{
    public class MemberComposerService
    {
        private readonly MemberService _memberService;
        private readonly MembershipHelper _membershipHelper;
        private readonly MemberGroupService _memberGroupService;

        public MemberComposerService(MemberService memberService, MembershipHelper membershipHelper, MemberGroupService memberGroupService)
        {
            _memberService = memberService;
            _membershipHelper = membershipHelper;
            _memberGroupService = memberGroupService;
        }

        public void CreateMembers(IContent content)
        {
            var members = _memberService.GetAllMembers();
            IMember existingMember = null;
            if (members.Any())
            {
                existingMember = members.FirstOrDefault(x => x.Name == content.Name);

            }
            if (existingMember == null && content != null)
            {
                User user = (User)content;
                var newMember = _memberService.CreateMember(user.Name, user.Email, user.Name, "member");
                
                List<IMemberGroup> groups = new List<IMemberGroup>();
                if (user.Groups.Any())
                {
                    foreach (var group in user.Groups)
                    {
                        var memberGroup = _memberGroupService.GetByName(group.Name);
                        groups.Add(memberGroup);
                    }
                }
                if (groups.Any())
                {
                    foreach (var item in groups)
                    {
                        _memberService.AssignRole(newMember.Name, item.Name);
                    }
                }
                newMember.LastPasswordChangeDate = DateTime.Now;
                _memberService.Save(newMember);
                _memberService.SavePassword(newMember, "12345qwert");
                return;
            }
            if(content != null) 
            {
                User user = (User)content;
                foreach(var group in user.Groups) 
                {
                    var memberGroup = _memberGroupService.GetByName(group.Name);
                }
            }
        }

    }
}