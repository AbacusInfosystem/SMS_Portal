using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortalInfo
{
    public class VendorInfo
    {
        public int Vendor_Id { get; set; }

        public string Vendor_Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public int State { get; set; }

        public int Pincode { get; set; }

        public string Contact_No_1 { get; set; }

        public string Contact_No_2 { get; set; }

        public string Email { get; set; }

        public bool Is_Active { get; set; }

        public DateTime Created_On { get; set; }

        public int Created_By { get; set; }

        public DateTime Updated_On { get; set; }

        public int Updated_By { get; set; }

        public List<Bank_Details> BankDetailsList { get; set; }

        public VendorInfo()
        {
            BankDetailsList = new List<Bank_Details>();
        }

    }

    public class Bank_Details
    {
        public string Bank_Name { get; set; }

        public string Account_No { get; set; }

        public string Ifsc_Code { get; set; }

        public bool Status { get; set; }
    }


}
