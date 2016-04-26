﻿using SMSPortalHelper;
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

       public List<ReceivableInfo> Get_Receivable_By_Name(string Invoice_No, ref PaginationInfo pager)
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

           receivable.Invoice_Id = Convert.ToInt32(dr["Invoice_Id"]);
           receivable.Invoice_No = Convert.ToString(dr["Invoice_No"]);
           receivable.Status = Convert.ToInt32(dr["Status"]);
           receivable.Amount = Convert.ToDecimal(dr["Amount"]);
           receivable.TransactionType = Convert.ToString(dr["Transaction_Type"]);
           receivable.CheckNumber = Convert.ToString(dr["CheckNumber"]);
           //receivable.Created_On = Convert.ToDateTime(dr["Created_On"]);
           //receivable.Created_By = Convert.ToInt32(dr["Created_By"]);
           //receivable.Updated_On = Convert.ToDateTime(dr["Updated_On"]);
           //receivable.Updated_By = Convert.ToInt32(dr["Updated_By"]);

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
    }
}
