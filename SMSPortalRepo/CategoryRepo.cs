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
    public class CategoryRepo
    {
        SQLHelper _sqlRepo = null;

        public CategoryRepo()
        {
            _sqlRepo = new SQLHelper();
        }

        public int Insert_Category(CategoryInfo category)
        {
            int category_id = 0;

            category_id=Convert.ToInt32(_sqlRepo.ExecuteScalerObj(Set_Values_In_Category(category), StoreProcedures.Insert_Category_Sp.ToString(), CommandType.StoredProcedure));

            return category_id;
        }

        public void Update_Category(CategoryInfo category)
        {
            _sqlRepo.ExecuteNonQuery(Set_Values_In_Category(category), StoreProcedures.Update_Category_Sp.ToString(), CommandType.StoredProcedure);
        }

        private List<SqlParameter> Set_Values_In_Category(CategoryInfo category)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            if (category.Category_Id != 0)
            {
                sqlParams.Add(new SqlParameter("@Category_Id", category.Category_Id));
            }
            sqlParams.Add(new SqlParameter("@Category_Name", category.Category_Name));
            sqlParams.Add(new SqlParameter("@IsActive", category.IsActive));

            if (category.Category_Id == 0)
            {
                sqlParams.Add(new SqlParameter("@Created_On", category.Created_On));
                sqlParams.Add(new SqlParameter("@Created_By", category.Created_By));
            }

            sqlParams.Add(new SqlParameter("@Updated_On", category.Updated_On));
            sqlParams.Add(new SqlParameter("@Updated_By", category.Updated_By));
            return sqlParams;
        }

        public List<CategoryInfo> Get_Categorys(ref PaginationInfo Pager)
        {
            List<CategoryInfo> categorys = new List<CategoryInfo>();
            DataTable dt = _sqlRepo.ExecuteDataTable(null, StoreProcedures.Get_Category_Sp.ToString(), CommandType.StoredProcedure);
            foreach (DataRow dr in CommonMethods.GetRows(dt, ref Pager))
            {
                categorys.Add(Get_Category_Values(dr));
            }
            return categorys;
        }

        public List<CategoryInfo> Get_Categorys_By_Id(int Category_Id,  ref PaginationInfo Pager)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Category_Id", Category_Id));

            List<CategoryInfo> categorys = new List<CategoryInfo>();

            DataTable dt = _sqlRepo.ExecuteDataTable(parameters, StoreProcedures.Get_Category_By_Id_Sp.ToString(), CommandType.StoredProcedure);
            foreach (DataRow dr in CommonMethods.GetRows(dt, ref Pager))
            {
                categorys.Add(Get_Category_Values(dr));
            }
            return categorys;
        }            

        public CategoryInfo Get_Category_By_Id(int Category_Id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Category_Id", Category_Id));

            CategoryInfo category = new CategoryInfo();
            DataTable dt = _sqlRepo.ExecuteDataTable(parameters, StoreProcedures.Get_Category_By_Id_Sp.ToString(), CommandType.StoredProcedure);
            List<DataRow> drList = new List<DataRow>();
            drList = dt.AsEnumerable().ToList();
            foreach (DataRow dr in drList)
            {
                category = Get_Category_Values(dr);
            }
            return category;
        }

        private CategoryInfo Get_Category_Values(DataRow dr)
        {
            CategoryInfo category = new CategoryInfo();

            category.Category_Id = Convert.ToInt32(dr["Category_Id"]);

            if (!dr.IsNull("Category_Name"))
            category.Category_Name = Convert.ToString(dr["Category_Name"]);            
            category.IsActive = Convert.ToBoolean(dr["IsActive"]);
            category.Created_On = Convert.ToDateTime(dr["Created_On"]);
            category.Created_By = Convert.ToInt32(dr["Created_By"]);
            category.Updated_On = Convert.ToDateTime(dr["Updated_On"]);
            category.Updated_By = Convert.ToInt32(dr["Updated_By"]);
            return category;
        }
      
        public void Delete_Category_By_Id(int category_id)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter("@Category_Id", category_id));
            _sqlRepo.ExecuteNonQuery(sqlParams, StoreProcedures.Delete_Category_By_Id_Sp.ToString(), CommandType.StoredProcedure);
        }

        public bool Check_Existing_Category(string Category_Name)
        {
            bool check = false;

            List<SqlParameter> sqlParam = new List<SqlParameter>();
            sqlParam.Add(new SqlParameter("@Category_Name", Category_Name));

            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParam, StoreProcedures.Check_Existing_Category.ToString(), CommandType.StoredProcedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                //int count = dt.Rows.Count;

                List<DataRow> drList = new List<DataRow>();

                drList = dt.AsEnumerable().ToList();

                foreach (DataRow dr in drList)
                {
                    check = Convert.ToBoolean(dr["Check_Category"]);
                }
            }

            return check;
        }

        public List<AutocompleteInfo> Get_Category_Autocomplete(string category)
        {
            List<AutocompleteInfo> autoList = new List<AutocompleteInfo>();
            List<SqlParameter> sqlparam = new List<SqlParameter>();
            sqlparam.Add(new SqlParameter("@Description", category == null ? System.String.Empty : category.Trim()));
            DataTable dt = _sqlRepo.ExecuteDataTable(sqlparam, StoreProcedures.Get_Category_Autocomplete_Sp.ToString(), CommandType.StoredProcedure);
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
