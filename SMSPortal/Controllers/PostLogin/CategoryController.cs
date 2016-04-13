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
        public ActionResult Search(CategoryViewModel categoryViewModel)
        {             
            try
            { 
                if (TempData["categoryViewMessage"] != null)
                {
                    categoryViewModel=(CategoryViewModel)TempData["categoryViewMessage"];
                }
            }
            catch (Exception ex)
            {
                categoryViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("UserController Search " + ex);
            }
            return View("Search",categoryViewModel);
        }

        public JsonResult Get_Categories(CategoryViewModel categoryViewModel)
        {
            PaginationInfo pager = new PaginationInfo();
            try
            {
                pager = categoryViewModel.Pager;
                if (categoryViewModel.Filter.Category_Name != null)
                {
                    categoryViewModel.Categories = _categoryManager.Get_Categorys_By_Name(categoryViewModel.Filter.Category_Name, ref pager);
                }
                else
                {
                    categoryViewModel.Categories = _categoryManager.Get_Categorys(ref pager);
                }

                categoryViewModel.Pager = pager;

                categoryViewModel.Pager.PageHtmlString = PageHelper.NumericPager("javascript:PageMore({0})", categoryViewModel.Pager.TotalRecords, categoryViewModel.Pager.CurrentPage + 1, categoryViewModel.Pager.PageSize, 10, true);

            }
            catch (Exception ex)
            {
                categoryViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("UserController Get_Categories " + ex);

            }

            return Json(categoryViewModel);
        }
        public ActionResult Index(CategoryViewModel categoryViewModel)
        {
            PaginationInfo Pager = new PaginationInfo();
            try
            {
                Pager.IsPagingRequired = false;               

            }
            catch(Exception ex)
            {
                categoryViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("UserController Index " + ex);
            }           
            return View("Index",categoryViewModel);
        }

        public ActionResult Insert_Category(CategoryViewModel categoryViewModel)
        {
            try
            {
                categoryViewModel.Category.Created_By = ((UserInfo)Session["SessionInfo"]).User_Id;
                categoryViewModel.Category.Created_On = DateTime.Now;
                categoryViewModel.Category.Updated_By = ((UserInfo)Session["SessionInfo"]).User_Id;
                categoryViewModel.Category.Updated_On = DateTime.Now;
                categoryViewModel.Category.Category_Id = _categoryManager.Insert_Category(categoryViewModel.Category);
                categoryViewModel.Friendly_Message.Add(MessageStore.Get("CO001"));
            }
            catch (Exception ex)
            {
                categoryViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("CategoryController Insert " + ex);
            }
            TempData["categoryViewMessage"] = categoryViewModel;
            return RedirectToAction("Search");
        }

        public ActionResult Update_Category(CategoryViewModel categoryViewModel)
        {
            try
            {
                categoryViewModel.Category.Updated_By = ((UserInfo)Session["SessionInfo"]).User_Id;
                categoryViewModel.Category.Updated_On = DateTime.Now;
                _categoryManager.Update_Category(categoryViewModel.Category);
                categoryViewModel.Friendly_Message.Add(MessageStore.Get("CO002"));
            }
            catch (Exception ex)
            {
                categoryViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("CategoryController Update " + ex);
            }

            TempData["categoryViewMessage"] = categoryViewModel;
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
        public ActionResult Get_Category_By_Id(CategoryViewModel categoryViewModel)
        {
            try
            {
                categoryViewModel.Category = _categoryManager.Get_Category_By_Id(categoryViewModel.Category.Category_Id);

            }
            catch (Exception ex)
            {
                categoryViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("CategoryController Get_Category_By_Id " + ex);
            }

            return View("Index", categoryViewModel);
        }

    }
}
