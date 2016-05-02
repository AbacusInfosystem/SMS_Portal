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
    public class CategoryController : Controller
    {
        public CategoryManager _categoryManager;      
        public CategoryController()
        {
            _categoryManager = new CategoryManager();
        }
        // GET: /Category/
        public ActionResult Search(CategoryViewModel cViewModel)
        {             
            try
            { 
                if (TempData["cViewModel"] != null)
                {
                    cViewModel=(CategoryViewModel)TempData["cViewModel"];
                }
            }
            catch (Exception ex)
            {
                cViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("UserController Search " + ex);
            }
            return View("Search",cViewModel);
        }

        public JsonResult Get_Categories(CategoryViewModel cViewModel)
        {
            PaginationInfo pager = new PaginationInfo();
            try
            {
                pager = cViewModel.Pager;
                if (cViewModel.Filter.Category_Id != 0)
                {
                    cViewModel.Categories = _categoryManager.Get_Categorys_By_Id(cViewModel.Filter.Category_Id, ref pager);
                }
                else
                {
                    cViewModel.Categories = _categoryManager.Get_Categorys(ref pager);
                }

                cViewModel.Pager = pager;

                cViewModel.Pager.PageHtmlString = PageHelper.NumericPager("javascript:PageMore({0})", cViewModel.Pager.TotalRecords, cViewModel.Pager.CurrentPage + 1, cViewModel.Pager.PageSize, 10, true);

            }
            catch (Exception ex)
            {
                cViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("UserController Get_Categories " + ex);

            }

            return Json(cViewModel);
        }

        public ActionResult Index(CategoryViewModel cViewModel)
        {
            PaginationInfo Pager = new PaginationInfo();
            try
            {
                Pager.IsPagingRequired = false;               

            }
            catch(Exception ex)
            {
                cViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("UserController Index " + ex);
            }           
            return View("Index",cViewModel);
        }

        public ActionResult Insert_Category(CategoryViewModel cViewModel)
        {
            try
            {
                cViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");
                cViewModel.Category.Created_By = cViewModel.Cookies.User_Id;
                cViewModel.Category.Created_On = DateTime.Now;
                cViewModel.Category.Updated_By = cViewModel.Cookies.User_Id;
                cViewModel.Category.Updated_On = DateTime.Now;
                cViewModel.Category.Category_Id = _categoryManager.Insert_Category(cViewModel.Category);
                cViewModel.Friendly_Message.Add(MessageStore.Get("CO001"));
            }
            catch (Exception ex)
            {
                cViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("CategoryController Insert " + ex);
            }
            TempData["cViewModel"] = cViewModel;
            return RedirectToAction("Search");
        }

        public ActionResult Update_Category(CategoryViewModel cViewModel)
        {
            try
            {
                cViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");
                cViewModel.Category.Updated_By = cViewModel.Cookies.User_Id;
                cViewModel.Category.Updated_On = DateTime.Now;
                _categoryManager.Update_Category(cViewModel.Category);
                cViewModel.Friendly_Message.Add(MessageStore.Get("CO002"));
            }
            catch (Exception ex)
            {
                cViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("CategoryController Update " + ex);
            }

            TempData["cViewModel"] = cViewModel;
            return RedirectToAction("Search");
        }

        public JsonResult Check_Existing_Category(string Category_Name)
        {
            bool check = false;

            try
            {
                check = _categoryManager.Check_Existing_Category(Category_Name);
            }
            catch (Exception ex)
            {
                Logger.Error("User Controller - Check_Existing_User " + ex.ToString());
            }

            return Json(check, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Get_Category_By_Id(CategoryViewModel cViewModel)
        {
            try
            {
                cViewModel.Category = _categoryManager.Get_Category_By_Id(cViewModel.Category.Category_Id);

            }
            catch (Exception ex)
            {
                cViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("CategoryController Get_Category_By_Id " + ex);
            }

            return View("Index", cViewModel);
        }

        public JsonResult Get_Category_Autocomplete(string Category)
        {
            List<AutocompleteInfo> autoList = new List<AutocompleteInfo>();
            try
            {
                autoList = _categoryManager.Get_Category_Autocomplete(Category);
            }
            catch (Exception ex)
            {
                Logger.Error("Error At Category_Controller - Get_Category_Autocomplete " + ex.ToString());
            }
            return Json(autoList, JsonRequestBehavior.AllowGet);
        }
    }
}
