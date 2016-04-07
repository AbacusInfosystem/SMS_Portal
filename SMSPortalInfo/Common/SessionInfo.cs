 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortalInfo.Common
{
    public class SessionInfo : UserInfo
    {
        public string Client_Id { get; set; }

        public string Language_Id { get; set; }

        public string System_Id { get; set; }

        //public bool Is_Admin {

        //    get
        //    {
        //        if (Role_Name == "Admin")
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    set
        //    {
        //        Is_Admin = value;

        //    }
        //}

        //public bool Is_Super_Admin {

        //    get 
        //    {
        //        if (Role_Name == "Super Admin")
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    set
        //    {
        //        Is_Super_Admin = value;
        //    }
        //}

        public List<RoleModuleMappingInfo> Role_Module_Mappings { get; set; }

        public SessionInfo ()
        {
            Role_Module_Mappings = new List<RoleModuleMappingInfo>();
        }
    }

    public class RoleModuleMappingInfo
    {
        public int Module_Id { get; set; }

        public string Module_Name { get; set; }

        public bool Is_Delete { get; set; }

        public bool Is_Approve { get; set; }

        public bool Is_Access { get; set; }
    }
}
