using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SMSPortalInfo;
using SMSPortal.Common;
using SMSPortalInfo.Common;
namespace SMSPortal.Models.PreLogin
{
    public class LoginViewModel
    {
        public LoginViewModel()
        {
            Session = new SessionInfo();
            Friendly_Message = new List<FriendlyMessage>();
        }
         
        public SessionInfo Session { get; set; }
        public List<FriendlyMessage> Friendly_Message { get;set;}
    }
}