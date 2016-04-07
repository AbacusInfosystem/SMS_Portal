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

namespace SMSPortalManager
{
    public class UserManager
    {
        public UsersRepo _usersRepo;

        public UserManager()
        {
            _usersRepo = new UsersRepo();
        }

        public UserInfo AuthenticateUser(string userName, string password)
        {
            return _usersRepo.AuthenticateUser(userName, password);
        }
        public void Insert_Users(UserInfo users)
        {
            _usersRepo.Insert_Users(users);
        }
    }
}
