using SMSPortalHelper.Logging;
using SMSPortalInfo;
using SMSPortalRepo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMSPortalInfo;
using SMSPortalInfo.Common;
namespace SMSPortalManager
{
    public class UserManager
    {
        public UsersRepo _usersRepo;

        public UserManager()
        {
            _usersRepo = new UsersRepo();
        }

        public CookiesInfo AuthenticateUser(string userName, string password)
        {
            return _usersRepo.AuthenticateUser(userName, password);
        }
        public string Set_User_Token_For_Cookies(string userName, string password)
        {
            return _usersRepo.Set_User_Token_For_Cookies(userName, password);
        }
        public void Insert_Users(UserInfo users)
        {
            _usersRepo.Insert_Users(users);
        }
        public List<UserInfo> Get_Users(ref PaginationInfo Pager)
        {
            return _usersRepo.Get_Users(ref Pager);
        }
        public void Update_User(UserInfo users)
        {
            _usersRepo.Update_User(users);
        }
        public UserInfo Get_User_By_Id(int User_Id)
        {
            return _usersRepo.Get_User_By_Id(User_Id);
        }
        public List<UserInfo> Get_Users_By_User_Name(string User_Name, ref PaginationInfo Pager)
        {
            return _usersRepo.Get_Users_By_User_Name(User_Name, ref Pager);
        }
        public List<RolesInfo> Get_Roles()
        {
            return _usersRepo.Get_Roles();
        }
        public List<Entity> Get_Entity_By_Role(int Role_Id)
        {
            return _usersRepo.Get_Entity_By_Role(Role_Id);
        }

        public bool Check_Existing_User(string User_Name)
        {
            return _usersRepo.Check_Existing_User(User_Name);
        }
     
    }
}
