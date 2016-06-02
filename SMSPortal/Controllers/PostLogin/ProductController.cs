using Newtonsoft.Json;
using SMSPortal.Common;
using SMSPortal.Models.PostLogin;
using SMSPortalHelper.Logging;
using SMSPortalHelper.PageHelper;
using SMSPortalInfo;
using SMSPortalInfo.Common;
using SMSPortalRepo;
using SMSPortalManager;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMSPortalRepo.Common;
using System.Data.SqlClient;

namespace SMSPortal.Controllers.PostLogin
{
    public class ProductController : Controller
    {
        private string _sqlCon = "";
        //
        // GET: /Product/
        public ProductManager _productManager;
        public DealerManager _dealerManager;
        public SubCategoryManager _subCategoryManager;
        public TaxManager _taxManager;
        public StateManager _stateManager;
        public OrdersManager _OrdersManager;
        public UserManager _userManager;

        public ProductController()
        {
            _productManager = new ProductManager();
            _dealerManager = new DealerManager();
            _subCategoryManager = new SubCategoryManager();
            _taxManager = new TaxManager();
            _stateManager = new StateManager();
            _OrdersManager = new OrdersManager();
            _userManager = new UserManager();
        }

        public ActionResult Index(ProductViewModel pViewModel)
        {
            try
            {

            }
            catch (Exception ex)
            {
                pViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("ProductController Index " + ex);
            }
            return View("Index", pViewModel);
        }

        public ActionResult Search(ProductViewModel pViewModel)
        {
            try
            {
                if (TempData["pViewModel"] != null)
                {
                    pViewModel = (ProductViewModel)TempData["pViewModel"];
                }
                if (TempData["Message"] != null)
                {
                    FriendlyMessage ms = (FriendlyMessage)TempData["Message"];
                    pViewModel.Friendly_Message.Add(ms);
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
                pViewModel.Brands = _productManager.Get_Brands();
                //pViewModel.Categories = _subCategoryManager.Get_Categories();
                pViewModel.Categories = _productManager.Get_Categorys();
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
                _productManager.Insert_Product(pViewModel.Product, pViewModel.Cookies.User_Id);
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
                _productManager.Update_Product(pViewModel.Product, pViewModel.Cookies.User_Id);
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
                pViewModel.SubCategories = _productManager.Get_SubCategories_By_CategoryId(Category_Id);
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
            if (pViewModel.ImagesList.Count == 0)
            {
                Is_Default = true;
            }

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
                    _productManager.Insert_Product_Image(pViewModel.ProductImage, pViewModel.Cookies.User_Id);

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

        public ActionResult Set_Image_Default(int Product_Id, int Product_Image_Id)
        {
            ProductViewModel pViewModel = new ProductViewModel();
            try
            {
                _productManager.Set_Default_Image(Product_Id, Product_Image_Id);
                pViewModel.ImagesList = _productManager.Get_Product_Images(Product_Id);
                pViewModel.Product.Product_Id = Product_Id;
            }
            catch (Exception ex)
            {
                pViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("Product Controller Set_Image_Default " + ex.Message);
            }
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

        public PartialViewResult GetCategories()
        {
            ProductViewModel pViewModel = new ProductViewModel();

            try
            {
                pViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");
                pViewModel.Categories = _productManager.Get_Categories_With_Product_Count(pViewModel.Cookies.Entity_Id);                
            }
            catch (Exception ex)
            {
                Logger.Error("Error At Product_Controller - GetCategories " + ex.ToString());
            }

            return PartialView("_Categories", pViewModel);
            
        }

        public PartialViewResult GetProductList(int? Category_Id, int? Sub_Category_Id, string Product_Name)
        {
            ProductViewModel pViewModel = new ProductViewModel();

            try
            {
                pViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");
                pViewModel.Products = _productManager.Get_Products_By_Dealer_Id(pViewModel.Cookies.Entity_Id, Category_Id, Sub_Category_Id, Product_Name);
            }
            catch (Exception ex)
            {
                Logger.Error("Error At Product_Controller - GetProductList " + ex.ToString());
            }
            
            return PartialView("_ProductList", pViewModel);
        }

        public PartialViewResult GetSubCategories(int Category_Id)
        {
            ProductViewModel pViewModel = new ProductViewModel();

            try
            {
                pViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");
                pViewModel.SubCategories = _productManager.Get_Sub_Categories_With_Product_Count(Category_Id, pViewModel.Cookies.Entity_Id);
            }
            catch (Exception ex)
            {
                Logger.Error("Error At Product_Controller - GetSubCategories " + ex.ToString());
            }
            
            return PartialView("_SubCategories", pViewModel);
        }

        public ActionResult PlaceOrder(ProductViewModel pViewModel)
        {
            try
            {
                //string ProductIds = Request.QueryString["ProductIds"];
                pViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");
                pViewModel.dealer = _dealerManager.Get_Dealer_By_Id(pViewModel.Cookies.Entity_Id);
                pViewModel.Products = _productManager.Get_Products_By_Ids(pViewModel.ProductIds, pViewModel.ProductQuantities);
                pViewModel.state = _stateManager.Get_State_By_Id(pViewModel.dealer.State);
                pViewModel.tax = _taxManager.Get_Tax_By_Id();
            }
            catch (Exception ex)
            {
                pViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("ProductController PlaceOrder " + ex);
            }
            return View("PlaceOrder", pViewModel);
        }

        public ActionResult SaveOrder(ProductViewModel pViewModel)
        {

            _sqlCon = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

            using (SqlConnection con = new SqlConnection(_sqlCon))
            {
                con.Open();
                using (SqlTransaction trans = con.BeginTransaction())
                {
                    try
                    {
                        InvoiceManager _invoiceManager = new InvoiceManager();

                        pViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");

                        pViewModel.order.Order_No = Utility.Generate_Ref_No("ORD-", "Order_No", "5", "15", "Orders");
                        pViewModel.order.Order_Date = DateTime.Now;
                        pViewModel.order.Status = Convert.ToString(Convert.ToInt32(OrderStatus.Order_Received));
                        pViewModel.order.Shipping_Date = DateTime.Now.AddDays(7);
                        pViewModel.order.Created_By = pViewModel.Cookies.User_Id;
                        pViewModel.order.Created_On = DateTime.Now;
                        pViewModel.order.Updated_By = pViewModel.Cookies.User_Id;
                        pViewModel.order.Updated_On = DateTime.Now;
                        pViewModel.order.Order_Id = _OrdersManager.Insert_Orders(pViewModel.order);
                        _OrdersManager.Send_Order_Status_Notification(pViewModel.Cookies.First_Name, pViewModel.Cookies.User_Email, pViewModel.order,false);
                        pViewModel.dealer = _dealerManager.Get_Dealer_By_Id(pViewModel.order.Dealer_Id);


                        // Dealer Invoice
                        InvoiceViewModel iViewModel = new InvoiceViewModel();

                        iViewModel.Invoice.Order_Id = pViewModel.order.Order_Id;
                        iViewModel.Invoice.Invoice_No = Utility.Generate_Ref_No("INV-", "Invoice_No", "5", "15", "Invoice");
                        iViewModel.Invoice.Role_Id = Convert.ToInt32(Roles.Dealer);
                        iViewModel.Invoice.Entity_Id = pViewModel.dealer.Dealer_Id;
                        iViewModel.Invoice.Amount = (pViewModel.dealer.Dealer_Percentage * pViewModel.order.Net_Amount) / 100; // Calculating Dealer percentage

                        iViewModel.Invoice.Invoice_Id = _invoiceManager.Insert_Invoice(iViewModel.Invoice, pViewModel.Cookies.User_Id);

                        _invoiceManager.Send_Invoice_Email(pViewModel.dealer.Email, iViewModel.Invoice, pViewModel.order, pViewModel.dealer);


                        // Brand Invoice
                        iViewModel = new InvoiceViewModel();

                        iViewModel.Invoice.Order_Id = pViewModel.order.Order_Id;
                        iViewModel.Invoice.Invoice_No = Utility.Generate_Ref_No("INV-", "Invoice_No", "5", "15", "Invoice");
                        iViewModel.Invoice.Role_Id = Convert.ToInt32(Roles.Brand);
                        iViewModel.Invoice.Entity_Id = pViewModel.dealer.Brand_Id;
                        iViewModel.Invoice.Amount = (pViewModel.dealer.Brand_Percentage * pViewModel.order.Net_Amount) / 100; // Calculating Brand percentage

                        iViewModel.Invoice.Invoice_Id = _invoiceManager.Insert_Invoice(iViewModel.Invoice, pViewModel.Cookies.User_Id);

                        UserInfo user = _userManager.Get_User_By_Entity_Id(pViewModel.dealer.Brand_Id, Convert.ToInt32(Roles.Brand));

                        _invoiceManager.Send_Invoice_Email(user.Email_Id, iViewModel.Invoice, pViewModel.order, pViewModel.dealer);                        
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();

                        pViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));

                        Logger.Error("ProductController SaveOrder " + ex);
                    }
                    finally
                    {
                        trans.Commit();

                        con.Close();

                        pViewModel.Friendly_Message.Add(MessageStore.Get("PO006"));

                        TempData["FriendlyMessage"] = MessageStore.Get("PO006");
                    }
                }
            }

            return RedirectToAction("Index", "Dashboard");
        }

        public PartialViewResult GetProductDetails(int Product_Id)
        {
            ProductViewModel pViewModel = new ProductViewModel();
            try
            {                
                pViewModel.Product = _productManager.Get_Product_By_Id(Product_Id);
                pViewModel.Product.ProductImages = _productManager.Get_Product_Images(Product_Id);
            }
            catch (Exception ex)
            {
                Logger.Error("ProductController GetProductDetails " + ex);
            }           
            return PartialView("_ProductDetails", pViewModel);
        }

        public ActionResult Bulk_Excel_Product_Upload(ProductViewModel pViewModel)
        {
            // Code to Upload Excel File 
            var fileName = "";
            var path = "";
            bool is_Error = false;

            try
            {
                ExcelReader _excel = new ExcelReader();
                pViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");
                if (pViewModel.UploadProductExcel.ContentLength > 0)
                {

                    fileName = Path.GetFileName(pViewModel.UploadProductExcel.FileName);

                    path = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["BrandLogoPath"].ToString()), fileName);

                    Logger.Debug("*************************** " + path.ToString());

                    pViewModel.UploadProductExcel.SaveAs(path);

                    DataSet ds = _excel.ExecuteDataSet(path);

                    is_Error = _productManager.Bulk_Excel_Upload_Default(ds.Tables[0], pViewModel.Cookies.User_Id);

                    if (is_Error == true)
                    {
                        pViewModel.Friendly_Message.Add(MessageStore.Get("PO004"));
                        TempData["Message"] = MessageStore.Get("PO004");
                    }
                    else
                    {
                        pViewModel.Friendly_Message.Add(MessageStore.Get("PO005"));
                        TempData["Message"] = MessageStore.Get("PO005");
                    }

                    System.IO.File.Delete(path);
                }
            }
            catch (Exception ex)
            {
                System.IO.File.Delete(path);

                pViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));

                TempData["Message"] = MessageStore.Get("SYS01");

                Logger.Error("Error At Product Controller Bulk_Excel_Product_Upload  " + ex.Message);
            }

            

            return RedirectToAction("Search");
        }

        public PartialViewResult Upload_Product_Excel()
        {
            return PartialView("_Product_Excel_Upload");
        }
    }
}
