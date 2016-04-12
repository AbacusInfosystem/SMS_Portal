using SMSPortal;
using SMSPortal.Common;
using SMSPortal.Models.PostLogin;
using SMSPortalHelper.Logging;
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

        public UserController()
        {
            _userMan = new UserManager();
        }
        public ActionResult Search(UserViewModel uViewModel)
        {
            PaginationInfo Pager = new PaginationInfo();
            try
            {
                Pager.IsPagingRequired = false;
                uViewModel.Users = _userMan.Get_Users(ref Pager);

                if (TempData["categoryViewMessage"] != null)
                {
                    uViewModel.Friendly_Message = (List<FriendlyMessage>)TempData["categoryViewMessage"];
                }
            }
            catch (Exception ex)
            {
                uViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("UserController Search " + ex);
            }
            return View("Search", uViewModel);
        }
        public ActionResult Index(UserViewModel uViewModel)
        {
            PaginationInfo Pager = new PaginationInfo();
            try
            {
                Pager.IsPagingRequired = false;
               
            }
            catch (Exception ex)
            {
                uViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));

                Logger.Error("UserController Index " + ex);
            }

            return View("Index", uViewModel);
      
        }
        public ActionResult Insert(UserViewModel uViewModel)
        {
            try
            {
                uViewModel.User.Created_By = ((UserInfo)Session["SessionInfo"]).User_Id;

                uViewModel.User.Created_On = DateTime.Now;

                uViewModel.User.Updated_By = ((UserInfo)Session["SessionInfo"]).User_Id;

                uViewModel.User.Updated_On = DateTime.Now;

                //uViewModel.Role.Created_By = 1;

                //uViewModel.Role.Created_Date = DateTime.Now;

                _userMan.Insert_Users(uViewModel.User);
                   // uViewModel.User.User_Id;
                //_userRepo.Insert_User_Role(uViewModel.User.UserId, uViewModel.Selected_User_Role, uViewModel.Role);

                uViewModel.Friendly_Message.Add(MessageStore.Get("UM001"));
            }
            catch (Exception ex)
            {
                uViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("UserController Insert " + ex);
            }

            TempData["uViewModel"] = uViewModel;
            return RedirectToAction("Search");
        }
        public ActionResult Update_User(UserViewModel uViewModel)
        {
            try
            {
                uViewModel.User.Updated_By = ((UserInfo)Session["SessionInfo"]).User_Id;
                uViewModel.User.Updated_On = DateTime.Now;
                _userMan.Update_User(uViewModel.User);
                uViewModel.Friendly_Message.Add(MessageStore.Get("UM002"));
            }
            catch (Exception ex)
            {
                uViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("CategoryController Update " + ex);
            }

            TempData["userViewMessage"] = uViewModel;
            return RedirectToAction("Search");
        }
        public ActionResult Get_User_By_Id(UserViewModel uViewModel)
        {
            try
            {
                uViewModel.User = _userMan.Get_User_By_Id(uViewModel.User.User_Id);

            }
            catch (Exception ex)
            {
                uViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("UserController Get_User_By_Id " + ex);
            }

            return View("Index", uViewModel);
        }
    }
}
