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

            Dealer = new DealerInfo();

            Invoice = new InvoiceInfo();

            Order = new OrdersInfo();

            User = new UserInfo();

         }

        public List<FriendlyMessage> Friendly_Message { get; set; }

        public PaginationInfo Pager { get; set; }

        public ReceivableInfo Receivable { get; set; }

        public List<ReceivableInfo> Receivables { get; set; }

        public ReceivableFilter Filter { get; set; } 

        public CookiesInfo Cookies { get; set; }

        public DealerInfo Dealer { get; set; }

        public InvoiceInfo Invoice { get; set; }

        public OrdersInfo Order { get; set; }

        public UserInfo User { get; set; }
    }

    public class ReceivableFilter
    {

        public int Invoice_Id { get; set; } 

        public bool Is_Active { get; set; }

        public string Invoice_No { get; set; }

        public int Invoice_Number { get; set; }

        public int Entity_Id { get; set; }

        public int Role_Id { get; set; }

        public string Mode { get; set; }
    }
}