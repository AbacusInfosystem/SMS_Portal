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
    public class NewVendorViewModel
    {
        public NewVendorViewModel()
        {
            Friendly_Message = new List<FriendlyMessage>();

            Pager = new PaginationInfo();

            Vendor = new VendorInfo();

            Vendors = new List<VendorInfo>();

            Filter = new NewVendorFilter();

            States = new List<StateInfo>();

            Cookies = new CookiesInfo();

            Brands = new List<BrandInfo>();
        }

        public List<FriendlyMessage> Friendly_Message { get; set; }

        public PaginationInfo Pager { get; set; }

        public VendorInfo Vendor { get; set; }

        public List<VendorInfo> Vendors { get; set; }

        public NewVendorFilter Filter { get; set; }

        public List<StateInfo> States { get; set; }

        public CookiesInfo Cookies { get; set; }

        public HttpPostedFileBase Upload_Logo { get; set; }

        public List<BrandInfo> Brands { get; set; }
    }

    public class NewVendorFilter
    {
        public int Vendor_Id { get; set; }

        public string Vendor_Name { get; set; }
    }

}