using SMSPortal.Models.PostLogin;
using SMSPortal.Common;
using SMSPortalHelper.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMSPortalInfo.Common;
using SMSPortalHelper.PageHelper;
using SMSPortalManager;

namespace SMSPortal.Controllers.PostLogin
{
    public class InvoiceController : Controller
    {

        public InvoiceManager _invoiceManager;         
        public OrdersManager _ordersManager;
        public UserManager _userManager;
        public VendorManager _vManager;

        public InvoiceController()
        {
            _invoiceManager = new InvoiceManager();            
            _ordersManager = new OrdersManager();
            _vManager = new VendorManager();
        }

        // GET: /Invoice/
        public ActionResult Search(InvoiceViewModel iViewModel)
        {
            try
            {
                if (TempData["iViewModel"] != null)
                {
                    iViewModel = (InvoiceViewModel)TempData["iViewModel"];
                }
            }
            catch (Exception ex)
            {
                iViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("InvoiceController Search " + ex);
            }            
            return View("Search",iViewModel);
        }

        public ActionResult ViewInvoice(InvoiceViewModel iViewModel)
        {
            try
            {
                if (TempData["iViewModel"] != null)
                {
                    iViewModel = (InvoiceViewModel)TempData["iViewModel"];
                }
            }
            catch (Exception ex)
            {
                iViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("Error at InvoiceController at method ViewInvoice " + ex);
            }
            return View("ViewInvoice", iViewModel);

        }

        public ActionResult Get_Invoice_By_Id(InvoiceViewModel iViewModel)
        {
            string dealer_Address = String.Empty;
            try
            {
                if (TempData["Invoice"] != null)
                {
                    iViewModel=(InvoiceViewModel)TempData["Invoice"];
                }

                iViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");

                if (iViewModel.Cookies.Role_Id == Convert.ToInt32(RolesIds.Vendor))
                {
                    iViewModel.Invoice = _invoiceManager.Get_Vendor_Invoice_By_Id(iViewModel.Invoice.Invoice_Id, iViewModel.Cookies.Entity_Id);
                    iViewModel.Order = _ordersManager.Get_Vendor_Order_Data_By_Id(iViewModel.Invoice.Order_Id);
                    if (iViewModel.Order.Order_Id != 0)
                    {
                        iViewModel.Order.OrderItems = _ordersManager.Get_Vendor_Orders_Item_By_Id(iViewModel.Order.Vendor_Order_Id);
                        if (iViewModel.Order.OrderItems != null)
                        {
                            foreach (var item in iViewModel.Order.OrderItems)
                            {
                                item.Product = _invoiceManager.Get_Product_By_Id(item.Order_Item_Id);
                                iViewModel.Tax = _invoiceManager.Get_Tax_By_Product_By_Id(item.Order_Item_Id);
                                iViewModel.Txes.Add(iViewModel.Tax);
                                //iViewModel.Txes.Distinct();
                            }
                        }
                    }
                    iViewModel.Dealer = _invoiceManager.Get_Dealer_By_Id(iViewModel.Order.Dealer_Id);
                    iViewModel.Vendor = _vManager.Get_Vendor_By_Id(iViewModel.Order.Vendor_Id);
                }
                else
                {
                    iViewModel.Invoice = _invoiceManager.Get_Dealer_Invoice_By_Id(iViewModel.Invoice.Invoice_Id, iViewModel.Cookies.Entity_Id);
                    iViewModel.Order = _ordersManager.Get_Vendor_Order_Data_By_Id(iViewModel.Invoice.Order_Id);
                    if (iViewModel.Order.Order_Id != 0)
                    {
                        iViewModel.Order.OrderItems = _ordersManager.Get_Vendor_Orders_Item_By_Id(iViewModel.Order.Vendor_Order_Id);
                        if (iViewModel.Order.OrderItems != null)
                        {
                            foreach (var item in iViewModel.Order.OrderItems)
                            {
                                item.Product = _invoiceManager.Get_Product_By_Id(item.Order_Item_Id);
                                iViewModel.Tax = _invoiceManager.Get_Tax_By_Product_By_Id(item.Order_Item_Id);
                                iViewModel.Txes.Add(iViewModel.Tax);
                                //iViewModel.Txes.Distinct();
                            }
                        }
                    }
                    iViewModel.Dealer = _invoiceManager.Get_Dealer_By_Id(iViewModel.Order.Dealer_Id);
                    iViewModel.Vendor = _vManager.Get_Vendor_By_Id(iViewModel.Order.Vendor_Id);
                }

                if (iViewModel.Txes.Count()>0)
                {
                    if (iViewModel.Dealer.State_Name == "MAHARASHTRA")
                    {
                        var uniquePeople = from p in iViewModel.Txes
                                           group p by new { p.Local_Tax } //or group by new {p.ID, p.Name, p.Whatever}
                                               into mygroup
                                               select mygroup.FirstOrDefault();

                        iViewModel.Txes = uniquePeople.ToList();
                    }
                    else
                    {
                        var uniquePeople = from p in iViewModel.Txes
                                           group p by new { p.Export_Tax } //or group by new {p.ID, p.Name, p.Whatever}
                                               into mygroup
                                               select mygroup.FirstOrDefault();

                        iViewModel.Txes = uniquePeople.ToList();
                    }
                }
               
                
                //if (iViewModel.Cookies.Role_Id == Convert.ToInt32(RolesIds.Vendor))
                //{
                //    iViewModel.Invoice = _invoiceManager.Get_Vendor_Invoice_By_Id(iViewModel.Invoice.Invoice_Id, iViewModel.Cookies.Entity_Id);
                //    iViewModel.Order = _ordersManager.Get_Vendor_Order_Data_By_Id(iViewModel.Invoice.Order_Id);
                //    if (iViewModel.Order.Order_Id != 0)
                //    {
                //        iViewModel.Order.OrderItems = _ordersManager.Get_Vendor_Orders_Item_By_Id(iViewModel.Order.Vendor_Order_Id);
                //        if (iViewModel.Order.OrderItems != null)
                //        {
                //            foreach (var item in iViewModel.Order.OrderItems)
                //            {
                //                item.Product = _invoiceManager.Get_Product_By_Id(item.Order_Item_Id);
                //                iViewModel.Txes = _invoiceManager.Get_Tax_By_Product_By_Id(item.Order_Item_Id);
                //            }
                //        }
                //    }
                //    iViewModel.Dealer = _invoiceManager.Get_Dealer_By_Id(iViewModel.Order.Dealer_Id);
                //}
                //else
                //{
                //    iViewModel.Invoice = _invoiceManager.Get_Dealer_Invoice_By_Id(iViewModel.Invoice.Invoice_Id, iViewModel.Cookies.Entity_Id);
                //    iViewModel.Order = _invoiceManager.Get_Orders_By_Id(iViewModel.Invoice.Order_Id);
                //    if (iViewModel.Order.Order_Id != 0)
                //    {
                //        iViewModel.Order.OrderItems = _invoiceManager.Get_Order_Items_By_Order_Id(iViewModel.Order.Order_Id);
                //        if (iViewModel.Order.OrderItems != null)
                //        {
                //            foreach (var item in iViewModel.Order.OrderItems)
                //            {
                //                item.Product = _invoiceManager.Get_Product_By_Id(item.Product_Id);
                //                iViewModel.Txes = _invoiceManager.Get_Tax_By_Product_By_Id(item.Order_Item_Id);
                //            }
                //        }
                //    }
                //    iViewModel.Dealer = _invoiceManager.Get_Dealer_By_Id(iViewModel.Order.Dealer_Id);
                //}


                
            }
            catch (Exception ex)
            {
                iViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("InvoiceController Get_Invoice_By_Id " + ex);
            }

            //if (iViewModel.Cookies.Role_Id == (int)Roles.Brand)
            //{
            //    return View("View_Brand_Invoice", iViewModel);
            //}
            //else
            //{
            //    return View("ViewInvoice", iViewModel);
            //}

            return View("ViewInvoice", iViewModel);
        }

        public JsonResult Get_Invoices(InvoiceViewModel iViewModel)
        {
            PaginationInfo pager = new PaginationInfo();
            try
            {
                iViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");

                if (iViewModel.Cookies.Role_Id == Convert.ToInt32(RolesIds.Vendor))
                {
                    pager = iViewModel.Pager;
                    if (iViewModel.Filter.Invoice_Id != 0)
                    {
                        iViewModel.Invoices = _invoiceManager.Get_Vendor_Invoices_By_Id(iViewModel.Filter.Invoice_Id,iViewModel.Cookies.Entity_Id, ref pager);
                    }
                    else
                    {
                        iViewModel.Invoices = _invoiceManager.Get_Vendor_Invoices(ref pager, iViewModel.Cookies.Entity_Id);
                    }
                    iViewModel.Pager = pager;
                    iViewModel.Pager.PageHtmlString = PageHelper.NumericPager("javascript:PageMore({0})", iViewModel.Pager.TotalRecords, iViewModel.Pager.CurrentPage + 1, iViewModel.Pager.PageSize, 10, true);
                }
                else
                {
                    pager = iViewModel.Pager;
                    if (iViewModel.Filter.Invoice_Id != 0)
                    {
                        iViewModel.Invoices = _invoiceManager.Get_Invoices_By_Id(iViewModel.Filter.Invoice_Id, ref pager);
                    }
                    else
                    {
                        iViewModel.Invoices = _invoiceManager.Get_Invoices(ref pager);
                    }
                    iViewModel.Pager = pager;
                    iViewModel.Pager.PageHtmlString = PageHelper.NumericPager("javascript:PageMore({0})", iViewModel.Pager.TotalRecords, iViewModel.Pager.CurrentPage + 1, iViewModel.Pager.PageSize, 10, true);
                }
               
            }
            catch (Exception ex)
            {
                iViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("InvoiceController Get_Invoices " + ex);
            }
            return Json(iViewModel);
        }

        public ActionResult CreateInvoice()
        {
            return View("CreateInvoice");
        }

        public JsonResult Get_Invoice_Autocomplete(string Invoice_No)
        {
            InvoiceViewModel iViewModel = new InvoiceViewModel();
           List<AutocompleteInfo> autoList = new List<AutocompleteInfo>();
            try
            {
                iViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");
                autoList = _invoiceManager.Get_Invoice_Autocomplete(Invoice_No, iViewModel.Cookies.Role_Id, iViewModel.Cookies.Entity_Id);
            }
            catch (Exception ex)
            {
                Logger.Error("Error At Invoice_Controller - Get_Invoice_Autocomplete " + ex.ToString());
            }
            return Json(autoList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Send_Mail(InvoiceViewModel iViewModel)
        {
            try
            {
                iViewModel.Invoice = _invoiceManager.Get_Invoice_By_Id(iViewModel.Invoice.Invoice_Id);
                iViewModel.Order = _invoiceManager.Get_Orders_By_Id(iViewModel.Invoice.Order_Id);
                if (iViewModel.Order.Order_Id != 0)
                {
                    iViewModel.Order.OrderItems = _invoiceManager.Get_Order_Items_By_Order_Id(iViewModel.Order.Order_Id);
                }
                iViewModel.Dealer = _invoiceManager.Get_Dealer_By_Id(iViewModel.Order.Dealer_Id);
                iViewModel.Vendor = _vManager.Get_Vendor_By_Id(iViewModel.Order.Vendor_Id);
                _invoiceManager.Send_Invoice_Email(iViewModel.Dealer.Email, iViewModel.Invoice, iViewModel.Order, iViewModel.Dealer, iViewModel.Invoice.Invoice_Id, "Send Invoice", iViewModel.Cookies.Entity_Id, iViewModel.Vendor);
               iViewModel.Friendly_Message.Add(MessageStore.Get("IO001"));
            }
            catch (Exception ex)
            {
                Logger.Error("Error At Invoice_Controller - Send_Mail " + ex.ToString());
            }
            TempData["iViewModel"] = iViewModel;
            return RedirectToAction("Search", iViewModel);
        
        }

        #region Brand Invoice

        public ActionResult Search_Brand_Invoice(InvoiceViewModel iViewModel)
        {
            try
            {
                if (TempData["iViewModel"] != null)
                {
                    iViewModel = (InvoiceViewModel)TempData["iViewModel"];
                }
                FriendlyMessage friendlyMessage = (FriendlyMessage)TempData["Friendly_Message"];
                iViewModel.Friendly_Message.Add(friendlyMessage);
                iViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");
            }
            catch (Exception ex)
            {
                iViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("Error in BrandController at method Search_Invoice " + ex);
            }

            return View("Search_Brand_Invoice", iViewModel);
        }

        public JsonResult Get_Brand_Invoices(InvoiceViewModel iViewModel)
        {
            PaginationInfo pager = new PaginationInfo();
            try
            {
                pager = iViewModel.Pager;
                iViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");
                iViewModel.Invoices = _invoiceManager.Get_Brand_Invoices(iViewModel.Cookies.Entity_Id, ref pager);
            }
            catch (Exception ex)
            {
                iViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("Error at BrandController at method - Get_Brand_Invoices " + ex);
            }
            return Json(iViewModel);
        }

        public JsonResult Get_Brand_Invoice_Autocomplete(string Invoice_No, int Brand_Id)
        {            
            List<AutocompleteInfo> autoList = new List<AutocompleteInfo>();
            try
            {
                autoList = _invoiceManager.Get_Brand_Invoice_Autocomplete(Invoice_No, Brand_Id);
            }
            catch (Exception ex)
            {
                Logger.Error("Error At Invoice_Controller - Get_Brand_Invoice_Autocomplete " + ex.ToString());
            }
            return Json(autoList, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
