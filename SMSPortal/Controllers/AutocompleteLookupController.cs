using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMSPortal.Models.PostLogin;
using SMSPortalManager;
using SMSPortal.Common;
using SMSPortalHelper.Logging;
using SMSPortalInfo.Common;
using SMSPortalInfo;
using SMSPortalHelper.PageHelper;

namespace SMSPortal.Controllers
{
    public class AutocompleteLookupController : Controller
    {
        
        public AutocompleteLookupManager _autocompleteLookupManager;

        public AutocompleteLookupController()
        {
            _autocompleteLookupManager = new AutocompleteLookupManager();
        }

        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Load_Vendor_Modal_Data(string table_Name, string columns, string headerNames, string page)
        {
            LookupViewModel LookupVM = new LookupViewModel();

            PaginationInfo pager = new PaginationInfo();

            string[] cols;
            string[] headerNamesArr;
            cols = columns.Split(',');

            if (headerNames != null)
            {
                headerNamesArr = headerNames.Split(',');
                LookupVM.HeaderNames = headerNamesArr;
            }

            try
            {

                LookupVM.PartialDt = _autocompleteLookupManager.Get_Lookup_Data(table_Name, cols, ref pager);

            }
            catch (Exception ex)
            {
                Logger.Error("LookupController - Load_Vendor_Modal_Data" + ex.Message);
            }

            return PartialView("_Partial", LookupVM);
        }

    }
}
