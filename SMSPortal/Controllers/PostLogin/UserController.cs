using SMSPortal;
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
using System.Configuration;

namespace SMSPortal.Controllers.PostLogin
{
    public class UserController : Controller
    {
        public UserManager _userMan;

        public CookiesInfo _cookies;


        //public string token = System.Web.HttpContext.Current.Request.Cookies["UserInfo"]["Token"];


        public UserController()
        {
            _userMan = new UserManager();
        }

        [AuthorizeUserAttribute(AppFunction.Token)]
        public ActionResult Search(UserViewModel uViewModel)
        {
            try
            {
                if (TempData["userViewMessage"] != null)
                {
                    uViewModel = (UserViewModel)TempData["userViewMessage"];
                }
            }
            catch (Exception ex)
            {
                uViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("Error At User_Controller - Search " + ex);
            }

            return View("Search", uViewModel);
        }

        public JsonResult Get_Users(UserViewModel uViewModel)
        {
            PaginationInfo pager = new PaginationInfo();
            try
            {
                pager = uViewModel.Pager;

                if (uViewModel.Filter.User_Id != 0)
                {
                    uViewModel.Users = _userMan.Get_Users_By_User_Id_List(uViewModel.Filter.User_Id, ref pager);
                }
                else
                {
                    uViewModel.Users = _userMan.Get_Users(ref pager);
                }

                uViewModel.Pager = pager;

                uViewModel.Pager.PageHtmlString = PageHelper.NumericPager("javascript:PageMore({0})", uViewModel.Pager.TotalRecords, uViewModel.Pager.CurrentPage + 1, uViewModel.Pager.PageSize, 10, true);

            }
            catch (Exception ex)
            {
                uViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("Error At User_Controller - Get_Users " + ex);
            }

            return Json(uViewModel);
        }

        [AuthorizeUserAttribute(AppFunction.Token)]
        public ActionResult Index(UserViewModel uViewModel)
        {
            try
            {
                if (TempData["Entity_Id"] != null && TempData["Role_Id"] != null)
                {
                    uViewModel.User = _userMan.Get_User_By_Entity_Id((int)TempData["Entity_Id"], (int)TempData["Role_Id"]);

                    if (uViewModel.User.User_Id == 0)
                    {
                        uViewModel.User.Role_Id = (int)TempData["Role_Id"];

                        uViewModel.User.Entity_Id = (int)TempData["Entity_Id"];
                    }
                   
                } 
                else
                  {
                       uViewModel.User.Role_Id = 1;

                       uViewModel.User.Entity_Id = 0;
                  }
         
            }
            catch (Exception ex)
            {
                uViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("Error At User Controller - Index " + ex);
            }

            return View("Index", uViewModel);
        }

        [AuthorizeUserAttribute(AppFunction.Token)]
        public ActionResult Insert(UserViewModel uViewModel)
        {
            string link=string.Empty;
            try
            {
                uViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");

                uViewModel.User.Pass_Token = Utility.Generate_Token();
            
              

                _userMan.Insert_Users(uViewModel.User , uViewModel.Cookies.User_Id);

                link = ConfigurationManager.AppSettings["DomainName"].ToString() + "Login/Reset_Password?passtoken="+ uViewModel.User.Pass_Token;

                _userMan.Send_Reset_Password_Email(uViewModel.User.Email_Id, link, uViewModel.User);

                uViewModel.Friendly_Message.Add(MessageStore.Get("UM001"));
            }
            catch (Exception ex)
            {
                uViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));

                Logger.Error("Error At User Controller - Insert " + ex);
            }

            TempData["userViewMessage"] = uViewModel;

            if (uViewModel.User.Role_Id == Convert.ToInt32(RolesIds.Vendor))
            {
                return RedirectToAction("Search", "Vendor");
            }
            else if (uViewModel.User.Role_Id == Convert.ToInt32(RolesIds.Brand))
            {
                return RedirectToAction("Search", "Brand");
            }
            else if (uViewModel.User.Role_Id == Convert.ToInt32(RolesIds.Dealer))
            {
                return RedirectToAction("Search", "Dealer");
            }
            else 
            {
                return RedirectToAction("Search");
            }
        }

        [AuthorizeUserAttribute(AppFunction.Token)]
        public ActionResult Update_User(UserViewModel uViewModel)
        {
            try
            {
                uViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");

                _userMan.Update_User(uViewModel.User , uViewModel.Cookies.User_Id);

                uViewModel.Friendly_Message.Add(MessageStore.Get("UM002"));
            }
            catch (Exception ex)
            {
                uViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));

                Logger.Error("Error At User_Controller - Update_User " + ex);
            }

            TempData["userViewMessage"] = uViewModel;

            if (uViewModel.User.Role_Id == Convert.ToInt32(RolesIds.Vendor))
            {
                return RedirectToAction("Search", "Vendor");
            }
            else if (uViewModel.User.Role_Id == Convert.ToInt32(RolesIds.Brand))
            {
                return RedirectToAction("Search", "Brand");
            }
            else if (uViewModel.User.Role_Id == Convert.ToInt32(RolesIds.Dealer))
            {
                return RedirectToAction("Search", "Dealer");
            }
            else
            {
                return RedirectToAction("Search");
            }
        }

        public JsonResult Check_Existing_User(string user_Name)
        {
            bool check = false;

            try
            {
                check = _userMan.Check_Existing_User(user_Name);
            }
            catch (Exception ex)
            {
                Logger.Error("Error At User Controller - Check_Existing_User " + ex.ToString());
            }

            return Json(check, JsonRequestBehavior.AllowGet);
        }

         [AuthorizeUserAttribute(AppFunction.Token)]
        public ActionResult Get_User_By_Id(UserViewModel uViewModel)
        {
            try
            {
                uViewModel.User = _userMan.Get_User_By_Id(uViewModel.User.User_Id);

                uViewModel.Roles = _userMan.Get_Roles();              
            }
            catch (Exception ex)
            {
                uViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));

                Logger.Error("Error At User Controller - Get_User_By_Id " + ex);
            }

            return View("Index", uViewModel);
        }

        public JsonResult Get_Entity_By_Role(int role_Id)
        {
            UserViewModel uViewModel = new UserViewModel();

            try
            {
                uViewModel.User.Entities = _userMan.Get_Entity_By_Role(role_Id);
            }
            catch (Exception ex)
            {
                Logger.Error("Error At User_Controller - Get_Entity_By_Role" + ex.ToString());
            }

            return Json(uViewModel.User.Entities, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Get_User_Autocomplete(string user)
        {
            List<AutocompleteInfo> autoList = new List<AutocompleteInfo>();

            try
            {
                autoList = _userMan.Get_User_Autocomplete(user);
            }
            catch (Exception ex)
            {
                Logger.Error("Error At User_Controller - Get_User_Autocomplete " + ex.ToString());
            }

            return Json(autoList, JsonRequestBehavior.AllowGet);
        }

    }
}
