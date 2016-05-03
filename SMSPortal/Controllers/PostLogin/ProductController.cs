using Newtonsoft.Json;
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
                if (TempData["pViewModel"] != null)
                {
                    pViewModel = (ProductViewModel)TempData["pViewModel"];
                }
            }
            catch (Exception ex)
            {
                pViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("ProductController Search " + ex);
            }
            return View("Search", pViewModel);
        }

        public ActionResult AddEdit_Product(ProductViewModel pViewModel)
        {
            PaginationInfo Pager = new PaginationInfo();
            try
            {

                pViewModel.Brands = _dealerManager.Get_Brands();
                //pViewModel.Categories = _subCategoryManager.Get_Categories();
                pViewModel.Categories = _categoryManager.Get_Categorys(ref Pager);

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
                pViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");
                pViewModel.Product.Created_By = pViewModel.Cookies.User_Id;
                pViewModel.Product.Created_On = DateTime.Now;
                pViewModel.Product.Updated_By = pViewModel.Cookies.User_Id;
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
                pViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");
                pViewModel.Product.Updated_By = pViewModel.Cookies.User_Id;
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
                if (pViewModel.Filter.Product_Id != 0)
                {
                    pViewModel.Products = _productManager.Get_Products_By_Id(pViewModel.Filter.Product_Id, ref pager);
                     
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

        public JsonResult Get_SubCategory_By_Category_Id(int Category_Id)
        {
            ProductViewModel pViewModel = new ProductViewModel();
            try
            {
                pViewModel.SubCategories = _subCategoryManager.Get_SubCategories_By_CategoryId(Category_Id);
            }
            catch (Exception ex)
            {
                Logger.Error("ProductController - Get_SubCategory_By_Category_Id" + ex.ToString());
            }
            return Json(pViewModel.SubCategories, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult Upload_Product_Image(int Product_Id)
        {
            ProductViewModel p1ViewModel = new ProductViewModel();
            try
            {
                p1ViewModel.ImagesList = _productManager.Get_Product_Images(Product_Id);
                p1ViewModel.Product.Product_Id = Product_Id;
            }
            catch (Exception ex)
            {
                Logger.Error("ProductController - Upload_Product_Image" + ex.ToString());
            }

            return PartialView("_Product_Images", p1ViewModel);

        }

        public ActionResult Product_Image_Upload(ProductViewModel pViewModel)
        {

            pViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");
            HttpPostedFileBase fileBase = null;
            var actualFileName = "";
            var fileName = "";
            var path = "";
            //bool is_Error = false;    

            if (Request.Files.Count > 0)
            {
                fileBase = Request.Files[0];
            }

            string Product_Id = Request.Form.Get("Product_Id");
            bool Is_Default = Convert.ToBoolean(Request.Form.Get("Is_Default"));

            pViewModel.ProductImage.File = fileBase;

            try
            {
                if (pViewModel.ProductImage.File.ContentLength > 0)
                {

                    fileName = Path.GetFileName(fileBase.FileName);
                    actualFileName = "P" + Product_Id + "_" + fileName;
                    path = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["BrandLogoPath"].ToString()), actualFileName);
                    // Logger.Debug("*************************** " + path.ToString());

                    pViewModel.ProductImage.File.SaveAs(path);

                    pViewModel.ProductImage.Product_Id = Convert.ToInt32(Product_Id);
                    pViewModel.ProductImage.Image_Code = actualFileName;
                    pViewModel.ProductImage.Is_Default = Is_Default;
                    pViewModel.ProductImage.Created_On = DateTime.Now;
                    pViewModel.ProductImage.Updated_By = pViewModel.Cookies.User_Id;
                    pViewModel.ProductImage.Updated_On = DateTime.Now;

                    _productManager.Insert_Product_Image(pViewModel.ProductImage);

                    pViewModel.ImagesList = _productManager.Get_Product_Images(Convert.ToInt32(Product_Id));
                    pViewModel.Product.Product_Id = Convert.ToInt32(Product_Id);
                }
            }
            catch (Exception ex)
            {

                pViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("Error uploading Product Images  " + ex.Message);
            }
            TempData["pViewModel"] = pViewModel;             
            return PartialView("_Product_Images", pViewModel);
        }

        public ActionResult Delete_Product_Image(int Product_Image_Id, int Product_Id, string Product_Image_Name)
        {
            string path = "";
            ProductViewModel pViewModel = new ProductViewModel();
            try
            {
                _productManager.Delete_Product_Image(Product_Image_Id);

                path = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["BrandLogoPath"].ToString()), Product_Image_Name);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                pViewModel.ImagesList = _productManager.Get_Product_Images(Product_Id);
                pViewModel.Product.Product_Id = Product_Id;
            }
            catch (Exception ex)
            {
                pViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("Error Deleting Product Image  " + ex.Message);
            }
            return PartialView("_Product_Images", pViewModel);
        }
        
        public JsonResult Get_Product_Autocomplete(string Product)
        {
            List<AutocompleteInfo> autoList = new List<AutocompleteInfo>();
            try
            {
                autoList = _productManager.Get_Product_Autocomplete(Product);
            }
            catch (Exception ex)
            {
                Logger.Error("Error At Product_Controller - Get_Product_Autocomplete " + ex.ToString());
            }
            return Json(autoList, JsonRequestBehavior.AllowGet);
        }


    }
}
