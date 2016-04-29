using SMSPortal.Models.PostLogin;
using SMSPortal.Common;
using SMSPortalHelper.Logging;
using System;
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
        public ActionResult Search(InvoiceViewModel iViewModel)
        {
            try
            {
                if (TempData["iViewModel"] != null)
                {
                    iViewModel = (InvoiceViewModel)TempData["iViewModel"];
                }
            }
            catch (Exception ex)
            {
                iViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("InvoiceController Search " + ex);
            }            
            return View("Search",iViewModel);
        }

        public ActionResult ViewInvoice(InvoiceViewModel iViewModel)
        {
            return View("ViewInvoice", iViewModel);
        }
        public ActionResult CreateInvoice()
        {
            return View("CreateInvoice");
        }
    }
}
