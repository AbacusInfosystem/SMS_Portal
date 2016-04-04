using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMSPortal.Controllers.PostLogin
{
    public class BrandController : Controller
    {
        //
        // GET: /Brand/

        public ActionResult Search()
        {
            return View("Search");
        }

        public ActionResult Index()
        {
            return View("Index");
        }

        public PartialViewResult Add_Brand_Logo()
        {
            return PartialView("_Upload_Brand_Logo");
        }

    }
}
