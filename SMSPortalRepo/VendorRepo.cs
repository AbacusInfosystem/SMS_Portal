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
        SQLHelper _sqlRepo;

        public VendorRepo()
        {
            _sqlRepo = new SQLHelper();
        }

        public void Insert_Vendor(VendorInfo vendor , int user_Id)
        {
            _sqlRepo.ExecuteNonQuery(Set_Values_In_Vendor(vendor, user_Id), StoreProcedures.Insert_Vendor_Sp.ToString(), CommandType.StoredProcedure);
        }

        public void Update_Vendor(VendorInfo vendor , int user_Id)
        {
            _sqlRepo.ExecuteNonQuery(Set_Values_In_Vendor(vendor, user_Id), StoreProcedures.Update_Vendor_Sp.ToString(), CommandType.StoredProcedure);
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
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParamList, StoreProcedures.Get_Vendor_By_Id_Sp.ToString(), CommandType.StoredProcedure);
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
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParamList, StoreProcedures.Get_Vendor_Profile_Data_Sp.ToString(), CommandType.StoredProcedure);
            foreach (DataRow dr in dt.Rows)
            {
                Vendor = Get_Vendor_Values(dr);
            }
            return Vendor;
        }

        public List<VendorInfo> Get_Vendors(ref PaginationInfo pager)
        {
            List<VendorInfo> Vendors = new List<VendorInfo>();
            DataTable dt = _sqlRepo.ExecuteDataTable(null, StoreProcedures.Get_Vendor_Sp.ToString(), CommandType.StoredProcedure);
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
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParamList, StoreProcedures.Get_Vendor_By_Id_Sp.ToString(), CommandType.StoredProcedure);
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

            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParam, StoreProcedures.Check_Existing_Vendor.ToString(), CommandType.StoredProcedure);

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

                _sqlRepo.ExecuteNonQuery(sqlparam, StoreProcedures.Insert_Vendor_Bank_Details_Sp.ToString(), CommandType.StoredProcedure);
            }
        }

        public List<Bank_Details> Get_Vendor_Bank_Details(int vendor_Id)
        {
            List<Bank_Details> bankdetailslist = new List<Bank_Details>();

            List<SqlParameter> sqlparam = new List<SqlParameter>();

            sqlparam.Add(new SqlParameter("@Vendor_Id", vendor_Id));

            DataTable dt = _sqlRepo.ExecuteDataTable(sqlparam, StoreProcedures.Get_Vendor_Bank_Details_By_Id_Sp.ToString(), CommandType.StoredProcedure);

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
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParams, StoreProcedures.Get_Productmapping.ToString(), CommandType.StoredProcedure);
            foreach (DataRow dr in CommonMethods.GetRows(dt))
            {
                products.Add(Get_Product_Values(dr));
            }
            return products;
        }

        private ProductInfo Get_Product_Values(DataRow dr)
        {
            ProductInfo product = new ProductInfo();

            product.Product_Id = Convert.ToInt32(dr["Product_Id"]);
            product.Product_Name = Convert.ToString(dr["Product_Name"]);
            product.Product_Image = Convert.ToString(dr["Image_Code"]);
            return product;
        }

        public List<BrandInfo> Get_Brands()
        {
            List<BrandInfo> brandslist = new List<BrandInfo>();
            List<SqlParameter> sqlparam = new List<SqlParameter>();
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlparam, StoreProcedures.Get_Brands_Sp.ToString(), CommandType.StoredProcedure);

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


            _sqlRepo.ExecuteDataTable(sqlparam, StoreProcedures.Delete_Vendor_Product_Mapping_By_Id_Sp.ToString(), CommandType.StoredProcedure);

            foreach (var item in product_List)
            {
                List<SqlParameter> sqlparamnew = new List<SqlParameter>();

                sqlparamnew.Add(new SqlParameter("@Vendor_Id", vendor_Id));
                sqlparamnew.Add(new SqlParameter("@Brand_Id", brand_Id));
                sqlparamnew.Add(new SqlParameter("@Product_Id", Convert.ToInt32(item.Product_Id)));

                sqlparamnew.Add(new SqlParameter("@Created_On", DateTime.Now));
                sqlparamnew.Add(new SqlParameter("@Created_By", user_Id));
                sqlparamnew.Add(new SqlParameter("@Updated_On", DateTime.Now));
                sqlparamnew.Add(new SqlParameter("@Updated_By", user_Id));

                if(item.Check == true)
                {
                    _sqlRepo.ExecuteNonQuery(sqlparamnew, StoreProcedures.Insert_Vendor_Product_Mapping_Details.ToString(), CommandType.StoredProcedure);
                }
              
            }
        }

        public List<ProductInfo> Get_Vendor_Mapped_Product_List(int vendor_Id , int brand_Id)
        {
            List<ProductInfo> productlist = new List<ProductInfo>();

            List<SqlParameter> sqlparam = new List<SqlParameter>();

            sqlparam.Add(new SqlParameter("@Vendor_Id", vendor_Id));

            //sqlparam.Add(new SqlParameter("@Vendor_Id", brand_Id));
          

            DataTable dt = _sqlRepo.ExecuteDataTable(sqlparam, StoreProcedures.Get_Vendor_Mapped_Products_Sp.ToString(), CommandType.StoredProcedure);

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
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlparam, StoreProcedures.Get_Vendor_Autocomplete_Sp.ToString(), CommandType.StoredProcedure);
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
