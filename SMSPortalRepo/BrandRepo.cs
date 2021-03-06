﻿using SMSPortalHelper;
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
    public class BrandRepo
    {
        SQLHelper _sqlRepo;

        public BrandRepo()
        {
            _sqlRepo = new SQLHelper();
        }

        public void Insert_Brand(BrandInfo brand, int user_id)
        {
            _sqlRepo.ExecuteNonQuery(Set_Values_In_Brand(brand, user_id), StoreProcedures.Insert_Brand_Sp.ToString(), CommandType.StoredProcedure);

        }

        public void Update_Brand(BrandInfo brand, int user_id)
        {
            _sqlRepo.ExecuteNonQuery(Set_Values_In_Brand(brand,user_id), StoreProcedures.Update_Brand_Sp.ToString(), CommandType.StoredProcedure);
        }

        private List<SqlParameter> Set_Values_In_Brand(BrandInfo brand, int user_id)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            if (brand.Brand_Id != 0)
            {
                sqlParams.Add(new SqlParameter("@Brand_Id", brand.Brand_Id));
            }
            sqlParams.Add(new SqlParameter("@Brand_Name", brand.Brand_Name));
            sqlParams.Add(new SqlParameter("@Brand_Category", brand.Brand_Category));
            sqlParams.Add(new SqlParameter("@Brand_Logo", brand.Brand_Logo));
            sqlParams.Add(new SqlParameter("@Website_Url", brand.Website_Url));
            sqlParams.Add(new SqlParameter("@Is_Active", brand.Is_Active));

            if (brand.Brand_Id == 0)
            {
                sqlParams.Add(new SqlParameter("@Created_On", DateTime.Now));
                sqlParams.Add(new SqlParameter("@Created_By", user_id));
            }

            sqlParams.Add(new SqlParameter("@Updated_On", DateTime.Now));
            sqlParams.Add(new SqlParameter("@Updated_By", user_id));
            return sqlParams;
        }

        public List<BrandInfo> Get_Brands(ref PaginationInfo Pager)
        {
            List<BrandInfo> brands = new List<BrandInfo>();
            DataTable dt = _sqlRepo.ExecuteDataTable(null, StoreProcedures.Get_Brand_Sp.ToString(), CommandType.StoredProcedure);
            foreach (DataRow dr in CommonMethods.GetRows(dt, ref Pager))
            {
                brands.Add(Get_Brand_Values(dr));
            }
            return brands;
        }

        public BrandInfo Get_Brand_By_Id(int Brand_Id)
        {
            List<SqlParameter> sqlParamList = new List<SqlParameter>();
            sqlParamList.Add(new SqlParameter("@Brand_Id", Brand_Id));

            BrandInfo brand = new BrandInfo();
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParamList, StoreProcedures.Get_Brand_By_Id_Sp.ToString(), CommandType.StoredProcedure);
             
            foreach (DataRow dr in dt.Rows)
            {
                brand = Get_Brand_Values(dr);
            }
            return brand;
        }
         
        public List<BrandInfo> Get_Brand_By_Id(int Brand_Id, ref PaginationInfo Pager)
        {
            List<SqlParameter> sqlParamList = new List<SqlParameter>();
            sqlParamList.Add(new SqlParameter("@Brand_Id", Brand_Id));

            List<BrandInfo> brands = new List<BrandInfo>();
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParamList, StoreProcedures.Get_Brand_By_Id_Sp.ToString(), CommandType.StoredProcedure);
             
            foreach (DataRow dr in CommonMethods.GetRows(dt, ref Pager))
            {
                brands.Add(Get_Brand_Values(dr));
            }
            return brands;
        }

        private BrandInfo Get_Brand_Values(DataRow dr)
        {
            BrandInfo brand = new BrandInfo();

            if (!dr.IsNull("Brand_Id"))
            brand.Brand_Id = Convert.ToInt32(dr["Brand_Id"]);

            if (!dr.IsNull("Brand_Name"))
            brand.Brand_Name = Convert.ToString(dr["Brand_Name"]);

            if (!dr.IsNull("Brand_Category"))
            brand.Brand_Category = Convert.ToInt32(dr["Brand_Category"]);

            if(brand.Brand_Category== (int)BrandCategory.Elite)
            {
                brand.Brand_Category_Name = BrandCategory.Elite.ToString();
            }
            if (brand.Brand_Category == (int)BrandCategory.Volumn_Based)
            {
                brand.Brand_Category_Name = BrandCategory.Volumn_Based.ToString().Replace('_', ' ');
            }
            if (brand.Brand_Category == (int)BrandCategory.Beyond_Borders)
            {
                brand.Brand_Category_Name = BrandCategory.Beyond_Borders.ToString().Replace('_', ' ');
            }

            if (!dr.IsNull("Brand_Logo"))
            brand.Brand_Logo = Convert.ToString(dr["Brand_Logo"]);

            if (!dr.IsNull("Website_Url"))
            brand.Website_Url = Convert.ToString(dr["Website_Url"]);

            if (!dr.IsNull("Is_Active"))
            brand.Is_Active = Convert.ToBoolean(dr["Is_Active"]);
            brand.Created_On = Convert.ToDateTime(dr["Created_On"]);
            brand.Created_By = Convert.ToInt32(dr["Created_By"]);
            brand.Updated_On = Convert.ToDateTime(dr["Updated_On"]);
            brand.Updated_By = Convert.ToInt32(dr["Updated_By"]);
            return brand;
        }

        public bool Check_Existing_Brand(string Brand_Name)
        {
            bool check = false;

            List<SqlParameter> sqlParam = new List<SqlParameter>();
            sqlParam.Add(new SqlParameter("@Brand_Name", Brand_Name));

            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParam, StoreProcedures.Check_Existing_Brand.ToString(), CommandType.StoredProcedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    check = Convert.ToBoolean(dr["Check_Brand"]);
                }
            }
            return check;
        }

        public void Update_Brand_FileName(int Brand_Id,string File_Name)
        {
            List<SqlParameter> sqlParam = new List<SqlParameter>();
            sqlParam.Add(new SqlParameter("@Brand_Id", Brand_Id));
            sqlParam.Add(new SqlParameter("@File_Name", File_Name));
            _sqlRepo.ExecuteNonQuery(sqlParam, StoreProcedures.Update_Brand_Image.ToString(), CommandType.StoredProcedure);

        }

        public void Delete_Brand_By_Id(int brand_id)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter("@Brand_Id", brand_id));
            _sqlRepo.ExecuteNonQuery(sqlParams, StoreProcedures.Delete_Brand_By_Id_Sp.ToString(), CommandType.StoredProcedure);
        }

        public List<AutocompleteInfo> Get_Brand_Autocomplete(string brandName)
        {
            List<AutocompleteInfo> autoList = new List<AutocompleteInfo>();
            List<SqlParameter> sqlparam = new List<SqlParameter>();
            sqlparam.Add(new SqlParameter("@Description", brandName == null ? System.String.Empty : brandName.Trim()));
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlparam, StoreProcedures.Get_Brand_Autocomplete_Sp.ToString(), CommandType.StoredProcedure);
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

        public void Update_Brand_Profile(BrandInfo brand,int user_Id)
        {
            _sqlRepo.ExecuteNonQuery(Set_Values_In_Brand_Profile(brand, user_Id), StoreProcedures.Update_Brand_Profile_Sp.ToString(), CommandType.StoredProcedure);
        }

        private List<SqlParameter> Set_Values_In_Brand_Profile(BrandInfo brand,int user_Id)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter("@Brand_Id", brand.Brand_Id));
            sqlParams.Add(new SqlParameter("@Brand_Name", brand.Brand_Name));
            sqlParams.Add(new SqlParameter("@Brand_Category", brand.Brand_Category));
            sqlParams.Add(new SqlParameter("@Website_Url", brand.Website_Url));
            sqlParams.Add(new SqlParameter("@Updated_On", DateTime.Now));
            sqlParams.Add(new SqlParameter("@Updated_By", user_Id));
            return sqlParams;
        }

        
    }
}
