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
   public class VendorRepo
    {
        SQLHelper _sqlHelper;

        public VendorRepo()
        {
            _sqlHelper = new SQLHelper();
        }

        public void Insert_Vendor(VendorInfo vendor , int user_Id)
        {
            _sqlHelper.ExecuteNonQuery(Set_Values_In_Vendor(vendor, user_Id), StoreProcedures.Insert_Vendor_Sp.ToString(), CommandType.StoredProcedure);
        }

        public void Update_Vendor(VendorInfo vendor , int user_Id)
        {
            _sqlHelper.ExecuteNonQuery(Set_Values_In_Vendor(vendor, user_Id), StoreProcedures.Update_Vendor_Sp.ToString(), CommandType.StoredProcedure);
        }

        private List<SqlParameter> Set_Values_In_Vendor(VendorInfo vendor, int user_Id)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            if (vendor.Vendor_Id != 0)
            {
                sqlParams.Add(new SqlParameter("@Vendor_Id", vendor.Vendor_Id));
            }

            sqlParams.Add(new SqlParameter("@Vendor_Name", vendor.Vendor_Name));
            sqlParams.Add(new SqlParameter("@Address", vendor.Address));
            sqlParams.Add(new SqlParameter("@City", vendor.City));
            sqlParams.Add(new SqlParameter("@State", vendor.State));
            sqlParams.Add(new SqlParameter("@Pincode", vendor.Pincode));
            sqlParams.Add(new SqlParameter("@Contact_No_1", vendor.Contact_No_1));
            sqlParams.Add(new SqlParameter("@Contact_No_2", vendor.Contact_No_2));
            sqlParams.Add(new SqlParameter("@Email", vendor.Email));
            sqlParams.Add(new SqlParameter("@Is_Active", vendor.Is_Active));
            if (vendor.Vendor_Id == 0)
            {
                sqlParams.Add(new SqlParameter("@Created_On", DateTime.Now));
                sqlParams.Add(new SqlParameter("@Created_By", user_Id));
            }
            sqlParams.Add(new SqlParameter("@Updated_On", DateTime.Now));
            sqlParams.Add(new SqlParameter("@Updated_By", user_Id));
            return sqlParams;
        }

        public VendorInfo Get_Vendor_By_Id(int vendor_Id)
        {
            List<SqlParameter> sqlParamList = new List<SqlParameter>();
            sqlParamList.Add(new SqlParameter("@Vendor_Id", vendor_Id));

            VendorInfo Vendor = new VendorInfo();
            DataTable dt = _sqlHelper.ExecuteDataTable(sqlParamList, StoreProcedures.Get_Vendor_By_Id_Sp.ToString(), CommandType.StoredProcedure);
            foreach (DataRow dr in dt.Rows)
            {
                Vendor = Get_Vendor_Values(dr);
            }
            return Vendor;
        }

        public VendorInfo Get_Vendor_Profile_Data_By_User_Id(int user_Id)
        {
            List<SqlParameter> sqlParamList = new List<SqlParameter>();
            sqlParamList.Add(new SqlParameter("@User_Id", user_Id));

            VendorInfo Vendor = new VendorInfo();
            DataTable dt = _sqlHelper.ExecuteDataTable(sqlParamList, StoreProcedures.Get_Vendor_Profile_Data_Sp.ToString(), CommandType.StoredProcedure);
            foreach (DataRow dr in dt.Rows)
            {
                Vendor = Get_Vendor_Values(dr);
            }
            return Vendor;
        }

        public List<VendorInfo> Get_Vendors(ref PaginationInfo pager)
        {
            List<VendorInfo> Vendors = new List<VendorInfo>();
            DataTable dt = _sqlHelper.ExecuteDataTable(null, StoreProcedures.Get_Vendor_Sp.ToString(), CommandType.StoredProcedure);
            foreach (DataRow dr in CommonMethods.GetRows(dt, ref pager))
            {
                Vendors.Add(Get_Vendor_Values(dr));
            }
            return Vendors;
        }

        public List<VendorInfo> Get_Vendor_By_Id_List(int vendor_Id, ref PaginationInfo pager)
        {
            List<SqlParameter> sqlParamList = new List<SqlParameter>();
            sqlParamList.Add(new SqlParameter("@Vendor_Id", vendor_Id));

            List<VendorInfo> Vendor = new List<VendorInfo>();
            DataTable dt = _sqlHelper.ExecuteDataTable(sqlParamList, StoreProcedures.Get_Vendor_By_Id_Sp.ToString(), CommandType.StoredProcedure);
            foreach (DataRow dr in CommonMethods.GetRows(dt, ref pager))
            {
                Vendor.Add(Get_Vendor_Values(dr));
            }
            return Vendor;
        }

        private VendorInfo Get_Vendor_Values(DataRow dr)
        {
            VendorInfo Vendor = new VendorInfo();

            Vendor.Vendor_Id = Convert.ToInt32(dr["Vendor_Id"]);
            Vendor.Vendor_Name = Convert.ToString(dr["Vendor_Name"]);
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

        public bool Check_Existing_Vendor(string vendor_Name)
        {
            bool check = false;
            List<SqlParameter> sqlParam = new List<SqlParameter>();
            sqlParam.Add(new SqlParameter("@Vendor_Name", vendor_Name));

            DataTable dt = _sqlHelper.ExecuteDataTable(sqlParam, StoreProcedures.Check_Existing_Vendor.ToString(), CommandType.StoredProcedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    check = Convert.ToBoolean(dr["Check_Vendor"]);
                }
            }
            return check;
        }

        public void Insert_Vendor_Bank_Details(VendorInfo vendor,int user_Id)
        {
            foreach (var item in vendor.BankDetailsList)
            {
                List<SqlParameter> sqlparam = new List<SqlParameter>();

                sqlparam.Add(new SqlParameter("@Vendor_Id", vendor.Vendor_Id));
                sqlparam.Add(new SqlParameter("@Bank_Name", item.Bank_Name));
                sqlparam.Add(new SqlParameter("@Account_No", item.Account_No));
                sqlparam.Add(new SqlParameter("@Ifsc_Code", item.Ifsc_Code));
                sqlparam.Add(new SqlParameter("@Status", item.Status));
                sqlparam.Add(new SqlParameter("@Created_On", DateTime.Now));
                sqlparam.Add(new SqlParameter("@Created_By", user_Id));
                sqlparam.Add(new SqlParameter("@Updated_On", DateTime.Now));
                sqlparam.Add(new SqlParameter("@Updated_By", user_Id));

                _sqlHelper.ExecuteNonQuery(sqlparam, StoreProcedures.Insert_Vendor_Bank_Details_Sp.ToString(), CommandType.StoredProcedure);
            }
        }

        public List<Bank_Details> Get_Vendor_Bank_Details(int vendor_Id)
        {
            List<Bank_Details> bankdetailslist = new List<Bank_Details>();

            List<SqlParameter> sqlparam = new List<SqlParameter>();

            sqlparam.Add(new SqlParameter("@Vendor_Id", vendor_Id));

            DataTable dt = _sqlHelper.ExecuteDataTable(sqlparam, StoreProcedures.Get_Vendor_Bank_Details_By_Id_Sp.ToString(), CommandType.StoredProcedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Bank_Details list = new Bank_Details();

                    if (!dr.IsNull("Bank_Name"))
                        list.Bank_Name = Convert.ToString(dr["Bank_Name"]);
                    if (!dr.IsNull("Account_No"))
                        list.Account_No = Convert.ToString(dr["Account_No"]);
                    if (!dr.IsNull("IFSC_Code"))
                        list.Ifsc_Code = Convert.ToString(dr["IFSC_Code"]);
                    if (!dr.IsNull("Is_Active"))
                        list.Status = Convert.ToBoolean(dr["Is_Active"]);

                    bankdetailslist.Add(list);
                }
            }

            return bankdetailslist;
        }

        public List<ProductInfo> Get_Productmapping(int brand_Id)
        {
            //Pager.PageSize = 20;
            List<ProductInfo> products = new List<ProductInfo>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter("@Brand_Id", brand_Id));
            DataTable dt = _sqlHelper.ExecuteDataTable(sqlParams, StoreProcedures.Get_Productmapping.ToString(), CommandType.StoredProcedure);
            foreach (DataRow dr in CommonMethods.GetRows(dt))
            {
                products.Add(Get_Product_Values(dr));
            }
            return products;
        }

        private ProductInfo Get_Product_Values(DataRow dr)
        {
            ProductInfo product = new ProductInfo();
            if (!dr.IsNull("Product_Id"))
            product.Product_Id = Convert.ToInt32(dr["Product_Id"]);
            if (!dr.IsNull("Product_Name"))
            product.Product_Name = Convert.ToString(dr["Product_Name"]);
             if (!dr.IsNull("Image_Code"))
            product.Product_Image = Convert.ToString(dr["Image_Code"]);
            if (!dr.IsNull("Product_Price"))
            product.Product_Price = Convert.ToDecimal(dr["Product_Price"]);
            if (!dr.IsNull("MasterProductPrice"))
                product.MasterProductPrice = Convert.ToDecimal(dr["MasterProductPrice"]);

            return product;
        }

        public List<BrandInfo> Get_Brands()
        {
            List<BrandInfo> brandslist = new List<BrandInfo>();
            List<SqlParameter> sqlparam = new List<SqlParameter>();
            DataTable dt = _sqlHelper.ExecuteDataTable(sqlparam, StoreProcedures.Get_Brands_Sp.ToString(), CommandType.StoredProcedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    BrandInfo list = new BrandInfo();

                    if (!dr.IsNull("Brand_Id"))
                        list.Brand_Id = Convert.ToInt32(dr["Brand_Id"]);
                    if (!dr.IsNull("Brand_Name"))
                        list.Brand_Name = Convert.ToString(dr["Brand_Name"]);

                    brandslist.Add(list);
                }
            }

            return brandslist;
        }

        public void Insert_Vendor_Product_Mapping_Details(List<ProductInfo> product_List, int user_Id, int vendor_Id,int brand_Id)
        {
            List<SqlParameter> sqlparam = new List<SqlParameter>();

            sqlparam.Add(new SqlParameter("@Vendor_Id", vendor_Id));
            sqlparam.Add(new SqlParameter("@Brand_Id", brand_Id));


            _sqlHelper.ExecuteDataTable(sqlparam, StoreProcedures.Delete_Vendor_Product_Mapping_By_Id_Sp.ToString(), CommandType.StoredProcedure);

            foreach (var item in product_List)
            {
                List<SqlParameter> sqlparamnew = new List<SqlParameter>();

                sqlparamnew.Add(new SqlParameter("@Vendor_Id", vendor_Id));
                sqlparamnew.Add(new SqlParameter("@Brand_Id", brand_Id));
                sqlparamnew.Add(new SqlParameter("@Product_Id", Convert.ToInt32(item.Product_Id)));
                sqlparamnew.Add(new SqlParameter("@Product_Price", Convert.ToDecimal(item.Product_Price)));

                sqlparamnew.Add(new SqlParameter("@Created_On", DateTime.Now));
                sqlparamnew.Add(new SqlParameter("@Created_By", user_Id));
                sqlparamnew.Add(new SqlParameter("@Updated_On", DateTime.Now));
                sqlparamnew.Add(new SqlParameter("@Updated_By", user_Id));

                if(item.Check == true)
                {
                    _sqlHelper.ExecuteNonQuery(sqlparamnew, StoreProcedures.Insert_Vendor_Product_Mapping_Details.ToString(), CommandType.StoredProcedure);
                }
              
            }
        }

        public List<ProductInfo> Get_Vendor_Mapped_Product_List(int vendor_Id , int brand_Id)
        {
            List<ProductInfo> productlist = new List<ProductInfo>();

            List<SqlParameter> sqlparam = new List<SqlParameter>();

            sqlparam.Add(new SqlParameter("@Vendor_Id", vendor_Id));

            //sqlparam.Add(new SqlParameter("@Vendor_Id", brand_Id));


            DataTable dt = _sqlHelper.ExecuteDataTable(sqlparam, StoreProcedures.Get_Vendor_Mapped_Products_Sp.ToString(), CommandType.StoredProcedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ProductInfo list = new ProductInfo();

                    if (!dr.IsNull("Product_Id"))
                        list.Product_Id = Convert.ToInt32(dr["Product_Id"]);

                    if (!dr.IsNull("Vendor_Id"))
                        list.Vendor_Id = Convert.ToInt32(dr["Vendor_Id"]);

                    if (!dr.IsNull("Brand_Id"))
                        list.Brand_Id = Convert.ToInt32(dr["Brand_Id"]);

                    //if (!dr.IsNull("Product_Price"))

                    list.Product_Price = Convert.ToDecimal(dr["Product_Price"]);    
                    list.Product_Ids = list.Product_Id + ",";


                    productlist.Add(list);
                }
            }

            return productlist;
        }

        public List<AutocompleteInfo> Get_Vendor_Autocomplete(string vendor)
        {
            List<AutocompleteInfo> autoList = new List<AutocompleteInfo>();
            List<SqlParameter> sqlparam = new List<SqlParameter>();
            sqlparam.Add(new SqlParameter("@Description", vendor == null ? System.String.Empty : vendor.Trim()));
            DataTable dt = _sqlHelper.ExecuteDataTable(sqlparam, StoreProcedures.Get_Vendor_Autocomplete_Sp.ToString(), CommandType.StoredProcedure);
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

        public void Update_Vendor_Profile(VendorInfo vendor, int user_Id)
        {
            _sqlHelper.ExecuteNonQuery(Set_Values_In_Vendor_Profile(vendor, user_Id), StoreProcedures.Update_Vendor_Profile_Sp.ToString(), CommandType.StoredProcedure);
        }

        private List<SqlParameter> Set_Values_In_Vendor_Profile(VendorInfo vendor, int user_Id)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            sqlParams.Add(new SqlParameter("@Vendor_Id", vendor.Vendor_Id));

            sqlParams.Add(new SqlParameter("@Vendor_Name", vendor.Vendor_Name));
            sqlParams.Add(new SqlParameter("@Address", vendor.Address));
            sqlParams.Add(new SqlParameter("@City", vendor.City));
            sqlParams.Add(new SqlParameter("@State", vendor.State));
            sqlParams.Add(new SqlParameter("@Pincode", vendor.Pincode));
            sqlParams.Add(new SqlParameter("@Contact_No_1", vendor.Contact_No_1));
            sqlParams.Add(new SqlParameter("@Contact_No_2", vendor.Contact_No_2));
            sqlParams.Add(new SqlParameter("@Email", vendor.Email));

            sqlParams.Add(new SqlParameter("@Updated_On", DateTime.Now));
            sqlParams.Add(new SqlParameter("@Updated_By", user_Id));

            return sqlParams;
        }

        #region Vendor Sales Orders

        public List<PurchaseOrderInfo> Get_Sales_Orders(int Vendor_Id, ref PaginationInfo Pager)
        {
            List<SqlParameter> sqlParamList = new List<SqlParameter>();
            sqlParamList.Add(new SqlParameter("@VendorId", Vendor_Id));

            List<PurchaseOrderInfo> purchaseorders = new List<PurchaseOrderInfo>();
            DataTable dt = _sqlHelper.ExecuteDataTable(sqlParamList, StoreProcedures.Get_Vendor_Sales_Orders_Sp.ToString(), CommandType.StoredProcedure);
            foreach (DataRow dr in CommonMethods.GetRows(dt, ref Pager))
            {
                purchaseorders.Add(Get_Sales_Order_Values(dr));
            }
            return purchaseorders;
        }

        public List<PurchaseOrderInfo> Get_Vendor_Sales_Order_By_Id(int Sales_Order_Id, int Vendor_Id, ref PaginationInfo Pager)
        {
            List<SqlParameter> sqlParamList = new List<SqlParameter>();
            sqlParamList.Add(new SqlParameter("@Purchase_Order_Id", Sales_Order_Id));
            sqlParamList.Add(new SqlParameter("@Vendor_Id", Vendor_Id));

            List<PurchaseOrderInfo> purchaseOrders = new List<PurchaseOrderInfo>();
            DataTable dt = _sqlHelper.ExecuteDataTable(sqlParamList, StoreProcedures.Get_Vendors_Sales_Order_By_Id_Sp.ToString(), CommandType.StoredProcedure);

            foreach (DataRow dr in CommonMethods.GetRows(dt, ref Pager))
            {
                purchaseOrders.Add(Get_Sales_Order_Values(dr));
            }
            return purchaseOrders;
        }

        private PurchaseOrderInfo Get_Sales_Order_Values(DataRow dr)
        {
            PurchaseOrderInfo purchaseorder = new PurchaseOrderInfo();

            purchaseorder.Purchase_Order_Id = Convert.ToInt32(dr["Purchase_Order_Id"]);

            if (!dr.IsNull("Purchase_Order_No"))
                purchaseorder.Purchase_Order_No = Convert.ToString(dr["Purchase_Order_No"]);

            if (!dr.IsNull("Vendor_Id"))
                purchaseorder.Vendor_Id = Convert.ToInt32(dr["Vendor_Id"]);

            if (!dr.IsNull("Gross_Amount"))
                purchaseorder.Gross_Amount = Convert.ToDecimal(dr["Gross_Amount"]);

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

        public List<PurchaseOrderItemInfo> Get_Sales_Order_Items_By_Id(int Purchase_Order_Id)
        {
            List<SqlParameter> sqlParamList = new List<SqlParameter>();
            sqlParamList.Add(new SqlParameter("@Purchase_Order_Id", Purchase_Order_Id));

            List<PurchaseOrderItemInfo> purchaseOrders = new List<PurchaseOrderItemInfo>();
            DataTable dt = _sqlHelper.ExecuteDataTable(sqlParamList, StoreProcedures.Get_Vendor_Sales_Order_Items_By_Id_Sp.ToString(), CommandType.StoredProcedure);

            foreach (DataRow dr in dt.Rows)
            {
                purchaseOrders.Add(Get_Sales_Order_Items_Values(dr));
            }
            return purchaseOrders;
        }

        private PurchaseOrderItemInfo Get_Sales_Order_Items_Values(DataRow dr)
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

        public PurchaseOrderInfo Get_Vendor_Sales_Order_By_Id(int Purchase_Order_Id,int Vendor_Id)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("@Purchase_Order_Id", Purchase_Order_Id));
            paramList.Add(new SqlParameter("@Vendor_Id", Vendor_Id));

            PurchaseOrderInfo purchaseorder = new PurchaseOrderInfo();
            DataTable dt = _sqlHelper.ExecuteDataTable(paramList, StoreProcedures.Get_Vendors_Sales_Order_By_Id_Sp.ToString(), CommandType.StoredProcedure);
            foreach (DataRow dr in dt.Rows)
            {
                purchaseorder = Get_Sales_Order_Values(dr);
            }
            return purchaseorder;
        }

        public ProductInfo Get_Product_By_Id(int Product_Id)
        {
            List<SqlParameter> sqlParamList = new List<SqlParameter>();
            sqlParamList.Add(new SqlParameter("@Product_Id", Product_Id));

            ProductInfo product = new ProductInfo();
            DataTable dt = _sqlHelper.ExecuteDataTable(sqlParamList, StoreProcedures.Get_Product_By_Id_Sp.ToString(), CommandType.StoredProcedure);

            foreach (DataRow dr in dt.Rows)
            {
                product = Get_ProductInfo_Values(dr);
            }
            return product;
        }

        private ProductInfo Get_ProductInfo_Values(DataRow dr)
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
            product.Is_Biddable = Convert.ToBoolean(dr["Is_Biddable"]);
            product.Is_Active = Convert.ToBoolean(dr["Is_Active"]);
            product.Created_On = Convert.ToDateTime(dr["Created_On"]);
            product.Created_By = Convert.ToInt32(dr["Created_By"]);
            product.Updated_On = Convert.ToDateTime(dr["Updated_On"]);
            product.Updated_By = Convert.ToInt32(dr["Updated_By"]);
            return product;
        }

        public List<AutocompleteInfo> Get_Vendor_Sales_Order_Autocomplete(string Purchase_Order_No, int Vendor_Id)
        {
            List<AutocompleteInfo> autoList = new List<AutocompleteInfo>();
            List<SqlParameter> sqlparam = new List<SqlParameter>();
            sqlparam.Add(new SqlParameter("@Description", Purchase_Order_No == null ? System.String.Empty : Purchase_Order_No.Trim()));
            sqlparam.Add(new SqlParameter("@Vendor_Id", Vendor_Id));

            DataTable dt = _sqlHelper.ExecuteDataTable(sqlparam, StoreProcedures.Get_Vendor_Sales_Order_Autocomplete_Sp.ToString(), CommandType.StoredProcedure);
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
    }
}
