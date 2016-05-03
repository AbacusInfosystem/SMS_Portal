using SMSPortalHelper;
using SMSPortalInfo;
using SMSPortalInfo.Common;
using SMSPortalRepo.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
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

       public List<ReceivableInfo> Get_Receivable_By_Id(int invoice_Id, ref PaginationInfo pager)
       {
           List<SqlParameter> sqlParamList = new List<SqlParameter>();
           sqlParamList.Add(new SqlParameter("@Invoice_Id", invoice_Id));

           List<ReceivableInfo> Receivables = new List<ReceivableInfo>();
           DataTable dt = _sqlRepo.ExecuteDataTable(sqlParamList, StoreProcedures.Get_Receivable_Data_By_Invoice_Id_Sp.ToString(), CommandType.StoredProcedure);
           foreach (DataRow dr in CommonMethods.GetRows(dt, ref pager))
           {
               Receivables.Add(Get_Receivable_Values(dr));
           }
           return Receivables;
       }

       public ReceivableInfo Get_Receivable_Data_By_Id(int invoice_Id)
       {
           ReceivableInfo receivable = new ReceivableInfo();

           List<SqlParameter> sqlparam = new List<SqlParameter>();

           sqlparam.Add(new SqlParameter("@Invoice_Id", invoice_Id));

           DataTable dt = _sqlRepo.ExecuteDataTable(sqlparam, StoreProcedures.Get_Receivable_Data_By_Id_Sp.ToString(), CommandType.StoredProcedure);

           if (dt != null && dt.Rows.Count > 0)
           {
               foreach (DataRow dr in dt.Rows)
               {
                   if (!dr.IsNull("Receivable_Id"))
                       receivable.Receivable_Id = Convert.ToInt32(dr["Receivable_Id"]);
                   if (!dr.IsNull("Status"))
                       receivable.Status = Convert.ToString(dr["Status"]);
                   if (!dr.IsNull("Amount"))
                       receivable.Invoice_Amount = Convert.ToDecimal(dr["Amount"]);
                   if (!dr.IsNull("Invoice_No"))
                       receivable.Invoice_No = Convert.ToString(dr["Invoice_No"]);
                   if (!dr.IsNull("Invoice_Id"))
                       receivable.Invoice_Id = Convert.ToInt32(dr["Invoice_Id"]);
                   if (!dr.IsNull("Balance_Amount"))
                       receivable.Balance_Amount = Convert.ToDecimal(dr["Balance_Amount"]);
               }
           }
           return receivable;
       }

       public List<ReceivableInfo> Get_Receivable_Items_By_Id(int receivable_Id)
       {
           List<ReceivableInfo> Receivables = new List<ReceivableInfo>();

           List<SqlParameter> sqlParamList = new List<SqlParameter>();

           sqlParamList.Add(new SqlParameter("@Receivable_Id", receivable_Id));
        
           DataTable dt = _sqlRepo.ExecuteDataTable(sqlParamList, StoreProcedures.Get_Receivable_Data_Item_By_Id_Sp.ToString(), CommandType.StoredProcedure);

           foreach (DataRow dr in CommonMethods.GetRows(dt))
           {
               Receivables.Add(Get_Receivable_Item_Values(dr));
           }
           return Receivables;
       }

       private ReceivableInfo Get_Receivable_Item_Values(DataRow dr)
       {
           ReceivableInfo receivable = new ReceivableInfo();

           receivable.Receivable_Item_Id = Convert.ToInt32(dr["Receivable_Item_Id"]);
           receivable.Receivable_Id = Convert.ToInt32(dr["Receivable_Id"]);           
           receivable.Receivable_Date = Convert.ToDateTime(dr["Receivable_Date"]);
           receivable.Receivable_Item_Amount = Convert.ToDecimal(dr["Receivable_Item_Amount"]);
           receivable.Transaction_Type = Convert.ToInt32(dr["Transaction_Type"]);

           if (receivable.Transaction_Type==1)
           {
               receivable.Transaction_Type_Name = "Cheque";
           }
           else if (receivable.Transaction_Type == 2)
           {
               receivable.Transaction_Type_Name = "NEFT";
           }
           else
           {
               receivable.Transaction_Type_Name = "Credit/Debit Card";
           }

           if (!dr.IsNull("Cheque_Number"))
           receivable.Cheque_Number = Convert.ToString(dr["Cheque_Number"]);
           if (!dr.IsNull("Cheque_Date"))
           receivable.Cheque_Date = Convert.ToDateTime(dr["Cheque_Date"]);
           if (!dr.IsNull("IFSC_Code"))
           receivable.IFSC_Code = Convert.ToString(dr["IFSC_Code"]);
           if (!dr.IsNull("Bank_Name"))
           receivable.Bank_Name = Convert.ToString(dr["Bank_Name"]);
           if (!dr.IsNull("NEFT"))
           receivable.NEFT = Convert.ToString(dr["NEFT"]);
           if (!dr.IsNull("Credit_Debit_Card"))
           receivable.Credit_Debit_Card = Convert.ToString(dr["Credit_Debit_Card"]);

           return receivable;
       }

       private ReceivableInfo Get_Receivable_Values(DataRow dr)
       {
           ReceivableInfo receivable = new ReceivableInfo();

           receivable.Status = Convert.ToString(dr["Status"]);
           receivable.Invoice_Amount = Convert.ToDecimal(dr["Amount"]);
           receivable.Invoice_No = Convert.ToString(dr["Invoice_No"]);
           receivable.Invoice_Id = Convert.ToInt32(dr["Invoice_Id"]);

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

       public int Get_Order_Id_By_Invoice_Id(int invoice_Id)
       {
           int Order_Id = 0;

           List<SqlParameter> sqlParams = new List<SqlParameter>();

           sqlParams.Add(new SqlParameter("@Invoice_Id", invoice_Id));

           DataTable dt = _sqlRepo.ExecuteDataTable(sqlParams, StoreProcedures.Get_Order_Id_By_Invoice_Id_Sp.ToString(), CommandType.StoredProcedure);

           if (dt != null && dt.Rows.Count > 0)
           {
               foreach (DataRow dr in dt.Rows)
               {
                   if (!dr.IsNull("Order_Id"))
                       Order_Id = Convert.ToInt32(dr["Order_Id"]);
               }
           }

           return Order_Id;
       }

       public decimal Get_Balance_Amount(int invoice_Id)
       {
           decimal Balance_Amount = 0;

           List<SqlParameter> sqlParams = new List<SqlParameter>();

           sqlParams.Add(new SqlParameter("@Invoice_Id", invoice_Id));

           DataTable dt = _sqlRepo.ExecuteDataTable(sqlParams, StoreProcedures.Get_Receivable_Balance_Amount_By_Id_Sp.ToString(), CommandType.StoredProcedure);

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

       public int Insert_Receivable(ReceivableInfo receivableInfo,int user_Id)
       {
           int Receivable_Id = 0;

           Receivable_Id = Convert.ToInt32(_sqlRepo.ExecuteScalerObj(Set_Values_In_Receivable(receivableInfo, user_Id), StoreProcedures.Insert_Receivable_Data_Sp.ToString(), CommandType.StoredProcedure));         

           if(receivableInfo.Receivable_Item_Id!=0)
           {
               Receivable_Id = receivableInfo.Receivable_Id;
           }

           return Receivable_Id;
       }

       public void Insert_Receivable_Items(ReceivableInfo receivableInfo, int user_Id)
       {
           _sqlRepo.ExecuteNonQuery(Set_Values_In_Receivable_Items(receivableInfo, user_Id), StoreProcedures.Insert_Receivable_Item_Data_Sp.ToString(), CommandType.StoredProcedure);
       }

       public void Insert_Receivable_Receipt_Data(ReceivableInfo receivableInfo, int user_Id)
       {
           _sqlRepo.ExecuteNonQuery(Set_Values_In_Receivable_Receipt(receivableInfo, user_Id), StoreProcedures.Insert_Receivable_Receipt_Data_Sp.ToString(), CommandType.StoredProcedure);
       }

       private List<SqlParameter> Set_Values_In_Receivable_Items(ReceivableInfo receivableInfo, int user_Id)
       {
           List<SqlParameter> sqlParams = new List<SqlParameter>();

           sqlParams.Add(new SqlParameter("@Receivable_Item_Id", receivableInfo.Receivable_Item_Id));

           sqlParams.Add(new SqlParameter("@Receivable_Id", receivableInfo.Receivable_Id));
           sqlParams.Add(new SqlParameter("@Invoice_Id", receivableInfo.Invoice_Id));
           sqlParams.Add(new SqlParameter("@Receivable_Date", receivableInfo.Receivable_Date));
           sqlParams.Add(new SqlParameter("@Receivable_Item_Amount", receivableInfo.Receivable_Item_Amount));
           sqlParams.Add(new SqlParameter("@Transaction_Type", receivableInfo.Transaction_Type));
           sqlParams.Add(new SqlParameter("@Status", "Paid"));

           if (receivableInfo.Transaction_Type == 1)
           {
               sqlParams.Add(new SqlParameter("@Cheque_Number", receivableInfo.Cheque_Number));
               sqlParams.Add(new SqlParameter("@Cheque_Date", receivableInfo.Cheque_Date));
               sqlParams.Add(new SqlParameter("@Bank_Name", receivableInfo.Bank_Name));
               sqlParams.Add(new SqlParameter("@IFSC_Code", receivableInfo.IFSC_Code));
               sqlParams.Add(new SqlParameter("@NEFT", "NA"));
               sqlParams.Add(new SqlParameter("@Credit_Debit_Card", "NA"));
           }
           else if (receivableInfo.Transaction_Type == 2)
           {
               sqlParams.Add(new SqlParameter("@Cheque_Number", "NA"));
               sqlParams.Add(new SqlParameter("@Cheque_Date", "01/01/1999"));
               sqlParams.Add(new SqlParameter("@Bank_Name", "NA"));
               sqlParams.Add(new SqlParameter("@IFSC_Code", "NA"));
               sqlParams.Add(new SqlParameter("@NEFT", receivableInfo.NEFT));
               sqlParams.Add(new SqlParameter("@Credit_Debit_Card", "NA"));
           }
           else
           {
               sqlParams.Add(new SqlParameter("@Cheque_Number", "NA"));
               sqlParams.Add(new SqlParameter("@Cheque_Date", "01/01/1999"));
               sqlParams.Add(new SqlParameter("@Bank_Name", "NA"));
               sqlParams.Add(new SqlParameter("@IFSC_Code", "NA"));
               sqlParams.Add(new SqlParameter("@NEFT", "NA"));
               sqlParams.Add(new SqlParameter("@Credit_Debit_Card", receivableInfo.Credit_Debit_Card));
           }

           sqlParams.Add(new SqlParameter("@Balance_Amount", receivableInfo.Balance_Amount));

           sqlParams.Add(new SqlParameter("@Created_On", DateTime.Now));
           sqlParams.Add(new SqlParameter("@Created_By", user_Id));

           sqlParams.Add(new SqlParameter("@Updated_On", DateTime.Now));
           sqlParams.Add(new SqlParameter("@Updated_By", user_Id));

           return sqlParams;
       }

       private List<SqlParameter> Set_Values_In_Receivable(ReceivableInfo receivableInfo, int user_Id)
       {
           decimal Total_Balance_Amount = 0;
          // decimal Amount = 0;

           List<SqlParameter> sqlParams = new List<SqlParameter>();

           decimal Balance_Amount = Get_Balance_Amount(receivableInfo.Invoice_Id);

           sqlParams.Add(new SqlParameter("@Receivable_Id", receivableInfo.Receivable_Id));

           sqlParams.Add(new SqlParameter("@Invoice_Id", receivableInfo.Invoice_Id));
           sqlParams.Add(new SqlParameter("@Amount", receivableInfo.Invoice_Amount));

           if(Balance_Amount>0)
           {
               Total_Balance_Amount = Balance_Amount - receivableInfo.Receivable_Item_Amount;              
           }
           else
           {
               Total_Balance_Amount = receivableInfo.Invoice_Amount-receivableInfo.Receivable_Item_Amount;
           }

           receivableInfo.Balance_Amount = Total_Balance_Amount;

           sqlParams.Add(new SqlParameter("@Balance_Amount", receivableInfo.Balance_Amount));

           if (receivableInfo.Balance_Amount!=0)
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

       private List<SqlParameter> Set_Values_In_Receivable_Receipt(ReceivableInfo receivableInfo, int user_Id)
       {           
           List<SqlParameter> sqlParams = new List<SqlParameter>();

           sqlParams.Add(new SqlParameter("@Receivable_Id", receivableInfo.Receivable_Id));

           return sqlParams;
       }

       public void Delete_Receivable_Data_Item_By_Id(int receivable_Item_Id)
       {
           List<SqlParameter> sqlparam = new List<SqlParameter>();

           sqlparam.Add(new SqlParameter("@Receivable_Item_Id", receivable_Item_Id));

           _sqlRepo.ExecuteNonQuery(sqlparam, StoreProcedures.Delete_Receivable_Item_By_Id_Sp.ToString(), CommandType.StoredProcedure);
       }

       public List<AutocompleteInfo> Get_Invoice_Autocomplete(string invoice_No)
       {
           List<AutocompleteInfo> autoList = new List<AutocompleteInfo>();
           List<SqlParameter> sqlparam = new List<SqlParameter>();
           sqlparam.Add(new SqlParameter("@Description", invoice_No == null ? System.String.Empty : invoice_No.Trim()));
           DataTable dt = _sqlRepo.ExecuteDataTable(sqlparam, StoreProcedures.Get_Invoice_No_Autocomplete_Sp.ToString(), CommandType.StoredProcedure);
           if (dt != null && dt.Rows.Count > 0)
           {
               List<DataRow> drList = new List<DataRow>();
               drList = dt.AsEnumerable().ToList();
               foreach (DataRow dr in drList)
               {
                   AutocompleteInfo auto = new AutocompleteInfo();
                   auto.Label = Convert.ToString(dr["Label"]);
                   auto.Value = Convert.ToInt32(dr["Value"]);
                   autoList.Add(auto);
               }
           }
           return autoList;
       }

       public void Send_Payment_Receipt(string email_Id, ReceivableInfo receivableInfo, List<ReceivableInfo> receivables)
       {
           StringBuilder html = new StringBuilder();

           string subject = " Payment receipt for invoice no : " + receivableInfo.Invoice_No;

           html.Append("<h4> Hello, </h4> \n");

           html.Append("<h4> This is a payment receipt against invoice No : " + receivableInfo.Invoice_No + "</h4>");

           html.Append("<br/>");

           html.Append("<h4> Following are transaction details.</h4>");

           html.Append("<br/>");

           html.Append("<table width=600px border=1 cellspacing=2 cellpadding=2 align=center bgcolor=White dir=ltr rules=all style=border-width: thin; border-style: solid; line-height: normal; vertical-align: baseline; text-align: center; font-family: Calibri; font-size: medium; font-weight: normal; font-style: normal; font-variant: normal; color: #000000; list-style-type: none;>");
 
           html.Append("<tr>");

           html.Append("<td width='40px'>");

           html.Append("Amount");

           html.Append("</td>");

           html.Append("<td width='60px'>");

           html.Append("Transaction Type");

           html.Append("</td>");

           html.Append("<td width='40px'>");

           html.Append("Bank Name");

           html.Append("</td>");

           html.Append("<td width='60px'>");

           html.Append("Ifsc Code");

           html.Append("</td>");

           html.Append("<td width='40px'>");

           html.Append("Cheque No");

           html.Append("</td>");

           html.Append("<td width='40px'>");

           html.Append("Cheque Date");

           html.Append("</td>");

           html.Append("<td width='60px'>");

           html.Append("NEFT Details");

           html.Append("</td>");

           html.Append("<td width='60px'>");

           html.Append("Credit/Debit Details");

           html.Append("</td>");

           html.Append("</tr>");

           for (int i = receivables.Count() - 1; i < receivables.Count(); i++)
           {
               html.Append("<tr>");

               html.Append("<td>");

               html.Append(receivables[i].Receivable_Item_Amount);

               html.Append("</td>");

               html.Append("<td>");

               html.Append(receivables[i].Transaction_Type_Name);

               html.Append("</td>");

               html.Append("<td>");

               html.Append(receivables[i].Bank_Name);

               html.Append("</td>");

               html.Append("<td>");

               html.Append(receivables[i].IFSC_Code);

               html.Append("</td>");

               html.Append("<td>");

               html.Append(receivables[i].Cheque_Number);

               html.Append("</td>");

               html.Append("<td>");

               html.Append(receivables[i].Cheque_Date.ToShortDateString());

               html.Append("</td>");

               html.Append("<td>");

               html.Append(receivables[i].NEFT);

               html.Append("</td>");

               html.Append("<td>");

               html.Append(receivables[i].Credit_Debit_Card);

               html.Append("</td>");

               html.Append("</tr>");
           }

           html.Append("</table>");

           MailAddress fromMail = new MailAddress(ConfigurationManager.AppSettings["fromMailAddress"].ToString(), ConfigurationManager.AppSettings["fromMailName"].ToString());


           MailMessage message = new MailMessage();

           message.From = fromMail;

           message.Subject = subject;

           message.IsBodyHtml = true;

           message.Body = html.ToString();

           MailAddress To = new MailAddress(email_Id);

           message.To.Add(To);

           SmtpClient client = new SmtpClient();

           client.Send(message);

       }

       public string Get_Receivable_Status(int invoice_Id)
       {
           string Status = "";

           List<SqlParameter> sqlParams = new List<SqlParameter>();

           sqlParams.Add(new SqlParameter("@Invoice_Id", invoice_Id));

           DataTable dt = _sqlRepo.ExecuteDataTable(sqlParams, StoreProcedures.Get_Receivable_Status_By_Id_Sp.ToString(), CommandType.StoredProcedure);

           if (dt != null && dt.Rows.Count > 0)
           {
               foreach (DataRow dr in dt.Rows)
               {
                   if (!dr.IsNull("Status"))
                       Status = Convert.ToString(dr["Status"]);
               }
           }

           return Status;
       }

       public decimal Get_Invoice_Amount(int invoice_Id)
       {
           decimal Amount = 0;

           List<SqlParameter> sqlParams = new List<SqlParameter>();

           sqlParams.Add(new SqlParameter("@Invoice_Id", invoice_Id));

           DataTable dt = _sqlRepo.ExecuteDataTable(sqlParams, StoreProcedures.Get_Invoice_Amount_By_Id_Sp.ToString(), CommandType.StoredProcedure);

           if (dt != null && dt.Rows.Count > 0)
           {
               foreach (DataRow dr in dt.Rows)
               {
                   if (!dr.IsNull("Gross_Amount"))
                       Amount = Convert.ToDecimal(dr["Gross_Amount"]);
               }
           }

           return Amount;
       }

       public void Update_Sales_Order_Status(int invoice_Id)
       {
           int Order_Id = Get_Order_Id_By_Invoice_Id(invoice_Id);

           List<SqlParameter> sqlParams = new List<SqlParameter>();

           sqlParams.Add(new SqlParameter("@Order_Id", Order_Id));

          _sqlRepo.ExecuteDataTable(sqlParams, StoreProcedures.Get_Receivable_Status_By_Id_Sp.ToString(), CommandType.StoredProcedure);
       }
    }
}
