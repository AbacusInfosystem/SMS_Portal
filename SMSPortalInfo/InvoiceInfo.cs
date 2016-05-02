using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortalInfo
{
    public class InvoiceInfo
    {
        public int Invoice_Id { get; set; }

        public int Order_Id { get; set; }

        public string Order_No { get; set; }

        public string Invoice_No { get; set; }

        public DateTime Invoice_Date { get; set; }

        public DateTime Created_On { get; set; }

        public int Created_By { get; set; }

        public DateTime Updated_On { get; set; }

        public int Updated_By { get; set; }
    }
}
