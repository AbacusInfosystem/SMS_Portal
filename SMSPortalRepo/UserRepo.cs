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
        private string _sqlCon = string.Empty;
        public UsersRepo()
        {
            _sqlCon = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        }

         public UserInfo AuthenticateUser(string userName, string password)
        {
            UserInfo retVal = new UserInfo();

            try
            {
                using (SqlConnection con = new SqlConnection(_sqlCon))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(StoreProcedures.Authenticate_User_sp.ToString(), con))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(new SqlParameter("@User_Name", userName));

                        command.Parameters.Add(new SqlParameter("@Password", password));

                        SqlDataReader dataReader = command.ExecuteReader();

                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {

                                retVal.UserId = Convert.ToInt32(dataReader["UserId"]);

                                retVal.Is_Active = Convert.ToBoolean(dataReader["Is_Active"]);
                            }
                        }

                        dataReader.Close();
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
