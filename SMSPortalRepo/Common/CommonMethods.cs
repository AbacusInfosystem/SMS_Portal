using SMSPortalHelper;
using SMSPortalInfo.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortalRepo.Common
{
	public static class CommonMethods
	{

		public static List<DataRow> GetRows(DataTable dt, ref PaginationInfo pager)
		{
			List<DataRow> drList = new List<DataRow>();

			if(dt != null && dt.Rows.Count > 0)
			{
				int count = 0;

				drList = dt.AsEnumerable().ToList();

				count = drList.Count();

				if(pager.IsPagingRequired)
				{
					drList = drList.Skip(pager.CurrentPage * pager.PageSize).Take(pager.PageSize).ToList();
				}

				pager.TotalRecords = count;

				int pages = (pager.TotalRecords + pager.PageSize - 1) / pager.PageSize;

				pager.TotalPages = pages;
			}

			return drList;
		}
        internal static IEnumerable<DataRow> GetRows(DataTable dt)
        {
            List<DataRow> drList = new List<DataRow>();

            if (dt != null && dt.Rows.Count > 0)
            {
                int count = 0;

                drList = dt.AsEnumerable().ToList();

                count = drList.Count();

                //if (pager.IsPagingRequired)
                //{
                //    drList = drList.Skip(pager.CurrentPage * pager.PageSize).Take(pager.PageSize).ToList();
                //}

                //pager.TotalRecords = count;

                //int pages = (pager.TotalRecords + pager.PageSize - 1) / pager.PageSize;

                //pager.TotalPages = pages;
            }

            return drList;
        }

        public static void Insert_Email_Data(int request_Id, string request_Type, string to_Email_Id, string subject, string body, int user_Id)
        {
            ExecuteNonQuery(Set_Values_In_Email_Send(request_Id, request_Type, to_Email_Id, subject, body, user_Id), StoreProcedures.Insert_Email_Data_Sp.ToString(), CommandType.StoredProcedure);

        }

        public static List<SqlParameter> Set_Values_In_Email_Send(int request_Id, string request_Type, string to_Email_Id, string subject, string body, int user_Id)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            sqlParams.Add(new SqlParameter("@Request_Id", request_Id));
            sqlParams.Add(new SqlParameter("@Request_Type", request_Type));
            sqlParams.Add(new SqlParameter("@To_Email_Id", to_Email_Id));
            sqlParams.Add(new SqlParameter("@Subject", subject));
            sqlParams.Add(new SqlParameter("@Body", body));
            sqlParams.Add(new SqlParameter("@Is_Email_Sent", false));
            sqlParams.Add(new SqlParameter("@Created_On", DateTime.Now));
            sqlParams.Add(new SqlParameter("@Created_By", user_Id));
            sqlParams.Add(new SqlParameter("@Email_Sent_On", DateTime.Now));

            return sqlParams;
        }

        public static void ExecuteNonQuery(List<SqlParameter> sqlParams, string sqlQuery, CommandType cmdType)
        {
           string _sqlCon = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString(); 

            SqlConnection con = new SqlConnection();
            try
            {
                using (con = new SqlConnection(_sqlCon))
                {
                    con.Open();
                    using (SqlCommand sqlCmd = new SqlCommand(sqlQuery, con))
                    {

                        //SqlCommand sqlCmd = new SqlCommand(sqlQuery, con);
                        sqlCmd.CommandType = cmdType;

                        if (sqlParams != null)
                        {
                            foreach (SqlParameter sqlPrm in sqlParams)
                            {
                                if (sqlPrm.Value == null)
                                    sqlPrm.Value = DBNull.Value;
                            }
                            sqlCmd.Parameters.AddRange(sqlParams.ToArray());
                        }

                        sqlCmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                if (con != null)
                    con.Close();

                throw sqlEx;
            }
            catch (Exception ex)
            {
                if (con != null)
                    con.Close();

                throw ex;
            }
        }
	}
}
