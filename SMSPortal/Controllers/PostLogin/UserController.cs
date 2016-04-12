using SMSPortal;
using SMSPortal.Common;
using SMSPortal.Models.PostLogin;
using SMSPortalHelper.Logging;
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
        public ActionResult Search()
        {
            return View("Search");
        }
        public ActionResult Index(UserViewModel uViewModel)
        {
            try
            {
                //uViewModel.Roles = _userMan.Get_Roles();
               
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
                uViewModel.User.Created_By = 1;

                uViewModel.User.Created_On = DateTime.Now;

                uViewModel.User.Updated_By = 1;

                uViewModel.User.Updated_On = DateTime.Now;

                //uViewModel.Role.Created_By = 1;

                //uViewModel.Role.Created_Date = DateTime.Now;

                _userMan.Insert_Users(uViewModel.User);
                   // uViewModel.User.User_Id;
                //_userRepo.Insert_User_Role(uViewModel.User.UserId, uViewModel.Selected_User_Role, uViewModel.Role);

                uViewModel.Friendly_Message.Add(MessageStore.Get("UM002"));
            }
            catch (Exception ex)
            {
                uViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                Logger.Error("UserController Insert " + ex);
            }

            TempData["uViewModel"] = uViewModel;
            return RedirectToAction("Search");
        }
    }
}
