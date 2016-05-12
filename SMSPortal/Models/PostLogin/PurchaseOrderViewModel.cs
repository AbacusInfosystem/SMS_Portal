using SMSPortal.Common;
using SMSPortalInfo;
using SMSPortalInfo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMSPortal.Models.PostLogin
{
    public class PurchaseOrderViewModel
    {
        public PurchaseOrderViewModel()
        {
            PurchaseOrder = new PurchaseOrderInfo();
            PurchaseOrders = new List<PurchaseOrderInfo>();
            Friendly_Message = new List<FriendlyMessage>();
            Pager = new PaginationInfo();
            Pager.IsPagingRequired = true;
            Pager.PageSize = 5;
            Cookies = new CookiesInfo();
            Filter = new PurchaseOrder_Filter();
            PurchaseOrderItem = new PurchaseOrderItemInfo();
            PurchaseOrderItems = new List<PurchaseOrderItemInfo>();
            Product = new ProductInfo();
            Vendor = new VendorInfo();
        }
        public PurchaseOrderInfo PurchaseOrder { get; set; }
        public List<PurchaseOrderInfo> PurchaseOrders { get; set; }
        public List<FriendlyMessage> Friendly_Message { get; set; }
        public PaginationInfo Pager { get; set; }
        public PurchaseOrder_Filter Filter { get; set; }
        public CookiesInfo Cookies { get; set; }
        public PurchaseOrderItemInfo PurchaseOrderItem { get; set; }
        public List<PurchaseOrderItemInfo> PurchaseOrderItems { get; set; }
        public ProductInfo Product { get; set; }
        public VendorInfo Vendor { get; set; }
    }

    public class PurchaseOrder_Filter
    {
        public int Purchase_Order_Id { get; set; }
        public string Purchase_Order_No { get; set; }
        public string Vendor_Id { get; set; }
        public bool Is_Active { get; set; }
    }

    

}