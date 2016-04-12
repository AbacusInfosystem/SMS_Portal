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
            Pager = new List<PaginationInfo>();
            Friendly_Message = new List<FriendlyMessage>();
            Category = new CategoryInfo();
        }

        public CategoryInfo Category { get; set; }
        public List<CategoryInfo> Categories { get; set; }
        public List<PaginationInfo> Pager { get; set; }
        public List<FriendlyMessage> Friendly_Message { get; set; }
        public CategoryFilter Filter { get; set; }        
    }

    public class CategoryFilter
    {
        public string Category_Id { get; set; }
        public string Category_Name { get; set; }
    }

    public class CategoryEditMode
    {
        public int Category_Id { get; set; }
    }
}