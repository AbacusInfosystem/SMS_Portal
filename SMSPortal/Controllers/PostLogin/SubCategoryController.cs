using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMSPortal.Models.PostLogin;
using SMSPortalManager;
using SMSPortal.Common;
using SMSPortalHelper.Logging;
using SMSPortalInfo.Common;
using SMSPortalInfo;
using SMSPortalHelper.PageHelper;

namespace SMSPortal.Controllers.PostLogin
{
    public class SubCategoryController : Controller
    {
        public SubCategoryManager _subcategoryManager;        

        public CookiesInfo _cookies;

        public string token = System.Web.HttpContext.Current.Request.Cookies["UserInfo"]["Token"];

        public SubCategoryController()
        {
            _subcategoryManager = new SubCategoryManager();           
        }

        public ActionResult Search(SubCategoryViewModel sViewModel)
        {
            try
            {
                if (TempData["sViewModel"] != null)
                {
                    sViewModel = (SubCategoryViewModel)TempData["sViewModel"];
                }
            }
            catch (Exception ex)
            {
                sViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));

                Logger.Error("Error At Sub_Category_Controller - Search" + ex.Message);
            }

            return View("Search", sViewModel);
        }

        public ActionResult Index(SubCategoryViewModel sViewModel)
        {
            try
            {
                sViewModel.Categories = _subcategoryManager.Get_Categories();
            }
            catch (Exception ex)
            {
                sViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));

                Logger.Error("Error At Sub_Category_Controller - Index " + ex.Message);
            }

            return View("Index", sViewModel);
        }

        public JsonResult Get_SubCategories(SubCategoryViewModel sViewModel)
        {
            PaginationInfo pager = new PaginationInfo();

            try
            {
                pager = sViewModel.Pager;

                if (sViewModel.Filter.SubCategory_Id != 0)
                {
                    sViewModel.SubCategories = _subcategoryManager.Get_Subcategories_By_Id(sViewModel.Filter.SubCategory_Id, ref pager);
                }
                else
                {
                    sViewModel.SubCategories = _subcategoryManager.Get_Subcategories(ref pager);
                }

                sViewModel.Pager.PageHtmlString = PageHelper.NumericPager("javascript:PageMore({0})", sViewModel.Pager.TotalRecords, sViewModel.Pager.CurrentPage + 1, sViewModel.Pager.PageSize, 10, true);

                sViewModel.Pager = pager;
            }
            catch (Exception ex)
            {
                Logger.Error("Error At Sub_Category_Controller - Get_SubCategories" + ex.Message);
            }

            return Json(sViewModel);
        }

        public ActionResult Insert_Subcategory(SubCategoryViewModel sViewModel)
        {
            try
            {
                sViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");

                if (sViewModel.Cookies.User_Id == 0)
                {
                    return RedirectToAction("Logout", "Login");
                }
                else
                {
                    _subcategoryManager.Insert_Sub_Category(sViewModel.SubCategory, sViewModel.Cookies.User_Id);

                    sViewModel.Friendly_Message.Add(MessageStore.Get("SBO001"));
                }
            }
            catch (Exception ex)
            {
                sViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));

                Logger.Error("Error At Sub_Category_Controller - Insert  " + ex.Message);
            }

            TempData["sViewModel"] = sViewModel;

            return RedirectToAction("Search");
        }

        public ActionResult Update_Subcategory(SubCategoryViewModel sViewModel)
        {
            try
            {
                sViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");

                if (sViewModel.Cookies.User_Id == 0)
                {
                    return RedirectToAction("Logout", "Login");
                }
                else
                {
                    _subcategoryManager.Update_Sub_Category(sViewModel.SubCategory, sViewModel.Cookies.User_Id);

                    sViewModel.Friendly_Message.Add(MessageStore.Get("SBO002"));
                }
            }
            catch (Exception ex)
            {
                sViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));

                Logger.Error("Error At Sub_Category_Controller - Update_Subcategory " + ex.Message);
            }

            TempData["sViewModel"] = sViewModel;

            return RedirectToAction("Search");
        }

        public ActionResult Get_Subcategory_By_Id(SubCategoryViewModel sViewModel)
        {
            try
            {
                sViewModel.SubCategory = _subcategoryManager.Get_Subcategory_By_Id(sViewModel.SubCategory.Subcategory_Id);

                sViewModel.Categories = _subcategoryManager.Get_Categories();
            }
            catch (Exception ex)
            {
                sViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));

                Logger.Error("Error At Sub_Category_Controller - Get_Subcategory_By_Id" + ex.Message);
            }

            return View("Index",sViewModel);
        }

        public JsonResult Get_Subcategory_Autocomplete(string subcategory)
        {
            List<AutocompleteInfo> autoList = new List<AutocompleteInfo>();

            try 
            {               
                autoList = _subcategoryManager.Get_Subcategory_Autocomplete(subcategory);                
            }
            catch (Exception ex)
            {
                Logger.Error("Error At Sub_Category_Controller - Get_Subcategory_Autocomplete " + ex.ToString());
            }

            return Json(autoList, JsonRequestBehavior.AllowGet);           
        }

        public JsonResult Check_Existing_Sub_Category(string subcategory)
        {
            bool check = false;

            try
            {
                check = _subcategoryManager.Check_Existing_Sub_Category(subcategory);
            }
            catch (Exception ex)
            {
                Logger.Error("Error At Sub_Category_Controller - Check_Existing_Sub_Category " + ex.ToString());
            }

            return Json(check, JsonRequestBehavior.AllowGet);
        }

    }
}
