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

        public CookiesInfo cookiesInfo;

        public string token = System.Web.HttpContext.Current.Request.Cookies["UserInfo"]["Token"];

        public DashboardController()
        {
            _dashboardManager = new DashboardManager();

            CookiesManager _cookiesManager = new CookiesManager();

            cookiesInfo = _cookiesManager.Get_Token_Data(token);            
        }

        public ActionResult Index(DashboardViewModel dViewModel)
        {
            try
            {
                if(cookiesInfo!=null)
                {
                    dViewModel.cookies = cookiesInfo;
                }
                else
                {
                    dViewModel.Friendly_Message.Add(MessageStore.Get("SYS02"));

                    return View("Index", dViewModel);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
            }

            return View("Index", dViewModel);
        }

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
