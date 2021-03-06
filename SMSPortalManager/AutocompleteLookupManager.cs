﻿using System;
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

        public DataTable Get_Lookup_Data(string table_Name, string[] cols, ref PaginationInfo Pager,string fieldValue,string fieldName,int entity_Id,int role_Id,int brand_Id)
        {
            return _autocompleteLookupRepo.Get_Lookup_Data(table_Name, cols, ref Pager, fieldValue, fieldName, entity_Id, role_Id, brand_Id);
        }

        public string Get_Lookup_Data_By_SubcategoryId(string field_Value, string table_Name, string[] columns)
        {
            return _autocompleteLookupRepo.Get_Lookup_Data_Add_For_Subcategory(field_Value, table_Name, columns);
        }
    }
}
