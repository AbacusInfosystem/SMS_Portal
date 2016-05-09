using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortalInfo
{
    public class PurchaseOrderInfo
    {
        public int Purchase_Order_Id { get; set; }

        public string Purchase_Order_No { get; set; }

        public int Vendor_Id { get; set; }

        public decimal Gross_Amount { get; set; }

        public string Vendor_Name { get; set; }

        public int Status { get; set; }

        public string Status_Text { get; set; }

        public DateTime Created_On { get; set; }

        public int Created_By { get; set; }

        public DateTime Updated_On { get; set; }

        public int Updated_By { get; set; }
    }
    public class PurchaseOrderItemInfo
    {
        public int Purchase_Order_Item_Id { get; set; }

        public int Purchase_Order_Id { get; set; }

        public int Product_Id { get; set; }

        public string Product_Name { get; set; }

        public int Product_Quantity { get; set; }

        public decimal Product_Unit_Price
        {
            get
            {
                if (Product_Price != 0)
                {
                    return Product_Price / Product_Quantity;
                }
                else
                {
                    return 0;
                }
            }

        }
        public decimal Product_Price { get; set; }

        public int Received_Quantity { get; set; }

        public int Balance_Quantity
        {
            get
            {
                if (Product_Quantity > 0)
                {
                    return Product_Quantity - Received_Quantity;
                }
                else 
                {
                    return 0;
                }
            }
        }

        public string Shipping_Address { get; set; }

        public DateTime Shipping_Date { get; set; }

        public int Status { get; set; }

        public string Status_Text { get; set; }

        public DateTime Created_On { get; set; }

        public int Created_By { get; set; }

        public DateTime Updated_On { get; set; }

        public int Updated_By { get; set; }        

    }
}
