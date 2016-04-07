using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SMSPortalInfo;
using SMSPortal.Common;
namespace SMSPortal.Models.PreLogin
{
    public class LoginViewModel
    {
        public LoginViewModel()
        {
            User = new UserInfo();
        }

        public UserInfo User { get; set; }
        public List<FriendlyMessage> Friendly_Message { get;set;}
    }
}