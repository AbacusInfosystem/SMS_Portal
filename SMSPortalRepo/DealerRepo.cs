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
    public class DealerRepo
    {
        SQLHelper _sqlRepo;

        public DealerRepo()
        {
            _sqlRepo = new SQLHelper();
        }

        public void Insert_Dealer(DealerInfo dealer)
        {
            _sqlRepo.ExecuteNonQuery(Set_Values_In_Dealer(dealer), StoreProcedures.Insert_Dealer_Sp.ToString(), CommandType.StoredProcedure);
        }

        public void Update_Dealer(DealerInfo dealer)
        {
            _sqlRepo.ExecuteNonQuery(Set_Values_In_Dealer(dealer), StoreProcedures.Update_Dealer_Sp.ToString(), CommandType.StoredProcedure);
        }

        private List<SqlParameter> Set_Values_In_Dealer(DealerInfo dealer)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            if (dealer.Dealer_Id != 0)
            {
                sqlParams.Add(new SqlParameter("@Dealer_Id", dealer.Dealer_Id));
            }
            sqlParams.Add(new SqlParameter("@Dealer_Name", dealer.Dealer_Name));
            sqlParams.Add(new SqlParameter("@Brand_Id", dealer.Brand_Id));
            sqlParams.Add(new SqlParameter("@Address", dealer.Address));
            sqlParams.Add(new SqlParameter("@City", dealer.City));
            sqlParams.Add(new SqlParameter("@State", dealer.State));
            sqlParams.Add(new SqlParameter("@Pincode", dealer.Pincode));
            sqlParams.Add(new SqlParameter("@Contact_No_1", dealer.Contact_No_1));
            sqlParams.Add(new SqlParameter("@Contact_No_2", dealer.Contact_No_2));
            sqlParams.Add(new SqlParameter("@Email", dealer.Email));
            sqlParams.Add(new SqlParameter("@Dealer_Percentage_Share", dealer.Dealer_Percentage_Share));
            sqlParams.Add(new SqlParameter("@Brand_Percentage_Share", dealer.Brand_Percentage_Share));
            sqlParams.Add(new SqlParameter("@Is_Active", dealer.Is_Active));
            if (dealer.Dealer_Id == 0)
            {
                sqlParams.Add(new SqlParameter("@Created_On", dealer.Created_On));
                sqlParams.Add(new SqlParameter("@Created_By", dealer.Created_By));
            }
            sqlParams.Add(new SqlParameter("@Updated_On", dealer.Updated_On));
            sqlParams.Add(new SqlParameter("@Updated_By", dealer.Updated_By));
            return sqlParams;
        }

        public List<DealerInfo> Get_Dealers(ref PaginationInfo Pager)
        {
            List<DealerInfo> dealers = new List<DealerInfo>();
            DataTable dt = _sqlRepo.ExecuteDataTable(null, StoreProcedures.Get_Dealer_Sp.ToString(), CommandType.StoredProcedure);
            foreach (DataRow dr in CommonMethods.GetRows(dt, ref Pager))
            {
                dealers.Add(Get_Dealer_Values(dr));
            }
            return dealers;
        }

        public DealerInfo Get_Dealer_By_Id(int Dealer_Id)
        {
            List<SqlParameter> sqlParamList = new List<SqlParameter>();
            sqlParamList.Add(new SqlParameter("@Dealer_Id", Dealer_Id));

            DealerInfo dealer = new DealerInfo();
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParamList, StoreProcedures.Get_Dealer_By_Id_Sp.ToString(), CommandType.StoredProcedure);
            foreach (DataRow dr in dt.Rows)
            {
                dealer = Get_Dealer_Values(dr);
            }
            return dealer;
        }

        public List<DealerInfo> Get_Dealer_By_Id(int Dealer_Id, ref PaginationInfo Pager)
        {
            List<SqlParameter> sqlParamList = new List<SqlParameter>();
            sqlParamList.Add(new SqlParameter("@Dealer_Id", Dealer_Id));

            List<DealerInfo> dealer = new List<DealerInfo>();
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParamList, StoreProcedures.Get_Dealer_By_Id_Sp.ToString(), CommandType.StoredProcedure);
            foreach (DataRow dr in CommonMethods.GetRows(dt, ref Pager))
            {
                dealer.Add(Get_Dealer_Values(dr));
            }
            return dealer;
        }

        private DealerInfo Get_Dealer_Values(DataRow dr)
        {
            DealerInfo dealer = new DealerInfo();

            dealer.Dealer_Id = Convert.ToInt32(dr["Dealer_Id"]);
            dealer.Dealer_Name = Convert.ToString(dr["Dealer_Name"]);
            dealer.Brand_Id = Convert.ToInt32(dr["Brand_Id"]);
            dealer.Brand_Name = Convert.ToString(dr["Brand_Name"]);
            dealer.Address = Convert.ToString(dr["Address"]);
            dealer.City = Convert.ToString(dr["City"]);
            dealer.State = Convert.ToInt32(dr["State"]);
            dealer.Pincode = Convert.ToInt32(dr["Pincode"]);
            dealer.Contact_No_1 = Convert.ToString(dr["Contact_No_1"]);
            dealer.Contact_No_2 = Convert.ToString(dr["Contact_No_2"]);
            dealer.Email = Convert.ToString(dr["Email"]);
            dealer.Dealer_Percentage_Share = Convert.ToInt32(dr["Dealer_Percentage_Share"]);
            dealer.Brand_Percentage_Share = Convert.ToInt32(dr["Brand_Percentage_Share"]);
            dealer.Is_Active = Convert.ToBoolean(dr["Is_Active"]);
            dealer.Created_On = Convert.ToDateTime(dr["Created_On"]);
            dealer.Created_By = Convert.ToInt32(dr["Created_By"]);
            dealer.Updated_On = Convert.ToDateTime(dr["Updated_On"]);
            dealer.Updated_By = Convert.ToInt32(dr["Updated_By"]);
            return dealer;
        }

        public bool Check_Existing_Dealer(string Dealer_Name)
        {
            bool check = false;
            List<SqlParameter> sqlParam = new List<SqlParameter>();
            sqlParam.Add(new SqlParameter("@Dealer_Name", Dealer_Name));

            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParam, StoreProcedures.Check_Existing_Dealer.ToString(), CommandType.StoredProcedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    check = Convert.ToBoolean(dr["Check_Dealer"]);
                }
            }
            return check;
        }

        public List<BrandInfo> Get_Brands()
        {
            List<BrandInfo> brandList = new List<BrandInfo>();
            List<SqlParameter> sqlparam = new List<SqlParameter>();

            DataTable dt = _sqlRepo.ExecuteDataTable(sqlparam, StoreProcedures.Get_Brand_Sp.ToString(), CommandType.StoredProcedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    BrandInfo list = new BrandInfo();

                    if (!dr.IsNull("Brand_Id"))
                        list.Brand_Id = Convert.ToInt32(dr["Brand_Id"]);
                    if (!dr.IsNull("Brand_Name"))
                        list.Brand_Name = Convert.ToString(dr["Brand_Name"]);

                    brandList.Add(list);
                }
            }

            return brandList;
        }

        public List<AutocompleteInfo> Get_Dealer_Autocomplete(string DealerName)
        {
            List<AutocompleteInfo> autoList = new List<AutocompleteInfo>();
            List<SqlParameter> sqlparam = new List<SqlParameter>();
            sqlparam.Add(new SqlParameter("@Description", DealerName == null ? System.String.Empty : DealerName.Trim()));
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlparam, StoreProcedures.Get_Dealer_Autocomplete_Sp.ToString(), CommandType.StoredProcedure);
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
