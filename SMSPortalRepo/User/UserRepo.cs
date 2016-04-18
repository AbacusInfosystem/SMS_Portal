using SMSPortalHelper;
using SMSPortalInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMSPortalRepo.Common;
using SMSPortalInfo.Common;

namespace SMSPortalRepo
{
	
    public class UsersRepo
    {

        SQLHelperRepo _sqlRepo;

        public UsersRepo()
        {
            _sqlRepo = new SQLHelperRepo();
        }

        public void Insert_Users(UsersInfo users)
        {
            _sqlRepo.ExecuteNonQuery(SetValues_In_Users(users), StoredProcedures.Insert_Users_Sp.ToString(), CommandType.StoredProcedure);
        }

        public void Update_Users(UsersInfo users)
        {
            _sqlRepo.ExecuteNonQuery(Set_Values_In_Users(users), StoredProcedures.Update_Users_Sp.ToString(), CommandType.StoredProcedure);
        }

        private List<SqlParameter> Set_Values_In_Users(UsersInfo users)
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

        public List<UsersInfo> Get_Userss(ref PaginationInfo Pager)
        {
            List<UsersInfo> userss = new List<UsersInfo>();
            DataTable dt = _sqlRepo.ExecuteDataTable(null, StoredProcedures.Get_Users_Sp.ToString(), CommandType.StoredProcedure);
            foreach (DataRow dr in CommonMethods.GetRows(dt, ref pager))
            {
                userss.Add(Get_Users_Values(dr));
            }
            return userss;
        }
        public UsersInfo Get_Users_By_Id(int User_Id)
        {
            UsersInfo users = new Users();
            DataTable dt = _sqlRepo.ExecuteDataTable(null, StoredProcedures.Get_Users_Sp.ToString(), CommandType.StoredProcedure);
            List<DataRow> drList = new List<DataRow>();
            drList = dt.AsEnumerable().ToList();
            foreach (DataRow dr in drList)
            {
                users = Get_Users_Values(dr);
            }
            return users;
        }

        private UsersInfo Get_Users_Values(DataRow dr)
        {
            UsersInfo users = new UsersInfo();

            users.User_Id = Convert.ToInt32(dr["User_Id"]);
            users.First_Name = Convert.ToString(dr["First_Name"]);
            users.Last_Name = Convert.ToString(dr["Last_Name"]);
            users.Contact_No_1 = Convert.ToString(dr["Contact_No_1"]);
            users.Contact_No_2 = Convert.ToString(dr["Contact_No_2"]);
            users.Gender = Convert.ToInt32(dr["Gender"]);
            users.User_Name = Convert.ToString(dr["User_Name"]);
            users.Password = Convert.ToString(dr["Password"]);
            users.Role_Id = Convert.ToInt32(dr["Role_Id"]);
            users.Is_Active = Convert.ToBoolean(dr["Is_Active"]);
            users.Created_On = Convert.ToDateTime(dr["Created_On"]);
            users.Created_By = Convert.ToInt32(dr["Created_By"]);
            users.Updated_On = Convert.ToDateTime(dr["Updated_On"]);
            users.Updated_By = Convert.ToInt32(dr["Updated_By"]);
            return users;
        }
        public void Delete_Users_By_Id(int user_id)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter("@User_Id", user_id));
            _sqlRepo.ExecuteNonQuery(sqlParams, StoredProcedures.Delete_Users_By_Id.ToString(), CommandType.StoredProcedure);
        }
    }
}
