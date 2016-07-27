using SMSPortalInfo.Common;
using SMSPortalManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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

        public static string Generate_Ven_Ref_No(string initialCharacter, string columnName, string substringStartIndex, string substringEndIndex, string tableName,int vendor_Id)
        {
            PurchaseOrderManager _purchaseOrderManager = new PurchaseOrderManager();
            return _purchaseOrderManager.Generate_Ven_Ref_No(initialCharacter, columnName, substringStartIndex, substringEndIndex, tableName, vendor_Id);
        }

        public static string Generate_Token()
        {
            return Guid.NewGuid().ToString();
        }

        public static string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
    }
}