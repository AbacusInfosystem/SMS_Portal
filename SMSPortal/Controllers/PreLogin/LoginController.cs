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

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View("ForgotPassword");
        }

        public ActionResult Authenticate(LoginViewModel loginViewModel)
        {
            try
            {
                UserManager userManager = new UserManager();
                UserInfo user = userManager.AuthenticateUser(loginViewModel.user.UserName, loginViewModel.user.Password);
                if (user.UserId != 0 && user.Is_Active == true)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    if (loginViewModel.user.UserId != 0 && loginViewModel.user.Is_Active == false)
                    {
                        TempData["FriendlyMessage"] = MessageStore.Get("SYS06");
                    }
                    else
                    {
                        TempData["FriendlyMessage"] = MessageStore.Get("SYS03");
                    }
                    return RedirectToAction("Index", "Login");
                }
            }
            catch(Exception ex)
            {
                loginViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                return RedirectToAction("Index","Login", loginViewModel);
                throw ex; 
            }

            


        }

    }
}
