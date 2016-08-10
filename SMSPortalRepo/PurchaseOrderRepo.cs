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
    public class PurchaseOrderRepo
    {
        SQLHelper _sqlRepo;

        public PurchaseOrderRepo()
        {
            _sqlRepo = new SQLHelper();
        }

        public int Insert_Purchase_Order(PurchaseOrderInfo purchaseorder, int user_id, int entity_Id)
        {
            int purchase_order_item_id = 0;
            purchase_order_item_id = Convert.ToInt32(_sqlRepo.ExecuteScalerObj(Set_Values_In_Purchase_Order(purchaseorder, user_id, entity_Id), StoreProcedures.Insert_Purchase_Order_Sp.ToString(), CommandType.StoredProcedure));
            return purchase_order_item_id;
        }

        public void Update_Purchase_Order(PurchaseOrderInfo purchaseorder, int user_id)
        {
            _sqlRepo.ExecuteNonQuery(Set_Values_In_Purchase_Order(purchaseorder, user_id, 0), StoreProcedures.Update_Purchase_Order_Sp.ToString(), CommandType.StoredProcedure);
        }

        private List<SqlParameter> Set_Values_In_Purchase_Order(PurchaseOrderInfo purchaseorder, int user_id,int entity_Id)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            if (purchaseorder.Purchase_Order_Id != 0)
            {
                sqlParams.Add(new SqlParameter("@Purchase_Order_Id", purchaseorder.Purchase_Order_Id));
            }
            if (purchaseorder.Purchase_Order_Id == 0)
            {
                sqlParams.Add(new SqlParameter("@Purchase_Order_No", purchaseorder.Purchase_Order_No));

                sqlParams.Add(new SqlParameter("@Vendor_Id", entity_Id));
            }
            sqlParams.Add(new SqlParameter("@Third_Party_Vendor_Id", purchaseorder.Vendor_Id));
            sqlParams.Add(new SqlParameter("@Gross_Amount", purchaseorder.Gross_Amount));

            sqlParams.Add(new SqlParameter("@Status", purchaseorder.Status));

            if (purchaseorder.Purchase_Order_Id == 0)
            {
                sqlParams.Add(new SqlParameter("@Created_On", DateTime.Now));
                sqlParams.Add(new SqlParameter("@Created_By", user_id));
            }
            sqlParams.Add(new SqlParameter("@Updated_On", DateTime.Now));
            sqlParams.Add(new SqlParameter("@Updated_By", user_id));
            return sqlParams;
        }

        public List<PurchaseOrderInfo> Get_Purchase_Orders(ref PaginationInfo Pager,int entity_Id)
        {
            List<PurchaseOrderInfo> purchaseorders = new List<PurchaseOrderInfo>();
            List<SqlParameter> sqlParamList = new List<SqlParameter>();
            sqlParamList.Add(new SqlParameter("@Vendor_Id", entity_Id));
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParamList, StoreProcedures.Get_Purchase_Order_Sp.ToString(), CommandType.StoredProcedure);
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

            purchaseorder.Vendor_Name = Convert.ToString(dr["Third_Party_Vendor_Name"]);

            if (!dr.IsNull("Status"))
                purchaseorder.Status = Convert.ToInt32(dr["Status"]);

            if (purchaseorder.Status == (int)PurchaseOrderStatus.Ordered)
            {
                purchaseorder.Status_Text = PurchaseOrderStatus.Ordered.ToString();
            }
            if (purchaseorder.Status == (int)PurchaseOrderStatus.Patially_Received)
            {
                purchaseorder.Status_Text = PurchaseOrderStatus.Patially_Received.ToString().Replace('_', ' ');
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

        public List<AutocompleteInfo> Get_Purchase_Order_Autocomplete(string Purchase_Order_No, int entity_Id)
        {
            List<AutocompleteInfo> autoList = new List<AutocompleteInfo>();
            List<SqlParameter> sqlparam = new List<SqlParameter>();
            sqlparam.Add(new SqlParameter("@Description", Purchase_Order_No == null ? System.String.Empty : Purchase_Order_No.Trim()));
            sqlparam.Add(new SqlParameter("@Vendor_Id", entity_Id));
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
                orderitem.Received_Quantity = Convert.ToInt32(dr["Received_Quantity"]);

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

        public void Insert_Purchase_Order_Item(PurchaseOrderItemInfo purchaseorderitem, int user_id)
        {
            _sqlRepo.ExecuteNonQuery(Set_Values_In_Purchase_Order_Item(purchaseorderitem, user_id), StoreProcedures.Insert_Purchase_Order_Item_Sp.ToString(), CommandType.StoredProcedure);
        }

        public void Update_Purchase_Order_Item(PurchaseOrderItemInfo purchaseorderitem, int user_id)
        {
            _sqlRepo.ExecuteNonQuery(Set_Values_In_Purchase_Order_Item(purchaseorderitem, user_id), StoreProcedures.Update_Purchase_Order_Item_Sp.ToString(), CommandType.StoredProcedure);
        }

        private List<SqlParameter> Set_Values_In_Purchase_Order_Item(PurchaseOrderItemInfo purchaseorderitem, int user_id)
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
                sqlParams.Add(new SqlParameter("@Created_On", DateTime.Now));
                sqlParams.Add(new SqlParameter("@Created_By", user_id));
            }
            sqlParams.Add(new SqlParameter("@Updated_On", DateTime.Now));
            sqlParams.Add(new SqlParameter("@Updated_By", user_id));

            return sqlParams;
        }

        public void Delete_Purchase_Order_Item_By_Id(int Purchase_Order_Item_Id)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter("@Purchase_Order_Item_Id", Purchase_Order_Item_Id));
            _sqlRepo.ExecuteNonQuery(sqlParams, StoreProcedures.Delete_Purchase_Order_Item_By_Id_Sp.ToString(), CommandType.StoredProcedure);
        }

        public bool Check_Duplicate_Product_PurchaseOrder(int Product_Id, int Purchase_Id)
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
        public string Generate_Ref_No(string initialCharacter, string columnName, string substringStartIndex, string substringEndIndex, string tableName)
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

            if (RefNo =="0")
            {
                RefNo = "1";
            }
            return RefNo;
        }

        public string Generate_Ven_Ref_No(string initialCharacter, string columnName, string substringStartIndex, string substringEndIndex, string tableName,int vendor_Id)
        {
            string RefNo = "";
            List<SqlParameter> sqp = new List<SqlParameter>();
            string strQry = "Select '" + initialCharacter + "' + CAST(ISNULL(max(CAST(substring(" + columnName + "," + substringStartIndex + "," + substringEndIndex + ") AS int))+1, 1) as nvarchar) as " + columnName + " from " + tableName;
            strQry += " where Entity_Id='" + vendor_Id + "' and " + columnName + " like '" + initialCharacter + "' + '%'";

            DataTable dt = _sqlRepo.ExecuteDataTable(sqp, strQry, CommandType.Text);
            foreach (DataRow dr in dt.Rows)
            {
                RefNo = Convert.ToString(dr[0]);
            }
            return RefNo;
        }

        public void Update_Purchase_Order_Gross_Amount(int Purchaser_Order_Id, decimal Gross_Amount)
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
                product.Product_Price = Convert.ToDecimal(dr["Product_Price"]);
            }
            return product;
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
            return product;
        }

        public ThirdPartyVendorInfo Get_Vendor_By_Id(int vendor_Id)
        {
            List<SqlParameter> sqlParamList = new List<SqlParameter>();
            sqlParamList.Add(new SqlParameter("@Third_Party_Vendor_Id", vendor_Id));

            ThirdPartyVendorInfo Vendor = new ThirdPartyVendorInfo();
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParamList, StoreProcedures.Get_Vendor_By_Id_Sp.ToString(), CommandType.StoredProcedure);
            foreach (DataRow dr in dt.Rows)
            {
                Vendor = Get_Vendor_Values(dr);
            }
            return Vendor;
        }

        private ThirdPartyVendorInfo Get_Vendor_Values(DataRow dr)
        {
            ThirdPartyVendorInfo Vendor = new ThirdPartyVendorInfo();

            Vendor.Vendor_Id = Convert.ToInt32(dr["Third_Party_Vendor_Id"]);
            Vendor.Vendor_Name = Convert.ToString(dr["Third_Party_Vendor_Name"]);
            Vendor.Address = Convert.ToString(dr["Address"]);
            Vendor.City = Convert.ToString(dr["City"]);
            Vendor.State = Convert.ToInt32(dr["State"]);
            Vendor.Pincode = Convert.ToInt32(dr["Pincode"]);
            Vendor.Contact_No_1 = Convert.ToString(dr["Contact_No_1"]);
            Vendor.Contact_No_2 = Convert.ToString(dr["Contact_No_2"]);
            Vendor.Email = Convert.ToString(dr["Email"]);
            Vendor.Is_Active = Convert.ToBoolean(dr["Is_Active"]);
            Vendor.Created_On = Convert.ToDateTime(dr["Created_On"]);
            Vendor.Created_By = Convert.ToInt32(dr["Created_By"]);
            Vendor.Updated_On = Convert.ToDateTime(dr["Updated_On"]);
            Vendor.Updated_By = Convert.ToInt32(dr["Updated_By"]);

            return Vendor;
        }

        public void Send_Purchase_Order_Email(string Vendor_Email_Id, PurchaseOrderInfo PurchaseOrder, List<PurchaseOrderItemInfo> purchaseOrderItems)
        {

            StringBuilder html = new StringBuilder();
            string subject = "Order : " + PurchaseOrder.Purchase_Order_No;

            #region Main Table

            html.Append("<table cellspacing='0' cellpadding='0' style='width:700px;border:1px solid #ccc;padding:5px;' >");            

            #region header dates first tr
            html.Append("<tr>");
            html.Append("<td style='width:60%;'>");
            html.Append("<img src='/UploadedFiles/logo.png' width:100px;> </td>");
            html.Append("<td style='width:40%;text-align:right;'><b>Date :</b> " + string.Format("{0:dd/MM/yyy}", DateTime.Now) + "</td>");
            html.Append("</tr>");
            #endregion

            #region sencode tr

            html.Append("<tr>");
            html.Append("<td colspan='2'>");

            #region From-to address & Order Details
            html.Append("<table cellspacing='0' cellpadding='0' style='width:100%'>");             
            html.Append("<tr>");

            #region Sms Address
            html.Append("<td width='70%' style='border-bottom:1px solid #ccc'>");
            html.Append("<table cellpadding='5' width='99%' style='margin:10px 0'>");            
            html.Append("<tr>");
            html.Append(" <td><b><span>From</span>: </b>");
            html.Append("<br>" + ConfigurationManager.AppSettings["CompanyName"].ToString());
            html.Append("<br>" + ConfigurationManager.AppSettings["CompanyAddress"].ToString());
            html.Append("<br>" + ConfigurationManager.AppSettings["CompanyTelephone"].ToString());
            html.Append("<br>" + ConfigurationManager.AppSettings["fromMailAddress"].ToString());
            html.Append("</td>");
            html.Append("</tr>");             
            html.Append("</table>");
            html.Append("</td>");
            #endregion

            #region Purchaes Order Details
            html.Append(" <td width='30%' style='border-bottom:1px solid #ccc;border-left:1px solid #ccc'>");
            html.Append("<table cellspacing='0' cellpadding='15'>");             
            html.Append("<tr>");
            html.Append("<td>");
            html.Append("<br><b>Order No :</b> " + PurchaseOrder.Purchase_Order_No);
            html.Append("<br><b>Order Date :</b> " + string.Format("{0:d}", PurchaseOrder.Created_On));
            html.Append("<br>");
            html.Append("</td>");
            html.Append("</tr>");            
            html.Append("</table>");
            html.Append(" </td>");
            #endregion

            html.Append("</tr>");             
            html.Append("</table>");
            #endregion

            html.Append("</td>");
            html.Append("</tr>");
            #endregion

            #region third tr
            html.Append("<tr>");
            html.Append("<td colspan='2'>");

            html.Append("<table  width='100%' cellpadding='0' cellspacing='0' style='background-color:#e2e2e2'>");

            html.Append("<tr>");
            html.Append("<th>Sr.No</th>");
            html.Append("<th>Product name</th>");
            html.Append("<th>Qty</th>");
            html.Append("<th>Unit Price</th>");
            html.Append("<th>Price</th>");
            html.Append("<th>Shipping Date</th>");
            html.Append("<th>Shipping Address</th>");
            html.Append("</tr>");

            int count = 0;
            if (PurchaseOrder != null)
            {
                if (purchaseOrderItems != null)
                {
                    foreach (var item in purchaseOrderItems)
                    {
                        ProductInfo ProductInfo = Get_Product_By_Id(item.Product_Id);
                        count++;
                        html.Append("<tr style='background-color:#fff'>");
                        html.Append("<td>" + count + "</td>");
                        html.Append("<td >" + ProductInfo.Product_Name + "</td>");
                        html.Append("<td >" + item.Product_Quantity + "</td>");
                        html.Append("<td >" + item.Product_Unit_Price + "</td>");
                        html.Append("<td >" + item.Product_Price + "</td>");
                        html.Append("<td >" + item.Shipping_Date + "</td>");
                        html.Append("<td >" + item.Shipping_Address + "</td>");
                        html.Append("</tr>");
                    }
                }
            }

            html.Append("<tr>");
            html.Append("<td colspan='6'>Total: </td>");
            html.Append("<td>" + PurchaseOrder.Gross_Amount + "</td>");
            html.Append("</tr>");

            html.Append("</table>");

            html.Append("</td>");
            html.Append("</tr>");
            #endregion

             
            html.Append("</table>");

            #endregion

            MailAddress fromMail = new MailAddress(ConfigurationManager.AppSettings["fromMailAddress"].ToString(), ConfigurationManager.AppSettings["fromMailName"].ToString());
            MailMessage message = new MailMessage();
            message.From = fromMail;
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = html.ToString();
            MailAddress To = new MailAddress(Vendor_Email_Id);
            message.To.Add(To);
            SmtpClient client = new SmtpClient();
            client.Send(message);
        }

        public List<PurchaseOrderInfo> Get_Purchase_Orders_By_Vendor(int vendor_Id)
        {
            List<PurchaseOrderInfo> purchaseorders = new List<PurchaseOrderInfo>();
            List<SqlParameter> sqlparam = new List<SqlParameter>();
            sqlparam.Add(new SqlParameter("@Vendor_Id", vendor_Id));
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlparam, StoreProcedures.Get_Purchase_Order_By_Vendor_Id_Sp.ToString(), CommandType.StoredProcedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    PurchaseOrderInfo info = new PurchaseOrderInfo();
                    info.Purchase_Order_No = Convert.ToString(dr["Purchase_Order_No"]);
                    info.Vendor_Name = Convert.ToString(dr["Vendor_Name"]);
                    info.Created_On = Convert.ToDateTime(dr["Created_On"]);
                    info.Gross_Amount = Convert.ToInt32(dr["Gross_Amount"]);
                    info.Status = Convert.ToInt32(dr["Status"]);
                    purchaseorders.Add(info);
                }
            }
            return purchaseorders;
        }
    }
}
