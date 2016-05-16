using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMSPortalRepo;

namespace SMSPortalManager
{
    public class CommonManager
    {
        CommonRepo _commonRepo;

        public CommonManager()
        {
            _commonRepo = new CommonRepo();
        }

        public int Is_Value_Already_Exist(string Tbl_Name, string Fld_Name, string Value)
        {
            return _commonRepo.Is_Value_Already_Exist(Tbl_Name, Fld_Name, Value);
        }
    }
}
