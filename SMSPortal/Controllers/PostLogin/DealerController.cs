using SMSPortal.Common;
using SMSPortal.Models.PostLogin;
using SMSPortalHelper.Logging;
using SMSPortalHelper.PageHelper;
using SMSPortalInfo;
using SMSPortalInfo.Common;
using SMSPortalManager;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMSPortal.Controllers.PostLogin
{
    public class DealerController : Controller

    {

        public DealerManager _dealerManager;

        public StateManager _stateManager;
        
        public DealerController()

        {

            _dealerManager = new DealerManager();

            _stateManager = new StateManager();
             
        }   

        public ActionResult Search(DealerViewModel dViewModel) 

        {
            try
            {
                if (TempData["dViewModel"] != null)
                {
                    dViewModel = (DealerViewModel)TempData["dViewModel"];
                }

                dViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");

               if(dViewModel.Cookies.Role_Id == 1)
               {
                   dViewModel.Filter.Brand_Id = 0;
               }
               else
               {
                dViewModel.Filter.Brand_Id = dViewModel.Cookies.Entity_Id;
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
                dViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");

                dViewModel.Dealer.Created_By = dViewModel.Cookies.User_Id; 

                dViewModel.Dealer.Created_On = DateTime.Now;

                dViewModel.Dealer.Updated_By = dViewModel.Cookies.User_Id; 

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
                dViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");

                dViewModel.Dealer.Updated_By = dViewModel.Cookies.User_Id; 

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

                if (dViewModel.Filter.Dealer_Id != 0)

                {
                    dViewModel.Dealers = _dealerManager.Get_Dealer_By_Id(dViewModel.Filter.Dealer_Id, dViewModel.Dealer.Brand_Id, ref pager);
                }

                else

                {
                    dViewModel.Dealers = _dealerManager.Get_Dealers(ref pager, dViewModel.Filter.Brand_Id);
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

            if (dViewModel.Filter.Mode == "Edit")
            {
                return AddEdit_Dealer(dViewModel);
            }
            else
            {
                return View("Dealer_Detail", dViewModel);
            }

            
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

        public JsonResult Get_Dealer_Autocomplete(string Dealer)

        {
            
            List<AutocompleteInfo> autoList = new List<AutocompleteInfo>();

            try

            {
                autoList = _dealerManager.Get_Dealer_Autocomplete(Dealer);
            }

            catch (Exception ex)

            {
                Logger.Error("Error At Dealer_Controller - Get_Dealer_Autocomplete " + ex.ToString());
            }

            return Json(autoList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add_Dealer_User(DealerViewModel dViewModel)
        {
            TempData["Entity_Id"] = dViewModel.Dealer.Dealer_Id;

            TempData["Role_Id"] = RolesIds.Dealer;

            return RedirectToAction("Index", "User");
        }

        [AuthorizeUserAttribute(AppFunction.Token)]
        public ActionResult Profile(DealerViewModel dViewModel)
        {
            try
            {
                if (TempData["dViewModel"] != null)
                {
                    dViewModel = (DealerViewModel)TempData["dViewModel"];
                }

                dViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");

                dViewModel.Dealer = _dealerManager.Get_Dealer_By_Id(dViewModel.Cookies.Entity_Id);

                dViewModel.States = _stateManager.Get_States();

                dViewModel.Brands = _dealerManager.Get_Brands();

            }
            catch (Exception ex)
            {
                dViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));

                Logger.Error("Error at Dealer controller - Profile " + ex);
            }

            return View("Profile", dViewModel);
        }

        [AuthorizeUserAttribute(AppFunction.Token)]
        public ActionResult Edit_Dealer_Profile(DealerViewModel dViewModel)
        {
            try
            {
                dViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");

                dViewModel.Dealer = _dealerManager.Get_Dealer_By_Id(dViewModel.Dealer.Dealer_Id);

                dViewModel.States = _stateManager.Get_States();

                dViewModel.Brands = _dealerManager.Get_Brands();

            }
            catch (Exception ex)
            {
                dViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));

                Logger.Error("Error at Dealer controller - Edit_Dealer_Profile " + ex);
            }

            return View("Update_Profile", dViewModel);
        }

        [AuthorizeUserAttribute(AppFunction.Token)]
        public ActionResult Update_Dealer_Profile(DealerViewModel dViewModel)
        {
            try
            {
                dViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");

                _dealerManager.Update_Dealer_Profile(dViewModel.Dealer, dViewModel.Cookies.User_Id);

                dViewModel.Friendly_Message.Add(MessageStore.Get("DO004"));

                TempData["dViewModel"] = dViewModel;
            }
            catch (Exception ex)
            {
                dViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));

                Logger.Error("Error at Dealer Controller - Update_Dealer_Profile " + ex);
            }

            return RedirectToAction("Profile");
        }
    }
}
