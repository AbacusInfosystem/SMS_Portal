using SMSPortal.Common;
using SMSPortalInfo;
using SMSPortalInfo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMSPortal.Models.PostLogin
{
    public class DashboardViewModel
    {
        public DashboardViewModel()
        {
            Cookies = new CookiesInfo();

            Friendly_Message = new List<FriendlyMessage>();
        }

        public CookiesInfo Cookies { get; set; }

        public List<FriendlyMessage> Friendly_Message { get; set; }
    }
}