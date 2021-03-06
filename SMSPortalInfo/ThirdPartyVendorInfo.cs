﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMSPortalRepo;

namespace SMSPortalInfo
{
    public class ThirdPartyVendorInfo
    {
        public int Vendor_Id { get; set; }

        public int Brand_Id { get; set; }

        public string Vendor_Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public int State { get; set; }

        public int Pincode { get; set; }

        public string Contact_No_1 { get; set; }

        public string Contact_No_2 { get; set; }

        public string Email { get; set; }

        public string Invoice_No { get; set; }

        public int Invoice_Id { get; set; }

        public bool Is_Active { get; set; }     

        public DateTime Created_On { get; set; }

        public int Created_By { get; set; }

        public DateTime Updated_On { get; set; }

        public int Updated_By { get; set; }

        public List<Bank_Details> BankDetailsList { get; set; }

        public StateInfo stateInfo { get; set; }

        public ThirdPartyVendorInfo()
        {
            BankDetailsList = new List<Bank_Details>();

            stateInfo = new StateInfo();
        }

    }

    public class Bank_Details
    {
        public int Vendor_Bank_Detail_Id { get; set; }

        public string Bank_Name { get; set; }

        public string Account_No { get; set; }

        public string Ifsc_Code { get; set; }

        public bool Status { get; set; }
    }


}
