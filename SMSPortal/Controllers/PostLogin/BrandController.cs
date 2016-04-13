using SMSPortal.Common;
using SMSPortal.Models.PostLogin;
using SMSPortalHelper.Logging;
using SMSPortalHelper.PageHelper;
using SMSPortalInfo.Common;
using SMSPortalManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMSPortal.Controllers.PostLogin
{
    public class BrandController : Controller
    {
        //
        // GET: /Brand/
        public BrandManager _brandManager;

        public BrandController()
        {
            _brandManager = new BrandManager();
        }
        public ActionResult Search(BrandViewModel bViewModel)
        {
            try
            {
                if (TempData["bViewModel"] != null)
                {
                    bViewModel = (BrandViewModel)TempData["bViewModel"];
                }
            }
            catch (Exception ex)
            {
                bViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("BrandController Search " + ex);
            }
            return View("Search",bViewModel);
        }

        public ActionResult AddEdit_Brand(BrandViewModel bViewModel)
        {
            PaginationInfo Pager=new PaginationInfo();
            try
            {
                bViewModel.Brands = _brandManager.Get_Brands(ref Pager);
            }
            catch(Exception ex)
            {
                Logger.Error("BrandController - AddEdit_Brand " + ex.Message);
            }

            return View("AddEdit_Brand",bViewModel);
        }

        public PartialViewResult Add_Brand_Logo()
        {
            return PartialView("_Upload_Brand_Logo");
        }

        public JsonResult Get_Brands(BrandViewModel bViewModel)
        {
            PaginationInfo pager = new PaginationInfo();
            try
            {
                pager = bViewModel.Pager;
                if (bViewModel.Filter.Brand_Name != null)
                {
                    bViewModel.Brands = _brandManager.Get_Brand_By_Name(bViewModel.Filter.Brand_Name, ref pager);
                }
                else
                {
                    bViewModel.Brands = _brandManager.Get_Brands(ref pager);
                }
                bViewModel.Pager = pager;
                bViewModel.Pager.PageHtmlString = PageHelper.NumericPager("javascript:PageMore({0})", bViewModel.Pager.TotalRecords, bViewModel.Pager.CurrentPage + 1, bViewModel.Pager.PageSize, 10, true);
            }
            catch (Exception ex)
            {
                bViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("BrandController Get_Brands " + ex);
            }
            return Json(bViewModel);
        }

        public ActionResult Insert_Update_Brand(BrandViewModel bViewModel)
        {
            try
            {
                bViewModel.Brand.Created_By = ((SessionInfo)Session["SessionInfo"]).User_Id;
                bViewModel.Brand.Created_On = DateTime.Now;
                bViewModel.Brand.Updated_By = ((SessionInfo)Session["SessionInfo"]).User_Id;
                bViewModel.Brand.Updated_On = DateTime.Now;

                if (bViewModel.Brand.Brand_Id == 0)
                {
                    _brandManager.Insert_Brand(bViewModel.Brand);
                    bViewModel.Friendly_Message.Add(MessageStore.Get("BO001"));
                }
                else
                {
                    _brandManager.Update_Brand(bViewModel.Brand);
                    bViewModel.Friendly_Message.Add(MessageStore.Get("BO002"));
                }
            }
            catch (Exception ex)
            {
                bViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("Error At Brand Insert/Update  " + ex.Message);
            }
            TempData["bViewModel"] = bViewModel;
            return RedirectToAction("Search");
        }

        public ActionResult Get_Brand_By_Id(BrandViewModel bViewModel)
        {
            try
            {
                bViewModel.Brand = _brandManager.Get_Brand_By_Id(bViewModel.Brand.Brand_Id);
            }
            catch (Exception ex)
            {
                bViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("Brand Get_Brand_By_Id " + ex);
            }

            return View("AddEdit_Brand", bViewModel);
        }

        public JsonResult Check_Existing_Brand(string Brand_Name)
        {
            bool check = false;
            try
            {
                check = _brandManager.Check_Brand(Brand_Name);
            }
            catch (Exception ex)
            {
                Logger.Error("Brand Controller - Check_Existing_Brand " + ex.ToString());
            }
            return Json(check, JsonRequestBehavior.AllowGet);
        }
    }
}
