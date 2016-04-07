﻿using System;
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
            user = new UserInfo();
        }

        public UserInfo user { get; set; }
        public List<FriendlyMessage> Friendly_Message { get;set;}
    }
}