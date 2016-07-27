using SMSPortal.Common;
using SMSPortalInfo;
using SMSPortalInfo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMSPortal.Models.PostLogin
{
    public class ConsolidatedOrderViewModel
    {
        public List<FriendlyMessage> Friendly_Message { get; set; }
        public List<ConsolidatedOrderInfo> ConsolidatedOrders { get; set; }
        public CookiesInfo Cookies { get; set; }
        public DateTime From_Date { get; set; }
        public DateTime To_Date { get; set; }
        public string Order_Nos { get; set; }
        public ConsolidatedOrderInfo Order { get; set; }

        public ConsolidatedOrderViewModel()
        {
            Friendly_Message = new List<FriendlyMessage>();
            ConsolidatedOrders = new List<ConsolidatedOrderInfo>();
            Cookies = new CookiesInfo();
            Order = new ConsolidatedOrderInfo();
        }
    }
}