using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMSPortal.Common
{
    public class CustomRazorViewEngine : RazorViewEngine
    {
        public CustomRazorViewEngine()
            : base()
        {
            

            ViewLocationFormats = new[] { 

                //This would search for view in
                //Views/PostLogin/<calling controllers name>/<view name>.cshtml
                "~/Views/PostLogin/{1}/{0}.cshtml", 

                //This would search for view in
                //Views/PostLogin/Home/<calling controllers name>/<view name>.cshtml
               //"~/Views/PostLogin/Home/{1}/{0}.cshtml", 

                //This would search for view in
                //Views/PostLogin/Masters/<calling controllers name>/<view name>.cshtml
                //"~/Views/PostLogin/Master/{1}/{0}.cshtml", 

                //This would search for view in
                //Views/PreLogin/<calling controllers name>/<view name>.cshtml
                "~/Views/PreLogin/{1}/{0}.cshtml",




              

                "~/Views/Shared/{0}.cshtml",
            };

            this.ViewLocationFormats = ViewLocationFormats;
            this.PartialViewLocationFormats = ViewLocationFormats;
            this.MasterLocationFormats = ViewLocationFormats;
        }
    }
}