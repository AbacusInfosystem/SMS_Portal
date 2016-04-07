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
namespace SMSPortalRepo
{
    
	public class UsersRepo
	{
        SQLHelper sqlHelper = null;
        public UsersRepo()
        {
            sqlHelper = new SQLHelper();
        }        
         public SessionInfo AuthenticateUser(string userName, string password)
        {
            SessionInfo user = new SessionInfo(); 
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
                        user.UserId = Convert.ToInt32(dr["User_Id"]);
                        user.Is_Active = Convert.ToBoolean(dr["Is_Active"]);
                        user.UserName = Convert.ToString(dr["User_Name"]);
                        user.FirstName = Convert.ToString(dr["First_Name"]);
                        user.LastName = Convert.ToString(dr["Last_Name"]);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("UserRepo - AuthenticateLoginCredentials: " + ex.ToString());
            }
            return user;
        }

         public void Insert_Users(UserInfo users)
         {
             sqlHelper.ExecuteNonQuery(Set_Values_In_Users(users), StoreProcedures.Insert_Users_Sp.ToString(), CommandType.StoredProcedure);
         }

         private List<SqlParameter> Set_Values_In_Users(UserInfo users)
         {
             List<SqlParameter> sqlParams = new List<SqlParameter>();
             sqlParams.Add(new SqlParameter("@User_Id", users.User_Id));
             sqlParams.Add(new SqlParameter("@First_Name", users.First_Name));
             sqlParams.Add(new SqlParameter("@Last_Name", users.Last_Name));
             sqlParams.Add(new SqlParameter("@Contact_No_1", users.Contact_No_1));
             sqlParams.Add(new SqlParameter("@Contact_No_2", users.Contact_No_2));
             sqlParams.Add(new SqlParameter("@Gender", users.Gender));
             sqlParams.Add(new SqlParameter("@User_Name", users.User_Name));
             sqlParams.Add(new SqlParameter("@Password", users.Password));
             sqlParams.Add(new SqlParameter("@Role_Id", users.Role_Id));
             sqlParams.Add(new SqlParameter("@Is_Active", users.Is_Active));
             sqlParams.Add(new SqlParameter("@Created_On", users.Created_On));
             sqlParams.Add(new SqlParameter("@Created_By", users.Created_By));
             sqlParams.Add(new SqlParameter("@Updated_On", users.Updated_On));
             sqlParams.Add(new SqlParameter("@Updated_By", users.Updated_By));
             return sqlParams;
         }

	}
}
