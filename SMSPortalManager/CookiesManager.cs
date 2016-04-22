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
    public class CookiesManager
    {
        public CookiesRepo _cookiesRepo;

        public CookiesManager()
        {
            _cookiesRepo = new CookiesRepo();
        }

        public CookiesInfo Get_Token_Data(string token)
        {
            return _cookiesRepo.Get_User_Data_By_User_Token(token);
        }
    }
}
