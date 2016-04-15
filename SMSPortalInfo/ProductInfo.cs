using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortalInfo
{
    public class ProductInfo
    {
        public int Product_Id { get; set; }

        public string Product_Name { get; set; }

        public string Product_Description { get; set; }

        public decimal Product_Price { get; set; }

        public int Brand_Id { get; set; }

        public int Category_Id { get; set; }

        public int SubCategory_Id { get; set; }

        public bool Is_Biddable { get; set; }

        public bool Is_Active { get; set; }

        public DateTime Created_On { get; set; }

        public int Created_By { get; set; }

        public DateTime Updated_On { get; set; }

        public int Updated_By { get; set; }
    }
}
