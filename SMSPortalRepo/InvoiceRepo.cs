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

        public int Insert_Invoice(InvoiceInfo invoice,int user_id)
        {
            int invoiceid = 0;
            invoiceid= Convert.ToInt32(_sqlRepo.ExecuteScalerObj(Set_Values_In_Invoice(invoice,user_id), StoreProcedures.Insert_Invoice_Sp.ToString(), CommandType.StoredProcedure));
            return invoiceid;
        }

        //public void Update_Invoice(InvoiceInfo invoice)
        //{
        //    _sqlRepo.ExecuteNonQuery(Set_Values_In_Invoice(invoice), StoreProcedures.Update_Invoice_Sp.ToString(), CommandType.StoredProcedure);
        //}

        private List<SqlParameter> Set_Values_In_Invoice(InvoiceInfo invoice,int user_id)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            if (invoice.Invoice_Id != 0)
            {
                sqlParams.Add(new SqlParameter("@Invoice_Id", invoice.Invoice_Id));
            }
            sqlParams.Add(new SqlParameter("@Order_Id", invoice.Order_Id));
            sqlParams.Add(new SqlParameter("@Invoice_No", invoice.Invoice_No));
            sqlParams.Add(new SqlParameter("@Invoice_Date", DateTime.Now));
            sqlParams.Add(new SqlParameter("@Role_Id", invoice.Role_Id));
            sqlParams.Add(new SqlParameter("@Entity_Id", invoice.Entity_Id));
            sqlParams.Add(new SqlParameter("@Amount", invoice.Amount));
            sqlParams.Add(new SqlParameter("@Created_On", DateTime.Now));
            sqlParams.Add(new SqlParameter("@Created_By", user_id));
            sqlParams.Add(new SqlParameter("@Updated_On", DateTime.Now));
            sqlParams.Add(new SqlParameter("@Updated_By", user_id));

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

            if (!dr.IsNull("Invoice_Id"))
            invoice.Invoice_Id = Convert.ToInt32(dr["Invoice_Id"]);

            if (!dr.IsNull("Order_Id"))
            invoice.Order_Id = Convert.ToInt32(dr["Order_Id"]);

            if (!dr.IsNull("Order_No"))
            invoice.Order_No = Convert.ToString(dr["Order_No"]);

            if (!dr.IsNull("Invoice_No"))
            invoice.Invoice_No = Convert.ToString(dr["Invoice_No"]);

            if (!dr.IsNull("Invoice_Date"))
            invoice.Invoice_Date = Convert.ToDateTime(dr["Invoice_Date"]);

            if (!dr.IsNull("Amount"))
            invoice.Amount = Convert.ToDecimal(dr["Amount"]);

            if (!dr.IsNull("Role_Id"))
            invoice.Role_Id = Convert.ToInt32(dr["Role_Id"]);

            if (!dr.IsNull("Entity_Id"))
            invoice.Entity_Id = Convert.ToInt32(dr["Entity_Id"]);

            if (!dr.IsNull("Created_On"))
            invoice.Created_On = Convert.ToDateTime(dr["Created_On"]);

            if (!dr.IsNull("Created_By"))
            invoice.Created_By = Convert.ToInt32(dr["Created_By"]);

            if (!dr.IsNull("Updated_On"))
            invoice.Updated_On = Convert.ToDateTime(dr["Updated_On"]);

            if (!dr.IsNull("Updated_By"))
            invoice.Updated_By = Convert.ToInt32(dr["Updated_By"]);

            return invoice;
        }

        public List<AutocompleteInfo> Get_Invoice_Autocomplete(string InoviceNo, int role_id, int entity_id)
        {
            List<AutocompleteInfo> autoList = new List<AutocompleteInfo>();
            List<SqlParameter> sqlparam = new List<SqlParameter>();
            sqlparam.Add(new SqlParameter("@Description", InoviceNo == null ? System.String.Empty : InoviceNo.Trim()));
            sqlparam.Add(new SqlParameter("@Role_Id", role_id));
            sqlparam.Add(new SqlParameter("@Entity_Id", entity_id));
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

        public DealerInfo Get_Dealer_By_Id(int Dealer_Id)
        {
            List<SqlParameter> sqlParamList = new List<SqlParameter>();

            sqlParamList.Add(new SqlParameter("@Dealer_Id", Dealer_Id));

            DealerInfo dealer = new DealerInfo();

            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParamList, StoreProcedures.Get_Dealer_By_Id_Sp.ToString(), CommandType.StoredProcedure);

            foreach (DataRow dr in dt.Rows)
            {
                dealer = Get_Dealer_Values(dr);
            }

            return dealer;
        }

        private DealerInfo Get_Dealer_Values(DataRow dr)
        {

            DealerInfo dealer = new DealerInfo();

            if (!dr.IsNull("Dealer_Id"))
                dealer.Dealer_Id = Convert.ToInt32(dr["Dealer_Id"]);

            if (!dr.IsNull("Dealer_Name"))
                dealer.Dealer_Name = Convert.ToString(dr["Dealer_Name"]);

            if (!dr.IsNull("Brand_Id"))
                dealer.Brand_Id = Convert.ToInt32(dr["Brand_Id"]);

            if (!dr.IsNull("Brand_Name"))
                dealer.Brand_Name = Convert.ToString(dr["Brand_Name"]);

            if (!dr.IsNull("Address"))
                dealer.Address = Convert.ToString(dr["Address"]);

            if (!dr.IsNull("City"))
                dealer.City = Convert.ToString(dr["City"]);

            if (!dr.IsNull("State"))
                dealer.State = Convert.ToInt32(dr["State"]);

            dealer.State_Name = Convert.ToString(dr["State_Name"]);

            if (!dr.IsNull("Pincode"))
                dealer.Pincode = Convert.ToInt32(dr["Pincode"]);

            if (!dr.IsNull("Contact_No_1"))
                dealer.Contact_No_1 = Convert.ToString(dr["Contact_No_1"]);

            if (!dr.IsNull("Contact_No_2"))
                dealer.Contact_No_2 = Convert.ToString(dr["Contact_No_2"]);

            if (!dr.IsNull("Dealer_Percentage"))
                dealer.Dealer_Percentage = Convert.ToDecimal(dr["Dealer_Percentage"]);

            if (!dr.IsNull("Brand_Percentage"))
                dealer.Brand_Percentage = Convert.ToDecimal(dr["Brand_Percentage"]);

            dealer.Email = Convert.ToString(dr["Email"]);

            dealer.Is_Active = Convert.ToBoolean(dr["Is_Active"]);

            dealer.Created_On = Convert.ToDateTime(dr["Created_On"]);

            dealer.Created_By = Convert.ToInt32(dr["Created_By"]);

            dealer.Updated_On = Convert.ToDateTime(dr["Updated_On"]);

            dealer.Updated_By = Convert.ToInt32(dr["Updated_By"]);

            return dealer;
        }

        public OrdersInfo Get_Orders_By_Id(int Order_Id)
        {
            List<SqlParameter> sqlParam = new List<SqlParameter>();
            sqlParam.Add(new SqlParameter("@Order_Id", Order_Id));

            OrdersInfo order = new OrdersInfo();
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParam, StoreProcedures.Get_Order_By_Id.ToString(), CommandType.StoredProcedure);

            foreach (DataRow dr in dt.Rows)
            {
                order = Get_Orders_Values(dr);
            }
            return order;
        }

        private OrdersInfo Get_Orders_Values(DataRow dr)
        {
            OrdersInfo orders = new OrdersInfo();
            orders.Order_Id = Convert.ToInt32(dr["Order_Id"]);
            orders.Order_No = Convert.ToString(dr["Order_No"]);
            orders.Dealer_Id = Convert.ToInt32(dr["Dealer_Id"]);
            orders.Dealer_Name = Convert.ToString(dr["Dealer_Name"]);
            orders.Order_Date = Convert.ToDateTime(dr["Order_Date"]);
            orders.Shipping_Address = Convert.ToString(dr["Shipping_Address"]);
            orders.Discount = Convert.ToDecimal(dr["Discount"]);
            orders.Gross_Amount = Convert.ToDecimal(dr["Gross_Amount"]);
            orders.Service_Tax = Convert.ToDecimal(dr["Service_Tax"]);
            orders.Vat = Convert.ToDecimal(dr["Vat"]);
            orders.Swatch_Bharat_Tax = Convert.ToDecimal(dr["Swatch_Bharat_Tax"]);
            orders.Net_Amount = Convert.ToDecimal(dr["Net_Amount"]);
            orders.Status_Id = Convert.ToInt32(dr["Status"]);
            if (!dr.IsNull("Shipping_Date"))
                orders.Shipping_Date = Convert.ToDateTime(dr["Shipping_Date"]);
            if (orders.Status_Id == 1)
            {
                //orders.Status = "Order Received";
            }
            else if (orders.Status_Id == 2)
            {
                //orders.Status = "Order Confirmed";
            }
            else if (orders.Status_Id == 3)
            {
                //orders.Status = "Order Dispatched";
            }
            else
            {
                //orders.Status = "Order Delivered";
            }
            //if (!dr.IsNull("Invoice_Id"))
            //    orders.Invoice_Id = Convert.ToInt32(dr["Invoice_Id"]);
            //if (!dr.IsNull("Invoice_No"))
            //    orders.Invoice_No = Convert.ToString(dr["Invoice_No"]);
            orders.Created_On = Convert.ToDateTime(dr["Created_On"]);
            orders.Created_By = Convert.ToInt32(dr["Created_By"]);
            orders.Updated_On = Convert.ToDateTime(dr["Updated_On"]);
            orders.Updated_By = Convert.ToInt32(dr["Updated_By"]);             
            return orders;
        }

        public List<OrderItemInfo> Get_Order_Items_By_Order_Id(int Order_Id)
        {
            List<SqlParameter> sqlParam = new List<SqlParameter>();
            sqlParam.Add(new SqlParameter("@Order_Id", Order_Id));

            List<OrderItemInfo> orderitems = new List<OrderItemInfo>();
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParam, StoreProcedures.Get_Order_Items_By_Order_Id.ToString(), CommandType.StoredProcedure);
            foreach (DataRow dr in dt.Rows)
            {
                orderitems.Add(Get_Order_Item_Values(dr));
            }
            return orderitems;
        }

        private OrderItemInfo Get_Order_Item_Values(DataRow dr)
        {
            OrderItemInfo orderitem = new OrderItemInfo();
            orderitem.Order_Item_Id = Convert.ToInt32(dr["Order_Item_Id"]);
            orderitem.Order_Id = Convert.ToInt32(dr["Order_Id"]);
            orderitem.Product_Id = Convert.ToInt32(dr["Product_Id"]);
            orderitem.Product_Name = Convert.ToString(dr["Product_Name"]);
            orderitem.Product_Quantity = Convert.ToInt32(dr["Product_Quantity"]);
            orderitem.Product_Price = Convert.ToDecimal(dr["Product_Price"]);
            orderitem.Order_Status = Convert.ToString(dr["Order_Status"]);
            orderitem.Created_On = Convert.ToDateTime(dr["Created_On"]);
            orderitem.Created_By = Convert.ToInt32(dr["Created_By"]);
            orderitem.Updated_On = Convert.ToDateTime(dr["Updated_On"]);
            orderitem.Updated_By = Convert.ToInt32(dr["Updated_By"]);

            return orderitem;
        }

        public ProductInfo Get_Product_By_Id(int Product_Id)
        {
            List<SqlParameter> sqlParamList = new List<SqlParameter>();
            sqlParamList.Add(new SqlParameter("@Product_Id", Product_Id));

            ProductInfo product = new ProductInfo();
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParamList, StoreProcedures.Get_Product_By_Id_Sp.ToString(), CommandType.StoredProcedure);

            foreach (DataRow dr in dt.Rows)
            {
                product = Get_Product_Values(dr);
            }
            return product;
        }

        private ProductInfo Get_Product_Values(DataRow dr)
        {
            ProductInfo product = new ProductInfo();

            product.Product_Id = Convert.ToInt32(dr["Product_Id"]);
            product.Product_Name = Convert.ToString(dr["Product_Name"]);
            product.Product_Description = Convert.ToString(dr["Product_Description"]);
            product.Product_Price = Convert.ToDecimal(dr["Product_Price"]);
            product.Brand_Id = Convert.ToInt32(dr["Brand_Id"]);
            product.Brand_Name = Convert.ToString(dr["Brand_Name"]);
            product.Category_Id = Convert.ToInt32(dr["Category_Id"]);
            product.Category_Name = Convert.ToString(dr["Category_Name"]);
            product.SubCategory_Id = Convert.ToInt32(dr["SubCategory_Id"]);
            product.SubCategory_Name = Convert.ToString(dr["SubCategory_Name"]);
            product.Product_Image = Convert.ToString(dr["Image_Code"]);
            product.Is_Biddable = Convert.ToBoolean(dr["Is_Biddable"]);
            product.Is_Active = Convert.ToBoolean(dr["Is_Active"]);
            product.Created_On = Convert.ToDateTime(dr["Created_On"]);
            product.Created_By = Convert.ToInt32(dr["Created_By"]);
            product.Updated_On = Convert.ToDateTime(dr["Updated_On"]);
            product.Updated_By = Convert.ToInt32(dr["Updated_By"]);

            //if (!dr.IsNull("Local_Tax"))
            //    product.Local_Tax = Convert.ToDecimal(dr["Local_Tax"]);
            //if (!dr.IsNull("Export_Tax"))
            //    product.Export_Tax = Convert.ToDecimal(dr["Export_Tax"]);
            

            return product;
        }

        public TaxInfo Get_Tax_Product_By_Id(int Product_Id)
        {
            List<SqlParameter> sqlParamList = new List<SqlParameter>();
            sqlParamList.Add(new SqlParameter("@Product_Id", Product_Id));

            TaxInfo tax = new TaxInfo();
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParamList, StoreProcedures.Get_Tax_Product_By_Id_Sp.ToString(), CommandType.StoredProcedure);

            foreach (DataRow dr in dt.Rows)
            {
                tax=(Get_Tax_Product_By_Id(dr));
            }
            return tax;
        }

        private TaxInfo Get_Tax_Product_By_Id(DataRow dr)
        {
            TaxInfo tax = new TaxInfo();

            int Id = 0;
            decimal price = 0;

            Id = Convert.ToInt32(dr["Product_Id"]);

            price = Convert.ToDecimal(dr["Product_Price"]);

            if (!dr.IsNull("Local_Tax"))
                tax.Local_Tax = Convert.ToDecimal(dr["Local_Tax"]);
            if (!dr.IsNull("Export_Tax"))
                tax.Export_Tax = Convert.ToDecimal(dr["Export_Tax"]);

            //decimal ToatalPrice = Get_Local_Product_Total_Price_Id(Id);

            ////decimal Export_Price = Get_Export_Product_Total_Price_Id(Id);

            //tax.Local_Tax_Amount = ToatalPrice * tax.Local_Tax / 100;

            //tax.Export_Tax_Amount = ToatalPrice * tax.Export_Tax / 100;
            return tax;
        }

        public decimal Get_Local_Product_Total_Price_Id(int product_Id)
        {
            List<SqlParameter> sqlParamList = new List<SqlParameter>();
            sqlParamList.Add(new SqlParameter("@Product_Id", product_Id));

            decimal Total_Price = 0;

            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParamList, StoreProcedures.Get_Local_Tax_Product_By_Id_Sp.ToString(), CommandType.StoredProcedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    VendorInfo list = new VendorInfo();

                    if (!dr.IsNull("Price"))
                        Total_Price = Convert.ToInt32(dr["Price"]);
                }
            }

            return Total_Price;
        }

        //public decimal Get_Export_Product_Total_Price_Id(int product_Id)
        //{
        //    List<SqlParameter> sqlParamList = new List<SqlParameter>();
        //    sqlParamList.Add(new SqlParameter("@Product_Id", product_Id));

        //    decimal Total_Price = 0;

        //    DataTable dt = _sqlRepo.ExecuteDataTable(sqlParamList, StoreProcedures.Get_Expoert_Tax_Product_By_Id_Sp.ToString(), CommandType.StoredProcedure);

        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            VendorInfo list = new VendorInfo();

        //            if (!dr.IsNull("Price"))
        //                Total_Price = Convert.ToInt32(dr["Price"]);
        //        }
        //    }

        //    return Total_Price;
        //}

        public void Send_Invoice_Email(string Email_Id, InvoiceInfo invoice, OrdersInfo Order, DealerInfo Dealer, int request_Id, string request_Type, int entity_Id,VendorInfo vendor)
        {
            StringBuilder html = new StringBuilder();
            string subject = "Invoice For Order No : " + Order.Order_No;

            string Image_Path = System.Configuration.ConfigurationManager.AppSettings["BrandLogoPath"].ToString();

            #region Main Table

            html.Append("<html>");
            html.Append("<head><link rel='stylesheet' type='text/css' href='http://webrupee.com/font'>");
            html.Append("<style type='text/css'></style>");
            html.Append("</head>");
            html.Append("<body>");
          

            html.Append("<table cellspacing='0' cellpadding='0' style='width:712px;border:2px solid #ccc;' >");
            html.Append("<tbody>");

            html.Append("<tr>");
            html.Append("<td style='width: 233px; padding: 5px 5px 5px 5px;'><label style='font-weight: bold;'>SMS</label> </td>");
            html.Append("<td style='width: 233px; padding: 5px 5px 5px 5px;'></td>");
            html.Append("<td style='width: 233px; padding: 5px 5px 5px 5px;'><label style='font-weight: bold;'>Invoice Date :<label style='font-weight: bold;'> " + string.Format("{0:dd/MM/yyy}", DateTime.Now.ToString("dd/MM/yyyy")) + "</td>");
            html.Append("</tr>");

            html.Append("<tr>");
            html.Append("<td style='width: 233px; padding: 5px 5px 0 5px;'></td>");
            html.Append("<td style='width: 233px; padding: 5px 5px 0 5px;'></td>");
            html.Append("<td style='width: 233px; padding: 5px 5px 5px 5px;'></td>");
            html.Append("</tr>");

            html.Append("<tr>");
            html.Append("<td style='width: 233px; vertical-align: top; padding: 0 5px 5px 5px;'>");
            html.Append("<label style='font-weight: bold;'>From :</label>");
            html.Append("<label>");
            if (vendor.Vendor_Id != 0)
            {
                if (!string.IsNullOrEmpty(vendor.Address))
                {
                    html.Append("<br>" + vendor.Address + " ,");
                }
                if (!string.IsNullOrEmpty(vendor.City))
                {
                    html.Append("<br>" + vendor.City + " ,");
                    if (!string.IsNullOrEmpty(Convert.ToString(vendor.Pincode)))
                    {
                        html.Append(" " + Convert.ToString(vendor.Pincode) + " ,");
                    }
                }
                if (!string.IsNullOrEmpty(vendor.State_Name))
                {
                    html.Append("<br>" + vendor.State_Name + " ,");
                }

                if (!string.IsNullOrEmpty(vendor.Contact_No_1))
                {
                    html.Append("<br> Mobile :" + vendor.Contact_No_1 + " ,");
                }
                if (string.IsNullOrEmpty(vendor.Contact_No_1))
                {
                    html.Append("<br> Mobile :" + vendor.Contact_No_2 + " ,");
                }
                if (!string.IsNullOrEmpty(vendor.Contact_No_1))
                {
                    html.Append("<br> Email :" + vendor.Email + "");
                }
            }
            else
            {
                html.Append("<br>" + ConfigurationManager.AppSettings["CompanyName"].ToString());
                html.Append("<br>" + ConfigurationManager.AppSettings["CompanyAddress"].ToString());
                html.Append("<br>" + ConfigurationManager.AppSettings["CompanyTelephone"].ToString());
                html.Append("<br>" + ConfigurationManager.AppSettings["fromMailAddress"].ToString());
            }
            html.Append("</label>");
            html.Append("</td>");            

            html.Append("<td style='width: 233px; vertical-align: top; padding: 0 5px 5px 5px;'>");
            html.Append("<label style='font-weight: bold;'>To :</label>");
            html.Append("<label>");
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
            html.Append("</label>");
            html.Append("</td>");

            html.Append("<td style='width: 233px; vertical-align: top; padding: 0 5px 5px 5px;'>");
            html.Append("<label style='font-weight: bold;'>Details     :</label>");
            html.Append("<label>");
            html.Append("<br>Invoice No &nbsp; : " + invoice.Invoice_No);
            html.Append("<br>Order No &nbsp;&nbsp; : " + Order.Order_No);
            html.Append("<br>Order Date : " + string.Format("{0:d}", Order.Order_Date.ToString("dd/MM/yyyy")));
            html.Append("</label>");
            html.Append("</td>");
            html.Append("</tr>");

            html.Append("</tbody>");
            html.Append("</table>");
    
            #endregion

            #region Detail Table

            html.Append("<table cellspacing='1' cellpadding='6' style='width: 700px; border: 2px solid #ccc; background-color: #e2e2e2'>");
            html.Append("<tbody>");
            html.Append("<tr>");
            html.Append("<td colspan='2'>");            
            html.Append("<table cellspacing='1' cellpadding='6' width='700px' style='background-color:#e2e2e2'>");
            html.Append("<tbody>");
            html.Append("<tr>");
            html.Append("<th style='width:25px;text-align:center;height:30px'>Sr.No</th>");
            html.Append("<th style='width:250px'>Product name</th>");
            html.Append("<th style='width:50px;text-align:center'>Qty</th>");
            html.Append("<th style='width:80px;text-align:right'>Unit Price</th>");
            html.Append("<th style='text-align:right;width:100px'>Price</th>");
            html.Append("</tr>");

            int count = 0;
            if (Order != null)  
            {
                if (Order.OrderItems != null)
                {
                    foreach (var item in Order.OrderItems)
                    {
                        if(item.Product_Id==0)
                        {
                            item.Product_Id = item.Order_Item_Id;
                        }
                        ProductInfo ProductInfo = Get_Product_By_Id(item.Product_Id);
                        count++;
                        html.Append("<tr style='background-color:#fff'>");
                        html.Append("<td>" + count + "</td>");
                        html.Append("<td style='text-align:center'>" + ProductInfo.Product_Name + "</td>");
                        html.Append("<td style='text-align:center'>" + item.Product_Quantity + "</td>");
                        html.Append("<td style='text-align:right'>" + ProductInfo.Product_Price + "</td>");
                        html.Append("<td style='text-align:right'>" + item.Product_Price + "</td>");
                        html.Append("</tr>");
                    }
                }
            }

            if (Email_Id.Contains("yahoo"))
            {
                html.Append("<tr>");
                html.Append("<td colspan='4' style='text-align:right'>Total: </td>");
                html.Append("<td style='text-align:right'>&#8377. " + Order.Gross_Amount.ToString("0.00") + "</td>");
                html.Append("</tr>");
                html.Append("<tr>");
                html.Append("<td colspan='4' style='text-align:right'>Service Tax(%):</td>");
                html.Append("<td style='text-align:right'>&#8377. " + Order.Service_Tax.ToString("0.00") + "</td>");
                html.Append("</tr>");
                html.Append("<tr>");
                html.Append("<td colspan='4' style='text-align:right'>Grand Total:</td>");
                html.Append("<td style='text-align:right'>&#8377. " + Order.Net_Amount.ToString("0.00") + "</td>");
                html.Append("</tr>");
            }
            else
            {
                html.Append("<tr>");
                html.Append("<td colspan='4' style='text-align:right'>Total: </td>");
                html.Append("<td style='text-align:right'><img src='http://i.stack.imgur.com/nGbfO.png' width='8' height='10'>. " + Order.Gross_Amount.ToString("0.00") + "</td>");
                html.Append("</tr>");
                html.Append("<tr>");
                html.Append("<td colspan='4' style='text-align:right'>Service Tax(%):</td>");
                html.Append("<td style='text-align:right'><img src='http://i.stack.imgur.com/nGbfO.png' width='8' height='10'>. " + Order.Service_Tax.ToString("0.00") + "</td>");
                html.Append("</tr>");
                html.Append("<tr>");
                html.Append("<td colspan='4' style='text-align:right'>Grand Total:</td>");
                html.Append("<td style='text-align:right'><img src='http://i.stack.imgur.com/nGbfO.png' width='8' height='10'>. " + Order.Net_Amount.ToString("0.00") + "</td>");
                html.Append("</tr>");
            }

            html.Append("<br />");
            html.Append("<br />");
            html.Append("</tbody>");
            html.Append("</table>");
            html.Append("<br />");
            html.Append("<br />");

            html.Append("</body>");
            html.Append("</html>");
            #endregion

            if (request_Type != "Send Invoice")
            {
                CommonMethods.Insert_Email_Data(request_Id, request_Type, Email_Id, subject, html.ToString(), entity_Id);
            }
            else
            {
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


        #region Brand Invoice

        public List<InvoiceInfo> Get_Brand_Invoices(int Entity_Id, ref PaginationInfo Pager)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("@Entity_Id", Entity_Id));

            List<InvoiceInfo> invoices = new List<InvoiceInfo>();
            DataTable dt = _sqlRepo.ExecuteDataTable(paramList, StoreProcedures.Get_Brand_Invoices_Sp.ToString(), CommandType.StoredProcedure);
            foreach (DataRow dr in CommonMethods.GetRows(dt, ref Pager))
            {
                invoices.Add(Get_Brand_Invoice_Values(dr));
            }
            return invoices;
        }

        private InvoiceInfo Get_Brand_Invoice_Values(DataRow dr)
        {
            InvoiceInfo invoice = new InvoiceInfo();

            invoice.Invoice_Id = Convert.ToInt32(dr["Invoice_Id"]);
            invoice.Order_Id = Convert.ToInt32(dr["Order_Id"]);
            invoice.Order_No = Convert.ToString(dr["Order_No"]);
            invoice.Invoice_No = Convert.ToString(dr["Invoice_No"]);
            invoice.Invoice_Date = Convert.ToDateTime(dr["Invoice_Date"]);
            invoice.Amount = Convert.ToDecimal(dr["Amount"]);           
            invoice.Created_On = Convert.ToDateTime(dr["Created_On"]);
            invoice.Created_By = Convert.ToInt32(dr["Created_By"]);
            invoice.Updated_On = Convert.ToDateTime(dr["Updated_On"]);
            invoice.Updated_By = Convert.ToInt32(dr["Updated_By"]);
            return invoice;
        }

        public List<AutocompleteInfo> Get_Brand_Invoice_Autocomplete(string InvoiceNo, int Brand_Id)
        {
            List<AutocompleteInfo> autoList = new List<AutocompleteInfo>();
            List<SqlParameter> sqlparam = new List<SqlParameter>();
            sqlparam.Add(new SqlParameter("@Description", InvoiceNo == null ? System.String.Empty : InvoiceNo.Trim()));
            sqlparam.Add(new SqlParameter("@Brand_Id", Brand_Id));
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlparam, StoreProcedures.Get_Brand_Invoice_Autocomplete_Sp.ToString(), CommandType.StoredProcedure);
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
        #endregion

        #region Vendor Invoice

        public int Insert_Vendor_Invoice(InvoiceInfo invoice, int user_id,int entity_Id)
        {
            int invoiceid = 0;
            invoiceid = Convert.ToInt32(_sqlRepo.ExecuteScalerObj(Set_Values_In_Vendor_Invoice(invoice, user_id, entity_Id), StoreProcedures.Insert_Vendor_Invoice_Sp.ToString(), CommandType.StoredProcedure));
            return invoiceid;
        }

        private List<SqlParameter> Set_Values_In_Vendor_Invoice(InvoiceInfo invoice, int user_id, int entity_Id)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            sqlParams.Add(new SqlParameter("@Vendor_Order_Id", invoice.Order_Id));
            sqlParams.Add(new SqlParameter("@Invoice_No", invoice.Invoice_No));
            sqlParams.Add(new SqlParameter("@Invoice_Date", DateTime.Now));
            sqlParams.Add(new SqlParameter("@Role_Id", invoice.Role_Id));
            sqlParams.Add(new SqlParameter("@Entity_Id", invoice.Entity_Id));
            sqlParams.Add(new SqlParameter("@Amount", invoice.Amount));
            sqlParams.Add(new SqlParameter("@Dealer_Id", entity_Id));
            sqlParams.Add(new SqlParameter("@Created_On", DateTime.Now));
            sqlParams.Add(new SqlParameter("@Created_By", user_id));
            sqlParams.Add(new SqlParameter("@Updated_On", DateTime.Now));
            sqlParams.Add(new SqlParameter("@Updated_By", user_id));

            return sqlParams;
        }

        public InvoiceInfo Get_Vendor_Invoice_By_Id(int Invoice_Id,int entity_id)
        {
            List<SqlParameter> sqlParam = new List<SqlParameter>();
            sqlParam.Add(new SqlParameter("@Invoice_Id", Invoice_Id));
            sqlParam.Add(new SqlParameter("@Entity_id", entity_id));

            InvoiceInfo invoice = new InvoiceInfo();
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParam, StoreProcedures.Get_Vendor_Invoice_By_Id_Sp.ToString(), CommandType.StoredProcedure);
            foreach (DataRow dr in dt.Rows)
            {
                invoice = Get_Vendor_Invoice_Values(dr);
            }
            return invoice;
        }

        public InvoiceInfo Get_Dealer_Invoice_By_Id(int Invoice_Id, int entity_id)
        {
            List<SqlParameter> sqlParam = new List<SqlParameter>();
            sqlParam.Add(new SqlParameter("@Invoice_Id", Invoice_Id));

            InvoiceInfo invoice = new InvoiceInfo();
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParam, StoreProcedures.Get_Dealer_Invoice_By_Id_Sp.ToString(), CommandType.StoredProcedure);
            foreach (DataRow dr in dt.Rows)
            {
                invoice = Get_Vendor_Invoice_Values(dr);
            }
            return invoice;
        }

        private InvoiceInfo Get_Vendor_Invoice_Values(DataRow dr)
        {
            InvoiceInfo invoice = new InvoiceInfo();

            if (!dr.IsNull("Vendor_Invoice_Id"))
                invoice.Invoice_Id = Convert.ToInt32(dr["Vendor_Invoice_Id"]);

            if (!dr.IsNull("Vendor_Order_Id"))
                invoice.Order_Id = Convert.ToInt32(dr["Vendor_Order_Id"]);

            if (!dr.IsNull("Order_No"))
                invoice.Order_No = Convert.ToString(dr["Order_No"]);

            if (!dr.IsNull("Invoice_No"))
                invoice.Invoice_No = Convert.ToString(dr["Invoice_No"]);

            if (!dr.IsNull("Invoice_Date"))
                invoice.Invoice_Date = Convert.ToDateTime(dr["Invoice_Date"]);

            if (!dr.IsNull("Amount"))
                invoice.Amount = Convert.ToDecimal(dr["Amount"]);

            if (!dr.IsNull("Role_Id"))
                invoice.Role_Id = Convert.ToInt32(dr["Role_Id"]);

            if (!dr.IsNull("Entity_Id"))
                invoice.Entity_Id = Convert.ToInt32(dr["Entity_Id"]);

            if (!dr.IsNull("Created_On"))
                invoice.Created_On = Convert.ToDateTime(dr["Created_On"]);

            if (!dr.IsNull("Created_By"))
                invoice.Created_By = Convert.ToInt32(dr["Created_By"]);

            if (!dr.IsNull("Updated_On"))
                invoice.Updated_On = Convert.ToDateTime(dr["Updated_On"]);

            if (!dr.IsNull("Updated_By"))
                invoice.Updated_By = Convert.ToInt32(dr["Updated_By"]);

            return invoice;
        }

        public List<InvoiceInfo> Get_Vendor_Invoices_By_Id(int Invoice_Id,int entity_Id, ref PaginationInfo Pager)
        {
            List<SqlParameter> sqlParam = new List<SqlParameter>();
            sqlParam.Add(new SqlParameter("@Invoice_Id", Invoice_Id));
            sqlParam.Add(new SqlParameter("@Entity_id", entity_Id));
            List<InvoiceInfo> invoices = new List<InvoiceInfo>();
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParam, StoreProcedures.Get_Vendor_Invoice_By_Id_Sp.ToString(), CommandType.StoredProcedure);
            foreach (DataRow dr in CommonMethods.GetRows(dt, ref Pager))
            {
                invoices.Add(Get_Vendor_Invoices_Values(dr));
            }
            return invoices;
        }

        private InvoiceInfo Get_Vendor_Invoices_Values(DataRow dr)
        {
            InvoiceInfo invoice = new InvoiceInfo();

            if (!dr.IsNull("Vendor_Invoice_Id"))
                invoice.Invoice_Id = Convert.ToInt32(dr["Vendor_Invoice_Id"]);

            if (!dr.IsNull("Vendor_Order_Id"))
                invoice.Order_Id = Convert.ToInt32(dr["Vendor_Order_Id"]);

            if (!dr.IsNull("Order_No"))
                invoice.Order_No = Convert.ToString(dr["Order_No"]);

            if (!dr.IsNull("Invoice_No"))
                invoice.Invoice_No = Convert.ToString(dr["Invoice_No"]);

            if (!dr.IsNull("Invoice_Date"))
                invoice.Invoice_Date = Convert.ToDateTime(dr["Invoice_Date"]);

            if (!dr.IsNull("Amount"))
                invoice.Amount = Convert.ToDecimal(dr["Amount"]);

            if (!dr.IsNull("Role_Id"))
                invoice.Role_Id = Convert.ToInt32(dr["Role_Id"]);

            if (!dr.IsNull("Entity_Id"))
                invoice.Entity_Id = Convert.ToInt32(dr["Entity_Id"]);

            if (!dr.IsNull("Created_On"))
                invoice.Created_On = Convert.ToDateTime(dr["Created_On"]);

            if (!dr.IsNull("Created_By"))
                invoice.Created_By = Convert.ToInt32(dr["Created_By"]);

            if (!dr.IsNull("Updated_On"))
                invoice.Updated_On = Convert.ToDateTime(dr["Updated_On"]);

            if (!dr.IsNull("Updated_By"))
                invoice.Updated_By = Convert.ToInt32(dr["Updated_By"]);

            return invoice;
        }

        public List<InvoiceInfo> Get_Vendor_Invoices(ref PaginationInfo Pager, int entity_id)
        {
            List<InvoiceInfo> invoices = new List<InvoiceInfo>();
            List<SqlParameter> sqlParam = new List<SqlParameter>();
            sqlParam.Add(new SqlParameter("@Entity_id", entity_id));
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParam, StoreProcedures.Get_Vendor_Invoice_Sp.ToString(), CommandType.StoredProcedure);
            foreach (DataRow dr in CommonMethods.GetRows(dt, ref Pager))
            {
                invoices.Add(Get_Vendor_Invoice_Values(dr));
            }
            return invoices;
        }

        #endregion
    }
}
