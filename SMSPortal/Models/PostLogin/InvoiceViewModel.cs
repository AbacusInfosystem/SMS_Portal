using SMSPortal.Common;
using SMSPortalInfo;
using SMSPortalInfo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace SMSPortal.Models.PostLogin
{
    public class InvoiceViewModel
    {
        public InvoiceViewModel()
        {
            Invoice = new InvoiceInfo();
            Invoices = new List<InvoiceInfo>();
            Pager = new PaginationInfo();
            Pager.IsPagingRequired = true;
            Pager.PageSize = 5;
            Friendly_Message = new List<FriendlyMessage>();
            Filter = new Invoice_Filter();
            Order = new OrdersInfo();
            Dealer = new DealerInfo();
            Cookies = new CookiesInfo();
        }

        public InvoiceInfo Invoice { get; set; }
        public List<InvoiceInfo> Invoices { get; set; }
        public PaginationInfo Pager { get; set; }
        public List<FriendlyMessage> Friendly_Message { get; set; }
        public Invoice_Filter Filter { get; set; }
        public OrdersInfo Order { get; set; }
        public DealerInfo Dealer { get; set; }
        public CookiesInfo Cookies { get; set; }
    }
    public class Invoice_Filter
    {
        public int Invoice_Id { get; set; }
        public string Invoice_No { get; set; }
        public int Order_Id { get; set; }
    }
}