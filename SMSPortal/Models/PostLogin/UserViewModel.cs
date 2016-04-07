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
            users = new UserInfo();
            userss = new List<UserInfo>();
        }
        public List<FriendlyMessage> Friendly_Message { get; set; }
        public PaginationInfo Pager { get; set; }
        public UserInfo users { get; set; }
        public List<UserInfo> userss { get; set; }
    }
}