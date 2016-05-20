using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMSPortal.Models;
using SMSPortal.Models.PreLogin;
using SMSPortal.Common;
using SMSPortalInfo;
using SMSPortalInfo.Common;
using SMSPortalManager;
using SMSPortalHelper.Logging;
using System.Configuration;
using SMSPortal.Models.PostLogin;

namespace SMSPortal.Controllers.PostLogin
{
    public class DashboardController : Controller
    {
        public DashboardManager _dashboardManager;

        public DashboardController()
        {
            _dashboardManager = new DashboardManager();           
        }

        [AuthorizeUserAttribute(AppFunction.Token)]
        public ActionResult Index(DashboardViewModel dViewModel)
        {
            try
            {
                dViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");

                if (dViewModel.Cookies == null)
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
            }

            return View("Index", dViewModel);
        }

        [AuthorizeUserAttribute(AppFunction.Token)]
        public ActionResult Get_Admin_Widgets()
        {
            DashboardViewModel dViewModel = new DashboardViewModel();

            try
            {

            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
            }

            return PartialView("_AdminWidgets");
        }

        [AuthorizeUserAttribute(AppFunction.Token)]
        public ActionResult Get_Vendor_Widgets()
        {
            DashboardViewModel dViewModel = new DashboardViewModel();

            try
            {

            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
            }

            return PartialView("_VendorWidgets");
        }

    }
}
