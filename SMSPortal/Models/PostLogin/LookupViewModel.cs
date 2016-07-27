using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using SMSPortal.Common;
using SMSPortalInfo;
using SMSPortalInfo.Common;

namespace SMSPortal.Models.PostLogin
{
    public class LookupViewModel
    {
        public LookupViewModel()
        {
            Pager = new PaginationInfo();

            PartialDt = new DataTable();

            HeaderNames = new string[0];

            Cookies = new CookiesInfo();
        }

        public PaginationInfo Pager { get; set; }

        public DataTable PartialDt { get; set; }

        public string[] HeaderNames { get; set; }

        public string Value { get; set; }

        public string EditLookupValue { get; set; }

        public CookiesInfo Cookies { get; set; }
    }
}