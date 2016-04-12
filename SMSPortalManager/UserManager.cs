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

        public SessionInfo AuthenticateUser(string userName, string password)
        {
            return _usersRepo.AuthenticateUser(userName, password);
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
   
    }
}
