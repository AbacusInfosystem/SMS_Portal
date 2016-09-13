using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortalInfo.Common
{
    public class CookiesInfo
    {
        public CookiesInfo()
        {
            Access_Functions = new List<AccessFunctionInfo>();
        }
        public List<AccessFunctionInfo> Access_Functions { get; set; }

        public int User_Id { get; set; }

        public string User_Name { get; set; }

        public string Password { get; set; }

        public int Role_Id { get; set; }

        public string Role_Name { get; set; }

        public bool Is_Active { get; set; }

        public string First_Name { get; set; }

        public string Last_Name { get; set; }

        public string User_Email { get; set; }

        public int Entity_Id { get; set; }

        public string Order_Status { get; set; }

        public string Brand_Name { get; set; }

        public int Brand_Id { get; set; }
    }
}
