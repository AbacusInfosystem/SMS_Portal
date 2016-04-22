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

        public CookiesInfo cookies;

        public string token = System.Web.HttpContext.Current.Request.Cookies["UserInfo"]["Token"];

        public SubCategoryController()
        {
            _subcategoryManager = new SubCategoryManager();

            CookiesManager _cookiesManager = new CookiesManager();

            cookies = _cookiesManager.Get_Token_Data(token);            
        }

        public ActionResult Search(SubCategoryViewModel sViewModel)
        {
            try
            {
                if (TempData["sViewModel"] != null)
                {
                    sViewModel = (SubCategoryViewModel)TempData["sViewModel"];
                }
                sViewModel.Masters = _subcategoryManager.Get_Subcategory_Modules();
            }
            catch (Exception ex)
            {
                Logger.Error("SubCategoryController - Search" + ex.Message);
            }

            return View("Search", sViewModel);
        }

        public ActionResult AddEdit_SubCategory(SubCategoryViewModel sViewModel)
        {
            try
            {
                sViewModel.Categories = _subcategoryManager.Get_Categories();
            }
            catch (Exception ex)
            {
                Logger.Error("SubCategoryController - Index " + ex.Message);
            }

            return View("AddEdit_SubCategory", sViewModel);
        }

        public JsonResult Get_SubCategories(SubCategoryViewModel sViewModel)
        {
            PaginationInfo pager = new PaginationInfo();

            try
            {
                pager = sViewModel.Pager;

                if (sViewModel.Filter.Module_Id != 0)
                {
                    sViewModel.SubCategories = _subcategoryManager.Get_Subcategories_By_Id(sViewModel.Filter.Module_Id, ref pager);
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
                Logger.Error("SubCategoryController - Get_Roles" + ex.Message);
            }

            return Json(sViewModel);
        }

        public ActionResult Insert_Subcategory(SubCategoryViewModel sViewModel)
        {
            try
            {
                string UserName = cookies.User_Name;

                sViewModel.SubCategory.Created_By = ((SessionInfo)Session["SessionInfo"]).User_Id;

                sViewModel.SubCategory.Created_Date = DateTime.Now;

                sViewModel.SubCategory.Updated_By = ((SessionInfo)Session["SessionInfo"]).User_Id;

                sViewModel.SubCategory.Updated_Date = DateTime.Now;

                _subcategoryManager.Insert_Sub_Category(sViewModel.SubCategory);

                sViewModel.Friendly_Message.Add(MessageStore.Get("SBO001"));
            }
            catch (Exception ex)
            {
                sViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));

                Logger.Error("Error At SubCategory Insert  " + ex.Message);
            }

            TempData["sViewModel"] = sViewModel;

            return RedirectToAction("Search");
        }

        public ActionResult Update_Subcategory(SubCategoryViewModel sViewModel)
        {
            try
            {
                string UserName = cookies.User_Name;

                sViewModel.SubCategory.Created_By = ((SessionInfo)Session["SessionInfo"]).User_Id;

                sViewModel.SubCategory.Created_Date = DateTime.Now;

                sViewModel.SubCategory.Updated_By = ((SessionInfo)Session["SessionInfo"]).User_Id;

                sViewModel.SubCategory.Updated_Date = DateTime.Now;

                _subcategoryManager.Update_Sub_Category(sViewModel.SubCategory);

                sViewModel.Friendly_Message.Add(MessageStore.Get("SBO002"));
            }
            catch (Exception ex)
            {
                sViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));

                Logger.Error("Error At SubCategory Insert  " + ex.Message);
            }

            TempData["sViewModel"] = sViewModel;

            return RedirectToAction("Search");
        }

        public ActionResult Get_Subcategory_By_Id(SubCategoryViewModel sViewModel)
        {
            try
            {
                sViewModel.SubCategory = _subcategoryManager.Get_Subcategory_By_Id(sViewModel.SubCategory.Subcategory_Id);
            }
            catch (Exception ex)
            {
                Logger.Error("SubCategoryController - Get_Roles" + ex.Message);
            }

            return AddEdit_SubCategory(sViewModel);
        }

        public JsonResult Get_Subcategory_Autocomplete(string subcategory)
        {
            List<AutocompleteInfo> autoList = new List<AutocompleteInfo>();
            autoList = _subcategoryManager.Get_Subcategory_Autocomplete(subcategory);
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
                Logger.Error("SubCategoryController - Check_Existing_Sub_Category " + ex.ToString());
            }

            return Json(check, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult Get_Subcategory_Popup()
        {
            return PartialView("_SubCategoruPopup");
        }
    }
}
