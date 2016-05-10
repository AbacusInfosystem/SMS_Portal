using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMSPortalInfo;
using SMSPortalInfo.Common;
using SMSPortalRepo;
using System.Data;

namespace SMSPortalManager
{
    public class AutocompleteLookupManager
    {
        AutocompleteLookupRepo _autocompleteLookupRepo;

        public AutocompleteLookupManager()
        {
            _autocompleteLookupRepo = new AutocompleteLookupRepo();
        }

        public DataTable Get_Lookup_Data(string table_Name, string[] cols, ref PaginationInfo Pager, string filter)
        {
            return _autocompleteLookupRepo.Get_Lookup_Data(table_Name, cols, ref Pager, filter);
        }

        public string Get_Lookup_Data_By_SubcategoryId(string field_Value, string table_Name, string[] columns)
        {
            return _autocompleteLookupRepo.Get_Lookup_Data_Add_For_Subcategory(field_Value, table_Name, columns);
        }
    }
}
