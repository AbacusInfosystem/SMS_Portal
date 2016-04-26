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

        public PartialViewResult Load_Modal_Data(string table_Name, string columns, string headerNames, string page, string editValue)
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

                LookupVM.EditLookupValue = editValue;

            }
            catch (Exception ex)
            {
                Logger.Error("LookupController - Load_Vendor_Modal_Data" + ex.Message);
            }

            return PartialView("_Partial", LookupVM);
        }

        public JsonResult Get_Lookup_Data_By_Id(string field_Value, string table_Name, string columns, string headerNames)
        {
            LookupViewModel LookupVM = new LookupViewModel();

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
                if (field_Value!=null)
                {
                    LookupVM.Value = _autocompleteLookupManager.Get_Lookup_Data_By_SubcategoryId(field_Value, table_Name, cols);
                }
            }
            catch (Exception ex)
            {

            }

            return Json(LookupVM.Value, JsonRequestBehavior.AllowGet);
        }

    }
}
