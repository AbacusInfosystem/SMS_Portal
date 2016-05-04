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
    public class OrdersRepo
    {
        SQLHelper _sqlRepo;

        public OrdersRepo()
        {
            _sqlRepo = new SQLHelper();

        }

        //public void Insert_Orders(OrdersInfo orders)
        //{
        //    _sqlRepo.ExecuteNonQuery(Set_Values_In_Orders(orders), StoreProcedures.Insert_Orders_Sp.ToString(), CommandType.StoredProcedure);
        //}

        //public void Update_Orders(OrdersInfo orders)
        //{
        //    _sqlRepo.ExecuteNonQuery(Set_Values_In_Orders(orders), StoreProcedures.Update_Orders_Sp.ToString(), CommandType.StoredProcedure);
        //}

        private List<SqlParameter> Set_Values_In_Orders(OrdersInfo orders)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter("@Order_Id", orders.Order_Id));
            sqlParams.Add(new SqlParameter("@Order_No", orders.Order_No));
            sqlParams.Add(new SqlParameter("@Dealer_Id", orders.Dealer_Id));
            sqlParams.Add(new SqlParameter("@Order_Date", orders.Order_Date));
            sqlParams.Add(new SqlParameter("@Shipping_Address", orders.Shipping_Address));
            sqlParams.Add(new SqlParameter("@Discount", orders.Discount));
            sqlParams.Add(new SqlParameter("@Gross_Amount", orders.Gross_Amount));
            sqlParams.Add(new SqlParameter("@Service_Tax", orders.Service_Tax));
            sqlParams.Add(new SqlParameter("@Vat", orders.Vat));
            sqlParams.Add(new SqlParameter("@Swatch_Bharat_Tax", orders.Swatch_Bharat_Tax));
            sqlParams.Add(new SqlParameter("@Net_Amount", orders.Net_Amount));
            sqlParams.Add(new SqlParameter("@Created_On", orders.Created_On));
            sqlParams.Add(new SqlParameter("@Created_By", orders.Created_By));
            sqlParams.Add(new SqlParameter("@Updated_On", orders.Updated_On));
            sqlParams.Add(new SqlParameter("@Updated_By", orders.Updated_By));
            return sqlParams;
        }

        public List<OrdersInfo> Get_Orders(ref PaginationInfo Pager)
        {
            List<OrdersInfo> orderss = new List<OrdersInfo>();
            DataTable dt = _sqlRepo.ExecuteDataTable(null, StoreProcedures.Get_Orders.ToString(), CommandType.StoredProcedure);

            foreach (DataRow dr in CommonMethods.GetRows(dt, ref Pager))
            {
                orderss.Add(Get_Orders_Values(dr));
            }
            return orderss;
        }

        public OrdersInfo Get_Orders_By_Id(int Order_Id)
        {
            List<SqlParameter> sqlParam = new List<SqlParameter>();
            sqlParam.Add(new SqlParameter("@Order_Id", Order_Id));

            OrdersInfo orders = new OrdersInfo();
            OrderItemInfo orderitem = new OrderItemInfo();
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParam, StoreProcedures.Get_Order_By_Id.ToString(), CommandType.StoredProcedure);

            foreach (DataRow dr in dt.Rows)
            {
                orders = Get_Orders_Values(dr);
            }
            return orders;
        }

        public List<OrdersInfo> Get_Orders_Data_By_Id(int Order_Id, ref PaginationInfo Pager)
        {
            List<SqlParameter> sqlParam = new List<SqlParameter>();
            sqlParam.Add(new SqlParameter("@Order_Id", Order_Id));

            List<OrdersInfo> orders = new List<OrdersInfo>();
            OrderItemInfo orderitem = new OrderItemInfo();
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParam, StoreProcedures.Get_Order_By_Id.ToString(), CommandType.StoredProcedure);

            foreach (DataRow dr in CommonMethods.GetRows(dt, ref Pager))
            {
                orders.Add(Get_Orders_Values(dr));
            }
            return orders;
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
            orders.Shipping_Date = Convert.ToDateTime(dr["Shipping_Date"]);
            if (orders.Status_Id==1)
            {
                orders.Status = "Order Received";
            }
            else if (orders.Status_Id == 2)
            {
                orders.Status = "Order Confirmed";
            }
            else if (orders.Status_Id == 3)
            {
                orders.Status = "Order Dispatched";
            }
            else
            {
                orders.Status = "Order Delivered";
            }
            if (!dr.IsNull("Invoice_Id"))
            orders.Invoice_Id = Convert.ToInt32(dr["Invoice_Id"]);
            if (!dr.IsNull("Invoice_No"))
            orders.Invoice_No = Convert.ToString(dr["Invoice_No"]);
            orders.Created_On = Convert.ToDateTime(dr["Created_On"]);
            orders.Created_By = Convert.ToInt32(dr["Created_By"]);
            orders.Updated_On = Convert.ToDateTime(dr["Updated_On"]);
            orders.Updated_By = Convert.ToInt32(dr["Updated_By"]);
            //orders.OrderItems = Get_Order_Items_By_Order_Id(orders.Order_Id); 
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

        public List<OrdersInfo> Get_Subcategories_By_Id(int order_Id, ref PaginationInfo pager)
        {
            List<OrdersInfo> orders = new List<OrdersInfo>();

            List<SqlParameter> sqlparam = new List<SqlParameter>();

            sqlparam.Add(new SqlParameter("@Order_Id", order_Id));

            DataTable dt = _sqlRepo.ExecuteDataTable(sqlparam, StoreProcedures.Get_Sales_OrderBy_Id_Sp.ToString(), CommandType.StoredProcedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in CommonMethods.GetRows(dt, ref pager))
                {
                    orders.Add(Get_Orders_Values(dr));
                }
            }

            return orders;
        }

        public void Update_Order_Status(OrdersInfo order)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            sqlParams.Add(new SqlParameter("@Order_Id", order.Order_Id));

            sqlParams.Add(new SqlParameter("@Status_Id", order.Status_Id));

            sqlParams.Add(new SqlParameter("@Shipping_Date", order.Shipping_Date));

            _sqlRepo.ExecuteDataTable(sqlParams, StoreProcedures.Update_Order_Status_Sp.ToString(), CommandType.StoredProcedure);
        }

        public void Send_Order_Status_Notification(string email_Id, OrdersInfo order)
        {
            if (order.Status_Id == 1)
            {
                order.Status = "Received";
            }
            else if (order.Status_Id == 2)
            {
                order.Status = "Confirmed";
            }
            else if (order.Status_Id == 3)
            {
                order.Status = "Dispatched";
            }
            else
            {
                order.Status = "Delivered";
            }

            StringBuilder html = new StringBuilder();

            string subject = " Order Status ";

            html.Append("<table width=1000px border=1 cellspacing=2 cellpadding=2 align=center bgcolor=White dir=ltr rules=all style=border-width: thin; line-height: normal; vertical-align: baseline; text-align: center; font-family: Calibri; font-size: medium; font-weight: normal; font-style: normal; font-variant: normal>");

            html.Append("<tr>");

            html.Append("<td align='right'>");

            html.Append("<h3>Order No : " + order.Order_No + "</h3>\n");

            html.Append("</td>");

            html.Append("</tr>");

            html.Append("<tr>");

            html.Append("<td align='center'>");

            html.Append("<p> Your order is : " + order.Status + ". <br/> We will inform you once the items in your order have been shipped. </p>");

            html.Append("</td>");

            html.Append("</tr>");

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

        public DealerInfo Get_Dealer_Data(int Dealer_Id)
        {
            List<SqlParameter> sqlParamList = new List<SqlParameter>();
            sqlParamList.Add(new SqlParameter("@Dealer_Id", Dealer_Id));

            DealerInfo dealer = new DealerInfo();
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParamList, StoreProcedures.Get_Dealer_Data_Sp.ToString(), CommandType.StoredProcedure);
            foreach (DataRow dr in dt.Rows)
            {
                dealer = Get_Dealer_Data_Values(dr);
            }
            return dealer;
        }

        private DealerInfo Get_Dealer_Data_Values(DataRow dr)
        {
            DealerInfo dealer = new DealerInfo();

            dealer.Dealer_Id = Convert.ToInt32(dr["Dealer_Id"]);
            dealer.Dealer_Name = Convert.ToString(dr["Dealer_Name"]);
            dealer.Address = Convert.ToString(dr["Address"]);
            dealer.City = Convert.ToString(dr["City"]);
            dealer.State = Convert.ToInt32(dr["State"]);
            dealer.State_Name = Convert.ToString(dr["State_Name"]);
            dealer.Pincode = Convert.ToInt32(dr["Pincode"]);
            dealer.Contact_No_1 = Convert.ToString(dr["Contact_No_1"]);
            dealer.Contact_No_2 = Convert.ToString(dr["Contact_No_2"]);
            dealer.Email = Convert.ToString(dr["Email"]);
            return dealer;
        }

        public List<AutocompleteInfo> Get_Order_No_Autocomplete(string order_No)
        {
            List<AutocompleteInfo> autoList = new List<AutocompleteInfo>();
            List<SqlParameter> sqlparam = new List<SqlParameter>();
            sqlparam.Add(new SqlParameter("@Description", order_No == null ? System.String.Empty : order_No.Trim()));
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlparam, StoreProcedures.Get_Order_No_Autocomplete_Sp.ToString(), CommandType.StoredProcedure);
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
         
    }
}
