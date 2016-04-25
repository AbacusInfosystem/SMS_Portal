using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMSPortal.Models;
using SMSPortal.Models.PreLogin;
using SMSPortal.Common;
using SMSPortalInfo;
using SMSPortalInfo.Common;
using SMSPortalManager;
using SMSPortalHelper.Logging;
using System.Configuration;
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

        public ActionResult Index(LoginViewModel loginViewModel)
        {
            try
            {
                if (Request.Cookies["UserInfo"] != null)
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    if (TempData["FriendlyMessage"] != null)
                    {
                        loginViewModel.Friendly_Message.Add((FriendlyMessage)TempData["FriendlyMessage"]);
                    }

                    return View("Index", loginViewModel);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error at Home : " + ex.Message);
                loginViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));

                return View("Index", loginViewModel);
            }


        }
        
        public ActionResult ForgotPassword()
        {
            return View("ForgotPassword");
        }

        public ActionResult Authenticate(LoginViewModel loginViewModel)
        {
            try
            {

                CookiesInfo cookies = userManager.AuthenticateUser(loginViewModel.Cookies.User_Name, loginViewModel.Cookies.Password);

                if (cookies.User_Id != 0 && cookies.Is_Active == true)
                {
                    if (cookies.User_Name == loginViewModel.Cookies.User_Name)
                    {
                        SetUsersCookies(loginViewModel.Cookies.User_Name, loginViewModel.Cookies.Password);
                    }                    

                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    if (cookies.User_Id != 0 && cookies.Is_Active == false)
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
            catch (Exception ex)
            {
                Logger.Error("Authentication : " + ex.Message);
                
                Logger.Error("Authentication : " + ex.StackTrace);

                HttpContext.Request.Cookies.Clear();

                loginViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                
                return RedirectToAction("Index", "Login", loginViewModel);
            }
        }               

        private void SetUsersCookies(string userName,string password)
        {
            LoginViewModel loginViewModel = new LoginViewModel();

            try
            {
                if (Request.Cookies["UserInfo"] == null)
                {
                    HttpCookie cookies = new HttpCookie("UserInfo");

                    string cookie_Token = userManager.Set_User_Token_For_Cookies(userName, password);
                    
                    cookies.Values.Add("Token", cookie_Token);

                    cookies.Expires = DateTime.Now.AddDays(2);

                    Response.Cookies.Add(cookies);
                }
            }
            catch (Exception ex)
            {
                HttpContext.Request.Cookies.Clear();

                Logger.Error("SetUsersCookies : " + ex.Message);
            }
        }

        public ActionResult Logout(string timeOut)
        {
            try
            {
                LogoutUser();                
                //if (timeOut == "Timeout")
               // {
                    TempData["FriendlyMessage"] = MessageStore.Get("SYS02");
                //}
            }
            catch (Exception ex)
            {
                Logger.Error("LoginController - Logout: " + ex.ToString());
            }

            return RedirectToAction("Index", "login");
        }

        private void LogoutUser()
        {
            Response.Cookies["UserInfo"].Expires = DateTime.Now.AddYears(-1);

            Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);

            Response.Expires = -1500;

            Response.CacheControl = "no-cache";

            Response.AddHeader("Cache-Control", "no-cache");

            Response.Cache.SetNoStore();

            Response.AddHeader("Pragma", "no-cache");
        }

    }
}
