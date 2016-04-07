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
         public UserInfo AuthenticateUser(string userName, string password)
        {
            UsersRepo usersRepo = new UsersRepo();

            return usersRepo.AuthenticateUser(userName, password);
        }

    }
}
