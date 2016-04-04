using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMSPortal.Controllers.PostLogin
{
    public class VendorController : Controller
    {
        //
        // GET: /Vendor/
        public ActionResult Search()
        {
            return View("Search");
        }
        public ActionResult Index()
        {
            return View("Index");
        }
        public ActionResult AddProductMapping()
        {
            return View("AddProductMapping");
        }
    }
}
