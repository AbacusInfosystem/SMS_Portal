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

        public OrdersManager _orderManager;

        public PurchaseOrderManager _purchaseOrderManager;

        public DashboardController()
        {
            _dashboardManager = new DashboardManager();

            _dealerManager = new DealerManager();

            _receivableManager = new ReceivableManager();

            _orderManager = new OrdersManager();

            _purchaseOrderManager = new PurchaseOrderManager();
        }
        
        public ActionResult Index(DashboardViewModel dViewModel)
        {
            try
            {
                dViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");

                PaginationInfo pager = new PaginationInfo();

                dViewModel.Dealers = _dealerManager.Get_Dealers(ref pager, dViewModel.Cookies.Entity_Id);

                dViewModel.Receivables = _receivableManager.Get_Receivables(ref pager, dViewModel.Cookies.Entity_Id, 2);

                if (dViewModel.Cookies.Role_Id==Convert.ToInt32(RolesIds.Admin))
                {
                    dViewModel.Sales_Orders = _orderManager.Get_Orders(ref pager, dViewModel.Cookies.Entity_Id);
                }
                else
                {
                    dViewModel.Sales_Orders = _orderManager.Get_Vendor_Orders(ref pager, dViewModel.Cookies.Entity_Id);
                }

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

        public ActionResult Get_Admin_Widgets()
        {
            DashboardViewModel dViewModel = new DashboardViewModel();

            try
            {
                dViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");

                PaginationInfo pager = new PaginationInfo();

                dViewModel.Sales_Orders = _orderManager.Get_All_Orders(ref pager);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
            }

            return PartialView("_AdminWidgets",dViewModel);
        }

        public ActionResult Get_Vendor_Widgets()
        {
            DashboardViewModel dViewModel = new DashboardViewModel();

            PaginationInfo pager = new PaginationInfo();

            try
            {
                //dViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");

                //dViewModel.PurchaseOrders = _purchaseOrderManager.Get_Purchase_Orders_By_Vendor_Id(dViewModel.Cookies.Entity_Id);

                dViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");

                //dViewModel.PurchaseOrders = _purchaseOrderManager.Get_Purchase_Orders_By_Vendor_Id(dViewModel.Cookies.Entity_Id);

                if (dViewModel.Cookies.Role_Id == Convert.ToInt32(RolesIds.Admin))
                {
                    dViewModel.Sales_Orders = _orderManager.Get_Orders(ref pager, dViewModel.Cookies.Entity_Id);
                }
                else
                {
                    dViewModel.Sales_Orders = _orderManager.Get_Vendor_Orders(ref pager, dViewModel.Cookies.Entity_Id);
                }

                //DateTime first_Slot_Todt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 07);

                //DateTime first_Slot_Frmdt = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, 21);

                //DateTime second_Slot_Frmdt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 08);

                //DateTime second_Slot_todt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 21);

                //dViewModel.Sales_Orders = _orderManager.Get_Vendor_Consolidated_Orders_Data_By_Dates(first_Slot_Frmdt, second_Slot_todt, dViewModel.Cookies.Entity_Id);

            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
            }

            return PartialView("_VendorWidgets", dViewModel);
        }

        public ActionResult Get_Third_Party_Vendor_Widgets()
        {
            DashboardViewModel dViewModel = new DashboardViewModel();

            try
            {
                dViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");

                dViewModel.PurchaseOrders = _purchaseOrderManager.Get_Purchase_Orders_By_Vendor_Id(dViewModel.Cookies.Entity_Id);

            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
            }

            return PartialView("_ThirdPartyVendorWidgets", dViewModel);
        }

        public PartialViewResult Display_Sales_Deatils(string Id)
        {
            DashboardViewModel model = new DashboardViewModel();

            model.Sales_Order.OrderItems = _orderManager.Get_Vendor_Orders_Item_By_Id(Convert.ToInt32(Id));

            return PartialView("_Vendor_Order_Details", model);
        }

    }
}
