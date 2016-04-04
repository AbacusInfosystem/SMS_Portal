using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMSPortal.Controllers.PostLogin
{
    public class SalesOrderController : Controller
    {
        //
        // GET: /SalesOrder/
        public ActionResult Search()
        {
            return View("Search");
        }
        public ActionResult Index()
        {
            return View("Index");
        }

    }
}
