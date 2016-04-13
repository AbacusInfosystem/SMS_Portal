using SMSPortal.Common;
using SMSPortalInfo;
using SMSPortalInfo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMSPortal.Models.PostLogin
{
    public class CategoryViewModel
    {
        public CategoryViewModel()
        {
            Category = new CategoryInfo();
            Categories = new List<CategoryInfo>();
            Friendly_Message = new List<FriendlyMessage>();           
            Pager = new PaginationInfo();
            Pager.IsPagingRequired = true;
            Pager.PageSize = 5;
            Filter = new CategoryFilter();
        }

        public CategoryInfo Category { get; set; }
        public List<CategoryInfo> Categories { get; set; }
        public PaginationInfo Pager { get; set; }
        public List<FriendlyMessage> Friendly_Message { get; set; }
        public CategoryFilter Filter { get; set; }        
    }

    public class CategoryFilter
    {
        public int Category_Id { get; set; }
        public string Category_Name { get; set; }
        public bool Is_Active { get; set; }
    }

}