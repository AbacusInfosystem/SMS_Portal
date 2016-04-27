using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SMSPortal.Common;
using SMSPortalInfo;
using SMSPortalInfo.Common;

namespace SMSPortal.Models.PostLogin
{
    public class SubCategoryViewModel
    {
        public List<FriendlyMessage> Friendly_Message { get; set; }

        public PaginationInfo Pager { get; set; }

        public SubCategoryInfo SubCategory { get; set; }

        public List<SubCategoryInfo> SubCategories { get; set; }

        public SubCategory_Filter Filter { get; set; }

        public CookiesInfo Cookies { get; set; }

        public SubCategoryViewModel()
        {
            Friendly_Message = new List<FriendlyMessage>();

            Pager = new PaginationInfo();

            SubCategory = new SubCategoryInfo();

            SubCategories = new List<SubCategoryInfo>();

            Filter = new SubCategory_Filter();

            Cookies = new CookiesInfo();
        }
    }

    public class SubCategory_Filter
    {
        public int SubCategory_Id { get; set; }

        public string SubCategory_Name { get; set; }
    }
}