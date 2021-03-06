﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortalInfo
{
  
    public class UsersInfo
    {
        public int User_Id { get; set; }

        public string First_Name { get; set; }

        public string Last_Name { get; set; }

        public string Contact_No_1 { get; set; }

        public string Contact_No_2 { get; set; }

        public int Gender { get; set; }

        public string User_Name { get; set; }

        public string Password { get; set; }

        public int Role_Id { get; set; }

        public bool Is_Active { get; set; }

        public DateTime Created_On { get; set; }

        public int Created_By { get; set; }

        public DateTime Updated_On { get; set; }

        public int Updated_By { get; set; }

    }
}
