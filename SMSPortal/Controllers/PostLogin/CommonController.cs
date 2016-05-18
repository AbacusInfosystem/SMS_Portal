using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMSPortalManager;
using SMSPortalHelper.Logging;

namespace SMSPortal.Controllers.PostLogin
{
    public class CommonController : Controller
    {
        //
        // GET: /Common/
        public CommonManager _commonManager;

        public CommonController()
        {
            _commonManager = new CommonManager();
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Is_Value_Already_Exist(string Tbl_Name, string Fld_Name, string Value, string Primary_Field_Name, int Id)
        {
            bool bExist = false;
            try
            {
                int count = _commonManager.Is_Value_Already_Exist(Tbl_Name, Fld_Name, Value, Primary_Field_Name, Id);
                if (count > 0)
                    bExist = true;
            }
            catch (Exception ex)
            {
                Logger.Error("Brand Controller - Check_Existing_Brand " + ex.ToString());
            }
            return Json(bExist, JsonRequestBehavior.AllowGet);
        }

    }
}
