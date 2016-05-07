using SMSPortal.Common;
using SMSPortal.Models.PostLogin;
using SMSPortalHelper.Logging;
using SMSPortalHelper.PageHelper;
using SMSPortalInfo;
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

        public JsonResult Insert_Update_Purchase_Order(PurchaseOrderViewModel pViewModel)
        {
            
            try
            {
                ProductManager _productManager = new ProductManager();

                pViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");
                pViewModel.PurchaseOrder.Created_By = pViewModel.Cookies.User_Id;
                pViewModel.PurchaseOrder.Created_On = DateTime.Now;
                pViewModel.PurchaseOrder.Updated_By = pViewModel.Cookies.User_Id;
                pViewModel.PurchaseOrder.Updated_On = DateTime.Now;
               
                pViewModel.PurchaseOrderItem.Created_By = pViewModel.Cookies.User_Id;
                pViewModel.PurchaseOrderItem.Created_On = DateTime.Now;
                pViewModel.PurchaseOrderItem.Updated_By = pViewModel.Cookies.User_Id;
                pViewModel.PurchaseOrderItem.Updated_On = DateTime.Now;

                if (pViewModel.PurchaseOrder.Purchase_Order_Id != 0)
                {                    
                    if (pViewModel.PurchaseOrderItem.Purchase_Order_Item_Id != 0)
                    {
                        pViewModel.PurchaseOrderItem.Product_Price = pViewModel.PurchaseOrderItem.Product_Quantity * pViewModel.PurchaseOrderItem.Product_Unit_Price;

                        if (pViewModel.PurchaseOrderItem.Received_Quantity > 0)
                        {
                            pViewModel.PurchaseOrderItem.Status = (int)PurchaseOrderStatus.Patially_Received;
                        }
                        if (pViewModel.PurchaseOrderItem.Received_Quantity == pViewModel.PurchaseOrderItem.Product_Quantity)
                        {
                            pViewModel.PurchaseOrderItem.Status = (int)PurchaseOrderStatus.Received;
                        }

                        _purchaseOrderManager.Update_Purchase_Order_Item(pViewModel.PurchaseOrderItem);
                        pViewModel.Friendly_Message.Add(MessageStore.Get("POR004"));
                    }
                    else
                    {
                        pViewModel.PurchaseOrderItem.Product_Price = pViewModel.PurchaseOrderItem.Product_Quantity * pViewModel.PurchaseOrderItem.Product_Unit_Price;

                        pViewModel.PurchaseOrderItem.Status = (int)PurchaseOrderStatus.Ordered;
                        _purchaseOrderManager.Insert_Purchase_Order_Item(pViewModel.PurchaseOrderItem);
                        pViewModel.PurchaseOrderItems = _purchaseOrderManager.Get_Purchase_Order_Items_By_Id(pViewModel.PurchaseOrder.Purchase_Order_Id);                         
                        pViewModel.Friendly_Message.Add(MessageStore.Get("POR003"));
                    }

                    pViewModel.PurchaseOrderItems = _purchaseOrderManager.Get_Purchase_Order_Items_By_Id(pViewModel.PurchaseOrder.Purchase_Order_Id);
                    pViewModel.PurchaseOrder.Gross_Amount = pViewModel.PurchaseOrderItems.Sum(item => item.Product_Price);

                    _purchaseOrderManager.Update_Purchase_Order_Gross_Amount(pViewModel.PurchaseOrder.Purchase_Order_Id, pViewModel.PurchaseOrder.Gross_Amount);
                    _purchaseOrderManager.Update_Purchase_Order(pViewModel.PurchaseOrder);
                }
                else
                {
                    pViewModel.PurchaseOrder.Purchase_Order_No = Utility.Generate_Ref_No("PO000", "Purchase_Order_No", "3", "15", "Purchase_Order");
                    pViewModel.PurchaseOrder.Status = (int)PurchaseOrderStatus.Ordered;
                    pViewModel.PurchaseOrder.Purchase_Order_Id = _purchaseOrderManager.Insert_Purchase_Order(pViewModel.PurchaseOrder);
                    pViewModel.Friendly_Message.Add(MessageStore.Get("POR001"));
                    if (pViewModel.PurchaseOrder.Purchase_Order_Id != 0)
                    {
                        pViewModel.PurchaseOrderItem.Purchase_Order_Id = pViewModel.PurchaseOrder.Purchase_Order_Id;
                        pViewModel.PurchaseOrderItem.Product_Price = pViewModel.PurchaseOrderItem.Product_Quantity * pViewModel.PurchaseOrderItem.Product_Unit_Price;

                        pViewModel.PurchaseOrderItem.Status = (int)PurchaseOrderStatus.Ordered;
                        _purchaseOrderManager.Insert_Purchase_Order_Item(pViewModel.PurchaseOrderItem);

                        pViewModel.PurchaseOrderItems = _purchaseOrderManager.Get_Purchase_Order_Items_By_Id(pViewModel.PurchaseOrder.Purchase_Order_Id);
                        pViewModel.PurchaseOrder.Gross_Amount = pViewModel.PurchaseOrderItems.Sum(item => item.Product_Price);

                        _purchaseOrderManager.Update_Purchase_Order_Gross_Amount(pViewModel.PurchaseOrder.Purchase_Order_Id, pViewModel.PurchaseOrder.Gross_Amount);
                        pViewModel.Friendly_Message.Add(MessageStore.Get("POR003"));
                    }
                }
                pViewModel.PurchaseOrder = _purchaseOrderManager.Get_Purchase_Order_By_Id(pViewModel.PurchaseOrder.Purchase_Order_Id);
                pViewModel.PurchaseOrderItems = _purchaseOrderManager.Get_Purchase_Order_Items_By_Id(pViewModel.PurchaseOrder.Purchase_Order_Id);

            }
            catch (Exception ex)
            {
                pViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("PurchaseOrderController Insert " + ex);
            }
            TempData["pViewModel"] = pViewModel;
            //return RedirectToAction("Search");
            return Json(pViewModel, JsonRequestBehavior.AllowGet);
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

            // return Json(pViewModel, JsonRequestBehavior.AllowGet);
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
            decimal gross_amount=0;
            List<FriendlyMessage> Friendly_Message = new List<FriendlyMessage>();
            PurchaseOrderViewModel pViewModel = new PurchaseOrderViewModel();
            pViewModel.PurchaseOrder.Purchase_Order_Id = Purchase_Order_Id;
            try
            {
                _purchaseOrderManager.Delete_Purchase_Order_Item_By_Id(Purchase_Order_Item_Id);
                pViewModel.PurchaseOrderItems = _purchaseOrderManager.Get_Purchase_Order_Items_By_Id(pViewModel.PurchaseOrder.Purchase_Order_Id);
                gross_amount=pViewModel.PurchaseOrderItems.Sum(item => item.Product_Price);
                _purchaseOrderManager.Update_Purchase_Order_Gross_Amount(pViewModel.PurchaseOrder.Purchase_Order_Id, gross_amount);
                pViewModel.PurchaseOrder.Gross_Amount = gross_amount;
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

        public JsonResult Check_Duplicate_ProductItems(int Product_Id,int Purchase_Order_Id)
        {
            bool check = false;
            try
            {
                check = _purchaseOrderManager.Check_Duplicate_Product_PurchaseOrder(Product_Id, Purchase_Order_Id);
            }
            catch (Exception ex)
            {
                Logger.Error("PurchaseOrder Controller - Check_Duplicate_ProductItems " + ex.ToString());
            }
            return Json(check, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Get_Product(int Product_Id, int Vendor_Id)
        {            
            ProductViewModel vModel = new ProductViewModel();
            try
            {
                vModel.Product = _purchaseOrderManager.Get_Vendor_Product_Price_Id(Product_Id, Vendor_Id);
            }
            catch (Exception ex)
            {
                Logger.Error("PurchaseOrder Controller - Get_Product " + ex.ToString());
            }
            return Json(vModel, JsonRequestBehavior.AllowGet);
        }


       
    }
}
