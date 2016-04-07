using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SMSPortalHelper
{
    public class SQLHelper
    {
        private string _sqlCon = string.Empty;

		public SQLHelper()
        {
            _sqlCon = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        }

        public DataSet ExecuteDataSet(List<SqlParameter> sqlParams, string sqlQuery, CommandType cmdType)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(_sqlCon))
                {
                    using (SqlCommand sqlCmd = new SqlCommand(sqlQuery, con))
                    {

                        //SqlCommand sqlCmd = new SqlCommand(sqlQuery, con);
                        sqlCmd.CommandType = cmdType;
                        sqlCmd.CommandTimeout = 300;
                        if (sqlParams != null)
                        {
                            foreach (SqlParameter sqlPrm in sqlParams)
                            {
                                if (sqlPrm.Value == null)
                                    sqlPrm.Value = DBNull.Value;
                            }
                            sqlCmd.Parameters.AddRange(sqlParams.ToArray());
                        }

                        SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCmd);
                        sqlDA.Fill(ds);
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }

        public DataTable ExecuteDataTable(List<SqlParameter> sqlParams, string sqlQuery, CommandType cmdType)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(_sqlCon))
                {
                    using (SqlCommand sqlCmd = new SqlCommand(sqlQuery, con))
                    {

                        //SqlCommand sqlCmd = new SqlCommand(sqlQuery, con);
                        sqlCmd.CommandType = cmdType;
                        sqlCmd.CommandTimeout = 300;
                        if (sqlParams != null)
                        {
                            foreach (SqlParameter sqlPrm in sqlParams)
                            {
                                if (sqlPrm.Value == null)
                                    sqlPrm.Value = DBNull.Value;
                            }
                            sqlCmd.Parameters.AddRange(sqlParams.ToArray());
                        }

                        SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCmd);
                        sqlDA.Fill(dt);
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }

        public SqlDataReader ExecuteDataReader(SqlConnection con, List<SqlParameter> sqlParams, string sqlQuery, CommandType cmdType)
        {
            SqlDataReader dr;
            
            try
            {
                using (con)
                {
                    //con.Open();
                    SqlCommand sqlCmd = new SqlCommand(sqlQuery, con);
                    sqlCmd.CommandType = cmdType;
                    sqlCmd.CommandTimeout = 300;

                    if (sqlParams != null)
                    {
                        foreach (SqlParameter sqlPrm in sqlParams)
                        {
                            if (sqlPrm.Value == null)
                                sqlPrm.Value = DBNull.Value;
                        }
                        sqlCmd.Parameters.AddRange(sqlParams.ToArray());
                    }

                    dr = sqlCmd.ExecuteReader();
                    //con.Close();
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

            return dr;
        }

        public void ExecuteNonQuery(List<SqlParameter> sqlParams, string sqlQuery, CommandType cmdType)
        {
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

        public object ExecuteScalerObj(List<SqlParameter> sqlParams, string sqlQuery, CommandType cmdType)
        {
            SqlConnection con = new SqlConnection();

            object result = 0;
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

                        result = sqlCmd.ExecuteScalar();
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
            return result;
        }


		public void ExecuteNonQueryWithTransaction(List<SqlParameter> sqlParams, string sqlQuery, CommandType cmdType, SqlTransaction transaction, SqlConnection con)
        {
            try
            {
				SqlCommand sqlCmd = new SqlCommand(sqlQuery, con, transaction);

                sqlCmd.CommandType = cmdType;

                if (sqlParams != null)
                {
					foreach(SqlParameter sqlPrm in sqlParams)
					{
						if(sqlPrm.Value == null)
							sqlPrm.Value = DBNull.Value;
					}
					sqlCmd.Parameters.AddRange(sqlParams.ToArray());
                }

                sqlCmd.ExecuteNonQuery();
            }
			catch(SqlException sqlEx)
            {
                if (con != null)
                    throw sqlEx;
            }
            catch (Exception ex)
            {
                if (con != null)
                    throw ex;
            }
        }

		public object ExecuteScalerObjWithTransaction(List<SqlParameter> sqlParams, string sqlQuery, CommandType cmdType, SqlTransaction transaction, SqlConnection con)
        {

            object result = 0;

            try
            {
				SqlCommand sqlCmd = new SqlCommand(sqlQuery, con, transaction);

                sqlCmd.CommandType = cmdType;

                if (sqlParams != null)
                {
					foreach(SqlParameter sqlPrm in sqlParams)
					{
						if(sqlPrm.Value == null)
							sqlPrm.Value = DBNull.Value;
					}
					sqlCmd.Parameters.AddRange(sqlParams.ToArray());
                }

                result = sqlCmd.ExecuteScalar();
            }
			catch(SqlException sqlEx)
            {
                if (con != null)
                    throw sqlEx;
            }
            catch (Exception ex)
            {
                if (con != null)
                    throw ex;
            }

            return result;
        }

		
    }
}
