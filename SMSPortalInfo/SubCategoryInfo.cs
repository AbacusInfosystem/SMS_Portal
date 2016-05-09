using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortalInfo
{
    public class SubCategoryInfo
    {
        public int Subcategory_Id { get; set; }

        public string Subcategory_Name { get; set; }

        public int Category_Id { get; set; }

        public string Category_Name { get; set; }

        public bool IsActive { get; set; }

        public string Status { get; set; }

        public int Created_By { get; set; }

        public int Updated_By { get; set; }        

        public DateTime Created_Date { get; set; }

        public DateTime Updated_Date { get; set; }

        public int Product_Count { get; set; }  
    }
}
