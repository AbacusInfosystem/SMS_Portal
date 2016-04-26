using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortalInfo
{
    public class ReceivableInfo
    {
        public string Invoice_No { get; set; }

        public int Invoice_Id { get; set; }

        public bool Is_Active { get; set; }

        public string TransactionType { get; set; }

        public string CheckNumber { get; set; }

        public DateTime Created_On { get; set; }

        public int Created_By { get; set; }

        public DateTime Updated_On { get; set; }

        public int Updated_By { get; set; }

        public int Status { get; set; }

        public decimal Amount { get; set; }
    }
}
