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

        public int Insert_Orders(OrdersInfo orders, out string order_Ids, out decimal order_Amount)
        {
            order_Ids = string.Empty;

            order_Amount = 0;

            int vendorOrderId = 0;

            decimal Gross_Amount = 0;

            decimal Net_Amount = 0;

            int orderId = _sqlRepo.ExecuteNonQuery(Set_Values_In_Orders(orders), StoreProcedures.Insert_Orders_Sp.ToString(), CommandType.StoredProcedure, "@Order_Id");

            var DistinctItems = orders.OrderItems.GroupBy(x => x.Vendor_Id).Select(y => y.First());

            foreach (var orderItem in orders.OrderItems)
            {
                orders.Order_Id = orderId;
                orderItem.Order_Id = orderId;
                orderItem.Created_By = orders.Created_By;
                orderItem.Created_On = orders.Created_On;
                orderItem.Updated_By = orders.Updated_By;
                orderItem.Updated_On = orders.Updated_On;                
                _sqlRepo.ExecuteNonQuery(Set_Values_In_Order_Item(orderItem), StoreProcedures.Insert_Order_Item_Sp.ToString(), CommandType.StoredProcedure);

                List<SqlParameter> sqlParam = new List<SqlParameter>();
                sqlParam.Add(new SqlParameter("@Order_Id", orders.Order_Id));
                sqlParam.Add(new SqlParameter("@Vendor_Id", orderItem.Vendor_Id));
                List<OrdersInfo> orderss = new List<OrdersInfo>();
                DataTable dt = _sqlRepo.ExecuteDataTable(sqlParam, StoreProcedures.Get_Vendor_Order_Id_Sp.ToString(), CommandType.StoredProcedure);

                if(dt.Rows.Count==0)
                {
                    vendorOrderId = _sqlRepo.ExecuteNonQuery(Set_Values_In_Vendor_Specific_Orders(orders, orderItem.Vendor_Id, orderItem.Product_GrossPrice, orderItem.Product_NetPrice), StoreProcedures.Insert_Vendor_Specific_Sales_Order_Sp.ToString(), CommandType.StoredProcedure, "@Vendor_Order_Id");

                    order_Ids = order_Ids+vendorOrderId + ",";
                }
                else
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.AsEnumerable().FirstOrDefault();

                        if (dr != null)
                        {
                            vendorOrderId = Convert.ToInt32(dr["Vendor_Order_Id"]);

                            Gross_Amount = Convert.ToDecimal(dr["Gross_Amount"]);

                            Net_Amount = Convert.ToDecimal(dr["Net_Amount"]);

                            order_Amount = Net_Amount;

                            orderItem.Product_GrossPrice = orderItem.Product_GrossPrice + Gross_Amount;

                            orderItem.Product_NetPrice = orderItem.Product_NetPrice + Net_Amount;

                            _sqlRepo.ExecuteNonQuery(Update_Set_Values_In_Vendor_Specific_Orders(orders, orderItem.Vendor_Id, orderItem.Product_GrossPrice, orderItem.Product_NetPrice), StoreProcedures.Update_Vendor_Order_Amount_Sp.ToString(), CommandType.StoredProcedure);
                        }
                    }
                }
                
                orderItem.Order_Vendor_Id = vendorOrderId;
                orderItem.Created_By = orders.Created_By;
                orderItem.Created_On = orders.Created_On;
                orderItem.Updated_By = orders.Updated_By;
                orderItem.Updated_On = orders.Updated_On;
                _sqlRepo.ExecuteNonQuery(Set_Values_In_Order_Vendor_Specific__Item(orderItem), StoreProcedures.Insert_Vendor_Specific_Sales_Order_Items_Sp.ToString(), CommandType.StoredProcedure);

            }

            return orderId;
        }

        //public void Update_Orders(OrdersInfo orders)
        //{
        //    _sqlRepo.ExecuteNonQuery(Set_Values_In_Orders(orders), StoreProcedures.Update_Orders_Sp.ToString(), CommandType.StoredProcedure);
        //}

        private List<SqlParameter> Set_Values_In_Orders(OrdersInfo orders)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();
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
            sqlParams.Add(new SqlParameter("@Status", Convert.ToString(orders.Status)));
            sqlParams.Add(new SqlParameter("@Shipping_Date", orders.Shipping_Date));
            sqlParams.Add(new SqlParameter("@Created_On", orders.Created_On));
            sqlParams.Add(new SqlParameter("@Created_By", orders.Created_By));
            sqlParams.Add(new SqlParameter("@Updated_On", orders.Updated_On));
            sqlParams.Add(new SqlParameter("@Updated_By", orders.Updated_By));
            return sqlParams;
        }

        private List<SqlParameter> Set_Values_In_Order_Item(OrderItemInfo orderItem)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter("@Order_Id", orderItem.Order_Id));
            sqlParams.Add(new SqlParameter("@Product_Id", orderItem.Product_Id));
            sqlParams.Add(new SqlParameter("@Product_Quantity", orderItem.Product_Quantity));
            sqlParams.Add(new SqlParameter("@Product_Price", orderItem.Product_Price));
            sqlParams.Add(new SqlParameter("@Order_Status", orderItem.Order_Status));
            sqlParams.Add(new SqlParameter("@Created_On", orderItem.Created_On));
            sqlParams.Add(new SqlParameter("@Created_By", orderItem.Created_By));
            sqlParams.Add(new SqlParameter("@Updated_On", orderItem.Updated_On));
            sqlParams.Add(new SqlParameter("@Updated_By", orderItem.Updated_By));
            return sqlParams;
        }

        private List<SqlParameter> Set_Values_In_Vendor_Specific_Orders(OrdersInfo orders,int vendor_Id,decimal product_Gross_Price,decimal product_Net_Price)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter("@Order_Id", orders.Order_Id));
            sqlParams.Add(new SqlParameter("@Vendor_Id", vendor_Id));
            sqlParams.Add(new SqlParameter("@Dealer_Id", orders.Dealer_Id));
            sqlParams.Add(new SqlParameter("@Order_Date", orders.Order_Date));
            sqlParams.Add(new SqlParameter("@Gross_Amount", product_Gross_Price));
            sqlParams.Add(new SqlParameter("@Net_Amount", product_Net_Price));
            sqlParams.Add(new SqlParameter("@Status", Convert.ToString(orders.Status)));
            sqlParams.Add(new SqlParameter("@Shipping_Date", orders.Shipping_Date));
            sqlParams.Add(new SqlParameter("@Created_On", orders.Created_On));
            sqlParams.Add(new SqlParameter("@Created_By", orders.Created_By));
            sqlParams.Add(new SqlParameter("@Updated_On", orders.Updated_On));
            sqlParams.Add(new SqlParameter("@Updated_By", orders.Updated_By));
            return sqlParams;
        }

        private List<SqlParameter> Update_Set_Values_In_Vendor_Specific_Orders(OrdersInfo orders, int vendor_Id, decimal product_Gross_Price, decimal product_Net_Price)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            sqlParams.Add(new SqlParameter("@Order_Id", orders.Order_Id));
            sqlParams.Add(new SqlParameter("@Vendor_Id", vendor_Id));

            sqlParams.Add(new SqlParameter("@Gross_Amount", product_Gross_Price));
            sqlParams.Add(new SqlParameter("@Net_Amount", product_Net_Price));

            return sqlParams;
        }

        private List<SqlParameter> Set_Values_In_Order_Vendor_Specific__Item(OrderItemInfo orderItem)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter("@Vendor_Order_Id", orderItem.Order_Vendor_Id));
            sqlParams.Add(new SqlParameter("@Order_Item_Id", orderItem.Product_Id));
            sqlParams.Add(new SqlParameter("@Order_Item_Quantity", orderItem.Product_Quantity));
            sqlParams.Add(new SqlParameter("@Order_Status", orderItem.Order_Status));
            sqlParams.Add(new SqlParameter("@Created_On", orderItem.Created_On));
            sqlParams.Add(new SqlParameter("@Created_By", orderItem.Created_By));
            sqlParams.Add(new SqlParameter("@Updated_On", orderItem.Updated_On));
            sqlParams.Add(new SqlParameter("@Updated_By", orderItem.Updated_By));
            return sqlParams;
        }

        public List<OrdersInfo> Get_Orders(ref PaginationInfo Pager, int dealer_Id)
        {
            List<SqlParameter> sqlParam = new List<SqlParameter>();
            sqlParam.Add(new SqlParameter("@Dealer_Id", dealer_Id));

            List<OrdersInfo> orderss = new List<OrdersInfo>();
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParam, StoreProcedures.Get_Orders.ToString(), CommandType.StoredProcedure);

            foreach (DataRow dr in CommonMethods.GetRows(dt, ref Pager))
            {
                orderss.Add(Get_Orders_Values(dr));
            }
            return orderss;
        }

        public List<OrdersInfo> Get_All_Orders(ref PaginationInfo Pager)
        {
            List<SqlParameter> sqlParam = new List<SqlParameter>();

            List<OrdersInfo> orderss = new List<OrdersInfo>();
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParam, StoreProcedures.Get_All_Orders.ToString(), CommandType.StoredProcedure);

            foreach (DataRow dr in CommonMethods.GetRows(dt, ref Pager))
            {
                orderss.Add(Get_Orders_Values(dr));
            }
            return orderss;
        }

        public List<OrdersInfo> Get_Vendor_Orders(ref PaginationInfo Pager, int vendor_Id)
        {
            List<SqlParameter> sqlParam = new List<SqlParameter>();
            sqlParam.Add(new SqlParameter("@Vendor_Id", vendor_Id));

            List<OrdersInfo> orderss = new List<OrdersInfo>();
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParam, StoreProcedures.Get_Vendor_Orders.ToString(), CommandType.StoredProcedure);

            foreach (DataRow dr in CommonMethods.GetRows(dt, ref Pager))
            {
                orderss.Add(Get_Vendor_Orders(dr));
            }
            return orderss;
        }

        public OrdersInfo Get_Orders_By_Id(int order_Id)
        {
            List<SqlParameter> sqlParam = new List<SqlParameter>();
            sqlParam.Add(new SqlParameter("@Order_Id", order_Id));

            OrdersInfo order = new OrdersInfo();             
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParam, StoreProcedures.Get_Order_By_Id.ToString(), CommandType.StoredProcedure);

            foreach (DataRow dr in dt.Rows)
            {
                order = Get_Orders_Values(dr);
            }

            return order;
            }

        public OrdersInfo Get_Dealer_Orders_By_Id(int order_Id)
        {
            List<SqlParameter> sqlParam = new List<SqlParameter>();
            sqlParam.Add(new SqlParameter("@Order_Id", order_Id));

            OrdersInfo order = new OrdersInfo();
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParam, StoreProcedures.Get_Dealer_Order_By_Id.ToString(), CommandType.StoredProcedure);

            foreach (DataRow dr in dt.Rows)
            {
                order = Get_Orders_Values(dr);
            }

            return order;
        }


        public OrdersInfo Get_Vendor_Orders_By_Id(int vendor_Order_Id)
        {
            List<SqlParameter> sqlParam = new List<SqlParameter>();
            sqlParam.Add(new SqlParameter("@Vendor_Order_Id", vendor_Order_Id));

            OrdersInfo order = new OrdersInfo();
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParam, StoreProcedures.Get_Vendor_Order_By_Id.ToString(), CommandType.StoredProcedure);

            foreach (DataRow dr in dt.Rows)
            {
                order = Get_Vendor_Orders_Values(dr);
            }

            return order;
        }

        public List<OrdersInfo> Get_Orders_Data_By_Id(int order_Id, ref PaginationInfo pager)
        {
            List<SqlParameter> sqlParam = new List<SqlParameter>();
            sqlParam.Add(new SqlParameter("@Order_Id", order_Id));

            List<OrdersInfo> orders = new List<OrdersInfo>();
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParam, StoreProcedures.Get_Order_By_Id.ToString(), CommandType.StoredProcedure);

            foreach (DataRow dr in CommonMethods.GetRows(dt, ref pager))
            {
                orders.Add(Get_Orders_Values(dr));
            }

            return orders;
        }

        public List<OrdersInfo> Get_Dealer_Orders_Data_By_Id(int order_Id, ref PaginationInfo pager)
        {
            List<SqlParameter> sqlParam = new List<SqlParameter>();
            sqlParam.Add(new SqlParameter("@Order_Id", order_Id));

            List<OrdersInfo> orders = new List<OrdersInfo>();
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParam, StoreProcedures.Get_Order_By_Id.ToString(), CommandType.StoredProcedure);

            foreach (DataRow dr in CommonMethods.GetRows(dt, ref pager))
            {
                orders.Add(Get_Orders_Values(dr));
            }

            return orders;
        }

        public List<OrdersInfo> Get_Orders_Data_By_Dates(DateTime fromDate,DateTime toDate, ref PaginationInfo pager)
        {
            List<SqlParameter> sqlParam = new List<SqlParameter>();
            sqlParam.Add(new SqlParameter("@FromDate", fromDate));
            sqlParam.Add(new SqlParameter("@ToDate", toDate));

            List<OrdersInfo> orders = new List<OrdersInfo>();
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParam, StoreProcedures.Get_Orders_By_Dates.ToString(), CommandType.StoredProcedure);

            foreach (DataRow dr in CommonMethods.GetRows(dt, ref pager))
            {
                orders.Add(Get_Orders_Values(dr));
            }

            return orders;
        }

        public List<OrdersInfo> Get_Orders_Data_By_Status(int Status, ref PaginationInfo pager, int role_Id, int entity_Id)
        {
            List<SqlParameter> sqlParam = new List<SqlParameter>();
            sqlParam.Add(new SqlParameter("@Role_Id", role_Id));
            sqlParam.Add(new SqlParameter("@Entity_Id", entity_Id));  
            sqlParam.Add(new SqlParameter("@Status", Status));             

            List<OrdersInfo> orders = new List<OrdersInfo>();
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParam, StoreProcedures.Get_Order_By_Id_Status.ToString(), CommandType.StoredProcedure);

            foreach (DataRow dr in CommonMethods.GetRows(dt, ref pager))
            {
                orders.Add(Get_Vendor_Orders_Values(dr));
            }

            return orders;
        }

        private OrdersInfo Get_Vendor_Orders_Values(DataRow dr)
        {
            OrdersInfo orders = new OrdersInfo();

            if (!dr.IsNull("Vendor_Order_Id"))
                orders.Vendor_Order_Id = Convert.ToInt32(dr["Vendor_Order_Id"]);

            if (!dr.IsNull("Order_Id"))
                orders.Order_Id = Convert.ToInt32(dr["Order_Id"]);

            if (!dr.IsNull("Vendor_Id"))
                orders.Vendor_Id = Convert.ToInt32(dr["Vendor_Id"]);

            if (!dr.IsNull("Order_No"))
                orders.Order_No = Convert.ToString(dr["Order_No"]);

            if (!dr.IsNull("Dealer_Id"))
                orders.Dealer_Id = Convert.ToInt32(dr["Dealer_Id"]);

            if (!dr.IsNull("Dealer_name"))
                orders.Dealer_Name = Convert.ToString(dr["Dealer_name"]);

            if (!dr.IsNull("Order_Date"))
                orders.Order_Date = Convert.ToDateTime(dr["Order_Date"]);

            if (!dr.IsNull("Shipping_Address"))
                orders.Shipping_Address = Convert.ToString(dr["Shipping_Address"]);

            if (!dr.IsNull("Discount"))
                orders.Discount = Convert.ToDecimal(dr["Discount"]);

            if (!dr.IsNull("Gross_Amount"))
                orders.Gross_Amount = Convert.ToDecimal(dr["Gross_Amount"]);

            if (!dr.IsNull("Service_Tax"))
                orders.Service_Tax = Convert.ToDecimal(dr["Service_Tax"]);

            if (!dr.IsNull("Vat"))
                orders.Vat = Convert.ToDecimal(dr["Vat"]);

            if (!dr.IsNull("Swatch_Bharat_Tax"))
                orders.Swatch_Bharat_Tax = Convert.ToDecimal(dr["Swatch_Bharat_Tax"]);

            if (!dr.IsNull("Net_Amount"))
                orders.Net_Amount = Convert.ToDecimal(dr["Net_Amount"]);

            if (!dr.IsNull("Status"))
                orders.Status_Id = Convert.ToInt32(dr["Status"]);

            if (!dr.IsNull("Shipping_Date"))
                orders.Shipping_Date = Convert.ToDateTime(dr["Shipping_Date"]);

            if (orders.Status_Id == 1)
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

            //if (!dr.IsNull("Invoice_Id"))
            //orders.Invoice_Id = Convert.ToInt32(dr["Invoice_Id"]);

            //if (!dr.IsNull("Invoice_No"))
            //orders.Invoice_No = Convert.ToString(dr["Invoice_No"]);

            orders.Created_On = Convert.ToDateTime(dr["Created_On"]);
            orders.Created_By = Convert.ToInt32(dr["Created_By"]);
            orders.Updated_On = Convert.ToDateTime(dr["Updated_On"]);
            orders.Updated_By = Convert.ToInt32(dr["Updated_By"]);
            //orders.OrderItems = Get_Order_Items_By_Order_Id(orders.Order_Id); 
            return orders;
        }

        private OrdersInfo Get_Orders_Values(DataRow dr)
        {
            OrdersInfo orders = new OrdersInfo();

            if (!dr.IsNull("Order_Id"))
            orders.Order_Id = Convert.ToInt32(dr["Order_Id"]);

            if (!dr.IsNull("Order_No"))
            orders.Order_No = Convert.ToString(dr["Order_No"]);

            if (!dr.IsNull("Dealer_Id"))
            orders.Dealer_Id = Convert.ToInt32(dr["Dealer_Id"]);

            if (!dr.IsNull("Dealer_name"))
                orders.Dealer_Name = Convert.ToString(dr["Dealer_name"]);

            if (!dr.IsNull("Order_Date"))
            orders.Order_Date = Convert.ToDateTime(dr["Order_Date"]);

            if (!dr.IsNull("Shipping_Address"))
            orders.Shipping_Address = Convert.ToString(dr["Shipping_Address"]);

            if (!dr.IsNull("Discount"))
            orders.Discount = Convert.ToDecimal(dr["Discount"]);

            if (!dr.IsNull("Gross_Amount"))
            orders.Gross_Amount = Convert.ToDecimal(dr["Gross_Amount"]);

            if (!dr.IsNull("Service_Tax"))
            orders.Service_Tax = Convert.ToDecimal(dr["Service_Tax"]);

            if (!dr.IsNull("Vat"))
            orders.Vat = Convert.ToDecimal(dr["Vat"]);

            if (!dr.IsNull("Swatch_Bharat_Tax"))
            orders.Swatch_Bharat_Tax = Convert.ToDecimal(dr["Swatch_Bharat_Tax"]);

            if (!dr.IsNull("Net_Amount"))
            orders.Net_Amount = Convert.ToDecimal(dr["Net_Amount"]);

            if (!dr.IsNull("Status"))
            orders.Status_Id = Convert.ToInt32(dr["Status"]);

            if (!dr.IsNull("Shipping_Date"))
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

            //if (!dr.IsNull("Invoice_Id"))
            //orders.Invoice_Id = Convert.ToInt32(dr["Invoice_Id"]);

            //if (!dr.IsNull("Invoice_No"))
            //orders.Invoice_No = Convert.ToString(dr["Invoice_No"]);

            orders.Created_On = Convert.ToDateTime(dr["Created_On"]);
            orders.Created_By = Convert.ToInt32(dr["Created_By"]);
            orders.Updated_On = Convert.ToDateTime(dr["Updated_On"]);
            orders.Updated_By = Convert.ToInt32(dr["Updated_By"]);
            //orders.OrderItems = Get_Order_Items_By_Order_Id(orders.Order_Id); 
            return orders;
        }

        private OrdersInfo Get_Vendor_Orders(DataRow dr)
        {
            OrdersInfo orders = new OrdersInfo();

            if (!dr.IsNull("Vendor_Order_Id"))
                orders.Vendor_Order_Id = Convert.ToInt32(dr["Vendor_Order_Id"]);

            if (!dr.IsNull("Order_Id"))
                orders.Order_Id = Convert.ToInt32(dr["Order_Id"]);

            if (!dr.IsNull("Order_No"))
                orders.Order_No = Convert.ToString(dr["Order_No"]);

            if (!dr.IsNull("Dealer_Id"))
                orders.Dealer_Id = Convert.ToInt32(dr["Dealer_Id"]);

            if (!dr.IsNull("Dealer_name"))
                orders.Dealer_Name = Convert.ToString(dr["Dealer_name"]);

            if (!dr.IsNull("Order_Date"))
                orders.Order_Date = Convert.ToDateTime(dr["Order_Date"]);

            if (!dr.IsNull("Shipping_Address"))
                orders.Shipping_Address = Convert.ToString(dr["Shipping_Address"]);

            if (!dr.IsNull("Discount"))
                orders.Discount = Convert.ToDecimal(dr["Discount"]);

            if (!dr.IsNull("Gross_Amount"))
                orders.Gross_Amount = Convert.ToDecimal(dr["Gross_Amount"]);

            if (!dr.IsNull("Service_Tax"))
                orders.Service_Tax = Convert.ToDecimal(dr["Service_Tax"]);

            if (!dr.IsNull("Vat"))
                orders.Vat = Convert.ToDecimal(dr["Vat"]);

            if (!dr.IsNull("Swatch_Bharat_Tax"))
                orders.Swatch_Bharat_Tax = Convert.ToDecimal(dr["Swatch_Bharat_Tax"]);

            if (!dr.IsNull("Net_Amount"))
                orders.Net_Amount = Convert.ToDecimal(dr["Net_Amount"]);

            if (!dr.IsNull("Status"))
                orders.Status_Id = Convert.ToInt32(dr["Status"]);

            if (!dr.IsNull("Shipping_Date"))
                orders.Shipping_Date = Convert.ToDateTime(dr["Shipping_Date"]);

            if (orders.Status_Id == 1)
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

            //if (!dr.IsNull("Invoice_Id"))
            //orders.Invoice_Id = Convert.ToInt32(dr["Invoice_Id"]);

            //if (!dr.IsNull("Invoice_No"))
            //orders.Invoice_No = Convert.ToString(dr["Invoice_No"]);

            orders.Created_On = Convert.ToDateTime(dr["Created_On"]);
            orders.Created_By = Convert.ToInt32(dr["Created_By"]);
            orders.Updated_On = Convert.ToDateTime(dr["Updated_On"]);
            orders.Updated_By = Convert.ToInt32(dr["Updated_By"]);
            //orders.OrderItems = Get_Order_Items_By_Order_Id(orders.Order_Id); 
            return orders;
        }

        public List<OrderItemInfo> Get_Order_Items_By_Order_Id(int order_Id)
        {
            List<SqlParameter> sqlParam = new List<SqlParameter>();
            sqlParam.Add(new SqlParameter("@Order_Id", order_Id));

            List<OrderItemInfo> orderitems = new List<OrderItemInfo>();
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParam, StoreProcedures.Get_Order_Items_By_Order_Id.ToString(), CommandType.StoredProcedure);
            foreach (DataRow dr in dt.Rows)
            {
                orderitems.Add(Get_Order_Item_Values(dr));
            }
            return orderitems;
        }

        public List<OrderItemInfo> Get_Dealer_Order_Items_By_Order_Id(int order_Id)
        {
            List<SqlParameter> sqlParam = new List<SqlParameter>();
            sqlParam.Add(new SqlParameter("@Order_Id", order_Id));

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

            if (!dr.IsNull("Order_Item_Id"))
            orderitem.Order_Item_Id = Convert.ToInt32(dr["Order_Item_Id"]);

            if (!dr.IsNull("Order_Id"))
            orderitem.Order_Id = Convert.ToInt32(dr["Order_Id"]);

            if (!dr.IsNull("Product_Id"))
            orderitem.Product_Id = Convert.ToInt32(dr["Product_Id"]);

            if (!dr.IsNull("Product_Name"))
            orderitem.Product_Name = Convert.ToString(dr["Product_Name"]);

            if (!dr.IsNull("Product_Quantity"))
            orderitem.Product_Quantity = Convert.ToInt32(dr["Product_Quantity"]);

            if (!dr.IsNull("Product_Price"))
            orderitem.Product_Price = Convert.ToDecimal(dr["Product_Price"]);

            if (!dr.IsNull("Order_Status"))
            orderitem.Order_Status = Convert.ToString(dr["Order_Status"]);

            orderitem.Created_On = Convert.ToDateTime(dr["Created_On"]);
            orderitem.Created_By = Convert.ToInt32(dr["Created_By"]);
            orderitem.Updated_On = Convert.ToDateTime(dr["Updated_On"]);
            orderitem.Updated_By = Convert.ToInt32(dr["Updated_By"]);
             
            return orderitem;
        }

        public List<OrderItemInfo>  Get_Vendor_Order_Items_By_Order_Id(int order_Id)
        {
            List<SqlParameter> sqlParam = new List<SqlParameter>();
            sqlParam.Add(new SqlParameter("@Vendor_Order_Id", order_Id));

            List<OrderItemInfo> orderitems = new List<OrderItemInfo>();
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParam, StoreProcedures.Get_Vendor_Order_Items_By_Order_Id.ToString(), CommandType.StoredProcedure);
            foreach (DataRow dr in dt.Rows)
            {
                orderitems.Add(Get_Vendor_Order_Item_Values(dr));
            }
            return orderitems;
        }

        private OrderItemInfo Get_Vendor_Order_Item_Values(DataRow dr)
        {
            OrderItemInfo orderitem = new OrderItemInfo();

            if (!dr.IsNull("Order_Item_Id"))
                orderitem.Order_Item_Id = Convert.ToInt32(dr["Order_Item_Id"]);

            if (!dr.IsNull("Product_Name"))
                orderitem.Product_Name = Convert.ToString(dr["Product_Name"]);

            if (!dr.IsNull("Order_Item_Quantity"))
                orderitem.Product_Quantity = Convert.ToInt32(dr["Order_Item_Quantity"]);

            if (!dr.IsNull("Product_Price"))
                orderitem.Product_Price = Convert.ToDecimal(dr["Product_Price"]);

            if (!dr.IsNull("Order_Status"))
                orderitem.Order_Status = Convert.ToString(dr["Order_Status"]);

            if (!dr.IsNull("Local_Tax"))
                orderitem.Local_Tax = Convert.ToDecimal(dr["Local_Tax"]);

            if (!dr.IsNull("Export_Tax"))
                orderitem.Export_Tax = Convert.ToDecimal(dr["Export_Tax"]);

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

        public void Update_Vendor_Order_Status(OrdersInfo order)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            sqlParams.Add(new SqlParameter("@Order_Id", order.Order_Id));

            sqlParams.Add(new SqlParameter("@Status_Id", order.Status_Id));

            sqlParams.Add(new SqlParameter("@Shipping_Date", order.Shipping_Date));

            _sqlRepo.ExecuteDataTable(sqlParams, StoreProcedures.Update_Vendor_Order_Status_Sp.ToString(), CommandType.StoredProcedure);
        }

        public void Send_Order_Status_Notification(string first_Name, string email_Id, OrdersInfo order, bool confirmed_Status,int request_Id,string request_Type,int entity_Id)
        {
            if (order.Status_Id == 1)
            {
                order.Status = "Received";
            }
            if (order.Status_Id == 0)
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

            if (confirmed_Status==true)
            {
                html.Append("<p>Hi " + first_Name + ",</p>");
                html.Append("<p>Your order of " + order.Order_No + " has been confirmed and will be dispatched in 7 days.</p>");
                html.Append("<br>");
                html.Append("<br>");
                html.Append("<br>");
                html.Append("<p>Thank you for using the b2bproject.</p>");
            }

            if (confirmed_Status == false)
            {
                html.Append("<p>Hi " + first_Name + ",</p>");
                html.Append("<p>Your order has been successfully placed.</p>");
                html.Append("<p>Your order no is " + order.Order_No + ".</p>");
                html.Append("<p> Your order status is " + order.Status + ". <br/> We will inform you once the items in your order have been shipped. </p>");
                html.Append("<br>");
                html.Append("<br>");
                html.Append("<br>");
                html.Append("<p>Thank you for using the b2bproject.</p>");
            }

            if (order.Status_Id == 3)
            {
                html.Append("<p>Hi " + first_Name + ",</p>");
                html.Append("<p>Your order of " + order.Order_No + " has been Dispatched.</p>");
                html.Append("<br>");
                html.Append("<br>");
                html.Append("<br>");
                html.Append("<p>Thank you for using the b2bproject.</p>");
            }

            if (order.Status_Id == 4)
            {
                html.Append("<p>Hi " + first_Name + ",</p>");
                html.Append("<p>Your order of " + order.Order_No + " has been successfully delivered.</p>");
                html.Append("<p>We would appreciate it if you could provide us some feedback. It would help us improve our services further.</p>");
                html.Append("<br>");
                html.Append("<br>");
                html.Append("<br>");
                html.Append("<p>Thank you for using the b2bproject.</p>");
            }

            if (request_Type == "Sales Order")
            {
                CommonMethods.Insert_Email_Data(request_Id, request_Type, email_Id, subject, html.ToString(), entity_Id);
            }
            else
            {
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

        }

        private List<SqlParameter> Set_Values_In_Email_Send(int request_Id,string request_Type,string to_Email_Id,string subject,string body,int user_Id)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            sqlParams.Add(new SqlParameter("@Request_Id", request_Id));
            sqlParams.Add(new SqlParameter("@Request_Type", request_Type));
            sqlParams.Add(new SqlParameter("@To_Email_Id", to_Email_Id));
            sqlParams.Add(new SqlParameter("@Subject", subject));
            sqlParams.Add(new SqlParameter("@Body", body));
            sqlParams.Add(new SqlParameter("@Is_Email_Sent", false));
            sqlParams.Add(new SqlParameter("@Created_On", DateTime.Now));
            sqlParams.Add(new SqlParameter("@Created_By", DateTime.Now));
            sqlParams.Add(new SqlParameter("@Email_Sent_On", user_Id));

            return sqlParams;
        }

        public DealerInfo Get_Dealer_Data(int dealer_Id)
        {
            List<SqlParameter> sqlParamList = new List<SqlParameter>();
            sqlParamList.Add(new SqlParameter("@Dealer_Id", dealer_Id));

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

            if (!dr.IsNull("Dealer_Id"))
            dealer.Dealer_Id = Convert.ToInt32(dr["Dealer_Id"]);
            if (!dr.IsNull("Dealer_Name"))
            dealer.Dealer_Name = Convert.ToString(dr["Dealer_Name"]);
            if (!dr.IsNull("Address"))
            dealer.Address = Convert.ToString(dr["Address"]);
            if (!dr.IsNull("City"))
            dealer.City = Convert.ToString(dr["City"]);
            if (!dr.IsNull("State"))
            dealer.State = Convert.ToInt32(dr["State"]);
            if (!dr.IsNull("State_Name"))
            dealer.State_Name = Convert.ToString(dr["State_Name"]);
            if (!dr.IsNull("Pincode"))
            dealer.Pincode = Convert.ToInt32(dr["Pincode"]);
            if (!dr.IsNull("Contact_No_1"))
            dealer.Contact_No_1 = Convert.ToString(dr["Contact_No_1"]);
            if (!dr.IsNull("Contact_No_2"))
            dealer.Contact_No_2 = Convert.ToString(dr["Contact_No_2"]);
            if (!dr.IsNull("Email"))
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
        public List<AutocompleteInfo> Get_Orders_No_Autocomplete_By_Dealer(string order_No,int Dealer_Id)
        {
            List<AutocompleteInfo> autoList = new List<AutocompleteInfo>();
            List<SqlParameter> sqlparam = new List<SqlParameter>();
            sqlparam.Add(new SqlParameter("@Description", order_No == null ? System.String.Empty : order_No.Trim()));
            sqlparam.Add(new SqlParameter("@Dealer_Id", Dealer_Id));
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlparam, StoreProcedures.Get_Order_No_Autocomplete_By_Dealer_Id.ToString(), CommandType.StoredProcedure);
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

        public void Set_Order_Balanace_Amount(int Order_Id, decimal Amount)
        {
            List<SqlParameter> sqlparam = new List<SqlParameter>();

            sqlparam.Add(new SqlParameter("@Order_Id", Order_Id));
            sqlparam.Add(new SqlParameter("@Amount", Amount));

            _sqlRepo.ExecuteScalerObj(sqlparam, StoreProcedures.Set_Order_Balanace_Amount_sp.ToString(), CommandType.StoredProcedure);
        }

        public void Set_Order_Status(int Order_Id, int Status)
        {
            List<SqlParameter> sqlparam = new List<SqlParameter>();

            sqlparam.Add(new SqlParameter("@Order_Id", Order_Id));
            sqlparam.Add(new SqlParameter("@Status", Status));            

            _sqlRepo.ExecuteScalerObj(sqlparam, StoreProcedures.Set_Order_Status_sp.ToString(), CommandType.StoredProcedure);
        }

        public List<OrdersInfo> Get_Consolidated_Sales_Orders_Data_By_Dates(DateTime fromDate, DateTime toDate,int entity_Id)
        {
            List<SqlParameter> sqlParam = new List<SqlParameter>();
            sqlParam.Add(new SqlParameter("@FromDate", fromDate));
            sqlParam.Add(new SqlParameter("@ToDate", toDate));
            sqlParam.Add(new SqlParameter("@Vendor_Id", entity_Id));

            List<OrdersInfo> orders = new List<OrdersInfo>();
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParam, StoreProcedures.Get_Consolidated_Orders_By_Date_Range.ToString(), CommandType.StoredProcedure);

            foreach (DataRow dr in CommonMethods.GetRows(dt))
            {
                orders.Add(Get_Vendor_Orders_Values(dr));
            }

            return orders;
        }

        public void Set_Vendor_Order_Balanace_Amount(int vendor_Order_Id, decimal Amount)
        {
            List<SqlParameter> sqlparam = new List<SqlParameter>();

            sqlparam.Add(new SqlParameter("@Vendor_Order_Id", vendor_Order_Id));
            sqlparam.Add(new SqlParameter("@Amount", Amount));

            _sqlRepo.ExecuteScalerObj(sqlparam, StoreProcedures.Set_Vendor_Order_Balanace_Amount_sp.ToString(), CommandType.StoredProcedure);
        }

        public void Set_Vendor_Order_Status(int vendor_Order_Id, int Status)
        {
            List<SqlParameter> sqlparam = new List<SqlParameter>();

            sqlparam.Add(new SqlParameter("@Vendor_Order_Id", vendor_Order_Id));
            sqlparam.Add(new SqlParameter("@Status", Status));

            _sqlRepo.ExecuteScalerObj(sqlparam, StoreProcedures.Set_Vendor_Order_Status_sp.ToString(), CommandType.StoredProcedure);
        }
         
    }
}
