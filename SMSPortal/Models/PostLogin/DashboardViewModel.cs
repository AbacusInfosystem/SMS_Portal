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

            Dealers = new List<DealerInfo>();

            Pager = new PaginationInfo();

            Receivables = new List<ReceivableInfo>();

            Sales_Orders = new List<OrdersInfo>();

            PurchaseOrders = new List<PurchaseOrderInfo>();

            Sales_Order = new OrdersInfo();
        }

        public CookiesInfo Cookies { get; set; }

        public List<FriendlyMessage> Friendly_Message { get; set; }

        public List<DealerInfo> Dealers { get; set; }

        public PaginationInfo Pager { get; set; }

        public List<ReceivableInfo> Receivables { get; set; }

        public List<OrdersInfo> Sales_Orders { get; set; }

        public List<PurchaseOrderInfo> PurchaseOrders { get; set; }

        public OrdersInfo Sales_Order { get; set; }
    }
}