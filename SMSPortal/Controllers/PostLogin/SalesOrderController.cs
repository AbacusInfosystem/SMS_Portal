﻿using SMSPortal.Common;
using SMSPortal.Models.PostLogin;
using SMSPortalHelper.Logging;
using SMSPortalHelper.PageHelper;
using SMSPortalInfo;
using SMSPortalInfo.Common;
using SMSPortalManager;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMSPortal.Controllers.PostLogin
{
    public class SalesOrderController : Controller
    {
        public OrdersManager _orderManager;

        public UserManager _userManager;

        public SalesOrderController()
        {
            _orderManager = new OrdersManager();

            _userManager = new UserManager();
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
                sViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");

                if (sViewModel.Cookies.Role_Id==Convert.ToInt32(RolesIds.Vendor))
                {
                    sViewModel.Sales_Order = _orderManager.Get_Vendor_Order_Data_By_Id(sViewModel.Sales_Order.Vendor_Order_Id);
                    sViewModel.Sales_Order.OrderItems = _orderManager.Get_Vendor_Orders_Item_By_Id(sViewModel.Sales_Order.Vendor_Order_Id);
                }
                else
                {
                    sViewModel.Sales_Order = _orderManager.Get_Order_Data_By_Id(sViewModel.Sales_Order.Order_Id);
                    sViewModel.Sales_Order.OrderItems = _orderManager.Get_Orders_Item_By_Id(sViewModel.Sales_Order.Vendor_Order_Id);
                }

                //sViewModel.Sales_Order = _orderManager.Get_Order_Data_By_Id(sViewModel.Sales_Order.Order_Id);
               // sViewModel.Sales_Order = _orderManager.Get_Vendor_Order_Data_By_Id(sViewModel.Sales_Order.Vendor_Order_Id);
                sViewModel.Dealer = _orderManager.Get_Dealer_Data_By_Id(sViewModel.Sales_Order.Dealer_Id);
                //sViewModel.Sales_Order.OrderItems = _orderManager.Get_Orders_Item_By_Id(sViewModel.Sales_Order.Vendor_Order_Id);
               // sViewModel.Sales_Order.OrderItems = _orderManager.Get_Vendor_Orders_Item_By_Id(sViewModel.Sales_Order.Vendor_Order_Id);
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
                sViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");

                pager = sViewModel.Pager;

                if (sViewModel.Filter.Order_Id != 0)
                {
                    sViewModel.Sales_Orders = _orderManager.Get_Orders_Data_By_Id(sViewModel.Filter.Order_Id, ref pager);
                }
                else if (sViewModel.Filter.Status != 0)
                {
                    sViewModel.Sales_Orders = _orderManager.Get_Orders_Data_By_Status(sViewModel.Filter.Status, ref pager, sViewModel.Cookies.Role_Id, sViewModel.Cookies.Entity_Id);
                }
                else if (!string.IsNullOrEmpty(sViewModel.Filter.OrderSlot))
                {
                    if (sViewModel.Filter.OrderSlot == "FirstSlot")
                    {
                        DateTime todt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 07);
                        DateTime frmdt = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, 21);
                        sViewModel.Sales_Orders = _orderManager.Get_Orders_Data_By_Dates(frmdt, todt, ref pager);
                    }
                    if (sViewModel.Filter.OrderSlot == "SecondSlot")
                    {
                        DateTime frmdt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 08);
                        DateTime todt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 21);
                        sViewModel.Sales_Orders = _orderManager.Get_Orders_Data_By_Dates(frmdt, todt, ref pager);
                    }
                }
                else if (!string.IsNullOrEmpty(sViewModel.Filter.Date_Range))
                {
                    string[] dates = sViewModel.Filter.Date_Range.Split('-');
                    DateTime frmdt = Convert.ToDateTime(dates[0]);
                    DateTime todt = Convert.ToDateTime(dates[1]);
                    sViewModel.Sales_Orders = _orderManager.Get_Orders_Data_By_Dates(frmdt, todt, ref pager);
                }
                else
                {
                    if (sViewModel.Cookies.Role_Id==Convert.ToInt32(RolesIds.Dealer))
                    {
                        sViewModel.Sales_Orders = _orderManager.Get_Orders(ref pager, sViewModel.Cookies.Entity_Id);
                    }
                    else
                    {
                        sViewModel.Sales_Orders = _orderManager.Get_Vendor_Orders(ref pager, sViewModel.Cookies.Entity_Id);
                    }
                    
                }

                sViewModel.Pager.PageHtmlString = PageHelper.NumericPager("javascript:PageMore({0})", sViewModel.Pager.TotalRecords, sViewModel.Pager.CurrentPage + 1, sViewModel.Pager.PageSize, 10, true);

                sViewModel.Pager = pager;
            }
            catch (Exception ex)
            {
                Logger.Error("Error At Sales Order Controller - Get_Orders" + ex.Message);
            }

            return Json(sViewModel, JsonRequestBehavior.AllowGet);
        }

        [AuthorizeUserAttribute(AppFunction.Token)]
        public ActionResult Update_Order_Status_By_Id(SalesOrderViewModel sViewModel)
        {
            try
            {
                sViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");

                string x = sViewModel.Sales_Order.Shipping_Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                _orderManager.Update_Order_Status(sViewModel.Sales_Order);

                _orderManager.Update_Vendor_Order_Status(sViewModel.Sales_Order);

                //sViewModel.Sales_Order = _orderManager.Get_Order_Data_By_Id(sViewModel.Sales_Order.Order_Id);

                sViewModel.Sales_Order = _orderManager.Get_Vendor_Order_Data_By_Id(sViewModel.Sales_Order.Order_Id);

                sViewModel.User = _userManager.Get_User_By_Entity_Id(sViewModel.Sales_Order.Dealer_Id, 3);

                if (sViewModel.User.Email_Id!=null)
                {
                    _orderManager.Send_Order_Status_Notification(sViewModel.User.First_Name, sViewModel.User.Email_Id, sViewModel.Sales_Order, false, sViewModel.Invoice.Invoice_Id, "Update Sales Order", sViewModel.Cookies.Entity_Id);
                }           
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
                sViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");

                if (sViewModel.Cookies.Role_Id==Convert.ToInt32(RolesIds.Dealer))
                {
                    sViewModel.Sales_Order = _orderManager.Get_Dealer_Order_Data_By_Id(sViewModel.Sales_Order.Order_Id);
                    sViewModel.Dealer = _orderManager.Get_Dealer_Data_By_Id(sViewModel.Sales_Order.Dealer_Id);
                    sViewModel.Sales_Order.OrderItems = _orderManager.Get_Orders_Item_By_Id(sViewModel.Sales_Order.Order_Id);
                }
                else
                {
                    sViewModel.Sales_Order = _orderManager.Get_Order_Data_By_Id(sViewModel.Sales_Order.Order_Id);
                    sViewModel.Dealer = _orderManager.Get_Dealer_Data_By_Id(sViewModel.Sales_Order.Dealer_Id);
                    sViewModel.Sales_Order.OrderItems = _orderManager.Get_Dealer_Order_Items_By_Order_Id(sViewModel.Sales_Order.Order_Id);
                }
 
            }
            catch (Exception ex)
            {
                sViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));

                Logger.Error("Error At Sales Order Controller - Display_Dealer_Sales_Order_Details " + ex.Message);

            }

            return View("My_Order_Details", sViewModel);
        }

        public JsonResult Get_Orders_Autocomplete_By_Dealer(string dealer)
        {
            List<AutocompleteInfo> autoList = new List<AutocompleteInfo>();
            CookiesInfo Cookies = new CookiesInfo();
            try
            {
                Cookies = Utility.Get_Login_User("UserInfo", "Token");
                autoList = _orderManager.Get_Orders_No_Autocomplete_By_Dealer(dealer, Cookies.Entity_Id);
            }
            catch (Exception ex)
            {
                Logger.Error("Error At Order_Controller - Get_Orders_Autocomplete_By_Dealer " + ex.ToString());
            }
            return Json(autoList, JsonRequestBehavior.AllowGet);
        }
    }
}
