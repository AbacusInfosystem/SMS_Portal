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

        public CookiesInfo _cookies;
        public string token = System.Web.HttpContext.Current.Request.Cookies["UserInfo"]["Token"];
        public ReceivableController()
        {
            _receivableManager = new ReceivableManager();
            CookiesManager _cookiesManager = new CookiesManager();
            _cookies = _cookiesManager.Get_Token_Data(token); 
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
                    rViewModel.Receivables = _receivableManager.Get_Receivable_By_Id(rViewModel.Filter.Invoice_Id, ref pager);
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

        [AuthorizeUserAttribute(AppFunction.Token)]
        public ActionResult Get_Receivables_By_Id(ReceivableViewModel rViewModel)
        {
            try
            {
                rViewModel.Receivable = _receivableManager.Get_Receivable_Data_By_Id(rViewModel.Receivable.Receivable_Id);

                rViewModel.Receivables = _receivableManager.Get_Receivable_Items(rViewModel.Receivable.Receivable_Id);
            }
            catch (Exception ex)
            {
                rViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));

                Logger.Error("Error At Receivable Controller - Get_Receivables_By_Id" + ex.Message);
            }

            return View("Index", rViewModel);
        }

        public JsonResult Insert_Receivable(ReceivableViewModel rViewModel)
        {
            try
            {
                rViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");

                rViewModel.Receivable.Receivable_Id = _receivableManager.Insert_Receivable(rViewModel.Receivable, rViewModel.Cookies.User_Id);

                _receivableManager.Insert_ReceivableItems(rViewModel.Receivable, rViewModel.Cookies.User_Id);

                rViewModel.Receivable = _receivableManager.Get_Receivable_Data_By_Id(rViewModel.Receivable.Receivable_Id);

                rViewModel.Receivables = _receivableManager.Get_Receivable_Items(rViewModel.Receivable.Receivable_Id);

                rViewModel.Friendly_Message.Add(MessageStore.Get("RE001"));
            }
            catch (Exception ex)
            {
                rViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("Error at Receivable Controller - Insert_Receivable " + ex);
            }

            return Json(rViewModel);
        }

        public JsonResult Delete_Receivable_Data_By_Id(int receivable_Item_Id,int receivable_Id)
        {
            ReceivableViewModel rViewModel = new ReceivableViewModel();
            try
            {
                _receivableManager.Get_Receivable_Data_By_Id(rViewModel.Receivable.Receivable_Id);

                rViewModel.Receivable = _receivableManager.Get_Receivable_Data_By_Id(rViewModel.Receivable.Receivable_Id);

                rViewModel.Receivables = _receivableManager.Get_Receivable_Items(rViewModel.Receivable.Receivable_Id);

                rViewModel.Friendly_Message.Add(MessageStore.Get("RE001"));
            }
            catch (Exception ex)
            {
                Logger.Error("DesignationInterview Controller - Delete_Company_By_PropertyId " + ex.ToString());
            }
            return Json(rViewModel, JsonRequestBehavior.AllowGet);
        }

    }
}
