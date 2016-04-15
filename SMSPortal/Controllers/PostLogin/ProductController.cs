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
    public class ProductController : Controller
    {
        //
        // GET: /Product/
        public ProductManager _productManager;
        public BrandManager _brandManager;
        public CategoryManager _categoryManager;
        public DealerManager _dealerManager;
        public SubCategoryManager _subCategoryManager;
        public ProductController()
        {
            _productManager = new ProductManager();
            _brandManager = new BrandManager();
            _categoryManager = new CategoryManager();
            _dealerManager = new DealerManager();
            _subCategoryManager = new SubCategoryManager();

        }
        public ActionResult Search(ProductViewModel pViewModel)
        {
            try
            {
                if (TempData["dViewModel"] != null)
                {
                    pViewModel = (ProductViewModel)TempData["pViewModel"];
                }
            }
            catch (Exception ex)
            {
                pViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("ProductController Search " + ex);
            }
            return View("Search",pViewModel);
        }
        public ActionResult AddEdit_Product(ProductViewModel pViewModel)
        {
            PaginationInfo Pager = new PaginationInfo();
            try
            {
                pViewModel.Brands = _dealerManager.Get_Brands();
                pViewModel.Categories = _subCategoryManager.Get_Categories();

            }
            catch (Exception ex)
            {
                Logger.Error("ProductController - AddEdit_Product " + ex.Message);
            }
            return View("AddEdit_Product", pViewModel);
        }

        public ActionResult Insert_Product(ProductViewModel pViewModel)
        {
            try
            {
                pViewModel.Product.Created_By = ((UserInfo)Session["SessionInfo"]).User_Id;
                pViewModel.Product.Created_On = DateTime.Now;
                pViewModel.Product.Updated_By = ((UserInfo)Session["SessionInfo"]).User_Id;
                pViewModel.Product.Updated_On = DateTime.Now;
                _productManager.Insert_Product(pViewModel.Product);
                pViewModel.Friendly_Message.Add(MessageStore.Get("PO001"));
            }
            catch (Exception ex)
            {
                pViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("ProductController Insert " + ex);
            }
            TempData["pViewModel"] = pViewModel;
            return RedirectToAction("Search");
        }

        public ActionResult Update_Product(ProductViewModel pViewModel)
        {
            try
            {
                pViewModel.Product.Updated_By = ((UserInfo)Session["SessionInfo"]).User_Id;
                pViewModel.Product.Updated_On = DateTime.Now;
                _productManager.Update_Product(pViewModel.Product);
                pViewModel.Friendly_Message.Add(MessageStore.Get("PO002"));
            }
            catch (Exception ex)
            {
                pViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("ProductController Update " + ex);
            }

            TempData["pViewModel"] = pViewModel;
            return RedirectToAction("Search");
        }
        public JsonResult Get_Products(ProductViewModel pViewModel)
        {
            PaginationInfo pager = new PaginationInfo();
            try
            {
                pager = pViewModel.Pager;
                if (pViewModel.Filter.Product_Name != null)
                {
                    pViewModel.Products = _productManager.Get_Products_By_Name  (pViewModel.Filter.Product_Name, ref pager);
                }
                else
                {
                    pViewModel.Products = _productManager.Get_Products(ref pager);
                }
                pViewModel.Pager = pager;
                pViewModel.Pager.PageHtmlString = PageHelper.NumericPager("javascript:PageMore({0})", pViewModel.Pager.TotalRecords, pViewModel.Pager.CurrentPage + 1, pViewModel.Pager.PageSize, 10, true);
            }
            catch (Exception ex)
            {
                pViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("ProductController Get_Products " + ex);
            }
            return Json(pViewModel);
        }
        public ActionResult Get_Product_By_Id(ProductViewModel pViewModel)
        {
            try
            {
                pViewModel.Product = _productManager.Get_Product_By_Id(pViewModel.Product.Product_Id);
            }
            catch (Exception ex)
            {
                pViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("Product Get_Product_By_Id " + ex);
            }
            return AddEdit_Product(pViewModel);
        }
        public JsonResult Check_Existing_Product(string Product_Name)
        {
            bool check = false;
            try
            {
                check = _productManager.Check_Existing_Product(Product_Name);
            }
            catch (Exception ex)
            {
                Logger.Error("Product Controller - Check_Existing_Product " + ex.ToString());
            }
            return Json(check, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Get_SubCategory_By_CategoryId(int Category_Id)
        {
            ProductViewModel pViewModel = new ProductViewModel();
            try
            {
                pViewModel.SubCategories = _subCategoryManager.Get_SubCategories_By_CategoryId(Category_Id);
            }
            catch (Exception ex)
            {
                Logger.Error("UserController - Get_Entity_By_Role" + ex.ToString());
            }
            return Json(pViewModel.SubCategories, JsonRequestBehavior.AllowGet);
        }


        public PartialViewResult Upload_Product_Image()
        {
            return PartialView("_Product_Images");
        }
        public PartialViewResult Get_Products1()
        {
            return PartialView("_Partial");
        }




    }
}
