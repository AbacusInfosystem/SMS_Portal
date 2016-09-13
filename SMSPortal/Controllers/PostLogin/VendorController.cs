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
    public class VendorController : Controller
    {
         public VendorManager _vendorManager;

        public StateManager _stateManager;

        public VendorController()
        {
            _vendorManager = new VendorManager();

            _stateManager = new StateManager();
        }

        [AuthorizeUserAttribute(AppFunction.Token)]
        public ActionResult Search(NewVendorViewModel vNewViewModel)
        {
            try
            {
                if (TempData["vViewModel"] != null)
                {
                    vNewViewModel = (NewVendorViewModel)TempData["vViewModel"];
                }

                FriendlyMessage ms = (FriendlyMessage)TempData["Friendly_Message"];
                vNewViewModel.Friendly_Message.Add(ms);
            }
            catch (Exception ex)
            {
                vNewViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("VendorController Search " + ex);
            }
            return View("Search", vNewViewModel);
        }

        [AuthorizeUserAttribute(AppFunction.Token)]
        public ActionResult Vendor_Mapping(NewVendorViewModel vNewViewModel)
        {
            try
            {
                if (TempData["vViewModel"] != null)
                {
                    vNewViewModel = (NewVendorViewModel)TempData["vViewModel"];
                }

                FriendlyMessage ms = (FriendlyMessage)TempData["Friendly_Message"];
                vNewViewModel.Friendly_Message.Add(ms);
            }
            catch (Exception ex)
            {
                vNewViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("VendorController Search " + ex);
            }
            return View("Vendor_Mapping", vNewViewModel);
        }

        [AuthorizeUserAttribute(AppFunction.Token)]
        public ActionResult Index(NewVendorViewModel vNewViewModel)
        {
            PaginationInfo Pager = new PaginationInfo();
            try
            {
                vNewViewModel.Brands = _vendorManager.Get_Brands();
                vNewViewModel.States = _stateManager.Get_States();
            }
            catch (Exception ex)
            {
                vNewViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("VendorController - Index " + ex.Message);
            }

            return View("Index", vNewViewModel);
        }

        [AuthorizeUserAttribute(AppFunction.Token)]
        public ActionResult Insert_Vendor(NewVendorViewModel vNewViewModel)
        {
            try
            {
                vNewViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");

                _vendorManager.Insert_Vendor(vNewViewModel.Vendor, vNewViewModel.Cookies.User_Id);

                _vendorManager.Send_Registration_Email(vNewViewModel.Vendor.Email, vNewViewModel.Vendor.Vendor_Name);

                vNewViewModel.Friendly_Message.Add(MessageStore.Get("VO001"));
            }
            catch (Exception ex)
            {
                vNewViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("VendorController Insert " + ex);
            }
            TempData["vViewModel"] = vNewViewModel;
            return RedirectToAction("Search");
        }

        [AuthorizeUserAttribute(AppFunction.Token)]
        public ActionResult Update_Vendor(NewVendorViewModel vNewViewModel)
        {
            try
            {
                vNewViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");

                _vendorManager.Update_Vendor(vNewViewModel.Vendor, vNewViewModel.Cookies.User_Id);

                vNewViewModel.Friendly_Message.Add(MessageStore.Get("VO002"));
            }
            catch (Exception ex)
            {
                vNewViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));

                Logger.Error("VendorController Update " + ex);
            }

            TempData["vViewModel"] = vNewViewModel;
            return RedirectToAction("Search");
        }

        public JsonResult Get_Vendors(NewVendorViewModel vNewViewModel)
        {
            PaginationInfo pager = new PaginationInfo();

            vNewViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");

            try
            {
                pager = vNewViewModel.Pager;

                if (vNewViewModel.Filter.Vendor_Id != 0)
                {
                    vNewViewModel.Vendors = _vendorManager.Get_Vendor_By_Id_List(vNewViewModel.Filter.Vendor_Id, ref pager);
                }
                else
                {
                    vNewViewModel.Vendors = _vendorManager.Get_Vendors(ref pager, vNewViewModel.Cookies.Entity_Id);
                }
                vNewViewModel.Pager = pager;
                vNewViewModel.Pager.PageHtmlString = PageHelper.NumericPager("javascript:PageMore({0})", vNewViewModel.Pager.TotalRecords, vNewViewModel.Pager.CurrentPage + 1, vNewViewModel.Pager.PageSize, 10, true);
            }
            catch (Exception ex)
            {
                vNewViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));

                Logger.Error("VendorController Get_Vendors " + ex);
            }
            return Json(vNewViewModel);
        }

        [AuthorizeUserAttribute(AppFunction.Token)]
        public ActionResult Get_Vendor_By_Id(NewVendorViewModel vNewViewModel)
        {
            try
            {
                vNewViewModel.Vendor = _vendorManager.Get_Vendor_By_Id(vNewViewModel.Vendor.Vendor_Id);
            }
            catch (Exception ex)
            {
                vNewViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("Vendor Get_Vendor_By_Id " + ex);
            }

            return Index(vNewViewModel);
        }

        public JsonResult Check_Existing_Vendor(string vendor_Name)
        {
            bool check = false;
            try
            {
                check = _vendorManager.Check_Existing_Vendor(vendor_Name);
            }
            catch (Exception ex)
            {
                Logger.Error("Vendor Controller - Check_Existing_Vendor " + ex.ToString());
            }
            return Json(check, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Get_Vendor_Autocomplete(string vendor)
        {
            List<AutocompleteInfo> autoList = new List<AutocompleteInfo>();

            autoList = _vendorManager.Get_Vendor_Autocomplete(vendor);

            return Json(autoList, JsonRequestBehavior.AllowGet);
        }

        [AuthorizeUserAttribute(AppFunction.Token)]
        public ActionResult Profile(NewVendorViewModel vViewModel)
        {
            try
            {
                if (TempData["vViewModel"] != null)
                {
                    vViewModel = (NewVendorViewModel)TempData["vViewModel"];
                }

                vViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");

                vViewModel.Vendor = _vendorManager.Get_Vendor_Profile_Data_By_User_Id(vViewModel.Cookies.Entity_Id);

                vViewModel.Vendor.stateInfo = _stateManager.Get_State_By_Id(vViewModel.Vendor.State);

               // vViewModel.Vendor.BankDetailsList = _vendorManager.Get_Vendor_Bank_Details(vViewModel.Vendor.Vendor_Id);

            }
            catch (Exception ex)
            {
                vViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));

                Logger.Error("Error at Vendor controller - Profile " + ex);
            }

            return View("Profile", vViewModel);
        }

        [AuthorizeUserAttribute(AppFunction.Token)]
        public ActionResult Edit_Vendor_Profile(NewVendorViewModel vViewModel)
        {
            try
            {
                vViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");

                vViewModel.Vendor = _vendorManager.Get_Vendor_Profile_Data_By_User_Id(vViewModel.Cookies.Entity_Id);

                vViewModel.States = _stateManager.Get_States();

              //  vViewModel.Vendor.BankDetailsList = _vendorManager.Get_Vendor_Bank_Details(vViewModel.Vendor.Vendor_Id);

            }
            catch (Exception ex)
            {
                vViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));

                Logger.Error("Error at Vendor controller - Profile " + ex);
            }

            return View("Update_Profile", vViewModel);
        }

        [AuthorizeUserAttribute(AppFunction.Token)]
        public ActionResult Update_Vendor_Profile(NewVendorViewModel vViewModel)
        {
            try
            {
                vViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");

                _vendorManager.Update_Vendor_Profile(vViewModel.Vendor, vViewModel.Cookies.User_Id);

                vViewModel.Friendly_Message.Add(MessageStore.Get("VO004"));

                TempData["vViewModel"] = vViewModel;
            }
            catch (Exception ex)
            {
                vViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));

                Logger.Error("Error at Vendor Controller - Update_Vendor_Profile " + ex);
            }

            return RedirectToAction("Profile");
        }

        public PartialViewResult Add_Vendor_Logo(string Id)
        {
            NewVendorViewModel model = new NewVendorViewModel();

            model.Vendor = _vendorManager.Get_Vendor_Logo_By_Id(Convert.ToInt32(Id));

            return PartialView("_Upload_Vendor_Logo", model);
        }

        public ActionResult Vendor_Logo_Upload(NewVendorViewModel vViewModel)
        {
            // Code to Upload Excel File 
            var actualFileName = "";
            var fileName = "";
            var path = "";
            //bool is_Error = false;    

            try
            {
                if (vViewModel.Upload_Logo.ContentLength > 0)
                {
                    actualFileName = Path.GetFileName(vViewModel.Upload_Logo.FileName);
                    path = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["BrandLogoPath"].ToString()), actualFileName);

                    // Logger.Debug("*************************** " + path.ToString());
                    vViewModel.Upload_Logo.SaveAs(path);
                    _vendorManager.Update_Vendor_FileName(vViewModel.Vendor.Vendor_Id, actualFileName);

                    if (vViewModel.Vendor.Vendor_Logo != null)
                    {
                        System.IO.File.Delete(Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["BrandLogoPath"].ToString()), vViewModel.Vendor.Vendor_Logo));
                        vViewModel.Friendly_Message.Add(MessageStore.Get("VO006"));
                    }
                    else
                    {
                        vViewModel.Friendly_Message.Add(MessageStore.Get("VO006"));
                    }

                }
            }
            catch (Exception ex)
            {
                System.IO.File.Delete(path);
                vViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("Error uploading Brand Logo  " + ex.Message);
            }
            TempData["vViewModel"] = vViewModel;
            return RedirectToAction("Search");
        }

    }
}
