﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMSPortal.Controllers.PostLogin
{
    public class SubCategoryController : Controller
    {
        //
        // GET: /SubCategory/

        public ActionResult Search()
        {
            return View("Search");
        }
        public ActionResult AddEdit_SubCategory()
        {
            return View("AddEdit_SubCategory");
        }
    }
}