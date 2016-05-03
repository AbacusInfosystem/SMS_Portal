using SMSPortal.Common;
using SMSPortalInfo;
using SMSPortalInfo.Common;
using SMSPortalRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMSPortal.Models.PostLogin
{
    public class PayableViewModel
    {
        public PayableViewModel()
        {
            Friendly_Message = new List<FriendlyMessage>();

            Pager = new PaginationInfo();

            Filter = new PayableFilter();

            Cookies = new CookiesInfo();

            Payable = new PayableInfo();

            Payables = new List<PayableInfo>();
        }

        public List<FriendlyMessage> Friendly_Message { get; set; }

        public PaginationInfo Pager { get; set; }

        public PayableInfo Payable { get; set; }

        public List<PayableInfo> Payables { get; set; }

        public PayableFilter Filter { get; set; }

        public CookiesInfo Cookies { get; set; }
    }
    public class PayableFilter
    {
        public int Payable_Id { get; set; }

        public int Purchase_Order_Id { get; set; }

        public bool Is_Active { get; set; }

        public string Purchase_Order_No { get; set; }


        public int Purchase_Order_Number { get; set; }
    }
}