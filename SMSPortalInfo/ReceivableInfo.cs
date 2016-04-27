using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortalInfo
{
    public class ReceivableInfo
    {
        public int  Receivable_Id{ get; set; }

        public string Invoice_No { get; set; }

        public int Invoice_Id { get; set; }

        public bool Is_Active { get; set; }

        public int TransactionType { get; set; }

        //public string CheckNumber { get; set; }

        public string ChequeDate { get; set; }

        public string ChequeNo { get; set; }

        public string BankName { get; set; }

        public string IFSC_Code { get; set; }

        public string NEFT { get; set; }

        public string Credit_Debit_Card { get; set; }

        public string ReceivableDate { get; set; }

        public DateTime Created_On { get; set; }

        public int Created_By { get; set; }

        public DateTime Updated_On { get; set; }

        public int Updated_By { get; set; }

        public int Status { get; set; }

        public decimal Amount { get; set; }
    }
}
