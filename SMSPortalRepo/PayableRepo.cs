using SMSPortalHelper;
using SMSPortalInfo;
using SMSPortalInfo.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortalRepo
{
    public class PayableRepo
    {

        SQLHelper _sqlHelper;

        public PayableRepo()
        {
            _sqlHelper = new SQLHelper();
        }

        public void Insert_Payable(PayableInfo payableInfo, int user_Id)
        {
            //int Payable_Id = 0;
             _sqlHelper.ExecuteScalerObj(Set_Values_In_Payable(payableInfo, user_Id), StoreProcedures.Insert_Payable_Item_Data_Sp.ToString(), CommandType.StoredProcedure);
            //return Payable_Id;
        }

        private List<SqlParameter> Set_Values_In_Payable(PayableInfo payableInfo, int user_Id)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            if (payableInfo.Payable_Item_Id != 0)
            {
                sqlParams.Add(new SqlParameter("@Payable_Item_Id", payableInfo.Payable_Item_Id));
            }

            sqlParams.Add(new SqlParameter("@Invoice_Id", payableInfo.Invoice_Id));
            sqlParams.Add(new SqlParameter("@TransactionType", payableInfo.Transaction_Type));
            sqlParams.Add(new SqlParameter("@ReceivableDate", payableInfo.Payable_Date));
            sqlParams.Add(new SqlParameter("@Invoice_No", payableInfo.Invoice_No));


            if (payableInfo.Transaction_Type == 1)
            {
                sqlParams.Add(new SqlParameter("@ChequeNo", payableInfo.Cheque_Number));
                sqlParams.Add(new SqlParameter("@ChequeDate", payableInfo.Cheque_Date));
                sqlParams.Add(new SqlParameter("@BankName", payableInfo.Bank_Name));
                sqlParams.Add(new SqlParameter("@IFSC_Code", payableInfo.IFSC_Code));
                sqlParams.Add(new SqlParameter("@NEFT", "NA"));
                sqlParams.Add(new SqlParameter("@Credit_Debit", "NA"));
            }
            else if (payableInfo.Transaction_Type == 2)
            {
                sqlParams.Add(new SqlParameter("@ChequeNo", "NA"));
                sqlParams.Add(new SqlParameter("@ChequeDate", DateTime.Now));
                sqlParams.Add(new SqlParameter("@BankName", "NA"));
                sqlParams.Add(new SqlParameter("@IFSC_Code", "NA"));
                sqlParams.Add(new SqlParameter("@NEFT", payableInfo.NEFT));
                sqlParams.Add(new SqlParameter("@Credit_Debit", "NA"));
            }
            else
            {
                sqlParams.Add(new SqlParameter("@ChequeNo", "NA"));
                sqlParams.Add(new SqlParameter("@ChequeDate", DateTime.Now));
                sqlParams.Add(new SqlParameter("@BankName", "NA"));
                sqlParams.Add(new SqlParameter("@IFSC_Code", "NA"));
                sqlParams.Add(new SqlParameter("@NEFT", "NA"));
                sqlParams.Add(new SqlParameter("@Credit_Debit", payableInfo.Credit_Debit_Card));
            }

            payableInfo.Balance_Amount = payableInfo.Invoice_Amount - payableInfo.Payable_Item_Amount;

            if (payableInfo.Payable_Id == 0)
            {
                sqlParams.Add(new SqlParameter("@Created_On", DateTime.Now));
                sqlParams.Add(new SqlParameter("@Created_By", user_Id));
            }

            sqlParams.Add(new SqlParameter("@Updated_On", DateTime.Now));
            sqlParams.Add(new SqlParameter("@Updated_By", user_Id));

            return sqlParams;
        }
    }
}
