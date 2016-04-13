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
    public class BrandRepo
    {
        SQLHelper _sqlRepo;

        public BrandRepo()
        {
            _sqlRepo = new SQLHelper();
        }

        public void Insert_Brand(BrandInfo brand)
        {
            _sqlRepo.ExecuteNonQuery(Set_Values_In_Brand(brand), StoreProcedures.Insert_Brand_Sp.ToString(), CommandType.StoredProcedure);

        }

        public void Update_Brand(BrandInfo brand)
        {
            _sqlRepo.ExecuteNonQuery(Set_Values_In_Brand(brand), StoreProcedures.Update_Brand_Sp.ToString(), CommandType.StoredProcedure);
        }

        private List<SqlParameter> Set_Values_In_Brand(BrandInfo brand)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            if (brand.Brand_Id != 0)
            {
                sqlParams.Add(new SqlParameter("@Brand_Id", brand.Brand_Id));
            }
            sqlParams.Add(new SqlParameter("@Brand_Name", brand.Brand_Name));
            sqlParams.Add(new SqlParameter("@Brand_Category", brand.Brand_Category));
            sqlParams.Add(new SqlParameter("@Brand_Logo", brand.Brand_Logo));
            sqlParams.Add(new SqlParameter("@Is_Active", brand.Is_Active));

            if (brand.Brand_Id == 0)
            {
                sqlParams.Add(new SqlParameter("@Created_On", brand.Created_On));
                sqlParams.Add(new SqlParameter("@Created_By", brand.Created_By));
            }

            sqlParams.Add(new SqlParameter("@Updated_On", brand.Updated_On));
            sqlParams.Add(new SqlParameter("@Updated_By", brand.Updated_By));
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
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParamList, StoreProcedures.Get_Brand_By_Name_Sp.ToString(), CommandType.StoredProcedure);
             
            foreach (DataRow dr in dt.Rows)
            {
                brand = Get_Brand_Values(dr);
            }
            return brand;
        }
         
        public List<BrandInfo> Get_Brand_By_Name(string Brand_Name, ref PaginationInfo Pager)
        {
            List<SqlParameter> sqlParamList = new List<SqlParameter>();             
            sqlParamList.Add(new SqlParameter("@Brand_Name", Brand_Name));

            List<BrandInfo> brands = new List<BrandInfo>();
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParamList, StoreProcedures.Get_Brand_By_Name_Sp.ToString(), CommandType.StoredProcedure);
             
            foreach (DataRow dr in CommonMethods.GetRows(dt, ref Pager))
            {
                brands.Add(Get_Brand_Values(dr));
            }
            return brands;
        }

        private BrandInfo Get_Brand_Values(DataRow dr)
        {
            BrandInfo brand = new BrandInfo();

            brand.Brand_Id = Convert.ToInt32(dr["Brand_Id"]);
            brand.Brand_Name = Convert.ToString(dr["Brand_Name"]);
            brand.Brand_Category = Convert.ToInt32(dr["Brand_Category"]);
            brand.Brand_Logo = Convert.ToString(dr["Brand_Logo"]);
            brand.Is_Active = Convert.ToBoolean(dr["Is_Active"]);
            brand.Created_On = Convert.ToDateTime(dr["Created_On"]);
            brand.Created_By = Convert.ToInt32(dr["Created_By"]);
            brand.Updated_On = Convert.ToDateTime(dr["Updated_On"]);
            brand.Updated_By = Convert.ToInt32(dr["Updated_By"]);
            return brand;
        }
        public void Delete_Brand_By_Id(int brand_id)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter("@Brand_Id", brand_id));
            _sqlRepo.ExecuteNonQuery(sqlParams, StoreProcedures.Delete_Brand_By_Id_Sp.ToString(), CommandType.StoredProcedure);
        }
    }
}
