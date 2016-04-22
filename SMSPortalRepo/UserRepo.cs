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
        public CookiesInfo AuthenticateUser(string userName, string password)
        {
            CookiesInfo user = new CookiesInfo();
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

        public string Set_User_Token_For_Cookies(string userName, string password)
        {
            string user_Token = "Token" + DateTime.Now.ToString("yyMMddHHmmssff");            
            try
            {                
                List<SqlParameter> sqlParam = new List<SqlParameter>();
                sqlParam.Add(new SqlParameter("@user_Token", user_Token));
                sqlParam.Add(new SqlParameter("@User_Name", userName));
                sqlHelper.ExecuteNonQuery(sqlParam, StoreProcedures.Insert_Token_In_User_Table_Sp.ToString(), CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Logger.Error("UserRepo - Set_User_Token_For_Cookies: " + ex.ToString());
            }

            return user_Token;
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

             if (users.User_Id != 0)
             {
                 sqlParams.Add(new SqlParameter("@User_Id", users.User_Id));
             }            
             sqlParams.Add(new SqlParameter("@First_Name", users.First_Name));
             sqlParams.Add(new SqlParameter("@Last_Name", users.Last_Name));
             sqlParams.Add(new SqlParameter("@Contact_No_1", users.Contact_No_1));
             sqlParams.Add(new SqlParameter("@Contact_No_2", users.Contact_No_2));
             sqlParams.Add(new SqlParameter("@Email_Id", users.Email_Id));
             sqlParams.Add(new SqlParameter("@Gender", users.Gender));
             sqlParams.Add(new SqlParameter("@User_Name", users.User_Name));
             sqlParams.Add(new SqlParameter("@Password", "jkj"));
             sqlParams.Add(new SqlParameter("@Entity_Id", users.Entity_Id));
             sqlParams.Add(new SqlParameter("@Role_Id", users.Role_Id));
             sqlParams.Add(new SqlParameter("@Is_Active", users.Is_Active));
             if (users.User_Id == 0)
             {
                 sqlParams.Add(new SqlParameter("@Created_On", users.Created_On));
                 sqlParams.Add(new SqlParameter("@Created_By", users.Created_By));
             }
            
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

         public List<RolesInfo> Get_Roles()
         {
             List<RolesInfo> roleslist = new List<RolesInfo>();
             List<SqlParameter> sqlparam = new List<SqlParameter>();
             DataTable dt = sqlHelper.ExecuteDataTable(sqlparam, StoreProcedures.Get_Roles_Sp.ToString(), CommandType.StoredProcedure);

             if (dt != null && dt.Rows.Count > 0)
             {
                 foreach (DataRow dr in dt.Rows)
                 {
                     RolesInfo list = new RolesInfo();

                     if (!dr.IsNull("Role_Id"))
                         list.Role_Id = Convert.ToInt32(dr["Role_Id"]);
                     if (!dr.IsNull("Role_Name"))
                         list.Role_Name = Convert.ToString(dr["Role_Name"]);

                     roleslist.Add(list);
                 }
             }

             return roleslist;
         }

         private UserInfo Get_Users_Values(DataRow dr)
         {
             UserInfo user = new UserInfo();

             user.User_Id = Convert.ToInt32(dr["User_Id"]);

             if (!dr.IsNull("User_Name"))
             user.First_Name = Convert.ToString(dr["First_Name"]);
             user.Last_Name = Convert.ToString(dr["Last_Name"]);
             user.Contact_No_1 = Convert.ToString(dr["Contact_No_1"]);
             user.Contact_No_2 = Convert.ToString(dr["Contact_No_2"]);
             user.Password = Convert.ToString(dr["Email_Id"]);
             user.Gender = Convert.ToInt32(dr["Gender"]);
             user.User_Name = Convert.ToString(dr["User_Name"]);
             user.Password = Convert.ToString(dr["Password"]);
             user.Password = Convert.ToString(dr["Entity_Id"]);
             user.Role_Id = Convert.ToInt32(dr["Role_Id"]);
             user.Is_Active = Convert.ToBoolean(dr["Is_Active"]);
             if (user.Is_Active == true)
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

         public List<Entity> Get_Entity_By_Role(int Role_Id)
         {
             List<Entity> Entities = new List<Entity>();
             List<SqlParameter> parameters = new List<SqlParameter>();
             parameters.Add(new SqlParameter("@Role_Id", Role_Id));
            DataTable dt = sqlHelper.ExecuteDataTable(parameters, StoreProcedures.Get_Entity_By_Role_Sp.ToString(), CommandType.StoredProcedure);

             if (dt != null && dt.Rows.Count > 0)
             {
                 foreach (DataRow dr in CommonMethods.GetRows(dt))
                 {
                     Entities.Add(Get_Entity_By_Role_val(dr));
                 }
             }

             return Entities;
         }

         public Entity Get_Entity_By_Role_val(DataRow dr)
         {
             Entity entity = new Entity();

             if (!dr.IsNull("ID"))
                 entity.Entity_Id = Convert.ToInt32(dr["ID"]);
             if (!dr.IsNull("Name"))
                 entity.Entity_Name = Convert.ToString(dr["Name"]);

             return entity;
         }

         public UserInfo Get_User_By_Id(int User_Id)
         {
             List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@User_Id", User_Id));
             UserInfo user = new UserInfo();
             DataTable dt = sqlHelper.ExecuteDataTable(parameters, StoreProcedures.Get_Users_By_Id_Sp.ToString(), CommandType.StoredProcedure);
             List<DataRow> drList = new List<DataRow>();
             drList = dt.AsEnumerable().ToList();

             foreach (DataRow dr in drList)
             {
                 user = Get_Users_Values(dr);
             }

             return user;
         }

         public bool Check_Existing_User(string User_Name)
         {
             bool check = false;
             List<SqlParameter> sqlParam = new List<SqlParameter>();
             sqlParam.Add(new SqlParameter("@User_Name", User_Name));
             DataTable dt = sqlHelper.ExecuteDataTable(sqlParam, StoreProcedures.Check_Existing_User.ToString(), CommandType.StoredProcedure);

             if (dt != null && dt.Rows.Count > 0)
             {
                 //int count = dt.Rows.Count;

                 List<DataRow> drList = new List<DataRow>();
                 drList = dt.AsEnumerable().ToList();

                 foreach (DataRow dr in drList)
                 {
                     check = Convert.ToBoolean(dr["Check_User"]);
                 }
             }

             return check;
         }
	}
}
