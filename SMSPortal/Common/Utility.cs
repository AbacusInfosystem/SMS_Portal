using SMSPortalInfo.Common;
using SMSPortalManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMSPortal.Common
{
    public static class Utility
    {
        public static string GetCookie(string cookieName, string key)
        {
            return System.Web.HttpContext.Current.Request.Cookies[cookieName][key];
        }

        public static CookiesInfo Get_Login_User(string cookieName, string key)
        {
            string token = System.Web.HttpContext.Current.Request.Cookies[cookieName][key];

            CookiesManager _cookiesManager = new CookiesManager();

            return _cookiesManager.Get_Token_Data(token); 
        }
    }
}