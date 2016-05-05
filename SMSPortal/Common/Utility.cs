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

        public static string Generate_Ref_No(string initialCharacter, string columnName, string substringStartIndex, string substringEndIndex, string tableName)
        {
            PurchaseOrderManager _purchaseOrderManager = new PurchaseOrderManager();
            return _purchaseOrderManager.Generate_Ref_No(initialCharacter, columnName, substringStartIndex, substringEndIndex, tableName);
        }

        public static string Generate_Token()
        {
            return Guid.NewGuid().ToString();
        }
    }
}