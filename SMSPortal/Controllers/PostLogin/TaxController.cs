using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMSPortal.Models.PostLogin;
using SMSPortalManager;
using SMSPortal.Common;
using SMSPortalHelper.Logging;
using SMSPortalInfo.Common;
using SMSPortalInfo;
using SMSPortalHelper.PageHelper;

namespace SMSPortal.Controllers.PostLogin
{
    public class TaxController : Controller
    {

        public TaxManager _taxManager;

        public TaxController()
        {
            _taxManager = new TaxManager();
        }

        public ActionResult Index(TaxViewModel tViewModel)
        
        {

            //if (TempData["tViewModel"] != null)
            //{
            //    tViewModel = (TaxViewModel)TempData["tViewModel"];
            //}


            try
            {
                tViewModel.Tax = _taxManager.Get_Tax_By_Id();

            }
            catch (Exception ex)
            {
                tViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));

                Logger.Error("TaxController Get_Tax_By_Id " + ex);
            }

            return View("Index", tViewModel);

        }

        public ActionResult Insert_Tax(TaxViewModel tViewModel)
        {
            try
            
            {
                tViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");

                tViewModel.Tax.Created_By = tViewModel.Cookies.User_Id;

                tViewModel.Tax.Created_On = DateTime.Now;

                tViewModel.Tax.Updated_By = tViewModel.Cookies.User_Id;

                tViewModel.Tax.Updated_On = DateTime.Now;

                tViewModel.Tax.Tax_Id = _taxManager.Insert_Tax(tViewModel.Tax);

                tViewModel.Friendly_Message.Add(MessageStore.Get("TO001"));

            }

            catch (Exception ex)
            
            {

                tViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));

                Logger.Error("TaxController Insert " + ex);

            }

            TempData["tViewModel"] = tViewModel;

            return RedirectToAction("Index");

        }

        public ActionResult Update_Tax(TaxViewModel tViewModel)
        {
            try
            {

                tViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");

                tViewModel.Tax.Updated_By = tViewModel.Cookies.User_Id;

                tViewModel.Tax.Updated_On = DateTime.Now;

                _taxManager.Update_Tax(tViewModel.Tax);

                tViewModel.Friendly_Message.Add(MessageStore.Get("TO002"));

            }
            catch (Exception ex)
            {
                
                tViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));

                Logger.Error("TaxController Update " + ex);

            }

            TempData["tViewModel"] = tViewModel;

            return RedirectToAction("Index");
        }

        public ActionResult Get_Tax_By_Id(TaxViewModel tViewModel)
        {
            try
            {
                tViewModel.Tax = _taxManager.Get_Tax_By_Id();

            }
            catch (Exception ex)
            {
                tViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));

                Logger.Error("TaxController Get_Tax_By_Id " + ex);
            }

            return View("Index", tViewModel);
        }

    }
}
