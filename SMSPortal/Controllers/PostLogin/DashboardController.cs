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
using SMSPortalHelper.PageHelper;

namespace SMSPortal.Controllers.PostLogin
{
    public class DashboardController : Controller
    {
        public DashboardManager _dashboardManager;

        public DealerManager _dealerManager;

        public ReceivableManager _receivableManager;

        public DashboardController()
        {
            _dashboardManager = new DashboardManager();

            _dealerManager = new DealerManager();

            _receivableManager = new ReceivableManager();
        }

        [AuthorizeUserAttribute(AppFunction.Token)]
        public ActionResult Index(DashboardViewModel dViewModel)
        {
            try
            {
                dViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");

                PaginationInfo pager = new PaginationInfo();

                dViewModel.Dealers = _dealerManager.Get_Dealers(ref pager, dViewModel.Cookies.Entity_Id);

                dViewModel.Receivables = _receivableManager.Get_Receivables(ref pager, dViewModel.Cookies.Entity_Id, 2);

                if (dViewModel.Cookies == null)
                {
                    return RedirectToAction("Index", "Login");
                }

                if (TempData["FriendlyMessage"] != null)
                {
                    dViewModel.Friendly_Message.Add((FriendlyMessage)TempData["FriendlyMessage"]);
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
