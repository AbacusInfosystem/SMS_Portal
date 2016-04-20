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
            catch(Exception ex)
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
                SessionInfo session = userManager.AuthenticateUser(loginViewModel.Session.User_Name, loginViewModel.Session.Password);               
                
                if (session.User_Id != 0 && session.Is_Active == true)
                {
                    if (session.User_Name == loginViewModel.Session.User_Name)
                    {
                        SetUsersSession(session);

                        SetUsersCookies(loginViewModel.Session.User_Name, loginViewModel.Session.Password);
                    }
                    if (Session["returnURL"] != null && !string.IsNullOrEmpty(Session["returnURL"].ToString()))
                    {
                        string returnURL = Session["returnURL"].ToString();

                        Session.Remove("returnURL");

                        Response.Redirect(returnURL);
                    }

                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    if (session.User_Id != 0 && session.Is_Active == false)
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
                HttpContext.Session.Clear();

                loginViewModel.Friendly_Message.Add(MessageStore.Get("SYS01"));
                return RedirectToAction("Index", "Login", loginViewModel);
            }
        }
                 
        private void SetUsersSession(SessionInfo session)
        {
            try
            {
                if (HttpContext.Session["SessionInfo"] == null)
                {
                    HttpContext.Session.Add("SessionInfo", session);
                }
                else
                {
                    HttpContext.Session["SessionInfo"] = session;
                }
            }
            catch (Exception ex)
            {
                HttpContext.Session.Clear();
            }
        }

        private void SetUsersCookies(string userName,string password)
        {
            try
            {
                if (Request.Cookies["UserInfo"] == null)
                {
                    HttpCookie cookies = new HttpCookie("UserInfo");

                    string cookie_Token = userManager.Set_User_Token_For_Cookies(userName, password);
                    cookies.Values.Add("Token", cookie_Token);

                    cookies.Expires = DateTime.Now.AddMinutes(2);

                    Response.Cookies.Add(cookies);
                }
            }
            catch (Exception ex)
            {
                HttpContext.Request.Cookies.Clear();
            }
        }

        public ActionResult Logout(string timeOut)
        {
            LoginViewModel loginViewModel = new LoginViewModel();
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
                //Logger.Error("HomeController - Logout: " + ex.ToString());
            }

            return RedirectToAction("Index", "login");
        }

        private void LogoutUser()
        {
            Session["SessionInfo"] = null;

            Session["ReturnURL"] = null;

            ClearExcpetionData();

            //FormsAuthentication.SignOut();

            Response.Cookies["UserInfo"].Expires = DateTime.Now.AddYears(-1);

            Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);

            Response.Expires = -1500;

            Response.CacheControl = "no-cache";

            Response.AddHeader("Cache-Control", "no-cache");

            Response.Cache.SetNoStore();

            Response.AddHeader("Pragma", "no-cache");

            //  Response.Redirect("/");
        }

        public void ClearExcpetionData()
        {
            try
            {
                string strFileName = Server.MapPath(ConfigurationManager.AppSettings["ErrorFilePath"].ToString());
                List<string> lines = System.IO.File.ReadAllLines(strFileName).ToList();

                System.IO.File.WriteAllLines(strFileName, lines.Take(1));
            }
            catch (Exception ex)
            {
                Logger.Error("Error At ClearExcpetionData : " + ex);
            }
        }

    }
}
