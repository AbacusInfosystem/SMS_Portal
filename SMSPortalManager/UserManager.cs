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

        public void Insert_Users(UserInfo users , int user_Id)
        {
            _usersRepo.Insert_Users(users ,user_Id);
        }

        public List<UserInfo> Get_Users(ref PaginationInfo pager,int brand_Id,int role_Id)
        {
            return _usersRepo.Get_Users(ref pager, brand_Id, role_Id);
        }

        public void Update_User(UserInfo users , int user_Id)
        {
            _usersRepo.Update_User(users, user_Id);
        }

        public UserInfo Get_User_By_Id(int user_Id)
        {
            return _usersRepo.Get_User_By_Id(user_Id);
        }

        //public UserInfo Get_User_Password_By_Id(int user_Id)
        //{
        //    return _usersRepo.Get_User_Password_By_Id(user_Id);
        //}

        public List<UserInfo> Get_Users_By_User_Id_List(int user_Id, ref PaginationInfo pager)
        {
            return _usersRepo.Get_Users_By_User_Id_List(user_Id, ref pager);
        }

        public List<RolesInfo> Get_Roles()
        {
            return _usersRepo.Get_Roles();
        }

        public List<Entity> Get_Entity_By_Role(int role_Id)
        {
            return _usersRepo.Get_Entity_By_Role(role_Id);
        }

        public bool Check_Existing_User(string user_Name)
        {
            return _usersRepo.Check_Existing_User(user_Name);
        }

        public List<AutocompleteInfo> Get_User_Autocomplete(string user)
        {
            return _usersRepo.Get_User_Autocomplete(user);
        }

        public UserInfo Get_User_By_Entity_Id(int entity_Id,int role_Id)
        {
            return _usersRepo.Get_User_By_Entity_Id(entity_Id, role_Id);
        }

        public UserInfo Get_User_By_Password_Token(string Password_Token)
        {
            return _usersRepo.Get_User_By_Password_Token(Password_Token);
        }

        public void Send_Reset_Password_Email(string Email_Id,string link,UserInfo User)
        {
            _usersRepo.Send_Reset_Password_Email(Email_Id, link, User);
        }

        public void Reset_Password(string New_Password, int User_Id,string Password_Token)
        {
            _usersRepo.Reset_Password(New_Password, User_Id, Password_Token);
        }

        public UserInfo Get_User_By_Email(string Email_Id)
        {
            return _usersRepo.Get_User_By_Email(Email_Id);
        }


    }
}
