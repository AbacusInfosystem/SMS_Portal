using SMSPortalHelper.Logging;
using SMSPortalInfo;
using SMSPortalInfo.Common;
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
    public class UsersManager
    {

        UsersRepo _usersRepo;

        public UsersManager()
        {
            _usersRepo = new UsersRepo();
        }

        public void Insert_Users(UsersInfo users)
        {
            _usersRepo.Insert_Users(users);
        }

        public void Update_Users(UsersInfo users)
        {
            _usersRepo.Update_Users(users);
        }

        public List<UsersInfo> Get_Userss(ref PaginationInfo Pager)
        {
            return _usersRepo.Get_Userss(ref Pager);
        }

        public UsersInfo Get_Users_By_Id(int User_Id)
        {
            return _usersRepo.Get_Users_By_Id(User_Id);
        }
        public void Delete_Users_By_Id(int User_Id)
        {
            _enquiryRepo.Delete_Users_By_Id(User_Id);
        }
    }

		
	
}
