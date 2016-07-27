using SMSPortalInfo;
using SMSPortalInfo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SMSPortal.Common;
namespace SMSPortal.Models.PostLogin
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            Friendly_Message = new List<FriendlyMessage>();

            Pager = new PaginationInfo();

            User = new UserInfo();

            Users = new List<UserInfo>();

            Filter = new UserFilter();

            Roles = new List<RolesInfo>();

            Cookies = new CookiesInfo();

            Entities = new List<Entity>();
            
        }

        public List<FriendlyMessage> Friendly_Message { get; set; }

        public PaginationInfo Pager { get; set; }

        public UserInfo User { get; set; }

        public List<UserInfo> Users { get; set; }

        public UserFilter Filter { get; set; }

        public List<RolesInfo> Roles { get; set; }

        public CookiesInfo Cookies { get; set; }

        public List<Entity> Entities { get; set; }
       
    }
    public class UserFilter
    {
        public int User_Id { get; set; }
        public string User_Name { get; set; }
    }
   
}