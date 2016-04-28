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
   public class ReceivableRepo
    {
       SQLHelper _sqlRepo;

       public ReceivableRepo()
       {
           _sqlRepo = new SQLHelper();
       }

       public List<ReceivableInfo> Get_Receivable_By_Id(string Invoice_No, ref PaginationInfo pager)
       {
           List<SqlParameter> sqlParamList = new List<SqlParameter>();
           sqlParamList.Add(new SqlParameter("@Invoice_No", Invoice_No));

           List<ReceivableInfo> Receivables = new List<ReceivableInfo>();
           DataTable dt = _sqlRepo.ExecuteDataTable(sqlParamList, StoreProcedures.Get_Receivable_By_Name_Sp.ToString(), CommandType.StoredProcedure);
           foreach (DataRow dr in CommonMethods.GetRows(dt, ref pager))
           {
               Receivables.Add(Get_Receivable_Values(dr));
           }
           return Receivables;
       }

       private ReceivableInfo Get_Receivable_Values(DataRow dr)
       {
           ReceivableInfo receivable = new ReceivableInfo();

           receivable.Receivable_Id = Convert.ToInt32(dr["Receivable_Id"]);
           receivable.Invoice_Id = Convert.ToInt32(dr["Invoice_Id"]);
           receivable.Invoice_No = Convert.ToString(dr["Invoice_No"]);
           receivable.Role_Id = Convert.ToInt32(dr["Roll_Id"]);
           receivable.Status = Convert.ToBoolean(dr["Status"]);
           receivable.Invoice_Amount = Convert.ToInt32(dr["Amount"]);
           receivable.Receivable_Item_Id = Convert.ToInt32(dr["Receivable_Item_Id"]);
           receivable.Receivable_Date = Convert.ToDateTime(dr["Receivable_Date"]);
           receivable.Receivable_Item_Amount = Convert.ToDecimal(dr["Receivable_Item_Amount"]);
           receivable.Transaction_Type = Convert.ToInt32(dr["Transaction_Type"]);
           receivable.Cheque_Number = Convert.ToString(dr["Cheque_Number"]);
           receivable.Cheque_Date = Convert.ToString(dr["Cheque_Date"]);
           receivable.IFSC_Code = Convert.ToString(dr["IFSC_Code"]);
           receivable.Bank_Name = Convert.ToString(dr["Bank_Name"]);
           receivable.NEFT = Convert.ToString(dr["NEFT"]);
           receivable.Credit_Debit_Card = Convert.ToString(dr["Credit_Debit_Card"]);

           return receivable;
       }

       public List<ReceivableInfo> Get_Receivables(ref PaginationInfo pager)
       {
           List<ReceivableInfo> Receivables = new List<ReceivableInfo>();
           DataTable dt = _sqlRepo.ExecuteDataTable(null, StoreProcedures.Get_Receivable_Sp.ToString(), CommandType.StoredProcedure);
           foreach (DataRow dr in CommonMethods.GetRows(dt, ref pager))
           {
               Receivables.Add(Get_Receivable_Values(dr));
           }
           return Receivables;
       }

       public List<AutocompleteInfo> Load_Receivable_InvoiceNo(string txtInvoice_No)
       { 
            List<AutocompleteInfo> Invoice_No = new List<AutocompleteInfo>();

            List<SqlParameter> sqlParams = new List<SqlParameter>();

            sqlParams.Add(new SqlParameter("@Invoice_No", txtInvoice_No));

            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParams, StoreProcedures.Get_InvoiceNo_AutoComplete_Sp.ToString(), CommandType.StoredProcedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                List<DataRow> drList = new List<DataRow>();

                drList = dt.AsEnumerable().ToList();

                foreach (DataRow dr in drList)
                {
                    AutocompleteInfo auto = new AutocompleteInfo();

                    auto.Label = Convert.ToString(dr["Label"]);

                    auto.Value = Convert.ToInt32(dr["Value"]);

                    Invoice_No.Add(auto);
                }
            }

            return Invoice_No;
        
       }

       public List<ReceivableInfo> Get_InvoiceNo()
       {
           List<ReceivableInfo> Receivables = new List<ReceivableInfo>();
           DataTable dt = _sqlRepo.ExecuteDataTable(null, StoreProcedures.Get_InvoiceNo_Sp.ToString(), CommandType.StoredProcedure);

           if (dt != null && dt.Rows.Count > 0)
           {
               foreach (DataRow dr in dt.Rows)
               {
                   ReceivableInfo list = new ReceivableInfo();

                   if (!dr.IsNull("Invoice_Id"))
                       list.Invoice_Id = Convert.ToInt32(dr["Invoice_Id"]);
                   if (!dr.IsNull("Invoice_No"))
                       list.Invoice_No = Convert.ToString(dr["Invoice_No"]);

                   Receivables.Add(list);
               }
           }

           return Receivables;
       }

       public int Insert_Receivable(ReceivableInfo receivableInfo,int user_Id)
       {
           int Receivable_Id = 0;
           Receivable_Id=Convert.ToInt32(_sqlRepo.ExecuteScalerObj(Set_Values_In_Receivable(receivableInfo, user_Id), StoreProcedures.Insert_Receivable_Data_Sp.ToString(), CommandType.StoredProcedure));
           return Receivable_Id;
       }

       private List<SqlParameter> Set_Values_In_Receivable(ReceivableInfo receivableInfo, int user_Id)
       {
           List<SqlParameter> sqlParams = new List<SqlParameter>();

           if (receivableInfo.Receivable_Id != 0)
           {
               sqlParams.Add(new SqlParameter("@Receivable_Id", receivableInfo.Receivable_Id));
           }

           sqlParams.Add(new SqlParameter("@Invoice_Id", receivableInfo.Invoice_Id));
           sqlParams.Add(new SqlParameter("@TransactionType", receivableInfo.Transaction_Type));
           sqlParams.Add(new SqlParameter("@ReceivableDate", receivableInfo.Receivable_Date));

           if (receivableInfo.Transaction_Type == 1)
           {
               sqlParams.Add(new SqlParameter("@ChequeNo", receivableInfo.Cheque_Number));
               sqlParams.Add(new SqlParameter("@ChequeDate", receivableInfo.Cheque_Date));
               sqlParams.Add(new SqlParameter("@BankName", receivableInfo.Bank_Name));
               sqlParams.Add(new SqlParameter("@IFSC_Code", receivableInfo.IFSC_Code));
               sqlParams.Add(new SqlParameter("@NEFT", "NA"));
               sqlParams.Add(new SqlParameter("@Credit_Debit", "NA"));
           }
           else if (receivableInfo.Transaction_Type == 2)
           {
               sqlParams.Add(new SqlParameter("@ChequeNo", "NA"));
               sqlParams.Add(new SqlParameter("@ChequeDate", "NA"));
               sqlParams.Add(new SqlParameter("@BankName", "NA"));
               sqlParams.Add(new SqlParameter("@IFSC_Code", "NA"));
               sqlParams.Add(new SqlParameter("@NEFT", receivableInfo.NEFT));
               sqlParams.Add(new SqlParameter("@Credit_Debit", "NA"));
           }
           else
           {
               sqlParams.Add(new SqlParameter("@ChequeNo", "NA"));
               sqlParams.Add(new SqlParameter("@ChequeDate", "NA"));
               sqlParams.Add(new SqlParameter("@BankName", "NA"));
               sqlParams.Add(new SqlParameter("@IFSC_Code", "NA"));
               sqlParams.Add(new SqlParameter("@NEFT","NA"));
               sqlParams.Add(new SqlParameter("@Credit_Debit", receivableInfo.Credit_Debit_Card));
           }

           receivableInfo.Balance_Amount = receivableInfo.Invoice_Amount - receivableInfo.Receivable_Item_Amount;

           if (receivableInfo.Receivable_Id == 0)
           {
               sqlParams.Add(new SqlParameter("@Created_On", receivableInfo.Created_On));
               sqlParams.Add(new SqlParameter("@Created_By", receivableInfo.Created_By));
           }

           sqlParams.Add(new SqlParameter("@Updated_On", receivableInfo.Updated_On));
           sqlParams.Add(new SqlParameter("@Updated_By", receivableInfo.Updated_By));

           return sqlParams;
       }
    }
}
