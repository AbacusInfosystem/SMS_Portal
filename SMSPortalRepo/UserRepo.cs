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
                        user.User_Id = Convert.ToInt32(dr["User_Id"]);
                        user.Is_Active = Convert.ToBoolean(dr["Is_Active"]);
                        user.User_Name = Convert.ToString(dr["User_Name"]);
                        user.First_Name = Convert.ToString(dr["First_Name"]);
                        user.Last_Name = Convert.ToString(dr["Last_Name"]);
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

         public void Update_User(UserInfo users)
         {
             sqlHelper.ExecuteNonQuery(Set_Values_In_Users(users), StoreProcedures.Update_Users_Sp.ToString(), CommandType.StoredProcedure);
         }

         private List<SqlParameter> Set_Values_In_Users(UserInfo users)
         {
             List<SqlParameter> sqlParams = new List<SqlParameter>();
             
             sqlParams.Add(new SqlParameter("@First_Name", users.First_Name));
             sqlParams.Add(new SqlParameter("@Last_Name", users.Last_Name));
             sqlParams.Add(new SqlParameter("@Contact_No_1", users.Contact_No_1));
             sqlParams.Add(new SqlParameter("@Contact_No_2", users.Contact_No_2));
             sqlParams.Add(new SqlParameter("@Gender", users.Gender));
             sqlParams.Add(new SqlParameter("@User_Name", users.User_Name));
             sqlParams.Add(new SqlParameter("@Password", "jkj"));
             sqlParams.Add(new SqlParameter("@Role_Id", users.Role_Id));
             sqlParams.Add(new SqlParameter("@Is_Active", users.Is_Active));
             sqlParams.Add(new SqlParameter("@Created_On", users.Created_On));
             sqlParams.Add(new SqlParameter("@Created_By", users.Created_By));
             sqlParams.Add(new SqlParameter("@Updated_On", users.Updated_On));
             sqlParams.Add(new SqlParameter("@Updated_By", users.Updated_By));
             return sqlParams;
         }
         public List<UserInfo> Get_Users(ref PaginationInfo Pager)
         {
             List<UserInfo> users = new List<UserInfo>();
             DataTable dt = sqlHelper.ExecuteDataTable(null, StoreProcedures.Get_Users_Sp.ToString(), CommandType.StoredProcedure);
             foreach (DataRow dr in CommonMethods.GetRows(dt, ref Pager))
             {
                 users.Add(Get_Users_Values(dr));
             }
             return users;
         }
         private UserInfo Get_Users_Values(DataRow dr)
         {
             UserInfo user = new UserInfo();

             user.User_Id = Convert.ToInt32(dr["User_Id"]);
             user.First_Name = Convert.ToString(dr["First_Name"]);
             user.Last_Name = Convert.ToString(dr["Last_Name"]);
             user.Contact_No_1 = Convert.ToString(dr["Contact_No_1"]);
             user.Contact_No_2 = Convert.ToString(dr["Contact_No_2"]);
             //user.Gender = Convert.ToInt32(dr["Gender"]);
             user.User_Name = Convert.ToString(dr["User_Name"]);
             user.Password = Convert.ToString(dr["Password"]);
             //user.Role_Id = Convert.ToInt32(dr["Role_Id"]);
             user.Is_Active = Convert.ToBoolean(dr["Is_Active"]);
             if (user.Is_Active==true)
             {
                 user.Status = "Active";
             }
             else
             {
                 user.Status = "InActive";
             }
             user.Created_On = Convert.ToDateTime(dr["Created_On"]);
             user.Created_By = Convert.ToInt32(dr["Created_By"]);
             user.Updated_On = Convert.ToDateTime(dr["Updated_On"]);
             user.Updated_By = Convert.ToInt32(dr["Updated_By"]);
             return user;
         }
         public List<UserInfo> Get_Users_By_User_Name(string User_Name, ref PaginationInfo Pager)
         {
             List<SqlParameter> parameters = new List<SqlParameter>();
             parameters.Add(new SqlParameter("@User_Name", User_Name));

             List<UserInfo> Users = new List<UserInfo>();

             DataTable dt = sqlHelper.ExecuteDataTable(parameters, StoreProcedures.Get_Users_By_User_Name_Sp.ToString(), CommandType.StoredProcedure);
             foreach (DataRow dr in CommonMethods.GetRows(dt, ref Pager))
             {
                 Users.Add(Get_Users_Values(dr));
             }
             return Users;
         }
         public UserInfo Get_User_By_Id(int User_Id)
         {
             UserInfo user = new UserInfo();
             DataTable dt = sqlHelper.ExecuteDataTable(null, StoreProcedures.Get_Users_Sp.ToString(), CommandType.StoredProcedure);
             List<DataRow> drList = new List<DataRow>();
             drList = dt.AsEnumerable().ToList();
             foreach (DataRow dr in drList)
             {
                 user = Get_Users_Values(dr);
             }
             return user;
         }
	}
}
