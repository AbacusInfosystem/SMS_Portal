using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMSPortal.Controllers.PostLogin
{
    public class PayablesController : Controller
    {
        //
        // GET: /Payables/

        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult Search()
        {
            return View("Search");
        }

    }
}
