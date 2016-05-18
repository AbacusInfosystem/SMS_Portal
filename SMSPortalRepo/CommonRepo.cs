using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMSPortalHelper;
using SMSPortalInfo.Common;
using System.Data.SqlClient;
using System.Data;

namespace SMSPortalRepo
{
    public class CommonRepo
    {
         SQLHelper _sqlHelper = null;

         public CommonRepo()
        {
            _sqlHelper = new SQLHelper();
        }

         public int Is_Value_Already_Exist(string Tbl_Name, string Fld_Name, string Value, string Primary_Field_Name, int Id)
         {
             List<SqlParameter> sqlparam = new List<SqlParameter>();

             sqlparam.Add(new SqlParameter("@Tbl_Name", Tbl_Name));
             sqlparam.Add(new SqlParameter("@Fld_Name", Fld_Name));
             sqlparam.Add(new SqlParameter("@Value", Value));
             sqlparam.Add(new SqlParameter("@Primary_Field_Name", Primary_Field_Name));
             sqlparam.Add(new SqlParameter("@Id", Id));

             return Convert.ToInt32(_sqlHelper.ExecuteScalerObj(sqlparam, StoreProcedures.Is_Value_Already_Exist_sp.ToString(), CommandType.StoredProcedure));
         }
    }
}
