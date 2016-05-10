using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMSPortalHelper;
using SMSPortalInfo;
using SMSPortalInfo.Common;
using SMSPortalRepo.Common;
using System.Data;
using System.Data.SqlClient;

namespace SMSPortalRepo
{
    public class AutocompleteLookupRepo
    {
         SQLHelper _sqlHelper = null;

        public AutocompleteLookupRepo()
        {
            _sqlHelper = new SQLHelper();
        }

        public DataTable Get_Lookup_Data(string table_Name, string[] cols, ref PaginationInfo pager,string filter)
        {
            string strquery = "";

           strquery = "select ";

           for (int i = 0; i < cols.Length; i++)
           {
               strquery += cols[i] + ",";
           }

           char[] removeCh = { ',', ' ' };

           strquery = strquery.TrimEnd(removeCh);

           strquery += " from " + table_Name;


           List<SqlParameter> paramList = new List<SqlParameter>();
           if (!string.IsNullOrEmpty(filter) )
           {
               if (table_Name == "purchase_order")
               {
                   strquery +=  " Where Vendor_Id= @Vendor_Id";
                   paramList.Add(new SqlParameter("@Vendor_Id", filter));     
               }
               if (table_Name =="product")
               {
                   strquery = " Select P.Product_Id,P.Product_Name from  Product_Vendor_Mapping Pv  inner join Product P on Pv.Product_Id=P.Product_Id  where Pv.Vendor_Id=@Vendor_Id ";
                   paramList.Add(new SqlParameter("@Vendor_Id",filter));                   
               }               
           }                      

            DataTable dt = _sqlHelper.ExecuteDataTable(paramList, strquery, CommandType.Text);

            return dt;
        }

        public string Get_Lookup_Data_Add_For_Subcategory(string field_Value, string table_Name, string[] columns)
        {
            string Value = "";

            string strquery = "";

            string col_Id="";

            string col_Value = "";

            strquery = "select ";

            for (int i = 0; i < columns.Length; i++)
            {
                strquery += columns[i] + ",";

                col_Id = columns[0].ToString();

                col_Value = columns[1].ToString();
            }

            char[] removeCh = { ',', ' ' };

            strquery = strquery.TrimEnd(removeCh);

            strquery += " from " + table_Name;

            strquery += " where " + table_Name + "." + col_Id + "=" + Convert.ToInt32(field_Value);

            DataTable dt = _sqlHelper.ExecuteDataTable(null, strquery, CommandType.Text);

            if (dt != null && dt.Rows.Count > 0)
            {
                int count = 0;
                List<DataRow> drList = new List<DataRow>();

                drList = dt.AsEnumerable().ToList();

                count = drList.Count();

                foreach (DataRow dr in drList)
                {
                    Value = Convert.ToString(dr[col_Value]);
                }
            }

            return Value;
        }
    }
}
