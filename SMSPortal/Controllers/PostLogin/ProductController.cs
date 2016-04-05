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

        public ActionResult Search()
        {
            return View("Search");
        }

        public ActionResult Index()
        {
            return View("Index");
        }

        public PartialViewResult Upload_Product_Image()
        {
            return PartialView("_Product_Images");
        }

        public PartialViewResult Get_Products()
        {
            return PartialView("_Partial");
        }

    }
}
