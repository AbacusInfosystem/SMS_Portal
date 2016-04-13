using SMSPortal.Common;
using SMSPortalInfo;
using SMSPortalInfo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMSPortal.Models.PostLogin
{
    public class BrandViewModel
    {

        public BrandViewModel()
        {
            Brand = new BrandInfo();
            Brands = new List<BrandInfo>();
            Pager = new PaginationInfo();
            Pager.IsPagingRequired = true;
            Pager.PageSize = 5;
            Friendly_Message = new List<FriendlyMessage>();
            Filter = new BrandFilter();
        }

        public BrandInfo Brand { get; set; }
        public List<BrandInfo> Brands { get; set; }
        public PaginationInfo Pager { get; set; }
        public List<FriendlyMessage> Friendly_Message { get; set; }
        public BrandFilter Filter { get; set; }
    }

    public class BrandFilter
    {
        public int Brand_Id { get; set; }
        public string Brand_Name { get; set; }
        public bool Is_Active { get; set; }
    }
}