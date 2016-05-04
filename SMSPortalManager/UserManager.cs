﻿using SMSPortalHelper.Logging;
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

        public List<UserInfo> Get_Users(ref PaginationInfo pager)
        {
            return _usersRepo.Get_Users(ref pager);
        }

        public void Update_User(UserInfo users , int user_Id)
        {
            _usersRepo.Update_User(users, user_Id);
        }

        public UserInfo Get_User_By_Id(int user_Id)
        {
            return _usersRepo.Get_User_By_Id(user_Id);
        }

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

        public UserInfo Get_User_By_Entity_Id(int Entity_Id)
        {
            return _usersRepo.Get_User_By_Entity_Id(Entity_Id);
        }

        public UserInfo Get_User_By_Password_Token(string Password_Token)
        {
            return _usersRepo.Get_User_By_Password_Token(Password_Token);
        }
    }
}
