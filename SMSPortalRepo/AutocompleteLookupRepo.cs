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
    }
}
