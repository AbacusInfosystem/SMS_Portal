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
                Logger.Error("RoleController - Get_Roles" + ex.Message);
            }

            return Json(sViewModel);
        }

        public ActionResult Insert_Update_Subcategory(SubCategoryViewModel sViewModel)
        {
            try
            {
                sViewModel.SubCategory.Created_By = ((SessionInfo)Session["SessionInfo"]).User_Id;

                sViewModel.SubCategory.Created_Date = DateTime.Now;

                sViewModel.SubCategory.Updated_By = ((SessionInfo)Session["SessionInfo"]).User_Id;

                sViewModel.SubCategory.Updated_Date = DateTime.Now;

                if (sViewModel.SubCategory.Subcategory_Id == 0)
                {
                    _subcategoryManager.Insert_Sub_Category(sViewModel.SubCategory);

                    sViewModel.Friendly_Message.Add(MessageStore.Get("CO001"));
                }
                else
                {
                    _subcategoryManager.Update_Sub_Category(sViewModel.SubCategory);

                    sViewModel.Friendly_Message.Add(MessageStore.Get("CO001"));
                }
            }
            catch (Exception ex)
            {
                sViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));

                Logger.Error("Error At Role Insert  " + ex.Message);
            }

            TempData["sViewModel"] = sViewModel;

            return RedirectToAction("Search");
        }
    }
}
