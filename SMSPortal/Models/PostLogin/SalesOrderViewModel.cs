﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SMSPortal.Common;
using SMSPortalInfo;
using SMSPortalInfo.Common;

namespace SMSPortal.Models.PostLogin
{
    public class SalesOrderViewModel
    {
        public List<FriendlyMessage> Friendly_Message { get; set; }

        public PaginationInfo Pager { get; set; }

        public OrdersInfo Sales_Order { get; set; }

        public List<OrdersInfo> Sales_Orders { get; set; }

        public Sales_Order_Filter Filter { get; set; }

        public DealerInfo Dealer { get; set; }

        public InvoiceInfo Invoice { get; set; }

        public CookiesInfo Cookies { get; set; }

        public SalesOrderViewModel()
        {
            Friendly_Message = new List<FriendlyMessage>();

            Pager = new PaginationInfo();

            Sales_Order = new OrdersInfo();

            Sales_Orders = new List<OrdersInfo>();

            Filter = new Sales_Order_Filter();

            Dealer = new DealerInfo();

            Invoice = new InvoiceInfo();

            Cookies = new CookiesInfo();
        }
    }

    public class Sales_Order_Filter
    {
        public int Order_Id { get; set; }

        public string Order_No { get; set; }
    }
}