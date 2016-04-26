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
    public class ReceivableViewModel
    {
        public ReceivableViewModel()
        {
            Friendly_Message = new List<FriendlyMessage>();
            Pager = new PaginationInfo(); 
            Filter = new ReceivableFilter(); 
            Cookies = new CookiesInfo();
            Receivable = new ReceivableInfo();
            Receivables = new List<ReceivableInfo>();
         }
        public List<FriendlyMessage> Friendly_Message { get; set; }
        public PaginationInfo Pager { get; set; }
        public ReceivableInfo Receivable { get; set; }
        public List<ReceivableInfo> Receivables { get; set; }
        public ReceivableFilter Filter { get; set; } 
        public CookiesInfo Cookies { get; set; }
    }
    public class ReceivableFilter
    {
        public int Invoice_Id { get; set; } 
        public bool Is_Active { get; set; }
        public string Invoice_No { get; set; }
        public int Invoice_Number { get; set; }
    }
}