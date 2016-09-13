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

        public DataTable Get_Lookup_Data(string table_Name, string[] cols, ref PaginationInfo pager, string fieldValue, string fieldName, int entity_Id, int role_Id, int brand_Id)
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
            if (fieldValue != "0" && fieldValue != "")
            {
                if (table_Name == "Purchase_Order")
                {
                    if(fieldName!=null)
                    {
                        if (fieldName == "Vendor_Id")
                        {
                            strquery = " Select Purchase_Order.Purchase_Order_ID , Purchase_Order.Purchase_Order_No  ";
                            strquery += "from Purchase_Order inner join Payables on Purchase_Order.Purchase_Order_Id=Payables.Purchase_Order_Id ";
                            strquery += "where Vendor_Id= @Vendor_Id";
                            paramList.Add(new SqlParameter("@Vendor_Id", fieldValue));
                        }
                        if (fieldValue != null)
                        {
                            strquery = " Select Purchase_Order.Purchase_Order_ID , Purchase_Order.Purchase_Order_No from Purchase_Order ";
                            strquery += "where Vendor_Id= @Vendor_Id";
                            paramList.Add(new SqlParameter("@Vendor_Id", fieldValue));
                        }
                    }
                    else
                    {
                        strquery += " Where Vendor_Id= @Vendor_Id";
                        paramList.Add(new SqlParameter("@Vendor_Id", entity_Id));
                    }
                
                }
                if (table_Name == "Product")
                {
                    strquery = " Select P.Product_Id,P.Product_Name from Product P where P.Vendor_Id=@Vendor_Id ";
                    paramList.Add(new SqlParameter("@Vendor_Id", fieldValue));
                }

                if (table_Name == "Invoice")
                {
                    if (fieldName == "Entity_Id")
                    {
                        strquery = "Select Vendor_Invoice_Id , Invoice_No from Vendor_Invoice Where Entity_Id=@Entity_Id  ";
                        paramList.Add(new SqlParameter("@Entity_Id", entity_Id));
                    }
                    if (fieldName == "Dealer_Id" || fieldName == "Brand_Id")                   
                    {
                        strquery = " Select Invoice.Invoice_Id , Invoice.Invoice_No  from Invoice inner join Receivables on Invoice.Invoice_Id=Receivables.Invoice_Id ";
                        strquery += "  Where Invoice.Entity_Id=@Entity_Id";
                        paramList.Add(new SqlParameter("@Entity_Id", fieldValue));
                    }

                }
                if (table_Name == "Vendor_Invoice")
                {
                    if (fieldName == "Entity_Id")
                    {
                        strquery = "Select Vendor_Invoice_Id , Invoice_No from Vendor_Invoice Where Entity_Id=@Brand_Id  ";
                        paramList.Add(new SqlParameter("@Brand_Id", fieldValue));
                    }
                    if (fieldName == "Dealer_Id" || fieldName == "Brand_Id")
                    {
                        strquery = " Select Vendor_Invoice.Vendor_Invoice_Id , Vendor_Invoice.Invoice_No  from Vendor_Invoice left join Vendor_Orders A on Vendor_Invoice.Vendor_Order_Id=A.Vendor_Order_Id ";
                        strquery += "  Where A.Dealer_Id=@Entity_Id";
                        paramList.Add(new SqlParameter("@Entity_Id", fieldValue));
                    }
                    if(fieldName==null)
                    {
                        strquery = "Select Vendor_Invoice_Id , Invoice_No from Vendor_Invoice Where Entity_Id=@Entity_Id  ";
                        paramList.Add(new SqlParameter("@Entity_Id", entity_Id));
                    }
                }
                if(table_Name=="Orders")
                {
                    if(role_Id==3)
                    {
                        strquery = "Select v.Vendor_Order_Id,a.Order_No from Vendor_Orders v left join Orders a on v.Order_Id=a.Order_Id Where v.Dealer_Id=@Entity_Id";
                        paramList.Add(new SqlParameter("@Entity_Id", entity_Id));
                    }
                    else
                    {
                        strquery = "Select v.Vendor_Order_Id,a.Order_No from Vendor_Orders v left join Orders a on v.Order_Id=a.Order_Id Where v.Vendor_Id=@Entity_Id";
                        paramList.Add(new SqlParameter("@Entity_Id", entity_Id));
                    }
                    
                }
                if (table_Name == "Dealer")
                {
                    strquery = "select Dealer_Id,Dealer_Name from Dealer ";
                    strquery += "  Where Brand_Id=@Brand_Id";
                    paramList.Add(new SqlParameter("@Brand_Id", fieldValue));
                }
                if (table_Name == "ThirdPartyVendor")
                {
                    if (fieldValue!=null)
                    {
                        strquery = "select Third_Party_Vendor_Id,Third_Party_Vendor_Name from ThirdPartyVendor ";
                        strquery += "  Where Vendor_Id=@User_Id";
                        paramList.Add(new SqlParameter("@User_Id", fieldValue));
                    }
                    else
                    {
                        strquery = "select Third_Party_Vendor_Id,Third_Party_Vendor_Name from ThirdPartyVendor ";
                    }
                    
                }
                if(role_Id==4 & table_Name=="Users")
                {
                    strquery = "select u.User_Id,u.User_Name from users u left join brand b on u.Entity_Id=b.Brand_Id ";
                    strquery += "  Where u.Brand_Id=@Brand_Id";
                    paramList.Add(new SqlParameter("@Brand_Id", brand_Id));
                }
                if (role_Id == 4 & table_Name == "ThirdPartyVendor")
                {
                    strquery = "select u.Third_Party_Vendor_Id,u.Third_Party_Vendor_Name from ThirdPartyVendor u ";
                    strquery += "  Where u.Vendor_Id=@Vendor_Id";
                    paramList.Add(new SqlParameter("@Vendor_Id", entity_Id));
                }
                //if (!string.IsNullOrEmpty(fieldName))
                //{
                //    strquery = "select ";

                //    for (int i = 0; i < cols.Length; i++)
                //    {
                //        strquery += cols[i] + ",";
                //    }

                //    strquery = strquery.TrimEnd(removeCh);

                //    strquery += " from " + table_Name;

                //    strquery += " Where " + fieldName + "="+ fieldValue;

                //}
            }

            DataTable dt = _sqlHelper.ExecuteDataTable(paramList, strquery, CommandType.Text);

            return dt;
        }

        public string Get_Lookup_Data_Add_For_Subcategory(string field_Value, string table_Name, string[] columns)
        {
            string Value = "";

            string strquery = "";

            string col_Id = "";

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
