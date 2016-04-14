using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortalInfo
{
    public class DealerInfo
    {
        public int Dealer_Id { get; set; }

        public string Dealer_Name { get; set; }

        public int Brand_Id { get; set; }

        public string Brand_Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public int State { get; set; }

        public int Pincode { get; set; }

        public string Contact_No_1 { get; set; }

        public string Contact_No_2 { get; set; }

        public string Email { get; set; }

        public int Dealer_Percentage_Share { get; set; }

        public int Brand_Percentage_Share { get; set; }

        public bool Is_Active { get; set; }

        public DateTime Created_On { get; set; }

        public int Created_By { get; set; }

        public DateTime Updated_On { get; set; }

        public int Updated_By { get; set; }

    }
}
