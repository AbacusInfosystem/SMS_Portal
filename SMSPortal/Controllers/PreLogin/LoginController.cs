﻿using System;
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
using SMSPortal.Models.PostLogin;
namespace SMSPortal.Controllers.PreLogin
{
    public class LoginController : Controller
    {

        public UserManager _userManager;        

        public LoginController()
        {
            _userManager = new UserManager();
        }
        
        public ActionResult Index(LoginViewModel lViewModel)
        {
            try
            {
                if (Request.Cookies["UserInfo"] != null)
                {
                    lViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");

                    if (lViewModel.Cookies == null)
                    {
                        lViewModel.Friendly_Message.Add(MessageStore.Get("SYS02"));
                    }
                }
                else
                {
                    if (TempData["FriendlyMessage"] != null)
                    {
                        lViewModel.Friendly_Message.Add((FriendlyMessage)TempData["FriendlyMessage"]);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error at Index : " + ex.Message);
                lViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                return View("Index", lViewModel);
            }

            if (TempData["BrandName"] != null)
            {
                lViewModel.Cookies.Brand_Name = TempData["BrandName"].ToString();
            }

            if (lViewModel.Cookies.Brand_Name == "Mercedes")
            {
                return View("Index", lViewModel);
            }
            else if (lViewModel.Cookies.Brand_Name == "Renault")
            {
                return RedirectToAction("Renault", "Login");
            }
            else
            {
                return RedirectToAction("Home", "Login");
            }

           
            
        }

        public ActionResult Home(LoginViewModel lViewModel)      
        {
            return View("Home");
        }

        public ActionResult ForgotPassword(UserViewModel uViewModel)
        {
            return View("ForgotPassword", uViewModel);
        }

        public ActionResult Send_Reset_Password(UserViewModel uViewModel)
        {
            string link = string.Empty;
            //UserViewModel uViewModel = new UserViewModel();
            UserInfo User = new UserInfo();
        
            try
            {

                uViewModel.User = _userManager.Get_User_By_Email(uViewModel.User.Email_Id);
                link = ConfigurationManager.AppSettings["DomainName"].ToString() + "Login/Reset_Password?passtoken=" + uViewModel.User.Pass_Token;

                _userManager.Send_Reset_Password_Email(uViewModel.User.Email_Id, link, uViewModel.User);
                uViewModel.Friendly_Message.Add(MessageStore.Get("UM001"));
            }
            catch (Exception ex)
            {
                uViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));

                Logger.Error("Error At User Controller - Insert " + ex);
            }

            TempData["userViewMessage"] = uViewModel;
            TempData["BrandName"] = uViewModel.User.Brand_Name;

            return RedirectToAction("Index");
        }

        public ActionResult Authenticate(LoginViewModel lViewModel)
        {
            try
            {                
                CookiesInfo cookies = _userManager.AuthenticateUser(lViewModel.Cookies.User_Name,Utility.Encrypt(lViewModel.Cookies.Password));

                if (cookies.User_Id != 0 && cookies.Is_Active == true)
                {
                    if (cookies.Role_Name == "admin")
                    {
                        if (cookies.User_Name == lViewModel.Cookies.User_Name)
                        {
                            SetUsersCookies(lViewModel.Cookies.User_Name, lViewModel.Cookies.Password);

                            return RedirectToAction("Index", "Dashboard");
                        }
                        else
                        {
                            lViewModel.Friendly_Message.Add(MessageStore.Get("SYS02"));

                            return View("Index", lViewModel);
                        }
                    }
                    else
                    {
                        if (cookies.User_Name == lViewModel.Cookies.User_Name)
                        {
                            SetUsersCookies(lViewModel.Cookies.User_Name, lViewModel.Cookies.Password);

                            return RedirectToAction("Index", "Dashboard");
                        }
                        else
                        {
                            lViewModel.Friendly_Message.Add(MessageStore.Get("SYS02"));

                            return View("Index", lViewModel);
                        }
                       
                    }                                             
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

                lViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                
                return RedirectToAction("Index", "Login", lViewModel);
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

                    string cookie_Token = _userManager.Set_User_Token_For_Cookies(userName, password);
                    
                    cookies.Values.Add("Token", cookie_Token);

                    cookies.Expires = DateTime.Now.AddDays(2);

                    Response.Cookies.Add(cookies);
                }
                else
                {
                    string cookie_Token = _userManager.Set_User_Token_For_Cookies(userName, password);

                    Response.Cookies["UserInfo"]["Token"] = cookie_Token;
                }
            }
            catch (Exception ex)
            {
                HttpContext.Request.Cookies.Clear();

                Logger.Error("Error at Login Controller - SetUsersCookies : " + ex.Message);
            }
        }

        public ActionResult Logout(string timeOut)
        {
            CookiesInfo Cookies = Utility.Get_Login_User("UserInfo", "Token");

            try
            {
                LogoutUser();

                //TempData["FriendlyMessage"] = MessageStore.Get("SYS010");

                TempData["BrandName"] = "Logout";
            }
            catch (Exception ex)
            {
                Logger.Error("Error at Login Controller - Logout: " + ex.ToString());
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

        public ActionResult Reset_Password()
        {
            string passtoken = String.Empty;
            UserViewModel uViewModel = new UserViewModel();
            UserInfo user = new UserInfo();
            if (!String.IsNullOrEmpty(Request.QueryString["passtoken"].ToString()))
            {
                 passtoken = Request.QueryString["passtoken"].ToString();
                 user = _userManager.Get_User_By_Password_Token(passtoken);
                 user.New_Password = "";
                 user.Confirm_Password = "";
                 uViewModel = new UserViewModel();
                 uViewModel.User = user;
                 if (user.User_Id == 0)
                 {
                     TempData["FriendlyMessage"] = MessageStore.Get("SYS09");
                     return RedirectToAction("Index", "login");
                 }
            }
              
            return View(uViewModel);
        }

        public ActionResult Update_Password(UserViewModel uViewModel )
        {
            try
            {
                if (uViewModel.User.User_Id != 0)
                {

                    _userManager.Reset_Password(Utility.Encrypt(uViewModel.User.New_Password), uViewModel.User.User_Id,Utility.Generate_Token());
                    TempData["FriendlyMessage"] = MessageStore.Get("SYS05");
                    TempData["BrandName"] = uViewModel.User.Brand_Name;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error at Login Controller - Logout: " + ex.ToString());
            }
            return RedirectToAction("Index", "login");
        }

        public ActionResult Anauthorize_Token(LoginViewModel lViewModel)
        {
            lViewModel.Friendly_Message.Add(MessageStore.Get("SYS02"));
            return View("Index", lViewModel);
        }

        public ActionResult Anauthorize_Access(LoginViewModel lViewModel)
        {
            lViewModel.Friendly_Message.Add(MessageStore.Get("SYS02"));
            return View("Unauthorize_Access");
        }

        public ActionResult Marsedes(LoginViewModel lViewModel)
        
        {
            try
            {
                if (Request.Cookies["UserInfo"] != null)
                {
                    lViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");

                    if (lViewModel.Cookies == null)
                    {
                        lViewModel.Friendly_Message.Add(MessageStore.Get("SYS02"));
                    }
                }
                else
                {
                    if (TempData["FriendlyMessage"] != null)
                    {
                        lViewModel.Friendly_Message.Add((FriendlyMessage)TempData["FriendlyMessage"]);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error at Index : " + ex.Message);
                lViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                return View("Index", lViewModel);
            }

            return View("Index", lViewModel);
        }

        public ActionResult Renault(LoginViewModel lViewModel)
        {
            try
            {
                if (Request.Cookies["UserInfo"] != null)
                {
                    lViewModel.Cookies = Utility.Get_Login_User("UserInfo", "Token");

                    if (lViewModel.Cookies == null)
                    {
                        lViewModel.Friendly_Message.Add(MessageStore.Get("SYS02"));
                    }
                }
                else
                {
                    if (TempData["FriendlyMessage"] != null)
                    {
                        lViewModel.Friendly_Message.Add((FriendlyMessage)TempData["FriendlyMessage"]);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error at Index : " + ex.Message);
                lViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                return View("Index", lViewModel);
            }

            return View("RenaultIndex", lViewModel);
        }

    }
}
