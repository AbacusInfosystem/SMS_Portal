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
            PaginationInfo Pager = new PaginationInfo();
            try
            {                
                Pager.IsPagingRequired = false;
                categoryViewModel.Categories = _categoryManager.Get_Categorys(ref Pager);

                if (TempData["categoryViewMessage"] != null)
                {
                    categoryViewModel.Friendly_Message=(List<FriendlyMessage>)TempData["categoryViewMessage"];
                }
            }
            catch (Exception ex)
            {
                categoryViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("UserController Search " + ex);
            }
            return View("Search",categoryViewModel);


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
            TempData["categoryViewMessage"] = categoryViewModel.Friendly_Message;
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
