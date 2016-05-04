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
    public class PayableRepo
    {

        SQLHelper _sqlHelper;

        public PayableRepo()
        {
            _sqlHelper = new SQLHelper();
        }

        public List<PayableInfo> Get_Payable_By_Id(int Purchase_Order_Id, ref PaginationInfo pager)
        {
            List<SqlParameter> sqlParamList = new List<SqlParameter>();
            sqlParamList.Add(new SqlParameter("@Purchase_Order_Id", Purchase_Order_Id));

            List<PayableInfo> Payables = new List<PayableInfo>();
            DataTable dt = _sqlHelper.ExecuteDataTable(sqlParamList, StoreProcedures.Get_Payable_By_Name_Sp.ToString(), CommandType.StoredProcedure);
            foreach (DataRow dr in CommonMethods.GetRows(dt, ref pager))
            {
                Payables.Add(Get_Payable_Values(dr));
            }
            return Payables;
        }

        private PayableInfo Get_Payable_Values(DataRow dr)
        {
            PayableInfo payable = new PayableInfo();

            payable.Payable_Id = Convert.ToInt32(dr["Payable_Id"]);
            payable.Status = Convert.ToString(dr["Status"]);
            payable.Purchase_Order_Amount = Convert.ToDecimal(dr["Amount"]);
            payable.Purchase_Order_No = Convert.ToString(dr["Purchase_Order_No"]);
            payable.Purchase_Order_Id = Convert.ToInt32(dr["Purchase_Order_Id"]);

            return payable;
        }

        public List<PayableInfo> Get_Payables(ref PaginationInfo pager)
        {
            List<PayableInfo> payables = new List<PayableInfo>();
            DataTable dt = _sqlHelper.ExecuteDataTable(null, StoreProcedures.Get_Payable_Sp.ToString(), CommandType.StoredProcedure);
            foreach (DataRow dr in CommonMethods.GetRows(dt, ref pager))
            {
                payables.Add(Get_Payable_Values(dr));
            }
            return payables;
        }

        public decimal Get_Balance_Amount(int payable_Id)
        {
            decimal Balance_Amount = 0;

            List<SqlParameter> sqlParams = new List<SqlParameter>();

            sqlParams.Add(new SqlParameter("@Payable_Id", payable_Id));

            DataTable dt = _sqlHelper.ExecuteDataTable(sqlParams, StoreProcedures.Get_Payable_Balance_Amount_By_Id_Sp.ToString(), CommandType.StoredProcedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (!dr.IsNull("Balance_Amount"))
                        Balance_Amount = Convert.ToDecimal(dr["Balance_Amount"]);
                }
            }

            return Balance_Amount;
        }

        public int Insert_Payable(PayableInfo payableInfo, int user_Id)
        {
            int Payable_Id = 0;
            Payable_Id = Convert.ToInt32(_sqlHelper.ExecuteScalerObj(Set_Values_In_Payable(payableInfo, user_Id), StoreProcedures.Insert_Payable_Data_Sp.ToString(), CommandType.StoredProcedure));

            if (payableInfo.Payable_Item_Id != 0)
            {
                Payable_Id = payableInfo.Payable_Id;
            }

            return Payable_Id;
        }

        public void Insert_PayableItems(PayableInfo payableInfo, int user_Id)
        {
           
            _sqlHelper.ExecuteScalerObj(Set_Values_In_Payable_Items(payableInfo, user_Id), StoreProcedures.Insert_Payable_Item_Data_Sp.ToString(), CommandType.StoredProcedure);
           
        }

        private List<SqlParameter> Set_Values_In_Payable(PayableInfo payableInfo, int user_Id)
        {
            decimal Total_Balance_Amount = 0;
            decimal Amount = 0;

            List<SqlParameter> sqlParams = new List<SqlParameter>();

            decimal Balance_Amount = Get_Balance_Amount(payableInfo.Payable_Id);

            sqlParams.Add(new SqlParameter("@Payable_Id", payableInfo.Payable_Id));

            sqlParams.Add(new SqlParameter("@Purchase_Order_Id", payableInfo.Purchase_Order_Id));
            sqlParams.Add(new SqlParameter("@Amount", payableInfo.Purchase_Order_Amount));

            if (Balance_Amount > 0)
            {
                Total_Balance_Amount = Balance_Amount - payableInfo.Payable_Item_Amount;
            }
            else
            {
                Total_Balance_Amount = payableInfo.Payable_Item_Amount;
            }

            Amount = payableInfo.Purchase_Order_Amount - Balance_Amount;

            decimal Total_Amount = (payableInfo.Purchase_Order_Amount - (Amount + payableInfo.Payable_Item_Amount));



            payableInfo.Balance_Amount = (Total_Amount - Total_Balance_Amount);

            sqlParams.Add(new SqlParameter("@Balance_Amount", payableInfo.Balance_Amount));

            if (payableInfo.Balance_Amount != 0)
            {
                sqlParams.Add(new SqlParameter("@Status", "Partially Paid"));
            }
            else
            {
                sqlParams.Add(new SqlParameter("@Status", "Payment Done"));
            }


            sqlParams.Add(new SqlParameter("@Created_On", DateTime.Now));
            sqlParams.Add(new SqlParameter("@Created_By", user_Id));

            sqlParams.Add(new SqlParameter("@Updated_On", DateTime.Now));
            sqlParams.Add(new SqlParameter("@Updated_By", user_Id));

            return sqlParams;
        }

        private List<SqlParameter> Set_Values_In_Payable_Items(PayableInfo payableInfo, int user_Id)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            sqlParams.Add(new SqlParameter("@Payable_Item_Id", payableInfo.Payable_Item_Id));

            sqlParams.Add(new SqlParameter("@Payable_Id", payableInfo.Payable_Id));
            sqlParams.Add(new SqlParameter("@Purchase_Order_Id", payableInfo.Purchase_Order_Id));           
            sqlParams.Add(new SqlParameter("@Payable_Date", payableInfo.Payable_Date));
            sqlParams.Add(new SqlParameter("@Payable_Item_Amount", payableInfo.Payable_Item_Amount));
            sqlParams.Add(new SqlParameter("@Transaction_Type", payableInfo.Transaction_Type));
            sqlParams.Add(new SqlParameter("@Status", "Done"));

            if (payableInfo.Transaction_Type == 1)
            {
                sqlParams.Add(new SqlParameter("@Cheque_Number", payableInfo.Cheque_Number));
                sqlParams.Add(new SqlParameter("@Cheque_Date", payableInfo.Cheque_Date));
                sqlParams.Add(new SqlParameter("@Bank_Name", payableInfo.Bank_Name));
                sqlParams.Add(new SqlParameter("@IFSC_Code", payableInfo.IFSC_Code));
                sqlParams.Add(new SqlParameter("@NEFT", "NA"));
                sqlParams.Add(new SqlParameter("@Credit_Debit_Card", "NA"));
            }
            else if (payableInfo.Transaction_Type == 2)
            {
                sqlParams.Add(new SqlParameter("@Cheque_Number", "NA"));
                sqlParams.Add(new SqlParameter("@Cheque_Date", DateTime.Now));
                sqlParams.Add(new SqlParameter("@Bank_Name", "NA"));
                sqlParams.Add(new SqlParameter("@IFSC_Code", "NA"));
                sqlParams.Add(new SqlParameter("@NEFT", payableInfo.NEFT));
                sqlParams.Add(new SqlParameter("@Credit_Debit_Card", "NA"));
            }
            else
            {
                sqlParams.Add(new SqlParameter("@Cheque_Number", "NA"));
                sqlParams.Add(new SqlParameter("@Cheque_Date", DateTime.Now));
                sqlParams.Add(new SqlParameter("@Bank_Name", "NA"));
                sqlParams.Add(new SqlParameter("@IFSC_Code", "NA"));
                sqlParams.Add(new SqlParameter("@NEFT", "NA"));
                sqlParams.Add(new SqlParameter("@Credit_Debit_Card", payableInfo.Credit_Debit_Card));
            }

            //payableInfo.Balance_Amount = payableInfo.Invoice_Amount - payableInfo.Payable_Item_Amount;
            sqlParams.Add(new SqlParameter("@Balance_Amount", payableInfo.Balance_Amount));

            sqlParams.Add(new SqlParameter("@Created_On", DateTime.Now));
            sqlParams.Add(new SqlParameter("@Created_By", user_Id));

            sqlParams.Add(new SqlParameter("@Updated_On", DateTime.Now));
            sqlParams.Add(new SqlParameter("@Updated_By", user_Id));

            return sqlParams;
        }

        public PayableInfo Get_Payable_Data_By_Id(int payable_Id)
        {
            PayableInfo payable = new PayableInfo();

            List<SqlParameter> sqlparam = new List<SqlParameter>();

            sqlparam.Add(new SqlParameter("@Payable_Id", payable_Id));

            DataTable dt = _sqlHelper.ExecuteDataTable(sqlparam, StoreProcedures.Get_Payable_Data_By_Id_Sp.ToString(), CommandType.StoredProcedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (!dr.IsNull("Payable_Id"))
                        payable.Payable_Id = Convert.ToInt32(dr["Payable_Id"]);
                    if (!dr.IsNull("Status"))
                        payable.Status = Convert.ToString(dr["Status"]);
                    if (!dr.IsNull("Amount"))
                        payable.Purchase_Order_Amount = Convert.ToDecimal(dr["Amount"]);
                    if (!dr.IsNull("Purchase_Order_No"))
                        payable.Purchase_Order_No = Convert.ToString(dr["Purchase_Order_No"]);
                    if (!dr.IsNull("Purchase_Order_Id"))
                        payable.Purchase_Order_Id = Convert.ToInt32(dr["Purchase_Order_Id"]);
                }
            }
            return payable;
        }

        public List<PayableInfo> Get_Payable_Items_By_Id(int payable_Id)
        {
            List<PayableInfo> Payables = new List<PayableInfo>();

            List<SqlParameter> sqlParamList = new List<SqlParameter>();

            sqlParamList.Add(new SqlParameter("@Payable_Id", payable_Id));

            DataTable dt = _sqlHelper.ExecuteDataTable(sqlParamList, StoreProcedures.Get_Payable_Data_Item_By_Id_Sp.ToString(), CommandType.StoredProcedure);

            foreach (DataRow dr in CommonMethods.GetRows(dt))
            {
                Payables.Add(Get_Payble_Item_Values(dr));
            }
            return Payables;
        }

        private PayableInfo Get_Payble_Item_Values(DataRow dr)
        {
            PayableInfo payable = new PayableInfo();

            payable.Payable_Item_Id = Convert.ToInt32(dr["Payable_Item_Id"]);
            payable.Payable_Id = Convert.ToInt32(dr["Payable_Id"]);
            payable.Payable_Date = Convert.ToDateTime(dr["Payable_Date"]);
            payable.Payable_Item_Amount = Convert.ToDecimal(dr["Payable_Item_Amount"]);
            payable.Transaction_Type = Convert.ToInt32(dr["Transaction_Type"]);

            if (!dr.IsNull("Cheque_Number"))
                payable.Cheque_Number = Convert.ToString(dr["Cheque_Number"]);
            if (!dr.IsNull("Cheque_Date"))
                payable.Cheque_Date = Convert.ToDateTime(dr["Cheque_Date"]);
            if (!dr.IsNull("IFSC_Code"))
                payable.IFSC_Code = Convert.ToString(dr["IFSC_Code"]);
            if (!dr.IsNull("Bank_Name"))
                payable.Bank_Name = Convert.ToString(dr["Bank_Name"]);
            if (!dr.IsNull("NEFT"))
                payable.NEFT = Convert.ToString(dr["NEFT"]);
            if (!dr.IsNull("Credit_Debit_Card"))
                payable.Credit_Debit_Card = Convert.ToString(dr["Credit_Debit_Card"]);

            return payable;
        }

        public void Delete_Payable_Data_Item_By_Id(int payable_Item_Id)
        {
            List<SqlParameter> sqlparam = new List<SqlParameter>();

            sqlparam.Add(new SqlParameter("@Receivable_Item_Id", payable_Item_Id));

            _sqlHelper.ExecuteNonQuery(sqlparam, StoreProcedures.Delete_Payable_Data_Item_By_Id.ToString(), CommandType.StoredProcedure);
        }

    }
}
