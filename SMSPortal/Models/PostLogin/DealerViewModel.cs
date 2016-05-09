using SMSPortal.Common;
using SMSPortalInfo;
using SMSPortalInfo.Common;
using SMSPortalRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMSPortal.Models.PostLogin
{
    public class DealerViewModel
    {
        public DealerViewModel()
        {
            Friendly_Message = new List<FriendlyMessage>();

            Pager = new PaginationInfo();

            Dealer = new DealerInfo();

            Dealers = new List<DealerInfo>();

            Filter = new DealerFilter();

            Brands = new List<BrandInfo>();

            States = new List<StateInfo>();

            Cookies = new CookiesInfo();

        }

        public List<FriendlyMessage> Friendly_Message { get; set; }

        public PaginationInfo Pager { get; set; }

        public DealerInfo Dealer { get; set; }

        public List<DealerInfo> Dealers { get; set; }

        public DealerFilter Filter {get;set; }

        public List<BrandInfo> Brands { get; set; }

        public List<StateInfo> States { get; set; }

        public CookiesInfo Cookies { get; set; }

        public string Is_Brand { get; set; }

    }

    public class DealerFilter
    
    {
        public int Brand_Id { get; set; }

        public int Dealer_Id { get; set; }

        public string Dealer_Name { get; set; }

        public bool Is_Active { get; set; }

        public string Mode { get; set; }
    }
}