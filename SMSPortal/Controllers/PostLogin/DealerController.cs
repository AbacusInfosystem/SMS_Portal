using SMSPortal.Common;
using SMSPortal.Models.PostLogin;
using SMSPortalHelper.Logging;
using SMSPortalHelper.PageHelper;
using SMSPortalInfo;
using SMSPortalInfo.Common;
using SMSPortalManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMSPortal.Controllers.PostLogin
{
    public class DealerController : Controller
    {

        public DealerManager _dealerManager;
        public StateManager _stateManager;
        public CookiesInfo cookies;
        public string token = System.Web.HttpContext.Current.Request.Cookies["UserInfo"]["Token"];
        public DealerController()
        {
            _dealerManager = new DealerManager();
            _stateManager = new StateManager();

            CookiesManager _cookiesManager = new CookiesManager();
            cookies = _cookiesManager.Get_Token_Data(token); 
        }   
        public ActionResult Search(DealerViewModel dViewModel) 
        {
            try
            {
                if (TempData["dViewModel"] != null)
                {
                    dViewModel = (DealerViewModel)TempData["dViewModel"];
                }
            }
            catch (Exception ex)
            {
                dViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("DealerController Search " + ex);
            }
            return View("Search", dViewModel);
        }

        public ActionResult AddEdit_Dealer(DealerViewModel dViewModel)
        {
            
            PaginationInfo Pager = new PaginationInfo();
            try
            {
                dViewModel.States = _stateManager.Get_States();
                dViewModel.Brands = _dealerManager.Get_Brands();
            }
            catch (Exception ex)
            {
                Logger.Error("DealerController - AddEdit_Dealer " + ex.Message);
            }

            return View("AddEdit_Dealer", dViewModel);
        }

        public ActionResult Insert_Dealer(DealerViewModel dViewModel)
        {
            try
            {
                dViewModel.Dealer.Created_By = cookies.User_Id; 
                dViewModel.Dealer.Created_On = DateTime.Now;
                dViewModel.Dealer.Updated_By = cookies.User_Id; 
                dViewModel.Dealer.Updated_On = DateTime.Now;
                _dealerManager.Insert_Dealer(dViewModel.Dealer);
                dViewModel.Friendly_Message.Add(MessageStore.Get("DO001"));
            }
            catch (Exception ex)
            {
                dViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("DealerController Insert " + ex);
            }
            TempData["dViewModel"] = dViewModel;
            return RedirectToAction("Search");
        }

        public ActionResult Update_Dealer(DealerViewModel dViewModel)
        {
            try
            {
                dViewModel.Dealer.Updated_By = cookies.User_Id; 
                dViewModel.Dealer.Updated_On = DateTime.Now;
                _dealerManager.Update_Dealer(dViewModel.Dealer);
                dViewModel.Friendly_Message.Add(MessageStore.Get("DO002"));
            }
            catch (Exception ex)
            {
                dViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("DealerController Update " + ex);
            }

            TempData["dViewModel"] = dViewModel;
            return RedirectToAction("Search");
        }

        public JsonResult Get_Dealers(DealerViewModel dViewModel)
        {
            PaginationInfo pager = new PaginationInfo();
            try
            {
                pager = dViewModel.Pager;
                if (dViewModel.Filter.Dealer_Name != null)
                {
                    dViewModel.Dealers = _dealerManager.Get_Dealer_By_Name(dViewModel.Filter.Dealer_Name, ref pager);
                }
                else
                {
                    dViewModel.Dealers = _dealerManager.Get_Dealers(ref pager);
                }
                dViewModel.Pager = pager;
                dViewModel.Pager.PageHtmlString = PageHelper.NumericPager("javascript:PageMore({0})", dViewModel.Pager.TotalRecords, dViewModel.Pager.CurrentPage + 1, dViewModel.Pager.PageSize, 10, true);
            }
            catch (Exception ex)
            {
                dViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("DealerController Get_Dealers " + ex);
            }
            return Json(dViewModel);
        }
        public ActionResult Get_Dealer_By_Id(DealerViewModel dViewModel)
        {
            try
            {
                dViewModel.Dealer = _dealerManager.Get_Dealer_By_Id(dViewModel.Dealer.Dealer_Id);
            }
            catch (Exception ex)
            {
                dViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("Dealer Get_Dealer_By_Id " + ex);
            }

            return AddEdit_Dealer(dViewModel);
        }
        public JsonResult Check_Existing_Dealer(string Dealer_Name)
        {
            bool check = false;
            try
            {
                check = _dealerManager.Check_Existing_Dealer(Dealer_Name);
            }
            catch (Exception ex)
            {
                Logger.Error("Dealer Controller - Check_Existing_Dealer " + ex.ToString());
            }
            return Json(check, JsonRequestBehavior.AllowGet);
        }

        public List<AutocompleteInfo> Get_Dealer_Autocomplete(string DealerName)
        {
            return _dealerManager.Get_Dealer_Autocomplete(DealerName);
        }
    }
}
