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

namespace SMSPortal.Controllers.PostLogin
{
    public class UserController : Controller
    {
        public UserManager _userMan;

        public CookiesInfo _cookies;

        public string token = System.Web.HttpContext.Current.Request.Cookies["UserInfo"]["Token"];

        public UserController()
        {
            _userMan = new UserManager();
        }

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

        public ActionResult Index(UserViewModel uViewModel)
        {
            try
            {
                uViewModel.Roles = _userMan.Get_Roles();            
            }
            catch (Exception ex)
            {
                uViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("Error At User Controller - Index " + ex);
            }

            return View("Index", uViewModel);
        }

        public ActionResult Insert(UserViewModel uViewModel)
        {
            try
            {
                uViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");
                _userMan.Insert_Users(uViewModel.User , uViewModel.Cookies.User_Id);
                uViewModel.Friendly_Message.Add(MessageStore.Get("UM001"));
            }
            catch (Exception ex)
            {
                uViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("Error At User Controller - Insert " + ex);
            }

            TempData["userViewMessage"] = uViewModel;
            return RedirectToAction("Search");
        }

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
            return RedirectToAction("Search");
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
