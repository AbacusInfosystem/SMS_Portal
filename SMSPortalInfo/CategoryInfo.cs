﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortalInfo
{
    public class CategoryInfo
    {
        public int Category_Id { get; set; }

        public string Category_Name { get; set; }

        public bool IsActive { get; set; }

        public DateTime Created_On { get; set; }

        public int Created_By { get; set; }

        public DateTime Updated_On { get; set; }

        public int Updated_By { get; set; }

    }
}
