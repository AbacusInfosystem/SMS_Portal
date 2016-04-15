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

        public void Insert_Vendor(VendorInfo Vendor)
        {
            _sqlRepo.ExecuteNonQuery(Set_Values_In_Vendor(Vendor), StoreProcedures.Insert_Vendor_Sp.ToString(), CommandType.StoredProcedure);
        }

        public void Update_Vendor(VendorInfo Vendor)
        {
            _sqlRepo.ExecuteNonQuery(Set_Values_In_Vendor(Vendor), StoreProcedures.Update_Vendor_Sp.ToString(), CommandType.StoredProcedure);
        }

        private List<SqlParameter> Set_Values_In_Vendor(VendorInfo Vendors)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            if (Vendors.Vendor_Id != 0)
            {
                sqlParams.Add(new SqlParameter("@Vendor_Id", Vendors.Vendor_Id));
            }

            sqlParams.Add(new SqlParameter("@Vendor_Name", Vendors.Vendor_Name));
            sqlParams.Add(new SqlParameter("@Address", Vendors.Address));
            sqlParams.Add(new SqlParameter("@City", Vendors.City));
            sqlParams.Add(new SqlParameter("@State", Vendors.State));
            sqlParams.Add(new SqlParameter("@Pincode", Vendors.Pincode));
            sqlParams.Add(new SqlParameter("@Contact_No_1", Vendors.Contact_No_1));
            sqlParams.Add(new SqlParameter("@Contact_No_2", Vendors.Contact_No_2));
            sqlParams.Add(new SqlParameter("@Email", Vendors.Email));
            sqlParams.Add(new SqlParameter("@Is_Active", Vendors.Is_Active));
            if (Vendors.Vendor_Id == 0)
            {
                sqlParams.Add(new SqlParameter("@Created_On", Vendors.Created_On));
                sqlParams.Add(new SqlParameter("@Created_By", Vendors.Created_By));
            }
            sqlParams.Add(new SqlParameter("@Updated_On", Vendors.Updated_On));
            sqlParams.Add(new SqlParameter("@Updated_By", Vendors.Updated_By));
            return sqlParams;
        }

        public VendorInfo Get_Vendor_By_Id(int Vendor_Id)
        {
            List<SqlParameter> sqlParamList = new List<SqlParameter>();
            sqlParamList.Add(new SqlParameter("@Vendor_Id", Vendor_Id));

            VendorInfo Vendor = new VendorInfo();
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParamList, StoreProcedures.Get_Vendor_By_Id_Sp.ToString(), CommandType.StoredProcedure);
            foreach (DataRow dr in dt.Rows)
            {
                Vendor = Get_Vendor_Values(dr);
            }
            return Vendor;
        }

        public List<VendorInfo> Get_Vendors(ref PaginationInfo Pager)
        {
            List<VendorInfo> Vendors = new List<VendorInfo>();
            DataTable dt = _sqlRepo.ExecuteDataTable(null, StoreProcedures.Get_Vendor_Sp.ToString(), CommandType.StoredProcedure);
            foreach (DataRow dr in CommonMethods.GetRows(dt, ref Pager))
            {
                Vendors.Add(Get_Vendor_Values(dr));
            }
            return Vendors;
        }

        public List<VendorInfo> Get_Vendor_By_Name(string Vendor_Name, ref PaginationInfo Pager)
        {
            List<SqlParameter> sqlParamList = new List<SqlParameter>();
            sqlParamList.Add(new SqlParameter("@Vendor_Name", Vendor_Name));

            List<VendorInfo> Vendor = new List<VendorInfo>();
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParamList, StoreProcedures.Get_Vendor_By_Name_Sp.ToString(), CommandType.StoredProcedure);
            foreach (DataRow dr in CommonMethods.GetRows(dt, ref Pager))
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

        public bool Check_Existing_Vendor(string Vendor_Name)
        {
            bool check = false;
            List<SqlParameter> sqlParam = new List<SqlParameter>();
            sqlParam.Add(new SqlParameter("@Vendor_Name", Vendor_Name));

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
    }
}
