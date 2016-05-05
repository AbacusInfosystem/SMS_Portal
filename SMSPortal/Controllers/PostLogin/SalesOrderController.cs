using SMSPortal.Common;
using SMSPortal.Models.PostLogin;
using SMSPortalHelper.Logging;
using SMSPortalHelper.PageHelper;
using SMSPortalInfo;
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
    public class SalesOrderController : Controller
    {
        public OrdersManager _orderManager;

        public SalesOrderController()
        {
            _orderManager = new OrdersManager();
        }

        [AuthorizeUserAttribute(AppFunction.Token)]
        public ActionResult Search(SalesOrderViewModel sViewModel)
        {
            try
            {
                if (TempData["sViewModel"] != null)
                {
                    sViewModel = (SalesOrderViewModel)TempData["sViewModel"];
                }
            }
            catch (Exception ex)
            {
                sViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));

                Logger.Error("Error At Sales Order Controller - Search" + ex.Message);
            }

            return View("Search", sViewModel);
        }

        [AuthorizeUserAttribute(AppFunction.Token)]
        public ActionResult Index(SalesOrderViewModel sViewModel)
        {
            try
            {
                sViewModel.Sales_Order = _orderManager.Get_Order_Data_By_Id(sViewModel.Sales_Order.Order_Id);
                sViewModel.Dealer = _orderManager.Get_Dealer_Data_By_Id(sViewModel.Sales_Order.Dealer_Id);
                sViewModel.Sales_Order.OrderItems = _orderManager.Get_Orders_Item_By_Id(sViewModel.Sales_Order.Order_Id);
            }
            catch (Exception ex)
            {
                sViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));

                Logger.Error("Error At Sales Order Controller - Index " + ex.Message);

            }

            return View("OrderDetails", sViewModel);
        }

        public JsonResult Get_Orders(SalesOrderViewModel sViewModel)
        {
            PaginationInfo pager = new PaginationInfo();

            try
            {
                pager = sViewModel.Pager;

                if (sViewModel.Filter.Order_Id != 0)
                {
                    sViewModel.Sales_Orders = _orderManager.Get_Orders_Data_By_Id(sViewModel.Filter.Order_Id, ref pager);
                }
                else
                {
                    sViewModel.Sales_Orders = _orderManager.Get_Orders(ref pager, sViewModel.Dealer.Dealer_Id);
                }

                sViewModel.Pager.PageHtmlString = PageHelper.NumericPager("javascript:PageMore({0})", sViewModel.Pager.TotalRecords, sViewModel.Pager.CurrentPage + 1, sViewModel.Pager.PageSize, 10, true);

                sViewModel.Pager = pager;
            }
            catch (Exception ex)
            {
                Logger.Error("Error At Sales Order Controller - Get_Orders" + ex.Message);
            }

            return Json(sViewModel);
        }

        [AuthorizeUserAttribute(AppFunction.Token)]
        public ActionResult Update_Order_Status_By_Id(SalesOrderViewModel sViewModel)
        {
            try
            {
                sViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");

                _orderManager.Update_Order_Status(sViewModel.Sales_Order);

                _orderManager.Send_Order_Status_Notification(sViewModel.Cookies.User_Email, sViewModel.Sales_Order);
            }
            catch (Exception ex)
            {
                sViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));

                Logger.Error("Error At Sales Order Controller - Update_Order_Status_By_Id" + ex.Message);
            }

            return View("Search", sViewModel);
        }
        
        public ActionResult OrderDetails()
        {
            return View("OrderDetails");
        }

        public ActionResult OrderItems()
        {
            return View("OrderItems");
        }

        public JsonResult Get_Order_No_Autocomplete(string orderno)
        {
            List<AutocompleteInfo> autoList = new List<AutocompleteInfo>();

            try
            {
                autoList = _orderManager.Get_Order_No_Autocomplete(orderno);
            }
            catch (Exception ex)
            {
                Logger.Error("Error at Sales Order Controller - Get_Order_No_Autocomplete " + ex.ToString());
            }

            return Json(autoList, JsonRequestBehavior.AllowGet);
        }

        [AuthorizeUserAttribute(AppFunction.Token)]
        public ActionResult Display_Dealer_Sales_Order_Details(SalesOrderViewModel sViewModel)
        {
            try
            {
                sViewModel.Sales_Order = _orderManager.Get_Order_Data_By_Id(sViewModel.Sales_Order.Order_Id);
                sViewModel.Dealer = _orderManager.Get_Dealer_Data_By_Id(sViewModel.Sales_Order.Dealer_Id);
                sViewModel.Sales_Order.OrderItems = _orderManager.Get_Orders_Item_By_Id(sViewModel.Sales_Order.Order_Id);
            }
            catch (Exception ex)
            {
                sViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));

                Logger.Error("Error At Sales Order Controller - Display_Dealer_Sales_Order_Details " + ex.Message);

            }

            return View("My_Order_Details", sViewModel);
        }

    }
}
