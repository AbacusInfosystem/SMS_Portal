using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMSPortalInfo;
using SMSPortalInfo.Common;
using System.Net;
using SMSPortal.Common;
using System.Web.Routing;

namespace SMSPortal
{
    public class AuthorizeUserAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public readonly AppFunction _appFunction;

        public CookiesInfo _cookies;

        public AuthorizeUserAttribute(AppFunction appFunction)
        {
            _appFunction = appFunction;

            _cookies=new CookiesInfo();
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            _cookies = Utility.Get_Login_User("UserInfo", "Token");

            //check if user contains specified access function

            //if (userInfo != null && userInfo.Roles.Count() != 0 && userInfo.Roles.Any(r => r.RoleName == _appFunction.ToString()))
            if (_cookies != null && _cookies.Access_Functions.Count() != 0 && _cookies.Access_Functions.Any(r => r.Access_Function_Name == _appFunction.ToString()))
            {
                // Log Activity.
            }
            else
            {

                filterContext.Result = new HttpUnauthorizedResult();

                /* check if request is ajax, then send unathorize status code.*/

                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    HttpContext.Current.Response.Clear();

                    HttpContext.Current.Response.StatusCode = Convert.ToInt32(HttpStatusCode.Unauthorized);
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Anauthorize_Access" }));
                   // filterContext.RequestContext.HttpContext.Response.Redirect(string.Format("/system/unauthorize-access?returnURL={0}", HttpContext.Current.Request.Url.AbsolutePath));
                }
            }

        }
    }
}