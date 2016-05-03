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

        private OrdersInfo Get_Orders_Values(DataRow dr)
        {
            OrdersInfo orders = new OrdersInfo();

            orders.Order_Id = Convert.ToInt32(dr["Order_Id"]);
            orders.Order_No = Convert.ToString(dr["Order_No"]);
            orders.Dealer_Id = Convert.ToInt32(dr["Dealer_Id"]);
            orders.Order_Date = Convert.ToDateTime(dr["Order_Date"]);
            orders.Shipping_Address = Convert.ToString(dr["Shipping_Address"]);
            orders.Discount = Convert.ToDecimal(dr["Discount"]);
            orders.Gross_Amount = Convert.ToDecimal(dr["Gross_Amount"]);
            orders.Service_Tax = Convert.ToDecimal(dr["Service_Tax"]);
            orders.Vat = Convert.ToDecimal(dr["Vat"]);
            orders.Swatch_Bharat_Tax = Convert.ToDecimal(dr["Swatch_Bharat_Tax"]);
            orders.Net_Amount = Convert.ToDecimal(dr["Net_Amount"]);
            orders.Created_On = Convert.ToDateTime(dr["Created_On"]);
            orders.Created_By = Convert.ToInt32(dr["Created_By"]);
            orders.Updated_On = Convert.ToDateTime(dr["Updated_On"]);
            orders.Updated_By = Convert.ToInt32(dr["Updated_By"]);
            orders.OrderItems = Get_Order_Items_By_Order_Id(orders.Order_Id); 
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
            orderitem.Product_Quantity = Convert.ToInt32(dr["Product_Quantity"]);
            orderitem.Product_Price = Convert.ToDecimal(dr["Product_Price"]);
            orderitem.Order_Status = Convert.ToString(dr["Order_Status"]);
            orderitem.Created_On = Convert.ToDateTime(dr["Created_On"]);
            orderitem.Created_By = Convert.ToInt32(dr["Created_By"]);
            orderitem.Updated_On = Convert.ToDateTime(dr["Updated_On"]);
            orderitem.Updated_By = Convert.ToInt32(dr["Updated_By"]);
             
            return orderitem;
        }
         
    }
}
