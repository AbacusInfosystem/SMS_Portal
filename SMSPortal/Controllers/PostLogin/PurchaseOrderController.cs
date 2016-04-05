using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMSPortal.Controllers.PostLogin
{
    public class PurchaseOrderController : Controller
    {
        //
        // GET: /PurchaseOrder/
        public ActionResult Search()
        {
            return View("Search");
        }

        public ActionResult Index()
        {
            return View("Index");
        }
        public ActionResult Add_Purchase_Order_Item()
        {
            return View("AddPurchaseOrderItem");
        }
    }
}
