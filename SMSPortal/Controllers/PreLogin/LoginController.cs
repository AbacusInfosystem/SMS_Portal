using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMSPortal.Models;
using SMSPortal.Models.PreLogin;
using SMSPortal.Common;
using SMSPortalInfo;
using SMSPortalManager;
namespace SMSPortal.Controllers.PreLogin
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public UserManager userManager;        
        public LoginController()
        {
            userManager = new UserManager();
            
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View("ForgotPassword");
        }

        //public ActionResult Authenticate(LoginViewModel loginViewModel)
        //{
        //    try
        //    {

        //        loginViewModel.User = userManager.AuthenticateUser(loginViewModel.User.UserName, loginViewModel.User.Password);
        //        if (loginViewModel.User.UserId != 0 && loginViewModel.User.Is_Active == true)
        //        {
        //            return RedirectToAction("Index", "Home");
        //        }
        //        else
        //        {
        //            if (loginViewModel.User.UserId != 0 && loginViewModel.User.Is_Active == false)
        //            {
        //                TempData["Friendly_Message"] = MessageStore.Get("SYS06");
        //            }
        //            else
        //            {
        //                TempData["Friendly_Message"] = MessageStore.Get("SYS03");
        //            }
        //            return RedirectToAction("Index", "Login");
        //        }
        //    }
        //    catch 
        //    {
        //        loginViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
        //        return RedirectToAction("Index","Login", loginViewModel);
                 
        //    }

            


        //}

    }
}
