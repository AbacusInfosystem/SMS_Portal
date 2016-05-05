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

        public ActionResult Searches(ReceivableViewModel rViewModel)
        {
            try
            
            {
                rViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");

                if (rViewModel.Cookies.Role_Id == 1)

                   {
                       rViewModel.Filter.Dealer_Id = 0;
                   }

                   else

                   {
                       rViewModel.Filter.Dealer_Id = rViewModel.Cookies.Entity_Id;
                   }

               }

            catch (Exception ex)

            {
                rViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));

                Logger.Error("DealerController Search " + ex);
            }   

            return View("Searches", rViewModel);
        }

        public ActionResult Index( ReceivableViewModel rViewModel)
        { 
            return View("Index", rViewModel);
        }     

        public JsonResult Get_Recievable(ReceivableViewModel rViewModel)

        {
            PaginationInfo pager = new PaginationInfo();

            try

            {
                pager = rViewModel.Pager;

                if (rViewModel.Filter.Invoice_Id != 0)

                {
                    rViewModel.Receivables = _receivableManager.Get_Receivable_By_Id(rViewModel.Filter.Dealer_Id, rViewModel.Filter.Invoice_Id, ref pager);
                }

                else

                {
                    rViewModel.Receivables = _receivableManager.Get_Receivables(ref pager, rViewModel.Filter.Dealer_Id);
                }

                rViewModel.Pager = pager;

                rViewModel.Pager.PageHtmlString = PageHelper.NumericPager("javascript:PageMore({0})", rViewModel.Pager.TotalRecords, rViewModel.Pager.CurrentPage + 1, rViewModel.Pager.PageSize, 10, true);
            }

            catch (Exception ex)
            
            {
                rViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));

                Logger.Error("Error at Receivable Controller - Get_Receivables " + ex);
            }

            return Json(rViewModel);
        }

        [AuthorizeUserAttribute(AppFunction.Token)]

        public ActionResult Get_Receivables_By_Id(ReceivableViewModel rViewModel)

        {
            try
            
            {
                int Id = rViewModel.Receivable.Invoice_Id;

                rViewModel.Receivable = _receivableManager.Get_Receivable_Data_By_Id(rViewModel.Receivable.Invoice_Id);

                rViewModel.Receivables = _receivableManager.Get_Receivable_Items(rViewModel.Receivable.Receivable_Id);

                rViewModel.Receivable.Invoice_Id = Id;

                rViewModel.Receivable.Invoice_Amount = _receivableManager.Get_Invoice_Amount(Id);
            }

            catch (Exception ex)
            {
                rViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));

                Logger.Error("Error At Receivable Controller - Get_Receivables_By_Id" + ex.Message);
            }


            if (rViewModel.Filter.Mode == "Edit")

            {
                return View("Index", rViewModel);
            }

            else
            {
                return View("Dealer_Payables_Detail", rViewModel);
            }
 
        }

        public JsonResult Insert_Receivable(ReceivableViewModel rViewModel)
        {
            try

            {

                rViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");

                rViewModel.Receivable.Receivable_Id = _receivableManager.Insert_Receivable(rViewModel.Receivable, rViewModel.Cookies.User_Id);

                _receivableManager.Insert_ReceivableItems(rViewModel.Receivable, rViewModel.Cookies.User_Id);

                rViewModel.Receivable = _receivableManager.Get_Receivable_Data_By_Id(rViewModel.Receivable.Invoice_Id);

                rViewModel.Receivables = _receivableManager.Get_Receivable_Items(rViewModel.Receivable.Receivable_Id);

                _receivableManager.Insert_Receivable_Receipt(rViewModel.Receivable, rViewModel.Cookies.User_Id);

                _receivableManager.Send_Payment_Receipt(rViewModel.Cookies.User_Email, rViewModel.Receivable, rViewModel.Receivables);               

                rViewModel.Friendly_Message.Add(MessageStore.Get("RC001"));
            }

            catch (Exception ex)
            
            {

                rViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));

                Logger.Error("Error at Receivable Controller - Insert_Receivable " + ex);
            }

            return Json(rViewModel);
        }

        //public JsonResult Delete_Receivable_Data_By_Id(int receivable_Item_Id,int receivable_Id)
        //{
        //    ReceivableViewModel rViewModel = new ReceivableViewModel();
        //    try
        //    {
        //        _receivableManager.Delete_Receivable_Data_Item_By_Id(receivable_Item_Id);

        //        rViewModel.Receivable = _receivableManager.Get_Receivable_Data_By_Id(receivable_Id);

        //        rViewModel.Receivables = _receivableManager.Get_Receivable_Items(receivable_Id);

        //        rViewModel.Friendly_Message.Add(MessageStore.Get("RE001"));
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error("Error at Receivable Controller - Delete_Receivable_Data_By_Id " + ex.ToString());
        //    }
        //    return Json(rViewModel, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult Get_Receivable_Invoice_Autocomplete(string invoiceno)
        {

            List<AutocompleteInfo> autoList = new List<AutocompleteInfo>();

            try

            {
                autoList = _receivableManager.Get_Invoice_Autocomplete(invoiceno);
            }

            catch (Exception ex)

            {
                Logger.Error("Error at Receivable Controller - Get_Receivable_Invoice_Autocomplete " + ex.ToString());
            }

            return Json(autoList, JsonRequestBehavior.AllowGet);

        }

    }
}
