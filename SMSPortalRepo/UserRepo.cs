using SMSPortalHelper;
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
using SMSPortalHelper.Logging;
namespace SMSPortalRepo
{
    
	public class UsersRepo
	{
        SQLHelper sqlHelper = null;
        public UsersRepo()
        {
            sqlHelper = new SQLHelper();
        }        
         public UserInfo AuthenticateUser(string userName, string password)
        {
            UserInfo retVal = new UserInfo();
            List<SqlParameter> sqlParam = new List<SqlParameter>();
            sqlParam.Add(new SqlParameter("@User_Name", userName));
            sqlParam.Add(new SqlParameter("@Password", password));
            try
            {
                DataTable dt = sqlHelper.ExecuteDataTable(sqlParam, StoreProcedures.Authenticate_User_sp.ToString(), CommandType.StoredProcedure);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.AsEnumerable().FirstOrDefault();
                    if (dr != null)
                    {
                        retVal.UserId = Convert.ToInt32(dr["User_Id"]);
                        retVal.Is_Active = Convert.ToBoolean(dr["Is_Active"]);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("UserRepo - AuthenticateLoginCredentials: " + ex.ToString());
            }

            return retVal;
        }
		


	}
}
