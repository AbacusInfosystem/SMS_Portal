﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortalInfo
{
   public class PayableInfo
    {
        public int  Payable_Id{ get; set; }

        public int Payable_Item_Id { get; set; }

        public string Purchase_Order_No { get; set; }

        public int Purchase_Order_Id { get; set; }

        public bool Is_Active { get; set; }

        public int Transaction_Type { get; set; }

        public DateTime Cheque_Date { get; set; }

        public string Cheque_Number { get; set; }

        public string Bank_Name { get; set; }

        public string IFSC_Code { get; set; }

        public string NEFT { get; set; }

        public string Credit_Debit_Card { get; set; }

        public DateTime Payable_Date { get; set; }

        public DateTime Created_On { get; set; }

        public int Created_By { get; set; }

        public DateTime Updated_On { get; set; }

        public int Updated_By { get; set; }

        public string Status { get; set; }

        public decimal Balance_Amount { get; set; }

        public decimal Payable_Item_Amount { get; set; }

        public decimal Purchase_Order_Amount { get; set; }

        public string Transaction_Type_Name { get; set; }

        public string Vendor_Name { get; set; }

        public DateTime Purchase_Order_Date { get; set; }

    }
}
