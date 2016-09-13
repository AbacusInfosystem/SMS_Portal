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
using System.Data.SqlClient;
 
namespace SMSPortal.Controllers.PostLogin
{
    public class ReceivableController : Controller
    {
        private string _sqlCon = "";

        public ReceivableManager _receivableManager;

        public InvoiceManager _invoiceManager;

        public OrdersManager _ordersManager;

        public UserManager _userManager;

        public ReceivableController()
        {
            _receivableManager = new ReceivableManager();
            _invoiceManager = new InvoiceManager();
            _userManager = new UserManager();
            _ordersManager = new OrdersManager();
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
                    rViewModel.Filter.Entity_Id = 0;
                }
                else
                {
                    rViewModel.Filter.Entity_Id = rViewModel.Cookies.Entity_Id;
                    rViewModel.Filter.Role_Id = rViewModel.Cookies.Role_Id;
                }

            }
            catch (Exception ex)
            {
                rViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));

                Logger.Error("Error at Receivable Controller - Searches " + ex);
            }

            return View("Searches", rViewModel);
        }

        public ActionResult Index(ReceivableViewModel rViewModel)
        {
            return View("Index", rViewModel);
        }

        public JsonResult Get_Recievable(ReceivableViewModel rViewModel)
        {
            PaginationInfo pager = new PaginationInfo();

            try
            {
                rViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");

                pager = rViewModel.Pager;

                if (rViewModel.Filter.Invoice_Id != 0)
                {
                    rViewModel.Receivables = _receivableManager.Get_Receivable_By_Id(rViewModel.Filter.Invoice_Id, ref pager);
                }
                else
                {
                    if (rViewModel.Cookies.Role_Id==Convert.ToInt32(RolesIds.Vendor))
                    {
                        rViewModel.Receivables = _receivableManager.Get_Vendor_Receivables(ref pager, rViewModel.Cookies.Entity_Id, rViewModel.Cookies.Role_Id);
                    }
                    else
                    {
                        rViewModel.Receivables = _receivableManager.Get_Receivables(ref pager, rViewModel.Filter.Entity_Id, rViewModel.Filter.Role_Id);
                    }
                    
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
                rViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");

                int Id = rViewModel.Receivable.Invoice_Id;

                if (rViewModel.Cookies.Role_Id == Convert.ToInt32(RolesIds.Vendor))
                {
                    rViewModel.Receivable = _receivableManager.Get_Receivable_Data_By_Id(rViewModel.Receivable.Invoice_Id, rViewModel.Cookies.Role_Id, rViewModel.Cookies.Entity_Id);
                }
                else
                {
                    rViewModel.Receivable = _receivableManager.Get_Dealer_Receivable_Data_By_Id(rViewModel.Receivable.Invoice_Id, rViewModel.Cookies.Role_Id, rViewModel.Cookies.Entity_Id);
                }

                rViewModel.Receivables = _receivableManager.Get_Receivable_Items(rViewModel.Receivable.Receivable_Id);

                rViewModel.Receivable.Invoice_Id = Id;

                if (rViewModel.Cookies.Role_Id == Convert.ToInt32(RolesIds.Vendor))
                {
                    rViewModel.Invoice = _invoiceManager.Get_Vendor_Invoice_By_Id(rViewModel.Receivable.Invoice_Id, rViewModel.Cookies.Entity_Id);

                    rViewModel.Order = _ordersManager.Get_Vendor_Order_Data_By_Id(rViewModel.Invoice.Order_Id);
                }
                else
                {
                    rViewModel.Invoice = _invoiceManager.Get_Invoice_By_Id(rViewModel.Receivable.Invoice_Id);

                    rViewModel.Order = _ordersManager.Get_Order_Data_By_Id(rViewModel.Invoice.Order_Id);
                }

                

                //rViewModel.Receivable.Invoice_Amount = _receivableManager.Get_Invoice_Amount(Id);

                rViewModel.Receivable.Invoice_Amount = rViewModel.Invoice.Amount;
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
            _sqlCon = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

            bool Status=false;

            using (SqlConnection con = new SqlConnection(_sqlCon))
            {
                con.Open();
                using (SqlTransaction trans = con.BeginTransaction())
                {
                    try
                    {
                        rViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");

                        if (rViewModel.Cookies.Role_Id==Convert.ToInt32(RolesIds.Vendor))
                        {
                            rViewModel.Invoice = _invoiceManager.Get_Vendor_Invoice_By_Id(rViewModel.Receivable.Invoice_Id, rViewModel.Cookies.Entity_Id); 
                        }
                        else
                        {
                            rViewModel.Invoice = _invoiceManager.Get_Invoice_By_Id(rViewModel.Receivable.Invoice_Id);  
                        }

                        rViewModel.Receivable.Receivable_Id = _receivableManager.Insert_Receivable(rViewModel.Receivable, rViewModel.Cookies.User_Id, out Status, rViewModel.Cookies.Role_Id, rViewModel.Cookies.Entity_Id);

                        _receivableManager.Insert_ReceivableItems(rViewModel.Receivable, rViewModel.Cookies.User_Id);

                        if (rViewModel.Cookies.Role_Id == Convert.ToInt32(RolesIds.Vendor))
                        {
                            _ordersManager.Set_Vendor_Order_Balanace_Amount(rViewModel.Invoice.Order_Id, rViewModel.Receivable.Receivable_Item_Amount);

                            _ordersManager.Set_Vendor_Order_Status(rViewModel.Invoice.Order_Id, Convert.ToInt32(OrderStatus.Order_Confirmed));

                            _ordersManager.Set_Order_Status(rViewModel.Invoice.Order_Id, Convert.ToInt32(OrderStatus.Order_Confirmed));
                        }
                        else
                        {
                            _ordersManager.Set_Order_Balanace_Amount(rViewModel.Invoice.Order_Id, rViewModel.Receivable.Receivable_Item_Amount);

                            _ordersManager.Set_Order_Status(rViewModel.Invoice.Order_Id, Convert.ToInt32(OrderStatus.Order_Confirmed));
                        }

                        rViewModel.Receivable = _receivableManager.Get_Receivable_Data_By_Id(rViewModel.Receivable.Invoice_Id, rViewModel.Cookies.Role_Id, rViewModel.Cookies.Entity_Id);

                        rViewModel.Receivables = _receivableManager.Get_Receivable_Items(rViewModel.Receivable.Receivable_Id);

                        _receivableManager.Insert_Receivable_Receipt(rViewModel.Receivable, rViewModel.Cookies.User_Id);

                        UserInfo user = _userManager.Get_User_By_Entity_Id(rViewModel.Invoice.Entity_Id, rViewModel.Invoice.Role_Id);

                        if (rViewModel.Cookies.Role_Id == Convert.ToInt32(RolesIds.Vendor))
                        {
                            rViewModel.Order = _ordersManager.Get_Vendor_Order_Data_By_Id(rViewModel.Invoice.Order_Id);
                            rViewModel.Order.OrderItems = _ordersManager.Get_Vendor_Orders_Item_By_Id(rViewModel.Order.Vendor_Order_Id);
                        }
                        else
                        {
                            rViewModel.Order = _ordersManager.Get_Order_Data_By_Id(rViewModel.Invoice.Order_Id);
                            rViewModel.Order.OrderItems = _ordersManager.Get_Orders_Item_By_Id(rViewModel.Invoice.Order_Id); 
                        }                      

                        if (rViewModel.Invoice.Role_Id==2)
                        {
                            rViewModel.User = _userManager.Get_User_By_Entity_Id(rViewModel.Invoice.Entity_Id, rViewModel.Invoice.Role_Id);

                            _receivableManager.Send_Payment_Receipt(rViewModel.User.First_Name, rViewModel.User.Email_Id, rViewModel.Receivable, rViewModel.Receivables, rViewModel.Order);
                        }

                        if (rViewModel.Invoice.Role_Id == 3)
                        {
                            rViewModel.User = _userManager.Get_User_By_Entity_Id(rViewModel.Invoice.Entity_Id, rViewModel.Invoice.Role_Id);

                            _receivableManager.Send_Payment_Receipt(rViewModel.User.First_Name, rViewModel.User.Email_Id, rViewModel.Receivable, rViewModel.Receivables, rViewModel.Order);
                        }
                        
                        if(Status==true)
                        {
                            rViewModel.User = _userManager.Get_User_By_Entity_Id(rViewModel.Order.Dealer_Id, 3);

                            if (rViewModel.User.Email_Id!=null)
                            {
                                _ordersManager.Send_Order_Status_Notification(rViewModel.User.First_Name, rViewModel.User.Email_Id, rViewModel.Order, true, rViewModel.Receivable.Receivable_Id, "Receivable Status", rViewModel.Cookies.Entity_Id);
                            }
                           
                        }
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();

                        rViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));

                        Logger.Error("Error at Receivable Controller - Insert_Receivable " + ex);
                    }
                    finally
                    {                        
                        trans.Commit();

                        con.Close();

                        rViewModel.Friendly_Message.Add(MessageStore.Get("RC001"));
                    }
                }
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

        public JsonResult Send_Receivable_Payment_Receipt(int invoice_Id, int receivable_Id)
        {
            ReceivableViewModel rViewModel = new ReceivableViewModel();

            try
            {
                rViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");

                if (rViewModel.Cookies.Role_Id == Convert.ToInt32(RolesIds.Vendor))
                {
                    rViewModel.Invoice = _invoiceManager.Get_Vendor_Invoice_By_Id(invoice_Id, rViewModel.Cookies.Entity_Id);
                }
                else
                {
                    rViewModel.Invoice = _invoiceManager.Get_Invoice_By_Id(receivable_Id);
                }

                rViewModel.Receivable = _receivableManager.Get_Receivable_Data_By_Id(invoice_Id, rViewModel.Cookies.Role_Id, rViewModel.Cookies.Entity_Id);

                rViewModel.Receivables = _receivableManager.Get_Receivable_Items(receivable_Id);

                if (rViewModel.Cookies.Role_Id == Convert.ToInt32(RolesIds.Vendor))
                {
                    rViewModel.Order = _ordersManager.Get_Vendor_Order_Data_By_Id(rViewModel.Invoice.Order_Id);
                    rViewModel.Order.OrderItems = _ordersManager.Get_Vendor_Orders_Item_By_Id(rViewModel.Order.Vendor_Order_Id);
                }
                else
                {
                    rViewModel.Order = _ordersManager.Get_Order_Data_By_Id(rViewModel.Invoice.Order_Id);
                    rViewModel.Order.OrderItems = _ordersManager.Get_Orders_Item_By_Id(rViewModel.Invoice.Order_Id);
                } 

                _receivableManager.Send_Payment_Receipt(rViewModel.Cookies.First_Name, rViewModel.Cookies.User_Email, rViewModel.Receivable, rViewModel.Receivables, rViewModel.Order);

                rViewModel.Friendly_Message.Add(MessageStore.Get("RC002"));
            }

            catch (Exception ex)
            {
                rViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));

                Logger.Error("Error at Receivable Controller - Send_Receivable_Payment_Receipt " + ex);
            }

            return Json(rViewModel);
        }


        public ActionResult Show_Invoice_Details(int invoice_id)
        {

            InvoiceViewModel iViewModel = new InvoiceViewModel();
            iViewModel.Invoice.Invoice_Id = invoice_id;
            TempData["Invoice"] = iViewModel;
            return RedirectToAction("Get_Invoice_By_Id","Invoice");
        }
    }
}
