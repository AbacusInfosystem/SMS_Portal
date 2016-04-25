
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
            name: "Brand-6",
            url: "brand/brands-upload-logo",
            defaults: new { controller = "Brand", action = "Brand_Logo_Upload", id = UrlParameter.Optional },
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