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
    public class AutocompleteLookupRepo
    {
         SQLHelper _sqlHelper = null;

        public AutocompleteLookupRepo()
        {
            _sqlHelper = new SQLHelper();
        }

        public DataTable Get_Lookup_Data(string table_Name, string[] cols, ref PaginationInfo pager)
        {
            string strquery = "";

           strquery = "select ";

           for (int i = 0; i < cols.Length; i++)
           {
               strquery += cols[i] + ",";
           }

           char[] removeCh = { ',', ' ' };

           strquery = strquery.TrimEnd(removeCh);

           strquery += " from " + table_Name;

            DataTable dt = _sqlHelper.ExecuteDataTable(null, strquery, CommandType.Text);

            return dt;
        }

        public string Get_Lookup_Data_Add_For_Subcategory(string field_Value)
        {
            string Value = "";

            List<SqlParameter> sqlParam = new List<SqlParameter>();

            sqlParam.Add(new SqlParameter("@LKey", field_Value));

            DataTable dt = _sqlHelper.ExecuteDataTable(sqlParam, StoreProcedures.Get_Lookup_Sub_Category_By_Id_Sp.ToString(), CommandType.StoredProcedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                int count = 0;
                List<DataRow> drList = new List<DataRow>();

                drList = dt.AsEnumerable().ToList();

                count = drList.Count();

                foreach (DataRow dr in drList)
                {
                    Value = Convert.ToString(dr["Value"]);
                }
            }

            return Value;
        }
    }
}
