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
        public PartialViewResult Add_Product_Mapping()
        {
            return PartialView("_AddProductMapping");
        }

        public PartialViewResult Add_Bank_Details()
        {
            return PartialView("_AddBankDetails");
        }

        public ActionResult SearchOrders()
        {
            return View("SearchOrders");
        }

        public ActionResult OrderDetails()
        {
            return View("OrderDetails");
        }

        public ActionResult CreateInvoice()
        {
            return View("CreateInvoice");
        }

        public ActionResult Profile()
        {
            return View("Profile");
        }
    }
}
