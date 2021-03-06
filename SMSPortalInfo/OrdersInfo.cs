﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortalInfo
{
    public class OrdersInfo
    {
        public int Order_Id { get; set; }

        public int Vendor_Order_Id { get; set; }

        public string Order_No { get; set; }

        public int Dealer_Id { get; set; }

        public string Dealer_Name { get; set; }

        public int Vendor_Id { get; set; }

        public string Vendor_Name { get; set; }

        public DateTime Order_Date { get; set; }

        public string Shipping_Address { get; set; }

        public decimal Discount { get; set; }

        public decimal Gross_Amount { get; set; }

        public decimal Service_Tax { get; set; }

        public decimal Vat { get; set; }

        public decimal Swatch_Bharat_Tax { get; set; }

        public decimal Net_Amount { get; set; }

        public string Status { get; set; }

        public int Status_Id { get; set; }

        public int Invoice_Id { get; set; }

        public string Invoice_No { get; set; }

        public DateTime Shipping_Date { get; set; }

        public DateTime Created_On { get; set; }

        public int Created_By { get; set; }

        public DateTime Updated_On { get; set; }

        public int Updated_By { get; set; }

        public List<OrderItemInfo> OrderItems { get; set; }
    }

    public class OrderItemInfo
    {
        public int Order_Item_Id { get; set; }

        public int Order_Id { get; set; }

        public int Product_Id { get; set; }

        public string Product_Name { get; set; }

        public int Product_Quantity { get; set; }

        public decimal Product_Price { get; set; }

        public string Order_Status { get; set; }

        public DateTime Created_On { get; set; }

        public int Created_By { get; set; }

        public DateTime Updated_On { get; set; }

        public int Updated_By { get; set; }

        public ProductInfo Product { get; set; }

        public int Order_Vendor_Id { get; set; }

        public int Vendor_Id { get; set; }

        public decimal Product_NetPrice { get; set; }

        public decimal Product_GrossPrice { get; set; }

        public decimal Local_Tax { get; set; }

        public decimal Export_Tax { get; set; }

    }

}
