
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
            url: "subcategory/insert-update-subcategories",
            defaults: new { controller = "SubCategory", action = "Insert_Update_Subcategory", id = UrlParameter.Optional },
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