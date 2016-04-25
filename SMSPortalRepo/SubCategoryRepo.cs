using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMSPortalHelper;
using SMSPortalInfo;
using SMSPortalInfo.Common;
using SMSPortalRepo.Common;
using System.Data;
using System.Data.SqlClient;

namespace SMSPortalRepo
{
    public class SubCategoryRepo
    {
        SQLHelper _sqlHelper = null;

        public SubCategoryRepo()
        {
            _sqlHelper = new SQLHelper();
        }

        public List<SubCategoryInfo> Get_Subcategory_Modules()
        {
            List<SubCategoryInfo> modulelist = new List<SubCategoryInfo>();

            List<SqlParameter> sqlparam = new List<SqlParameter>();

            DataTable dt = _sqlHelper.ExecuteDataTable(sqlparam, StoreProcedures.Get_Sub_Category_Sp.ToString(), CommandType.StoredProcedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SubCategoryInfo list = new SubCategoryInfo();

                    if (!dr.IsNull("Sub_Category_Id"))
                        list.Subcategory_Id = Convert.ToInt32(dr["Sub_Category_Id"]);
                    if (!dr.IsNull("Sub_Category_Name"))
                        list.Subcategory_Name = Convert.ToString(dr["Sub_Category_Name"]);

                    modulelist.Add(list);
                }
            }

            return modulelist;
        }

        public List<CategoryInfo> Get_Categories()
        {
            List<CategoryInfo> categorylist = new List<CategoryInfo>();

            List<SqlParameter> sqlparam = new List<SqlParameter>();

            DataTable dt = _sqlHelper.ExecuteDataTable(sqlparam, StoreProcedures.Get_Category_Sp.ToString(), CommandType.StoredProcedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    CategoryInfo list = new CategoryInfo();

                    if (!dr.IsNull("Category_Id"))
                        list.Category_Id = Convert.ToInt32(dr["Category_Id"]);
                    if (!dr.IsNull("Category_Name"))
                        list.Category_Name = Convert.ToString(dr["Category_Name"]);

                    categorylist.Add(list);
                }
            }

            return categorylist;
        }

        public List<SubCategoryInfo> Get_SubCategories(ref PaginationInfo Pager)
        {
            List<SubCategoryInfo> subcategories = new List<SubCategoryInfo>();

            DataTable dt = _sqlHelper.ExecuteDataTable(null, StoreProcedures.Get_Sub_Category_Sp.ToString(), CommandType.StoredProcedure);

            foreach (DataRow dr in CommonMethods.GetRows(dt, ref Pager))
            {
                subcategories.Add(Get_SubCategory_Values(dr));
            }

            return subcategories;
        }

        public List<SubCategoryInfo> Get_SubCategories_By_CategoryId(int Category_Id)
        {
            List<SqlParameter> sqlParam = new List<SqlParameter>();
            sqlParam.Add(new SqlParameter("@Category_Id", Category_Id));

            List<SubCategoryInfo> subcategories = new List<SubCategoryInfo>();

            DataTable dt = _sqlHelper.ExecuteDataTable(sqlParam, StoreProcedures.Get_Sub_Category_By_Category_Sp.ToString(), CommandType.StoredProcedure);

            foreach (DataRow dr in dt.Rows)
            {
                subcategories.Add(Get_SubCategory_Values_By_Category(dr));
            }

            return subcategories;
        }

        public List<SubCategoryInfo> Get_Subcategory_By_Module_Id(int module_Id, ref PaginationInfo pager)
        {
            List<SubCategoryInfo> subcategory = new List<SubCategoryInfo>();

            List<SqlParameter> sqlparam = new List<SqlParameter>();

            sqlparam.Add(new SqlParameter("Sub_Category_Id", module_Id));

            DataTable dt = _sqlHelper.ExecuteDataTable(sqlparam, StoreProcedures.Get_Sub_Category_By_Id_Sp.ToString(), CommandType.StoredProcedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in CommonMethods.GetRows(dt, ref pager))
                {
                    subcategory.Add(Get_SubCategory_Values(dr));
                }
            }

            return subcategory;
        }

        public SubCategoryInfo Get_Subcategory_By_Id(int subcategory_Id)
        {
            SubCategoryInfo subcategory = new SubCategoryInfo();

            List<SqlParameter> sqlparam = new List<SqlParameter>();

            sqlparam.Add(new SqlParameter("Sub_Category_Id", subcategory_Id));

            DataTable dt = _sqlHelper.ExecuteDataTable(sqlparam, StoreProcedures.Get_Sub_Category_By_Id_Sp.ToString(), CommandType.StoredProcedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (!dr.IsNull("Sub_Category_Id"))
                        subcategory.Subcategory_Id = Convert.ToInt32(dr["Sub_Category_Id"]);
                    if (!dr.IsNull("Sub_Category_Name"))
                        subcategory.Subcategory_Name = Convert.ToString(dr["Sub_Category_Name"]);
                    if (!dr.IsNull("Category_Id"))
                        subcategory.Category_Id = Convert.ToInt32(dr["Category_Id"]);
                    if (!dr.IsNull("IsActive"))
                        subcategory.IsActive = Convert.ToBoolean(dr["IsActive"]);
                }
            }

            return subcategory;
        }

        private SubCategoryInfo Get_SubCategory_Values_By_Category(DataRow dr)
        {
            SubCategoryInfo subcategory = new SubCategoryInfo();

            subcategory.Subcategory_Id = Convert.ToInt32(dr["Sub_Category_Id"]);
            subcategory.Subcategory_Name = Convert.ToString(dr["Sub_Category_Name"]);
            subcategory.Category_Id = Convert.ToInt32(dr["Category_Id"]);             
            subcategory.IsActive = Convert.ToBoolean(dr["IsActive"]);
            if (subcategory.IsActive == true)
            {
                subcategory.Status = "Active";
            }
            else
            {
                subcategory.Status = "InActive";
            }
            subcategory.Created_Date = Convert.ToDateTime(dr["Created_On"]);
            subcategory.Created_By = Convert.ToInt32(dr["Created_By"]);
            subcategory.Updated_Date = Convert.ToDateTime(dr["Updated_On"]);
            subcategory.Updated_By = Convert.ToInt32(dr["Updated_By"]);

            return subcategory;
        }

        private SubCategoryInfo Get_SubCategory_Values(DataRow dr)
        {
            SubCategoryInfo subcategory = new SubCategoryInfo();

            subcategory.Subcategory_Id = Convert.ToInt32(dr["Sub_Category_Id"]);
            subcategory.Subcategory_Name = Convert.ToString(dr["Sub_Category_Name"]);
            subcategory.Category_Id = Convert.ToInt32(dr["Sub_Category_Id"]);
            subcategory.Category_Name = Convert.ToString(dr["Category_Name"]);
            subcategory.IsActive = Convert.ToBoolean(dr["IsActive"]);
            if (subcategory.IsActive==true)
            {
                subcategory.Status = "Active";
            }
            else
            {
                subcategory.Status = "InActive";
            }
            subcategory.Created_Date = Convert.ToDateTime(dr["Created_On"]);
            subcategory.Created_By = Convert.ToInt32(dr["Created_By"]);
            subcategory.Updated_Date = Convert.ToDateTime(dr["Updated_On"]);
            subcategory.Updated_By = Convert.ToInt32(dr["Updated_By"]);

            return subcategory;
        }

        public void Insert_Sub_Category(SubCategoryInfo subcategory)
        {
            _sqlHelper.ExecuteNonQuery(Set_Values_In_Sub_Category(subcategory), StoreProcedures.Insert_Sub_Category_Sp.ToString(), CommandType.StoredProcedure);
        }

        public void Update_Sub_Category(SubCategoryInfo subcategory)
        {
            _sqlHelper.ExecuteNonQuery(Set_Values_In_Sub_Category(subcategory), StoreProcedures.Update_Sub_Category_Sp.ToString(), CommandType.StoredProcedure);
        }

        private List<SqlParameter> Set_Values_In_Sub_Category(SubCategoryInfo subcategory)
        {
                List<SqlParameter> sqlParams = new List<SqlParameter>();

            if (subcategory.Subcategory_Id!=0)
            {
                sqlParams.Add(new SqlParameter("@Sub_Category_Id", subcategory.Subcategory_Id));
            }
            
            sqlParams.Add(new SqlParameter("@Category_Id", subcategory.Category_Id));
            sqlParams.Add(new SqlParameter("@Sub_Category_Name", subcategory.Subcategory_Name));
            sqlParams.Add(new SqlParameter("@IsActive", subcategory.IsActive));

            if (subcategory.Subcategory_Id == 0)
            {
                sqlParams.Add(new SqlParameter("@Created_On", subcategory.Created_Date));
                sqlParams.Add(new SqlParameter("@Created_By", subcategory.Created_By));
            }

            sqlParams.Add(new SqlParameter("@Updated_On", subcategory.Updated_Date));
            sqlParams.Add(new SqlParameter("@Updated_By", subcategory.Updated_By));
            return sqlParams;
        }

        public List<AutocompleteInfo> Get_Subcategory_Autocomplete(string subcategory)
        {
            List<AutocompleteInfo> autoList = new List<AutocompleteInfo>();
            List<SqlParameter> sqlparam = new List<SqlParameter>();
            sqlparam.Add(new SqlParameter("@Description", subcategory == null ? System.String.Empty : subcategory.Trim()));
            DataTable dt = _sqlHelper.ExecuteDataTable(sqlparam, StoreProcedures.Get_Subcateory_Autocomplete_Sp.ToString(), CommandType.StoredProcedure);
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

        public bool Check_Existing_SubCategory(string subcategory)
        {
            bool check = false;

            List<SqlParameter> sqlParam = new List<SqlParameter>();
            sqlParam.Add(new SqlParameter("@Subategory_Name", subcategory));

            DataTable dt = _sqlHelper.ExecuteDataTable(sqlParam, StoreProcedures.Check_Existing_Sub_Category.ToString(), CommandType.StoredProcedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                List<DataRow> drList = new List<DataRow>();

                drList = dt.AsEnumerable().ToList();

                foreach (DataRow dr in drList)
                {
                    check = Convert.ToBoolean(dr["Check_SubCategory"]);
                }
            }

            return check;
        }
    }
}
