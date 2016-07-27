using SMSPortal.Common;
using SMSPortal.Models.PostLogin;
using SMSPortalHelper.Logging;
using SMSPortalHelper.PageHelper;
using SMSPortalInfo.Common;
using SMSPortalManager;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMSPortal.Controllers.PostLogin
{
    public class ConsolidatedController : Controller
    {
        public ConsolidatedOrderManager _consolidatedOrderManager;

        public ConsolidatedController()
        {
            _consolidatedOrderManager = new ConsolidatedOrderManager();
        }

        public ActionResult Index()
        {
            ConsolidatedOrderViewModel cViewModel = new ConsolidatedOrderViewModel();

            try
            {
                cViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");
             
                DateTime frmdt = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, 20);

                DateTime todt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 22);

                cViewModel.ConsolidatedOrders = _consolidatedOrderManager.Get_Orders(cViewModel.Cookies.Entity_Id, frmdt, todt);

                cViewModel.From_Date = frmdt;

                cViewModel.To_Date = todt;

                cViewModel.Order.From_Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, 21);

                cViewModel.Order.To_Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 21);

                cViewModel.Order_Nos = _consolidatedOrderManager.Get_Order_No_By_Dealer_Id(cViewModel.Cookies.Entity_Id);

            }
            catch (Exception ex)
            {
                cViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("ConsolidatedController Index " + ex);
            }

            return View("Index", cViewModel);
        }

        public ActionResult Update_Order_Status(ConsolidatedOrderViewModel cViewModel)
        {
            try
            {
                cViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");

                _consolidatedOrderManager.Update_Order(cViewModel.ConsolidatedOrders, cViewModel.From_Date, cViewModel.To_Date, cViewModel.Cookies.Entity_Id);

                cViewModel.Friendly_Message.Add(MessageStore.Get("BO001"));
            }
            catch (Exception ex)
            {
                cViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("ConsolidatedController Update_Order_Status " + ex.Message);
            }
            TempData["cViewModel"] = cViewModel;
            return RedirectToAction("Index");
        }

    }
}
