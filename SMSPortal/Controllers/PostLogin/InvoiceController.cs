﻿using SMSPortal.Models.PostLogin;
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

        public InvoiceController()
        {
            _invoiceManager = new InvoiceManager();            
            _ordersManager = new OrdersManager();
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
                iViewModel.Invoice = _invoiceManager.Get_Invoice_By_Id(iViewModel.Invoice.Invoice_Id);
                iViewModel.Order = _invoiceManager.Get_Orders_By_Id(iViewModel.Invoice.Order_Id);
                if (iViewModel.Order.Order_Id != 0)
                {
                    iViewModel.Order.OrderItems = _invoiceManager.Get_Order_Items_By_Order_Id(iViewModel.Order.Order_Id);
                    if (iViewModel.Order.OrderItems != null)
                    { 
                        foreach(var item in iViewModel.Order.OrderItems)
                        {
                            item.Product = _invoiceManager.Get_Product_By_Id(item.Product_Id);
                        }
                    }
                }
                iViewModel.Dealer = _invoiceManager.Get_Dealer_By_Id(iViewModel.Order.Dealer_Id);                
            }
            catch (Exception ex)
            {
                iViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("InvoiceController Get_Invoice_By_Id " + ex);
            }

            return View("ViewInvoice", iViewModel);
        }

        public JsonResult Get_Invoices(InvoiceViewModel iViewModel)
        {
            PaginationInfo pager = new PaginationInfo();
            try
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
            
           List<AutocompleteInfo> autoList = new List<AutocompleteInfo>();
            try
            {
                autoList = _invoiceManager.Get_Invoice_Autocomplete(Invoice_No);
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
               _invoiceManager.Send_Invoice_Email(iViewModel.Dealer.Email, iViewModel.Invoice, iViewModel.Order,iViewModel.Dealer);
               iViewModel.Friendly_Message.Add(MessageStore.Get("IO001"));
            }
            catch (Exception ex)
            {
                Logger.Error("Error At Invoice_Controller - Send_Mail " + ex.ToString());
            }
            TempData["iViewModel"] = iViewModel;
            return RedirectToAction("Search", iViewModel);
        
        }

    }
}
