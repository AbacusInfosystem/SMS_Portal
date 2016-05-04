using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMSPortal.Common;
using SMSPortalInfo;
using SMSPortalInfo.Common;

namespace SMSPortal.Models.PostLogin
{
    public class TaxViewModel
    {
      
        public TaxViewModel()
        {
            Tax = new TaxInfo();

            Taxes = new List<TaxInfo>();

            Friendly_Message = new List<FriendlyMessage>();

            Cookies = new CookiesInfo();
        }

        public TaxInfo Tax { get; set; }

        public List<FriendlyMessage> Friendly_Message { get; set; }

        public CookiesInfo Cookies { get; set; }

        public List<TaxInfo> Taxes { get; set; }

    }
}
