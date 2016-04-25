using SMSPortalInfo;
using SMSPortalInfo.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using SMSPortalHelper;
using SMSPortalHelper.Logging;
using SMSPortalRepo.Common;

namespace SMSPortalRepo
{
    public class CookiesRepo
    {
        SQLHelper sqlHelper = null;
        public CookiesRepo()
        {
            sqlHelper = new SQLHelper();
        }
        public CookiesInfo Get_User_Data_By_User_Token(string token)
        {
            CookiesInfo cookie = new CookiesInfo();
            List<SqlParameter> sqlParam = new List<SqlParameter>();
            sqlParam.Add(new SqlParameter("@Token", token));
            try
            {
                DataTable dt = sqlHelper.ExecuteDataTable(sqlParam, StoreProcedures.Get_User_Data_By_Token_sp.ToString(), CommandType.StoredProcedure);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.AsEnumerable().FirstOrDefault();
                    if (dr != null)
                    {
                        cookie.User_Id = Convert.ToInt32(dr["User_Id"]);
                        cookie.Role_Id = Convert.ToInt32(dr["Role_Id"]);
                        cookie.Role_Name = Convert.ToString(dr["Role_Name"]);
                        cookie.Is_Active = Convert.ToBoolean(dr["Is_Active"]);
                        cookie.User_Name = Convert.ToString(dr["User_Name"]);
                        cookie.First_Name = Convert.ToString(dr["First_Name"]);
                        cookie.Last_Name = Convert.ToString(dr["Last_Name"]);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("CookiesRepo - Get_User_Data_By_User_Token: " + ex.ToString());
            }
            return cookie;
        }
    }
}
