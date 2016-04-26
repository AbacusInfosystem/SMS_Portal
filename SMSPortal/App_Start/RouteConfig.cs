﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SMSPortal
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            #region PostLogin

            #region SubCategory

            routes.MapRoute(
            name: "SubCategory-1",
            url: "subcategory/get-subcategories",
            defaults: new { controller = "SubCategory", action = "Get_SubCategories", id = UrlParameter.Optional },
            namespaces: new string[] { "SMSPortal.Controllers" });

            routes.MapRoute(
            name: "SubCategory-2",
            url: "subcategory/insert-subcategories",
            defaults: new { controller = "SubCategory", action = "Insert_Subcategory", id = UrlParameter.Optional },
            namespaces: new string[] { "SMSPortal.Controllers" });

            routes.MapRoute(
            name: "SubCategory-3",
            url: "subcategory/edit-subcategories",
            defaults: new { controller = "SubCategory", action = "Get_Subcategory_By_Id", id = UrlParameter.Optional },
            namespaces: new string[] { "SMSPortal.Controllers" });

            routes.MapRoute(
            name: "SubCategory-4",
            url: "subcategory/subcategory-autocomplete/{subcategory}",
            defaults: new { controller = "SubCategory", action = "Get_Subcategory_Autocomplete", id = UrlParameter.Optional },
            namespaces: new string[] { "SMSPortal.Controllers" });

            routes.MapRoute(
            name: "SubCategory-5",
            url: "subcategory/subcategory-exist-check/{subcategory}",
            defaults: new { controller = "SubCategory", action = "Check_Existing_Sub_Category", id = UrlParameter.Optional },
            namespaces: new string[] { "SMSPortal.Controllers" });

            routes.MapRoute(
            name: "SubCategory-6",
            url: "subcategory/update-subcategories",
            defaults: new { controller = "SubCategory", action = "Update_Subcategory", id = UrlParameter.Optional },
            namespaces: new string[] { "SMSPortal.Controllers" });
          
            #endregion

            #region Brand

            routes.MapRoute(
            name: "Brand-1",
            url: "brand/get-brands",
            defaults: new { controller = "Brand", action = "Get_Brands", id = UrlParameter.Optional },
            namespaces: new string[] { "SMSPortal.Controllers" });

            routes.MapRoute(
            name: "Brand-2",
            url: "brand/insert-brands",
            defaults: new { controller = "Brand", action = "Insert_Brand", id = UrlParameter.Optional },
            namespaces: new string[] { "SMSPortal.Controllers" });

            routes.MapRoute(
            name: "Brand-3",
            url: "brand/update-brands",
            defaults: new { controller = "Brand", action = "Update_Brand", id = UrlParameter.Optional },
            namespaces: new string[] { "SMSPortal.Controllers" });

            routes.MapRoute(
            name: "Brand-4",
            url: "brand/get-brand",
            defaults: new { controller = "Brand", action = "Get_Brand_By_Id", id = UrlParameter.Optional },
            namespaces: new string[] { "SMSPortal.Controllers" });           
                    
            routes.MapRoute(
            name: "Brand-5",
            url: "brand/brands-upload-logo",
            defaults: new { controller = "Brand", action = "Brand_Logo_Upload", id = UrlParameter.Optional },
            namespaces: new string[] { "SMSPortal.Controllers" });

            routes.MapRoute(
            name: "Brand-6",
            url: "brand/brands-autocomplete/{brand}",
            defaults: new { controller = "Brand", action = "Get_Brand_Autocomplete", id = UrlParameter.Optional },
            namespaces: new string[] { "SMSPortal.Controllers" });
            #endregion

            #region Category

            routes.MapRoute(
            name: "Category-1",
            url: "category/get-categories",
            defaults: new { controller = "Category", action = "Get_Categories", id = UrlParameter.Optional },
            namespaces: new string[] { "SMSPortal.Controllers" });

            routes.MapRoute(
            name: "Category-2",
            url: "category/insert-category",
            defaults: new { controller = "Category", action = "Insert_Category", id = UrlParameter.Optional },
            namespaces: new string[] { "SMSPortal.Controllers" });

            routes.MapRoute(
            name: "Category-3",
            url: "category/edit-category",
            defaults: new { controller = "Category", action = "Update_Category", id = UrlParameter.Optional },
            namespaces: new string[] { "SMSPortal.Controllers" });
                       
            routes.MapRoute(
            name: "Category-4",
            url: "category/edit-category",
            defaults: new { controller = "Category", action = "Get_Category_By_Id", id = UrlParameter.Optional },
            namespaces: new string[] { "SMSPortal.Controllers" });

            routes.MapRoute(
            name: "Category-5",
            url: "category/category_autocomplete/{category}",
            defaults: new { controller = "Category", action = "Get_Category_Autocomplete", id = UrlParameter.Optional },
            namespaces: new string[] { "SMSPortal.Controllers" });


            #endregion

            #region Dealer

            routes.MapRoute(
            name: "Dealer-1",
            url: "dealer/get-dealers",
            defaults: new { controller = "Dealer", action = "Get_Dealers", id = UrlParameter.Optional },
            namespaces: new string[] { "SMSPortal.Controllers" });

            routes.MapRoute(
            name: "Dealer-2",
            url: "dealer/insert-dealer",
            defaults: new { controller = "Dealer", action = "Insert_Dealer", id = UrlParameter.Optional },
            namespaces: new string[] { "SMSPortal.Controllers" });

            routes.MapRoute(
            name: "Dealer-3",
            url: "dealer/update-dealer",
            defaults: new { controller = "Dealer", action = "Update_Dealer", id = UrlParameter.Optional },
            namespaces: new string[] { "SMSPortal.Controllers" });

            routes.MapRoute(
            name: "Dealer-4",
            url: "dealer/edit-dealer",
            defaults: new { controller = "Dealer", action = "Get_Dealer_By_Id", id = UrlParameter.Optional },
            namespaces: new string[] { "SMSPortal.Controllers" });


            routes.MapRoute(
            name: "Dealer-5",
            url: "dealer/Check_Existing_Dealer",
            defaults: new { controller = "Dealer", action = "Check_Existing_Dealer", id = UrlParameter.Optional },
            namespaces: new string[] { "SMSPortal.Controllers" });

            routes.MapRoute(
            name: "Dealer-6",
            url: "dealer/dealer-autocomplete/{dealer}",
            defaults: new { controller = "Dealer", action = "Get_Dealer_Autocomplete", id = UrlParameter.Optional },
            namespaces: new string[] { "SMSPortal.Controllers" });


            #endregion

            #region Product

            routes.MapRoute(
            name: "Product-1",
            url: "product/get-products",
            defaults: new { controller = "Product", action = "Get_Products", id = UrlParameter.Optional },
            namespaces: new string[] { "SMSPortal.Controllers" });

            routes.MapRoute(
            name: "Product-2",
            url: "product/insert-product",
            defaults: new { controller = "Product", action = "Insert_Product", id = UrlParameter.Optional },
            namespaces: new string[] { "SMSPortal.Controllers" });

            routes.MapRoute(
            name: "Product-3",
            url: "product/update-product",
            defaults: new { controller = "Product", action = "Update_Product", id = UrlParameter.Optional },
            namespaces: new string[] { "SMSPortal.Controllers" });

            routes.MapRoute(
            name: "Product-4",
            url: "product/edit-product",
            defaults: new { controller = "Product", action = "Get_Product_By_Id", id = UrlParameter.Optional },
            namespaces: new string[] { "SMSPortal.Controllers" });

            routes.MapRoute(
            name: "Product-5",
            url: "product/Get-SubCategory-By-Category-Id",
            defaults: new { controller = "Product", action = "Get_SubCategory_By_Category_Id", id = UrlParameter.Optional },
            namespaces: new string[] { "SMSPortal.Controllers" });

            routes.MapRoute(
            name: "Product-6",
            url: "product/check-existing-product",
            defaults: new { controller = "Product", action = "Check_Existing_Product", id = UrlParameter.Optional },
            namespaces: new string[] { "SMSPortal.Controllers" });

            routes.MapRoute(
           name: "Product-7",
           url: "product/product-autocomplete/{product}",
           defaults: new { controller = "Product", action = "Get_Product_Autocomplete", id = UrlParameter.Optional },
           namespaces: new string[] { "SMSPortal.Controllers" });

            #endregion

            #region Autocomplete

            routes.MapRoute(
            name: "Autocomplete-1",
            url: "autocomplete/autocomplete-get-lookup-data",
            defaults: new { controller = "AutocompleteLookup", action = "Load_Modal_Data", id = UrlParameter.Optional },
            namespaces: new string[] { "SMSPortal.Controllers" });

            #endregion

            #region Lookup

            routes.MapRoute(
            name: "Lookup-1",
            url: "lookup/Lookup-get-lookup-data_by_id",
            defaults: new { controller = "AutocompleteLookup", action = "Get_Lookup_Data_By_Id", id = UrlParameter.Optional },
            namespaces: new string[] { "SMSPortal.Controllers" });

            #endregion

            #region Vendor

            routes.MapRoute(
            name: "Vendor-1",
            url: "vendor/get-vendor-profile-details",
            defaults: new { controller = "Vendor", action = "Get_Vendor_By_Id", id = UrlParameter.Optional },
            namespaces: new string[] { "SMSPortal.Controllers" });

            routes.MapRoute(
            name: "Vendor-2",
            url: "vendor/add-vendor-bank-details",
            defaults: new { controller = "Vendor", action = "Insert_Bank_Details", id = UrlParameter.Optional },
            namespaces: new string[] { "SMSPortal.Controllers" });

            routes.MapRoute(
           name: "Vendor-3",
           url: "vendor/edit-vendor-details",
           defaults: new { controller = "Vendor", action = "Get_Vendor_By_Id", id = UrlParameter.Optional },
           namespaces: new string[] { "SMSPortal.Controllers" });

            routes.MapRoute(
           name: "Vendor-4",
           url: "vendor/get-product-list-omchange-brands",
           defaults: new { controller = "Vendor", action = "Add_Product_Mapping", id = UrlParameter.Optional },
           namespaces: new string[] { "SMSPortal.Controllers" });

            routes.MapRoute(
            name: "Vendor-5",
            url: "vendor/insert-vendors",
            defaults: new { controller = "Vendor", action = "Insert_Vendor", id = UrlParameter.Optional },
            namespaces: new string[] { "SMSPortal.Controllers" });

            routes.MapRoute(
           name: "Vendor-6",
           url: "vendor/update-vendors",
           defaults: new { controller = "Vendor", action = "Update_Vendor", id = UrlParameter.Optional },
           namespaces: new string[] { "SMSPortal.Controllers" });

            routes.MapRoute(
           name: "Vendor-7",
           url: "vendor/insert-vendor-product-mapping-details",
           defaults: new { controller = "Vendor", action = "Insert_Vendor_Product_Mapping_Details", id = UrlParameter.Optional },
           namespaces: new string[] { "SMSPortal.Controllers" });

            routes.MapRoute(
         name: "Vendor-8",
         url: "vendor/vendor-autocomplete/{vendor}",
         defaults: new { controller = "Vendor", action = "Get_Vendor_Autocomplete", id = UrlParameter.Optional },
         namespaces: new string[] { "SMSPortal.Controllers" });


            #endregion

            #region User

            routes.MapRoute(
          name: "User-1",
          url: "User/edit-users",
          defaults: new { controller = "User", action = "Get_User_By_Id", id = UrlParameter.Optional },
          namespaces: new string[] { "SMSPortal.Controllers" });

            routes.MapRoute(
          name: "User-2",
          url: "User/insert-users",
          defaults: new { controller = "User", action = "Insert", id = UrlParameter.Optional },
          namespaces: new string[] { "SMSPortal.Controllers" });


            routes.MapRoute(
          name: "User-3",
          url: "User/update-users",
          defaults: new { controller = "User", action = "Update_User", id = UrlParameter.Optional },
          namespaces: new string[] { "SMSPortal.Controllers" });
            #endregion

            #region Receivable

            routes.MapRoute(
           name: "Receivable-1",
           url: "Receivable/autocomplete-Invoice-No/{txtInvoice_No}",
           defaults: new { controller = "Receivable", action = "Load_Receivable_InvoiceNo", id = UrlParameter.Optional },
           namespaces: new string[] { "SMSPortal.Controllers" });

            routes.MapRoute(
            name: "Receivable-2",
            url: "Receivable/Get-Recievable",
            defaults: new { controller = "Receivable", action = "Get_Recievable", id = UrlParameter.Optional },
            namespaces: new string[] { "SMSPortal.Controllers" });
            #endregion


            #endregion

            #region PreLogin

            routes.MapRoute(
              name: "Home-1",
              url: "home/error",
              defaults: new { controller = "Home", action = "SystemError", id = UrlParameter.Optional },
              namespaces: new string[] { "SMSPortal.Controllers" });

            routes.MapRoute(
              name: "5.0",
              url: "{controller}/{action}/{id}",
              defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional },
              namespaces: new string[] { "SMSPortal.Controllers" });

            routes.MapRoute(
              name: "System-1",
              url: "system/unauthorize-access/{id}",
              defaults: new { controller = "System", action = "UnAuthorize", id = UrlParameter.Optional },
              namespaces: new string[] { "SMSPortal.Controllers" });

            #endregion
        }
	}
}