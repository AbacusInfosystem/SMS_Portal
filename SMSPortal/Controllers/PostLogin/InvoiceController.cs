﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMSPortal.Controllers.PostLogin
{
    public class InvoiceController : Controller
    {
        //
        // GET: /Invoice/
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