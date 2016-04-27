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

        public void Insert_Purchase_Order(PurchaseOrderInfo purchaseorder)
        {
            _sqlRepo.ExecuteNonQuery(Set_Values_In_Purchase_Order(purchaseorder), StoreProcedures.Insert_Purchase_Order_Sp.ToString(), CommandType.StoredProcedure);
        }

        //public void Update_Purchase_Order(PurchaseOrderInfo purchaseorder)
        //{
        //    _sqlRepo.ExecuteNonQuery(Set_Values_In_Purchase_Order(purchaseorder), StoredProcedures.Update_Purchase_Order_Sp.ToString(), CommandType.StoredProcedure);
        //}

        private List<SqlParameter> Set_Values_In_Purchase_Order(PurchaseOrderInfo purchaseorder)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter("@Purchase_Order_Id", purchaseorder.Purchase_Order_Id));
            sqlParams.Add(new SqlParameter("@Purchase_Order_No", purchaseorder.Purchase_Order_No));
            sqlParams.Add(new SqlParameter("@Vendor_Id", purchaseorder.Vendor_Id));
            sqlParams.Add(new SqlParameter("@Created_On", purchaseorder.Created_On));
            sqlParams.Add(new SqlParameter("@Created_By", purchaseorder.Created_By));
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

            PurchaseOrderInfo purchaseorder = new PurchaseOrderInfo();
            DataTable dt = _sqlRepo.ExecuteDataTable(null, StoreProcedures.Get_Purchase_Order_By_Id_Sp.ToString(), CommandType.StoredProcedure);            
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
            purchaseorder.Purchase_Order_No = Convert.ToString(dr["Purchase_Order_No"]);
            purchaseorder.Vendor_Id = Convert.ToInt32(dr["Vendor_Id"]);
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
    }
}
