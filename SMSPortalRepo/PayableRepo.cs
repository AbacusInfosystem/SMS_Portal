using SMSPortalHelper;
using SMSPortalInfo;
using SMSPortalInfo.Common;
using SMSPortalRepo.Common;
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

        public int Insert_Payable(PayableInfo payableInfo, int user_Id)
        {
            int Payable_Id = 0;
            Payable_Id = Convert.ToInt32(_sqlHelper.ExecuteScalerObj(Set_Values_In_Payable(payableInfo, user_Id), StoreProcedures.Insert_Payable_Data_Sp.ToString(), CommandType.StoredProcedure));

            if (payableInfo.Payable_Item_Id != 0)
            {
                Payable_Id = payableInfo.Payable_Item_Id;
            }

            return Payable_Id;
        }

        public void Insert_PayableItems(PayableInfo payableInfo, int user_Id)
        {
            //int Payable_Id = 0;
            _sqlHelper.ExecuteScalerObj(Set_Values_In_Payable_Items(payableInfo, user_Id), StoreProcedures.Insert_Payable_Item_Data_Sp.ToString(), CommandType.StoredProcedure);
            //return Payable_Id;
        }

        private List<SqlParameter> Set_Values_In_Payable(PayableInfo payableInfo, int user_Id)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            sqlParams.Add(new SqlParameter("@Payable_Id", payableInfo.Payable_Id));

            sqlParams.Add(new SqlParameter("@Invoice_Id", payableInfo.Invoice_Id));
            sqlParams.Add(new SqlParameter("@Amount", payableInfo.Invoice_Amount));

            payableInfo.Balance_Amount = payableInfo.Invoice_Amount - payableInfo.Payable_Item_Amount;

            if (payableInfo.Balance_Amount != 0)
            {
                sqlParams.Add(new SqlParameter("@Status", "Partially Paid"));
            }
            else
            {
                sqlParams.Add(new SqlParameter("@Status", "Payment Completed"));
            }


            sqlParams.Add(new SqlParameter("@Created_On", DateTime.Now));
            sqlParams.Add(new SqlParameter("@Created_By", payableInfo.Created_By));

            sqlParams.Add(new SqlParameter("@Updated_On", DateTime.Now));
            sqlParams.Add(new SqlParameter("@Updated_By", payableInfo.Updated_By));

            return sqlParams;
        }

        private List<SqlParameter> Set_Values_In_Payable_Items(PayableInfo payableInfo, int user_Id)
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

        public PayableInfo Get_Payable_Data_By_Id(int payable_Id)
        {
            PayableInfo payable = new PayableInfo();

            List<SqlParameter> sqlparam = new List<SqlParameter>();

            sqlparam.Add(new SqlParameter("@Payable_Id", payable_Id));

            DataTable dt = _sqlHelper.ExecuteDataTable(sqlparam, StoreProcedures.Get_Receivable_Data_By_Id_Sp.ToString(), CommandType.StoredProcedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (!dr.IsNull("Payable_Id"))
                        payable.Payable_Id = Convert.ToInt32(dr["Payable_Id"]);
                    if (!dr.IsNull("Status"))
                        payable.Status = Convert.ToString(dr["Status"]);
                    if (!dr.IsNull("Amount"))
                        payable.Invoice_Amount = Convert.ToDecimal(dr["Amount"]);
                    if (!dr.IsNull("Invoice_No"))
                        payable.Invoice_No = Convert.ToString(dr["Invoice_No"]);
                    if (!dr.IsNull("Invoice_Id"))
                        payable.Invoice_Id = Convert.ToInt32(dr["Invoice_Id"]);
                }
            }
            return payable;
        }

        //public List<PayableInfo> Get_Receivable_Items_By_Id(int payable_Id)
        //{
        //    List<PayableInfo> Payables = new List<PayableInfo>();

        //    List<SqlParameter> sqlParamList = new List<SqlParameter>();

        //    sqlParamList.Add(new SqlParameter("@payable_Id", payable_Id));

        //    DataTable dt = _sqlHelper.ExecuteDataTable(sqlParamList, StoreProcedures.Get_Receivable_Data_Item_By_Id_Sp.ToString(), CommandType.StoredProcedure);

        //    foreach (DataRow dr in CommonMethods.GetRows(dt))
        //    {
        //        Payables.Add(Get_Payable_Item_Values(dr));
        //    }
        //    return Payables;
        //}

    }
}
