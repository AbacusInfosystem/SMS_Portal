using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortalInfo
{
    public class BrandInfo
    {
        public int Brand_Id { get; set; }

        public string Brand_Name { get; set; }

        public int Brand_Category { get; set; }

        public string Brand_Category_Name { get; set; }

        public string Brand_Logo { get; set; }

        public string Website_Url { get; set; }

        public bool Is_Active { get; set; }

        public DateTime Created_On { get; set; }

        public int Created_By { get; set; }

        public DateTime Updated_On { get; set; }

        public int Updated_By { get; set; }
    }
}
