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
    public class ConsolidatedOrderRepo
    {
          SQLHelper _sqlRepo;

         public ConsolidatedOrderRepo()
        {
            _sqlRepo = new SQLHelper();
        }


         public List<ConsolidatedOrderInfo> Get_Consolidated_Orders(int entity_Id,DateTime from_Date,DateTime to_Date)
         {
             List<ConsolidatedOrderInfo> orders = new List<ConsolidatedOrderInfo>();

             List<SqlParameter> sqlParam = new List<SqlParameter>();
             sqlParam.Add(new SqlParameter("@Vendor_Id", entity_Id));
             sqlParam.Add(new SqlParameter("@From_Date", from_Date));
             sqlParam.Add(new SqlParameter("@To_Date", to_Date));

             DataTable dt = _sqlRepo.ExecuteDataTable(sqlParam, StoreProcedures.Get_Consolidated_Order_By_Vendor_Sp.ToString(), CommandType.StoredProcedure);

             foreach (DataRow dr in CommonMethods.GetRows(dt))
             {
                 orders.Add(Get_Order_Values(dr, from_Date, to_Date));
             }

             return orders;
         }

         private ConsolidatedOrderInfo Get_Order_Values(DataRow dr,DateTime frm_Date,DateTime to_Date)
         {
             string status="";

             ConsolidatedOrderInfo order = new ConsolidatedOrderInfo();

             if (!dr.IsNull("Order_Item_Id"))
                 order.Product_Id = Convert.ToInt32(dr["Order_Item_Id"]);
             if (!dr.IsNull("Product_Quantity"))
                 order.Product_Quantity = Convert.ToInt32(dr["Product_Quantity"]);

             order.Product_Name = Get_Product_Name_By_Id(order.Product_Id);

             status = Get_Product_Status_By_Id(order.Product_Id, frm_Date, to_Date);

             if(status=="Po Generated")
             {
                 order.Is_Po_Generated = true;
             }
             else
             {
                 order.Is_Po_Generated = false;
             }


             return order;
         }

         public string Get_Product_Name_By_Id(int product_Id)
         {
             string Product_Name="";

             List<ConsolidatedOrderInfo> orders = new List<ConsolidatedOrderInfo>();

             List<SqlParameter> sqlParam = new List<SqlParameter>();

             sqlParam.Add(new SqlParameter("@Product_Id", product_Id));

             DataTable dt = _sqlRepo.ExecuteDataTable(sqlParam, StoreProcedures.Get_Product_By_Consolidated_Id_Sp.ToString(), CommandType.StoredProcedure);

             if (dt != null && dt.Rows.Count > 0)
             {
                 DataRow dr = dt.AsEnumerable().FirstOrDefault();

                 if (dr != null)
                 {
                     Product_Name = Convert.ToString(dr["Product_Name"]);
                 }
             }

             return Product_Name;
         }

         public string Get_Product_Status_By_Id(int product_Id, DateTime frm_Date, DateTime to_Date)
         {
             string Status = "";

             List<ConsolidatedOrderInfo> orders = new List<ConsolidatedOrderInfo>();

             List<SqlParameter> sqlParam = new List<SqlParameter>();

             sqlParam.Add(new SqlParameter("@Product_Id", product_Id));

             sqlParam.Add(new SqlParameter("@From_Date", frm_Date));

             sqlParam.Add(new SqlParameter("@To_Date", to_Date));

             DataTable dt = _sqlRepo.ExecuteDataTable(sqlParam, StoreProcedures.Get_Product_Status_By_Consolidated_Id_Sp.ToString(), CommandType.StoredProcedure);

             if (dt != null && dt.Rows.Count > 0)
             {
                 DataRow dr = dt.AsEnumerable().FirstOrDefault();

                 if (dr != null)
                 {
                     Status = Convert.ToString(dr["Order_Status"]);
                 }
             }

             return Status;
         }

         public List<int> Get_Vendor_Item_Ids(int entity_Id, DateTime from_Date, DateTime to_Date, int product_Id)
         {
              List<int> IDs=new List<int>();

              List<SqlParameter> sqlParam = new List<SqlParameter>();
              sqlParam.Add(new SqlParameter("@Vendor_Id", entity_Id));
              sqlParam.Add(new SqlParameter("@From_Date", from_Date));
              sqlParam.Add(new SqlParameter("@To_Date", to_Date));
              sqlParam.Add(new SqlParameter("@Product_Id", product_Id));

              DataTable dt = _sqlRepo.ExecuteDataTable(sqlParam, StoreProcedures.Get_Consolidated_Order_Vendor_Item_By_Vendor_Sp.ToString(), CommandType.StoredProcedure);             

              if (dt != null && dt.Rows.Count > 0)
              {
                  foreach (DataRow dr in dt.Rows)
                  {
                      int ID = Convert.ToInt32(dr["Vendor_Order_Id"]);
                      IDs.Add(ID);
                  }
              }

              return IDs;
         }

         public void Update_Order(List<ConsolidatedOrderInfo> consolidated_Orders, DateTime frm_Date, DateTime to_Date,int entity_Id)
         {
             List<int> Vendor_Item_Ids = new List<int>();

             foreach (var item in consolidated_Orders)
             {
                 if (item.Is_Po_Generated==true)
                 {
                     Vendor_Item_Ids = Get_Vendor_Item_Ids(entity_Id, frm_Date, to_Date, item.Product_Id);

                     foreach (var id in Vendor_Item_Ids)
                     {
                         List<SqlParameter> sqlParams = new List<SqlParameter>();

                         sqlParams.Add(new SqlParameter("@Vendor_Order_Id", id));
                         sqlParams.Add(new SqlParameter("@Product_Id", item.Product_Id));
                         sqlParams.Add(new SqlParameter("@Status", "Po Generated"));

                         _sqlRepo.ExecuteNonQuery(sqlParams, StoreProcedures.Update_Order_Status_By_Product_Id_Sp.ToString(), CommandType.StoredProcedure);
                     }
                 }

             }
             
         }

         public string Get_Order_No_By_Dealer_Id(int dealer_Id)
         {
             string Order_Nos = "";

             List<ConsolidatedOrderInfo> orders = new List<ConsolidatedOrderInfo>();

             List<SqlParameter> sqlParam = new List<SqlParameter>();

             sqlParam.Add(new SqlParameter("@Dealer_Id", dealer_Id));

             DataTable dt = _sqlRepo.ExecuteDataTable(sqlParam, StoreProcedures.Get_Order_No_By_Daealer_Sp.ToString(), CommandType.StoredProcedure);

             if (dt != null && dt.Rows.Count > 0)
             {
                 DataRow dr = dt.AsEnumerable().FirstOrDefault();

                 if (dr != null)
                 {
                     Order_Nos = Convert.ToString(dr["OrderNos"]);
                 }
             }

             return Order_Nos;
         }

    }
}
