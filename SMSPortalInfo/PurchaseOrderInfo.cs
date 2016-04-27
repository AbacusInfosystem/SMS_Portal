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

        public DateTime Created_On { get; set; }

        public int Created_By { get; set; }

        public DateTime Updated_On { get; set; }

        public int Updated_By { get; set; }
    }
}
