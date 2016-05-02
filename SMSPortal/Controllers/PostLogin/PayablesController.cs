﻿using SMSPortal.Common;
using SMSPortal.Models.PostLogin;
using SMSPortalHelper.Logging;
using SMSPortalHelper.PageHelper;
using SMSPortalInfo.Common;
using SMSPortalManager;
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

        public PayableManager _payableManager;

        public CookiesInfo _cookies;

        public string token = System.Web.HttpContext.Current.Request.Cookies["UserInfo"]["Token"];

        public PayablesController()
        {
            _payableManager = new PayableManager();

            CookiesManager _cookiesManager = new CookiesManager();

            _cookies = _cookiesManager.Get_Token_Data(token); 
        }

        public ActionResult Index(PayableViewModel pViewModel)
        {
            return View("Index", pViewModel);
        }

        public ActionResult Search()
        {
            return View("Search");
        }

        public JsonResult Get_Payable(PayableViewModel pViewModel)
        {
            PaginationInfo pager = new PaginationInfo();
            try
            {
                pager = pViewModel.Pager;

                if (pViewModel.Filter.Invoice_No != null)
                {
                    pViewModel.Payables = _payableManager.Get_Payable_By_Id(pViewModel.Filter.Invoice_Id, ref pager);
                }
                else
                {
                    pViewModel.Payables = _payableManager.Get_Payables(ref pager);
                }
                pViewModel.Pager = pager;
                pViewModel.Pager.PageHtmlString = PageHelper.NumericPager("javascript:PageMore({0})", pViewModel.Pager.TotalRecords, pViewModel.Pager.CurrentPage + 1, pViewModel.Pager.PageSize, 10, true);
            }
            catch (Exception ex)
            {
                pViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("PayableController Get_Payables " + ex);
            }
            return Json(pViewModel);
        }

        [AuthorizeUserAttribute(AppFunction.Token)]
        public ActionResult Get_Payables_By_Id(PayableViewModel pViewModel)
        {
            try
            {
                pViewModel.Payable = _payableManager.Get_Payable_Data_By_Id(pViewModel.Payable.Payable_Id);

                pViewModel.Payables = _payableManager.Get_Payable_Items_By_Id(pViewModel.Payable.Payable_Id);
            }
            catch (Exception ex)
            {
                pViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));

                Logger.Error("Error At Payable Controller - Get_Payables_By_Id" + ex.Message);
            }

            return View("Index", pViewModel);
        }

        public JsonResult Insert_Payable(PayableViewModel pViewModel)
        {
            try
            {
                pViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");

                pViewModel.Payable.Payable_Id = _payableManager.Insert_Payable(pViewModel.Payable, pViewModel.Cookies.User_Id);

                _payableManager.Insert_PayableItems(pViewModel.Payable, pViewModel.Cookies.User_Id);

                pViewModel.Payable = _payableManager.Get_Payable_Data_By_Id(pViewModel.Payable.Payable_Id);

                pViewModel.Payables = _payableManager.Get_Payable_Items_By_Id(pViewModel.Payable.Payable_Id);

                pViewModel.Friendly_Message.Add(MessageStore.Get("RE001"));

            }
            catch (Exception ex)
            {
                pViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));

                Logger.Error("ReceivableController Insert " + ex);
            }
            TempData["pViewModel"] = pViewModel;
            return Json(pViewModel);
        }

       

    }
}
