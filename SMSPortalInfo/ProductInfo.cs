using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace SMSPortalInfo
{
    public class ProductInfo
    {
        public int Product_Id { get; set; }

        public int Vendor_Id { get; set; }

        public string Product_Name { get; set; }

        public string Product_Description { get; set; }

        public decimal Product_Price { get; set; }

        public decimal MasterProductPrice { get; set; }

        public int Brand_Id { get; set; }

        public string Brand_Name { get; set; }

        public int Category_Id { get; set; }

        public string Category_Name { get; set; }

        public int SubCategory_Id { get; set; }

        public string SubCategory_Name { get; set; }

        public bool Is_Biddable { get; set; }

        public bool Is_Active { get; set; }

        public DateTime Created_On { get; set; }

        public int Created_By { get; set; }

        public DateTime Updated_On { get; set; }

        public int Updated_By { get; set; }

        public List<ProductImageInfo> ProductImages { get; set; }

        public bool Is_Mapped { get; set; }


        public string Product_Image { get; set; }

        public bool Check { get; set; }

        public string Product_Ids { get; set; }

        //public string Product_Prices { get; set; }
        
    }

    public class ProductImageInfo
    {
        public int Product_Image_Id { get; set; }

        public int Product_Id { get; set; }

        public string Image_Code { get; set; }

        public bool Is_Default { get; set; }

        public DateTime Created_On { get; set; }

        public int Created_By { get; set; }

        public DateTime Updated_On { get; set; }

        public int Updated_By { get; set; }

        public HttpPostedFileBase File { get; set; }

    }
}
