﻿using SMSPortalInfo;
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
        }
        public List<FriendlyMessage> Friendly_Message { get; set; }
        public PaginationInfo Pager { get; set; }
        public UserInfo User { get; set; }
        public List<UserInfo> Users { get; set; }
    }
}