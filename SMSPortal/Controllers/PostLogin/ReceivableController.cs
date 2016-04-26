using SMSPortal.Common;
using SMSPortal.Models.PostLogin;
using SMSPortalHelper.Logging;
using SMSPortalHelper.PageHelper;
using SMSPortalInfo;
using SMSPortalInfo.Common;
using SMSPortalManager;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace SMSPortal.Controllers.PostLogin
{
    public class ReceivableController : Controller
    {
        //
        // GET: /Receivable/

        public ReceivableManager _receivableManager;

        public ReceivableController()
        {
            _receivableManager = new ReceivableManager();
        }

        public ActionResult Search(ReceivableViewModel rViewModel)
        {
            return View("Search", rViewModel);
        }

        public ActionResult Index( ReceivableViewModel rViewModel)
        { 
            //rViewModel.Receivables = _receivableManager.Get_InvoiceNo();
            return View("Index", rViewModel);
        }
       
        public ActionResult ReceivableIndex()
        {
            return View("ReceivableIndex");
        }

        public JsonResult Get_Recievable(ReceivableViewModel rViewModel)
        {
            PaginationInfo pager = new PaginationInfo();
            try
            {
                pager = rViewModel.Pager;

                if (rViewModel.Filter.Invoice_No != null)
                {
                    rViewModel.Receivables = _receivableManager.Get_Receivable_By_Name(rViewModel.Filter.Invoice_No, ref pager);
                }
                else
                {
                    rViewModel.Receivables = _receivableManager.Get_Receivables(ref pager);
                }
                rViewModel.Pager = pager;
                rViewModel.Pager.PageHtmlString = PageHelper.NumericPager("javascript:PageMore({0})", rViewModel.Pager.TotalRecords, rViewModel.Pager.CurrentPage + 1, rViewModel.Pager.PageSize, 10, true);
            }
            catch (Exception ex)
            {
                rViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("ReceivableController Get_Receivables " + ex);
            }
            return Json(rViewModel);
        }

        public JsonResult Load_Receivable_InvoiceNo(string txtInvoice_No)
        { 
            List<AutocompleteInfo> Invoice_No = new List<AutocompleteInfo>();

            Invoice_No = _receivableManager.Load_Receivable_InvoiceNo(txtInvoice_No);

            return Json(Invoice_No, JsonRequestBehavior.AllowGet);
        }
    }
}
