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
    public class InvoiceRepo
    {
        SQLHelper _sqlRepo;

        public InvoiceRepo()
        {
            _sqlRepo = new SQLHelper();
        }

        public void Insert_Invoice(InvoiceInfo invoice)
        {
            _sqlRepo.ExecuteNonQuery(Set_Values_In_Invoice(invoice), StoreProcedures.Insert_Invoice_Sp.ToString(), CommandType.StoredProcedure);
        }

        //public void Update_Invoice(InvoiceInfo invoice)
        //{
        //    _sqlRepo.ExecuteNonQuery(Set_Values_In_Invoice(invoice), StoreProcedures.Update_Invoice_Sp.ToString(), CommandType.StoredProcedure);
        //}

        private List<SqlParameter> Set_Values_In_Invoice(InvoiceInfo invoice)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            sqlParams.Add(new SqlParameter("@Invoice_Id", invoice.Invoice_Id));
            sqlParams.Add(new SqlParameter("@Order_Id", invoice.Order_Id));
            sqlParams.Add(new SqlParameter("@Invoice_No", invoice.Invoice_No));
            sqlParams.Add(new SqlParameter("@Invoice_Date", invoice.Invoice_Date));
            sqlParams.Add(new SqlParameter("@Created_On", invoice.Created_On));
            sqlParams.Add(new SqlParameter("@Created_By", invoice.Created_By));
            sqlParams.Add(new SqlParameter("@Updated_On", invoice.Updated_On));
            sqlParams.Add(new SqlParameter("@Updated_By", invoice.Updated_By));

            return sqlParams;
        }

        public List<InvoiceInfo> Get_Invoices(ref PaginationInfo Pager)
        {
            List<InvoiceInfo> invoices = new List<InvoiceInfo>();
            DataTable dt = _sqlRepo.ExecuteDataTable(null, StoreProcedures.Get_Invoice_Sp.ToString(), CommandType.StoredProcedure);
            foreach (DataRow dr in CommonMethods.GetRows(dt, ref Pager))
            {
                invoices.Add(Get_Invoice_Values(dr));
            }
            return invoices;
        }

        public List<InvoiceInfo> Get_Invoices_By_Id(int Invoice_Id, ref PaginationInfo Pager)
        {
            List<SqlParameter> sqlParam = new List<SqlParameter>();
            sqlParam.Add(new SqlParameter("@Invoice_Id", Invoice_Id));

            List<InvoiceInfo> invoices = new List<InvoiceInfo>();
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParam, StoreProcedures.Get_Invoice_By_Id_Sp.ToString(), CommandType.StoredProcedure);
            foreach (DataRow dr in CommonMethods.GetRows(dt, ref Pager))
            {
                invoices.Add(Get_Invoice_Values(dr));
            }
            return invoices;
        }


        public InvoiceInfo Get_Invoice_By_Id(int Invoice_Id)
        {
            List<SqlParameter> sqlParam = new List<SqlParameter>();
            sqlParam.Add(new SqlParameter("@Invoice_Id", Invoice_Id));

            InvoiceInfo invoice = new InvoiceInfo();
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParam, StoreProcedures.Get_Invoice_By_Id_Sp.ToString(), CommandType.StoredProcedure);
            foreach (DataRow dr in dt.Rows)
            {
                invoice = Get_Invoice_Values(dr);
            }
            return invoice;
        }

        private InvoiceInfo Get_Invoice_Values(DataRow dr)
        {
            InvoiceInfo invoice = new InvoiceInfo();

            invoice.Invoice_Id = Convert.ToInt32(dr["Invoice_Id"]);
            invoice.Order_Id = Convert.ToInt32(dr["Order_Id"]);
            invoice.Order_No = Convert.ToString(dr["Order_No"]);
            invoice.Invoice_No = Convert.ToString(dr["Invoice_No"]);
            invoice.Invoice_Date = Convert.ToDateTime(dr["Invoice_Date"]);
            invoice.Created_On = Convert.ToDateTime(dr["Created_On"]);
            invoice.Created_By = Convert.ToInt32(dr["Created_By"]);
            invoice.Updated_On = Convert.ToDateTime(dr["Updated_On"]);
            invoice.Updated_By = Convert.ToInt32(dr["Updated_By"]);
            return invoice;
        }

        public List<AutocompleteInfo> Get_Invoice_Autocomplete(string InoviceNo)
        {
            List<AutocompleteInfo> autoList = new List<AutocompleteInfo>();
            List<SqlParameter> sqlparam = new List<SqlParameter>();
            sqlparam.Add(new SqlParameter("@Description", InoviceNo == null ? System.String.Empty : InoviceNo.Trim()));
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlparam, StoreProcedures.Get_Invoice_Autocomplete_Sp.ToString(), CommandType.StoredProcedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    AutocompleteInfo auto = new AutocompleteInfo();
                    auto.Label = Convert.ToString(dr["Label"]);
                    auto.Value = Convert.ToInt32(dr["Value"]);
                    autoList.Add(auto);
                }
            }
            return autoList;
        }

        public void Send_Invoice_Email(string Email_Id, InvoiceInfo invoice, OrdersInfo Order, DealerInfo Dealer)
        {           

            StringBuilder html = new StringBuilder();
            string subject = "Invoice For Order No : " + Order.Order_No;

            #region Main Table

            html.Append("<table cellspacing='0' cellpadding='0' style='width:700px;border:2px solid #ccc;' >");
            html.Append("<tbody>");

            #region header dates first tr
            html.Append("<tr>");
            html.Append("<td style='width:60%;'>SMS </td>");
            html.Append("<td style='width:40%;text-align:right;'><b>Invoice Date :</b> " + string.Format("{0:dd/MM/yyy}", invoice.Invoice_Date) + "</td>");
            html.Append("</tr>");
            #endregion            

            #region sencode tr           
           
            html.Append("<tr>");
            html.Append("<td colspan='2'>");

            #region From-to address & Order Details
            html.Append("<table cellspacing='0' cellpadding='0' style='width:600px'>");
            html.Append("<tbody>");
            html.Append("<tr>");

            #region Sms Address
            html.Append("<td width='35%' style='border-bottom:1px solid #ccc'>");
            html.Append("<table cellpadding='5' width='99%' style='margin:10px 0'>");
            html.Append("<tbody>");
            html.Append("<tr>");
            html.Append(" <td><b><span>From</span>: </b>");
            html.Append("<br>" + ConfigurationManager.AppSettings["CompanyName"].ToString());
            html.Append("<br>" + ConfigurationManager.AppSettings["CompanyAddress"].ToString());
            html.Append("<br>" + ConfigurationManager.AppSettings["CompanyTelephone"].ToString());
            html.Append("<br>" + ConfigurationManager.AppSettings["fromMailAddress"].ToString());
            html.Append("</td>");
            html.Append("</tr>");
            html.Append("</tbody>");
            html.Append("</table>");
            html.Append("</td>");
            #endregion

            #region To Address
            html.Append("<td width='35%' style='border-bottom:1px solid #ccc;border-left:1px solid #ccc'>");
            html.Append("<table cellspacing='0' cellpadding='15'>");
            html.Append("<tbody>");
            html.Append("<tr>");
            html.Append("<td width='100%' valign='top' rowspan='2'><span><b>To:</b><br><br></span><strong>" + Dealer.Dealer_Name + "</strong>");

            if (!string.IsNullOrEmpty(Dealer.Address))
            {
                html.Append("<br>" + Dealer.Address + " ,");
            }
            if (!string.IsNullOrEmpty(Dealer.City))
            {
                html.Append("<br>" + Dealer.City + " ,");
                if (!string.IsNullOrEmpty(Convert.ToString(Dealer.Pincode)))
                {
                    html.Append(" " + Convert.ToString(Dealer.Pincode) + " ,");
                }
            }
            if (!string.IsNullOrEmpty(Dealer.State_Name))
            {
                html.Append("<br>" + Dealer.State_Name + " ,");
            }

            if (!string.IsNullOrEmpty(Dealer.Contact_No_1))
            {
                html.Append("<br> Mobile :" + Dealer.Contact_No_1 + " ,");
            }
            if (string.IsNullOrEmpty(Dealer.Contact_No_1))
            {
                html.Append("<br> Mobile :" + Dealer.Contact_No_2 + " ,");
            }
            if (!string.IsNullOrEmpty(Dealer.Contact_No_1))
            {
                html.Append("<br> Email :" + Dealer.Email + "");
            }

            html.Append("</td>");
            html.Append("</tr>");
            html.Append(" </tbody>");
            html.Append("</table>");
            html.Append("</td>");
            #endregion

            #region Order Details
            html.Append(" <td width='30%' style='border-bottom:1px solid #ccc;border-left:1px solid #ccc'>");
            html.Append("<table cellspacing='0' cellpadding='15'>");
            html.Append("<tbody>");
            html.Append("<tr>");
            html.Append("<td>");
            html.Append("<br><b>Invoice No :</b> " + invoice.Invoice_No );
            html.Append("<br><b>Order No :</b> " + Order.Order_No );
            html.Append("<br><b>Order Date :</b> " + string.Format("{0:d}", Order.Order_Date) );

            html.Append("<br>");
            html.Append("</td>");
            html.Append("</tr>");
            html.Append("</tbody>");
            html.Append("</table>");
            html.Append(" </td>");
            #endregion
            
            html.Append("</tr>");
            html.Append("</tbody>");
            html.Append("</table>");
            #endregion           
            
            html.Append("</td>");
            html.Append("</tr>");
            #endregion

            #region third tr
            html.Append("<tr>");
            html.Append("<td colspan='2'>");
            
            html.Append("<table cellspacing='1' cellpadding='6' width='700px' style='background-color:#e2e2e2'>");
            html.Append("<tbody>");

            html.Append("<th");
            html.Append("<th style='width:20px;text-align:center;height:30px'>Sr.No</th>");
            html.Append("<th style='width:250px'>Product name</th>");
            html.Append("<th style='width:100px;text-align:center'>Qty</th>");
            html.Append("<th style='width:110px;text-align:right'>Unit Price</th>");
            html.Append("<th style='text-align:center;width:45px'>Price</th>");
            html.Append("</th>");

            int count = 0;
            if (Order != null)
            {
                foreach (var item in Order.OrderItems)
                {
                    ProductRepo _productRepo = new ProductRepo();
                    ProductInfo ProductInfo = _productRepo.Get_Product_By_Id(item.Product_Id);

                    count++;
                    html.Append("<tr style='background-color:#fff'>");
                    html.Append("<td>" + count + "</td>");
                    html.Append("<td style='text-align:center'>" + ProductInfo.Product_Name + "</td>");
                    html.Append("<td style='text-align:right'>" + item.Product_Quantity + "</td>");
                    html.Append("<td style='text-align:right'>" + ProductInfo.Product_Price + "</td>");
                    html.Append("<td style='text-align:right'>" + item.Product_Price + "</td>");
                    html.Append("</tr>");
                }
            }

            html.Append("<tr>");
            html.Append("<td colspan='4'>Total: </td>");
            html.Append("<td>" + Order.Net_Amount + "</td>");
            html.Append("</tr>");

            html.Append("<tr>");
            html.Append("<td colspan='4'>VAT(%): </td>");
            html.Append("<td>" + Order.Vat + "</td>");
            html.Append("</tr>");

            html.Append("<tr>");
            html.Append("<td colspan='4'>Service Tax(%):</td>");
            html.Append("<td>" + Order.Service_Tax + "</td>");
            html.Append("</tr>");

            html.Append("<tr>");
            html.Append("<td colspan='4'>Swatch Bharat Tax:</td>");
            html.Append("<td>" + Order.Swatch_Bharat_Tax + "</td>");
            html.Append("</tr>");

            html.Append("<tr>");
            html.Append("<td colspan='4'>Grand Total:</td>");
            html.Append("<td>" + Order.Gross_Amount + "</td>");
            html.Append("</tr>");

            html.Append("</tbody>");
            html.Append("</table>");
                                          

            html.Append("</td>");
            html.Append("</tr>");
            #endregion

            html.Append("</tbody>");
            html.Append("</table");

            #endregion

            MailAddress fromMail = new MailAddress(ConfigurationManager.AppSettings["fromMailAddress"].ToString(), ConfigurationManager.AppSettings["fromMailName"].ToString());
            MailMessage message = new MailMessage();
            message.From = fromMail;
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = html.ToString();
            MailAddress To = new MailAddress(Email_Id);
            message.To.Add(To);
            SmtpClient client = new SmtpClient();
            client.Send(message);
        }

    }
}
