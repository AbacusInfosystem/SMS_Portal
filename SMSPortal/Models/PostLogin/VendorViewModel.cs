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
    
    public class VendorViewModel
    {
        public VendorViewModel()
        {
            Friendly_Message = new List<FriendlyMessage>();
            Pager = new PaginationInfo();
            Vendor = new VendorInfo();
            Vendors = new List<VendorInfo>();
            Filter = new VendorFilter();
            States = new List<StateInfo>();
            Cookies = new CookiesInfo();
            
            Products = new List<ProductInfo>();
            Brands = new List<BrandInfo>();
            MappedProducts = new List<ProductInfo>();
        }
        public List<FriendlyMessage> Friendly_Message { get; set; }
        public PaginationInfo Pager { get; set; }
        public VendorInfo Vendor { get; set; }
        public List<VendorInfo> Vendors { get; set; }
        public VendorFilter Filter { get; set; }
        public List<StateInfo> States { get; set; }
        public CookiesInfo Cookies { get; set; }
        
        public List<ProductInfo> Products { get; set; }
        public List<BrandInfo> Brands { get; set; }
        public string ImagePath { get; set; }
        public string Active_Products { get; set; }
        
        public List<ProductInfo> MappedProducts { get; set; }
    }
    public class VendorFilter
    {
        public int Vendor_Id { get; set; }
        public string Vendor_Name { get; set; }
        public bool Is_Active { get; set; }
        public string Invoice_No { get; set; }
        public int Invoice_Number { get; set; }
    }
}