
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
            url: "product/Delete-Product-Image",
            defaults: new { controller = "Product", action = "Delete_Product_Image", id = UrlParameter.Optional },
            namespaces: new string[] { "SMSPortal.Controllers" });

            #endregion

            #region Autocomplete

            routes.MapRoute(
            name: "Autocomplete-1",
            url: "autocomplete/autocomplete-get-lookup-data",
            defaults: new { controller = "AutocompleteLookup", action = "Load_Vendor_Modal_Data", id = UrlParameter.Optional },
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