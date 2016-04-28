using SMSPortal.Common;
using SMSPortal.Models.PostLogin;
using SMSPortalHelper.Logging;
using SMSPortalHelper.PageHelper;
using SMSPortalInfo.Common;
using SMSPortalManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMSPortal.Controllers.PostLogin
{
    public class PurchaseOrderController : Controller
    {
        public PurchaseOrderManager _purchaseOrderManager;

        public PurchaseOrderController()
        {
            _purchaseOrderManager = new PurchaseOrderManager();
        }
        public ActionResult Search(PurchaseOrderViewModel pViewModel)
        {
            try
            {
                if (TempData["pViewModel"] != null)
                {
                    pViewModel = (PurchaseOrderViewModel)TempData["pViewModel"];
                }
            }
            catch (Exception ex)
            {
                pViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("PurchaseOrderController Search " + ex);
            }
            return View("Search", pViewModel);
        }

        public ActionResult AddEdit_Purchase_Order(PurchaseOrderViewModel pViewModel)
        {
            PaginationInfo Pager = new PaginationInfo();
            try 
            {
                pViewModel.PurchaseOrders = _purchaseOrderManager.Get_Purchase_Orders(ref Pager);
            }
            catch (Exception ex)
            {
                Logger.Error("PurchaseOrderController - AddEdit_Purchase_Order " + ex.Message);
            }

            return View("AddEdit_Purchase_Order", pViewModel);             
        }

        public ActionResult Insert_Purchase_Order(PurchaseOrderViewModel pViewModel)
        {
            try
            {
                pViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");
                pViewModel.PurchaseOrder.Created_By = pViewModel.Cookies.User_Id;
                pViewModel.PurchaseOrder.Created_On = DateTime.Now;
                pViewModel.PurchaseOrder.Updated_By = pViewModel.Cookies.User_Id;
                pViewModel.PurchaseOrder.Updated_On = DateTime.Now;

                pViewModel.PurchaseOrder.Purchase_Order_Id=_purchaseOrderManager.Insert_Purchase_Order(pViewModel.PurchaseOrder);


                pViewModel.Friendly_Message.Add(MessageStore.Get("DO001"));
            }
            catch (Exception ex)
            {
                pViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("PurchaseOrderController Insert " + ex);
            }
            TempData["pViewModel"] = pViewModel;
            return RedirectToAction("Search");
        }

        public ActionResult Update_Purchase_Order(PurchaseOrderViewModel pViewModel)
        {
            try
            {
                pViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");
                pViewModel.PurchaseOrder.Updated_By = pViewModel.Cookies.User_Id;
                pViewModel.PurchaseOrder.Updated_On = DateTime.Now;
                _purchaseOrderManager.Update_Purchase_Order(pViewModel.PurchaseOrder);
                pViewModel.Friendly_Message.Add(MessageStore.Get("DO002"));
            }
            catch (Exception ex)
            {
                pViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("PurchaseOrderController Update " + ex);
            }

            TempData["pViewModel"] = pViewModel;
            return RedirectToAction("Search");
        }

        public ActionResult Get_Purchase_Order_By_Id(PurchaseOrderViewModel pViewModel)
        {
            try
            {
                pViewModel.PurchaseOrder = _purchaseOrderManager.Get_Purchase_Order_By_Id(pViewModel.PurchaseOrder.Purchase_Order_Id);
                pViewModel.PurchaseOrderItems = _purchaseOrderManager.Get_Purchase_Order_Items_By_Id(pViewModel.PurchaseOrder.Purchase_Order_Id);
            }
            catch (Exception ex)
            {
                pViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("PurchaseOrderController Get_Purchase_Order_By_Id " + ex);
            }

            return AddEdit_Purchase_Order(pViewModel);
        }

        public JsonResult Get_Purchase_Orders_Items(PurchaseOrderViewModel pViewModel)
        {
            PaginationInfo pager = new PaginationInfo();
            try
            {
                pViewModel.PurchaseOrderItems = _purchaseOrderManager.Get_Purchase_Order_Items_By_Id(pViewModel.PurchaseOrder.Purchase_Order_Id);
            }
            catch (Exception ex)
            {
                pViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("PurchaseOrderController Get_Purchase_Orders_Items " + ex);
            }
            return Json(pViewModel);
        }

        public JsonResult Get_Purchase_Orders(PurchaseOrderViewModel pViewModel)
        {
            PaginationInfo pager = new PaginationInfo();
            try
            {
                pager = pViewModel.Pager;
                if (pViewModel.Filter.Purchase_Order_Id != 0)
                {
                    pViewModel.PurchaseOrders = _purchaseOrderManager.Get_Purchase_Orders_By_Id(pViewModel.Filter.Purchase_Order_Id, ref pager);
                }
                else
                {
                    pViewModel.PurchaseOrders = _purchaseOrderManager.Get_Purchase_Orders(ref pager);
                }
                pViewModel.Pager = pager;
                pViewModel.Pager.PageHtmlString = PageHelper.NumericPager("javascript:PageMore({0})", pViewModel.Pager.TotalRecords, pViewModel.Pager.CurrentPage + 1, pViewModel.Pager.PageSize, 10, true);
            }
            catch (Exception ex)
            {
                pViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("PurchaseOrderController Get_Purchase_Orders " + ex);
            }
            return Json(pViewModel);
        }
        
        public JsonResult Get_Purchase_Order_Autocomplete(string Purchase_Order_No)
        {
            List<AutocompleteInfo> autoList = new List<AutocompleteInfo>();
            try
            {
                autoList = _purchaseOrderManager.Get_Purchase_Order_Autocomplete(Purchase_Order_No);
            }
            catch (Exception ex)
            {
                Logger.Error("Error At Purchase_Controller - Get_Purchase_Order_Autocomplete " + ex.ToString());
            }
            return Json(autoList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete_Purchase_Order_Item(int Purchase_Order_Item_Id, int Purchase_Order_Id)
        {
            List<FriendlyMessage> Friendly_Message = new List<FriendlyMessage>();
            PurchaseOrderViewModel pViewModel= new PurchaseOrderViewModel();
            pViewModel.PurchaseOrder.Purchase_Order_Id = Purchase_Order_Id;
            try
            {
                _purchaseOrderManager.Delete_Purchase_Order_Item_By_Id(Purchase_Order_Item_Id);
                pViewModel.PurchaseOrderItems = _purchaseOrderManager.Get_Purchase_Order_Items_By_Id(pViewModel.PurchaseOrder.Purchase_Order_Id);

                pViewModel.Friendly_Message.Add(MessageStore.Get("POR005"));
            }
            catch (Exception ex)
            {
                Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("PurchaseOrder-Controller - Delete " + ex.ToString());
            }
            return Json(pViewModel, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Add_Purchase_Order_Item()
        {
            return View("AddPurchaseOrderItem");
        }
    }
}
