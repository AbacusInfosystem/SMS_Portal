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
    public class PurchaseOrderRepo
    {
        SQLHelper _sqlRepo;

        public PurchaseOrderRepo()
        {
            _sqlRepo = new SQLHelper();
        }

        public int Insert_Purchase_Order(PurchaseOrderInfo purchaseorder)
        {
            int purchase_order_item_id = 0;
            purchase_order_item_id=Convert.ToInt32(_sqlRepo.ExecuteScalerObj(Set_Values_In_Purchase_Order(purchaseorder), StoreProcedures.Insert_Purchase_Order_Sp.ToString(), CommandType.StoredProcedure));
            return purchase_order_item_id;
        }

        public void Update_Purchase_Order(PurchaseOrderInfo purchaseorder)
        {
            _sqlRepo.ExecuteNonQuery(Set_Values_In_Purchase_Order(purchaseorder), StoreProcedures.Update_Purchase_Order_Sp.ToString(), CommandType.StoredProcedure);
        }

        private List<SqlParameter> Set_Values_In_Purchase_Order(PurchaseOrderInfo purchaseorder)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            if (purchaseorder.Purchase_Order_Id != 0)
            {
                sqlParams.Add(new SqlParameter("@Purchase_Order_Id", purchaseorder.Purchase_Order_Id));
            }
            if (purchaseorder.Purchase_Order_Id == 0)
            {
                sqlParams.Add(new SqlParameter("@Purchase_Order_No", purchaseorder.Purchase_Order_No));
            }
            sqlParams.Add(new SqlParameter("@Vendor_Id", purchaseorder.Vendor_Id));
            sqlParams.Add(new SqlParameter("@Gross_Amount", purchaseorder.Gross_Amount));

            if (purchaseorder.Purchase_Order_Id == 0)
            {
                sqlParams.Add(new SqlParameter("@Status", purchaseorder.Status));
            }

            if (purchaseorder.Purchase_Order_Id == 0)
            {
                sqlParams.Add(new SqlParameter("@Created_On", purchaseorder.Created_On));
                sqlParams.Add(new SqlParameter("@Created_By", purchaseorder.Created_By));
            }
            sqlParams.Add(new SqlParameter("@Updated_On", purchaseorder.Updated_On));
            sqlParams.Add(new SqlParameter("@Updated_By", purchaseorder.Updated_By));
            return sqlParams;
        }

        public List<PurchaseOrderInfo> Get_Purchase_Orders(ref PaginationInfo Pager)
        {
            List<PurchaseOrderInfo> purchaseorders = new List<PurchaseOrderInfo>();
            DataTable dt = _sqlRepo.ExecuteDataTable(null, StoreProcedures.Get_Purchase_Order_Sp.ToString(), CommandType.StoredProcedure);
            foreach (DataRow dr in CommonMethods.GetRows(dt, ref Pager))
            {
                purchaseorders.Add(Get_Purchase_Order_Values(dr));
            }
            return purchaseorders;
        }

        public List<PurchaseOrderInfo> Get_Purchase_Orders_By_Id(int Purchase_Order_Id, ref PaginationInfo Pager)
        {
            List<SqlParameter> sqlParamList = new List<SqlParameter>();
            sqlParamList.Add(new SqlParameter("@Purchase_Order_Id", Purchase_Order_Id));

            List<PurchaseOrderInfo> purchaseOrders = new List<PurchaseOrderInfo>();
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParamList, StoreProcedures.Get_Purchase_Order_By_Id_Sp.ToString(), CommandType.StoredProcedure);

            foreach (DataRow dr in CommonMethods.GetRows(dt, ref Pager))
            {
                purchaseOrders.Add(Get_Purchase_Order_Values(dr));
            }
            return purchaseOrders;
        }

        public PurchaseOrderInfo Get_Purchase_Order_By_Id(int Purchase_Order_Id)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("@Purchase_Order_Id", Purchase_Order_Id));

            PurchaseOrderInfo purchaseorder = new PurchaseOrderInfo();
            DataTable dt = _sqlRepo.ExecuteDataTable(paramList, StoreProcedures.Get_Purchase_Order_By_Id_Sp.ToString(), CommandType.StoredProcedure);            
            foreach (DataRow dr in dt.Rows)
            {
                purchaseorder = Get_Purchase_Order_Values(dr);
            }
            return purchaseorder;
        }

        private PurchaseOrderInfo Get_Purchase_Order_Values(DataRow dr)
        {
            PurchaseOrderInfo purchaseorder = new PurchaseOrderInfo();

            purchaseorder.Purchase_Order_Id = Convert.ToInt32(dr["Purchase_Order_Id"]);

            if (!dr.IsNull("Purchase_Order_No"))
            purchaseorder.Purchase_Order_No = Convert.ToString(dr["Purchase_Order_No"]);

            if (!dr.IsNull("Vendor_Id"))
            purchaseorder.Vendor_Id = Convert.ToInt32(dr["Vendor_Id"]);

            if (!dr.IsNull("Gross_Amount"))
            purchaseorder.Gross_Amount = Convert.ToDecimal(dr["Gross_Amount"]);

            purchaseorder.Vendor_Name = Convert.ToString(dr["Vendor_Name"]);

            if (!dr.IsNull("Status"))
            purchaseorder.Status = Convert.ToInt32(dr["Status"]);

            if (purchaseorder.Status == (int)PurchaseOrderStatus.Ordered)
            {
                purchaseorder.Status_Text = PurchaseOrderStatus.Ordered.ToString();
            }
            if (purchaseorder.Status == (int)PurchaseOrderStatus.Patially_Received)
            {
                purchaseorder.Status_Text = PurchaseOrderStatus.Patially_Received.ToString().Replace('_',' ');
            }
            if (purchaseorder.Status == (int)PurchaseOrderStatus.Received)
            {
                purchaseorder.Status_Text = PurchaseOrderStatus.Received.ToString();
            }             

            purchaseorder.Created_On = Convert.ToDateTime(dr["Created_On"]);
            purchaseorder.Created_By = Convert.ToInt32(dr["Created_By"]);
            purchaseorder.Updated_On = Convert.ToDateTime(dr["Updated_On"]);
            purchaseorder.Updated_By = Convert.ToInt32(dr["Updated_By"]);
            return purchaseorder;
        }

        public List<AutocompleteInfo> Get_Purchase_Order_Autocomplete(string Purchase_Order_No)
        {
            List<AutocompleteInfo> autoList = new List<AutocompleteInfo>();
            List<SqlParameter> sqlparam = new List<SqlParameter>();
            sqlparam.Add(new SqlParameter("@Description", Purchase_Order_No == null ? System.String.Empty : Purchase_Order_No.Trim()));
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlparam, StoreProcedures.Get_Purchase_Order_Autocomplete_Sp.ToString(), CommandType.StoredProcedure);
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

        public List<PurchaseOrderItemInfo> Get_Purchase_Order_Items_By_Id(int Purchase_Order_Id)
        {
            List<SqlParameter> sqlParamList = new List<SqlParameter>();
            sqlParamList.Add(new SqlParameter("@Purchase_Order_Id", Purchase_Order_Id));

            List<PurchaseOrderItemInfo> purchaseOrders = new List<PurchaseOrderItemInfo>();
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParamList, StoreProcedures.Get_Purchase_Order_Items_By_Id_Sp.ToString(), CommandType.StoredProcedure);

            foreach (DataRow dr in dt.Rows)
            {
                purchaseOrders.Add(Get_Purchase_Order_Items_Values(dr));
            }
            return purchaseOrders;
        }

        private PurchaseOrderItemInfo Get_Purchase_Order_Items_Values(DataRow dr)
        {
            PurchaseOrderItemInfo orderitem = new PurchaseOrderItemInfo();

            orderitem.Purchase_Order_Item_Id = Convert.ToInt32(dr["Purchase_Order_Item_Id"]);
            orderitem.Purchase_Order_Id = Convert.ToInt32(dr["Purchase_Order_Id"]);
            orderitem.Product_Id = Convert.ToInt32(dr["Product_Id"]);
            orderitem.Product_Name = Convert.ToString(dr["Product_Name"]);

            if (!dr.IsNull("Product_Quantity")) 
            orderitem.Product_Quantity = Convert.ToInt32(dr["Product_Quantity"]);

            if (!dr.IsNull("Received_Quantity"))
                orderitem.Received_Quantity  = Convert.ToInt32(dr["Received_Quantity"]);

            if (!dr.IsNull("Product_Price")) 
            orderitem.Product_Price = Convert.ToDecimal(dr["Product_Price"]);

            orderitem.Shipping_Address = Convert.ToString(dr["Shipping_Address"]);

            if (!dr.IsNull("Shipping_Date")) 
            orderitem.Shipping_Date = Convert.ToDateTime(dr["Shipping_Date"]);

            if (!dr.IsNull("Status"))
                orderitem.Status = Convert.ToInt32(dr["Status"]);

            if (orderitem.Status == (int)PurchaseOrderStatus.Ordered)
            {
                orderitem.Status_Text = PurchaseOrderStatus.Ordered.ToString();
            }
            if (orderitem.Status == (int)PurchaseOrderStatus.Patially_Received)
            {
                orderitem.Status_Text = PurchaseOrderStatus.Patially_Received.ToString().Replace('_', ' ');
            }
            if (orderitem.Status == (int)PurchaseOrderStatus.Received)
            {
                orderitem.Status_Text = PurchaseOrderStatus.Received.ToString();
            } 

            orderitem.Created_On = Convert.ToDateTime(dr["Created_On"]);
            orderitem.Created_By = Convert.ToInt32(dr["Created_By"]);
            orderitem.Updated_On = Convert.ToDateTime(dr["Updated_On"]);
            orderitem.Updated_By = Convert.ToInt32(dr["Updated_By"]);
            return orderitem;
        }

        public void Insert_Purchase_Order_Item(PurchaseOrderItemInfo purchaseorderitem)
        {
            _sqlRepo.ExecuteNonQuery(Set_Values_In_Purchase_Order_Item(purchaseorderitem), StoreProcedures.Insert_Purchase_Order_Item_Sp.ToString(), CommandType.StoredProcedure);
        }

        public void Update_Purchase_Order_Item(PurchaseOrderItemInfo purchaseorderitem)
        {
            _sqlRepo.ExecuteNonQuery(Set_Values_In_Purchase_Order_Item(purchaseorderitem), StoreProcedures.Update_Purchase_Order_Item_Sp.ToString(), CommandType.StoredProcedure);
        }

        private List<SqlParameter> Set_Values_In_Purchase_Order_Item(PurchaseOrderItemInfo purchaseorderitem)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            if (purchaseorderitem.Purchase_Order_Item_Id != 0)
            {
                sqlParams.Add(new SqlParameter("@Purchase_Order_Item_Id", purchaseorderitem.Purchase_Order_Item_Id));
            }
            sqlParams.Add(new SqlParameter("@Purchase_Order_Id", purchaseorderitem.Purchase_Order_Id));
            sqlParams.Add(new SqlParameter("@Product_Id", purchaseorderitem.Product_Id));
            sqlParams.Add(new SqlParameter("@Product_Quantity", purchaseorderitem.Product_Quantity));
            sqlParams.Add(new SqlParameter("@Received_Quantity", purchaseorderitem.Received_Quantity));
            sqlParams.Add(new SqlParameter("@Product_Price", purchaseorderitem.Product_Price));
            sqlParams.Add(new SqlParameter("@Shipping_Address", purchaseorderitem.Shipping_Address));
            sqlParams.Add(new SqlParameter("@Shipping_Date", purchaseorderitem.Shipping_Date));
            sqlParams.Add(new SqlParameter("@Status", purchaseorderitem.Status));            
            if (purchaseorderitem.Purchase_Order_Item_Id == 0)
            {
                sqlParams.Add(new SqlParameter("@Created_On", purchaseorderitem.Created_On));
                sqlParams.Add(new SqlParameter("@Created_By", purchaseorderitem.Created_By));
            }
            sqlParams.Add(new SqlParameter("@Updated_On", purchaseorderitem.Updated_On));
            sqlParams.Add(new SqlParameter("@Updated_By", purchaseorderitem.Updated_By));

            return sqlParams;
        }
                
        public void Delete_Purchase_Order_Item_By_Id(int Purchase_Order_Item_Id)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter("@Purchase_Order_Item_Id", Purchase_Order_Item_Id));
            _sqlRepo.ExecuteNonQuery(sqlParams, StoreProcedures.Delete_Purchase_Order_Item_By_Id_Sp.ToString(), CommandType.StoredProcedure);
        }

        public bool Check_Duplicate_Product_PurchaseOrder(int Product_Id,int Purchase_Id)
        {
            bool check = false;

            List<SqlParameter> sqlParam = new List<SqlParameter>();
            sqlParam.Add(new SqlParameter("@Product_Id", Product_Id));
            sqlParam.Add(new SqlParameter("@Purchase_Order_Id", Purchase_Id));

            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParam, StoreProcedures.Check_DuplicateProduct_PurchaseOrder.ToString(), CommandType.StoredProcedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    check = Convert.ToBoolean(dr["Check_Product_Item"]);
                }
            }
            return check;
        }

        ///<summary>
        ///Generate Ref No
        /// </summary>         
        public  string Generate_Ref_No(string initialCharacter, string columnName, string substringStartIndex, string substringEndIndex, string tableName)
        {
            string RefNo = "";
            List<SqlParameter> sqp = new List<SqlParameter>();
            string strQry = "Select '" + initialCharacter + "' + CAST(ISNULL(max(CAST(substring(" + columnName + "," + substringStartIndex + "," + substringEndIndex + ") AS int))+1, 1) as nvarchar) as " + columnName + " from " + tableName;
            strQry += " where " + columnName + " like '" + initialCharacter + "' + '%'";

            DataTable dt = _sqlRepo.ExecuteDataTable(sqp, strQry, CommandType.Text);
            foreach (DataRow dr in dt.Rows)
            {
                RefNo = Convert.ToString(dr[0]);
            }
            return RefNo;
        }

        public void Update_Purchase_Order_Gross_Amount(int Purchaser_Order_Id , decimal Gross_Amount )
        {
            List<SqlParameter> sqlparam = new List<SqlParameter>();
            sqlparam.Add(new SqlParameter("@Purchase_Order_Id", Purchaser_Order_Id));
            sqlparam.Add(new SqlParameter("@Gross_Amount", Gross_Amount));
            Convert.ToInt32(_sqlRepo.ExecuteScalerObj(sqlparam, StoreProcedures.Update_Purchase_Order_Gross_Amount.ToString(), CommandType.StoredProcedure));            
        }

        public ProductInfo Get_Vendor_Product_Price_Id(int Product_Id, int Vendor_Id)
        {
            List<SqlParameter> sqlParamList = new List<SqlParameter>();
            sqlParamList.Add(new SqlParameter("@Product_Id", Product_Id));
            sqlParamList.Add(new SqlParameter("@Vendor_Id", Vendor_Id));
             
            ProductInfo product = new ProductInfo();
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParamList, StoreProcedures.Get_Vendor_Product_Price_Id_Sp.ToString(), CommandType.StoredProcedure);

            foreach (DataRow dr in dt.Rows)
            {
                if (!dr.IsNull("Product_Id"))
                    product.Product_Id = Convert.ToInt32(dr["Product_Id"]);
                    product.Product_Price=Convert.ToDecimal(dr["Product_Price"]);
            }
            return product;
        }
    }
}
