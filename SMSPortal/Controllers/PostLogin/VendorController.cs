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
    public class VendorController : Controller
    {
        public VendorManager _vendorManager;
        public StateManager _stateManager;

        public VendorController()
        {
            _vendorManager = new VendorManager();
            _stateManager = new StateManager();
        }

        public ActionResult Search(VendorViewModel vViewModel)
        {
            try
            {
                if (TempData["vViewModel"] != null)
                {
                    vViewModel = (VendorViewModel)TempData["vViewModel"];
                }
            }
            catch (Exception ex)
            {
                vViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("VendorController Search " + ex);
            }
            return View("Search", vViewModel);
        }

        public ActionResult Index(VendorViewModel vViewModel)
        {
            PaginationInfo Pager = new PaginationInfo();
            try
            {
                vViewModel.States = _stateManager.Get_States();
               
            }
            catch (Exception ex)
            {
                Logger.Error("VendorController - Index " + ex.Message);
            }

            return View("Index", vViewModel);
        }

        public ActionResult Insert_Vendor(VendorViewModel vViewModel)
        {
            try
            {
                vViewModel.Vendor.Created_By = ((UserInfo)Session["SessionInfo"]).User_Id;
                vViewModel.Vendor.Created_On = DateTime.Now;
                vViewModel.Vendor.Updated_By = ((UserInfo)Session["SessionInfo"]).User_Id;
                vViewModel.Vendor.Updated_On = DateTime.Now;
                _vendorManager.Insert_Vendor(vViewModel.Vendor);
                vViewModel.Friendly_Message.Add(MessageStore.Get("DO001"));
            }
            catch (Exception ex)
            {
                vViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("VendorController Insert " + ex);
            }
            TempData["vViewModel"] = vViewModel;
            return RedirectToAction("Search");
        }

        public ActionResult Update_Vendor(VendorViewModel vViewModel)
        {
            try
            {
                vViewModel.Vendor.Updated_By = ((UserInfo)Session["SessionInfo"]).User_Id;
                vViewModel.Vendor.Updated_On = DateTime.Now;
                _vendorManager.Update_Vendor(vViewModel.Vendor);
                vViewModel.Friendly_Message.Add(MessageStore.Get("DO002"));
            }
            catch (Exception ex)
            {
                vViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("VendorController Update " + ex);
            }

            TempData["vViewModel"] = vViewModel;
            return RedirectToAction("Search");
        }

        public JsonResult Get_Vendors(VendorViewModel vViewModel)
        {
            PaginationInfo pager = new PaginationInfo();
            try
            {
                pager = vViewModel.Pager;
                
                if (vViewModel.Filter.Vendor_Name != null)
                {
                    vViewModel.Vendors = _vendorManager.Get_Vendor_By_Name(vViewModel.Filter.Vendor_Name, ref pager);
                }
                else
                {
                    vViewModel.Vendors = _vendorManager.Get_Vendors(ref pager);
                }
                vViewModel.Pager = pager;
                vViewModel.Pager.PageHtmlString = PageHelper.NumericPager("javascript:PageMore({0})", vViewModel.Pager.TotalRecords, vViewModel.Pager.CurrentPage + 1, vViewModel.Pager.PageSize, 10, true);
            }
            catch (Exception ex)
            {
                vViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("VendorController Get_Vendors " + ex);
            }
            return Json(vViewModel);
        }

        public ActionResult Get_Vendor_By_Id(VendorViewModel vViewModel)
        {
            try
            {
                vViewModel.Vendor = _vendorManager.Get_Vendor_By_Id(vViewModel.Vendor.Vendor_Id);
            }
            catch (Exception ex)
            {
                vViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("Vendor Get_Vendor_By_Id " + ex);
            }

            return Index(vViewModel);
        }

        public JsonResult Check_Existing_Vendor(string Vendor_Name)
        {
            bool check = false;
            try
            {
                check = _vendorManager.Check_Existing_Vendor(Vendor_Name);
            }
            catch (Exception ex)
            {
                Logger.Error("Vendor Controller - Check_Existing_Vendor " + ex.ToString());
            }
            return Json(check, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add_Product_Mapping(VendorViewModel vViewModel)
         {
            PaginationInfo pager = new PaginationInfo();
            try
            {
                vViewModel.Brands = _vendorManager.Get_Brands(); 
              
                vViewModel.Pager = pager;
                vViewModel.Pager.PageHtmlString = PageHelper.NumericPager("javascript:PageMore({0})", vViewModel.Pager.TotalRecords, vViewModel.Pager.CurrentPage + 1, vViewModel.Pager.PageSize, 10, true);
            }

            catch (Exception ex)
            {
                vViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("VendorController Index " + ex);
            }


            return View("AddProductMapping" , vViewModel);
        }

   

        public JsonResult Get_Product_By_Brand(int Brand_Id, int CurrentPage)
        {

            VendorViewModel vViewModel = new VendorViewModel();
            PaginationInfo pager = new PaginationInfo();
            
            try
            {

                pager.CurrentPage = CurrentPage;
                vViewModel.Products = _vendorManager.Get_Productmapping(Brand_Id , ref pager);
                vViewModel.Pager = pager;
                vViewModel.Pager.PageHtmlString = PageHelper.NumericPager("javascript:PageMore({0})", vViewModel.Pager.TotalRecords, vViewModel.Pager.CurrentPage + 1, vViewModel.Pager.PageSize, 10, true);
              
            }
            catch (Exception ex)
            {
                Logger.Error("VendorController - Get_Product_By_Brand" + ex.ToString());
            }

            return Json(vViewModel, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult Add_Bank_Details()
        {
            return PartialView("_AddBankDetails");
        }

        public ActionResult SearchOrders()
        {
            return View("SearchOrders");
        }
         
        public ActionResult OrderDetails()
        {
            return View("OrderDetails");
        }

        public ActionResult CreateInvoice()
        {
            return View("CreateInvoice");
        }

        public ActionResult Profile()
        {
            return View("Profile");
        }

        public ActionResult VendorReceivables()
        {
            return View("VendorReceivables");
        }

        public ActionResult AddVendorReceivables()
        {
            return View("VendorReceivable");
        }
    }
}
