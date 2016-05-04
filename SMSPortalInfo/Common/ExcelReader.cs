using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web;
namespace SMSPortalRepo.Common
{
    public class ExcelReader
    {
        private string _excelCon = string.Empty;

        public DataSet ExecuteDataSet(string dataSource)
        {
            DataSet ds = new DataSet();
            try
            {
                _excelCon = string.Format(ConfigurationManager.ConnectionStrings["ExcelConnection"].ToString(), dataSource);

                using (OleDbConnection con = new OleDbConnection(_excelCon))
                {
                    con.Open();
                    DataTable dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.Connection = con;

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            string sheetName = dr["TABLE_NAME"].ToString();

                            // Get all rows from the Sheet
                            cmd.CommandText = "SELECT * FROM [" + sheetName + "]";

                            DataTable dtSheet = new DataTable();
                            dtSheet.TableName = sheetName.Replace("$", "");

                            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                            da.Fill(dtSheet);

                            dtSheet.TableName = sheetName.Remove(sheetName.Length - 1, 1);

                            ds.Tables.Add(dtSheet);
                        }
                    }
                }
            }
            catch (OleDbException oledbEx)
            {
                throw oledbEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }

        public void LogExceptions(List<ExceptionInfo> Exceptions)
        {
            string strFileName = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["ErrorFilePath"].ToString());

            StringBuilder csvData = new StringBuilder();

            if (Exceptions != null && Exceptions.Count() > 0)
            {
                csvData.Append(string.Format("{0},{1},{2},{3},{4},{5},{6}", "Date of Upload", "Excel File Name", "Table Name", "Row No", "Column No", "Error Message", Environment.NewLine));

                foreach (var item in Exceptions)
                {
                    csvData.Append(string.Format("{0},{1},{2},{3},{4},{5},{6}", item.UploadedDate, Path.GetFileName(item.FileName), item.TableName, item.RowNo, item.Columns, item.ErrorMessage, Environment.NewLine));
                }
            }
            System.IO.File.AppendAllText(strFileName, csvData.ToString());
        }

        //public void LogExceptions(DataTable dt)
        //{
        //    string strFileName = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["ErrorFilePath1"].ToString());

        //    StringBuilder csvData = new StringBuilder();

        //    int columnCount = dt.Columns.Count;

        //    string str = string.Empty;

        //    if (columnCount > 0)
        //    {
        //        for (int i = 0; i < columnCount; i++)
        //        {
        //            csvData.Append(dt.Columns[i].ColumnName.ToString() + ",");
        //        }

        //        csvData.ToString().Remove(csvData.ToString().Length - 1, 1);

        //        csvData.Append(Environment.NewLine);
        //    }

        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        for (int j = 0; j < columnCount; j++)
        //        {
        //            csvData.Append(Convert.ToString(dt.Rows[i][j]) + ",");
        //        }

        //        csvData.ToString().Remove(csvData.ToString().Length - 1, 1);

        //        csvData.Append(Environment.NewLine);

        //        //csvData.Append(string.Format("{0},{1},{2},{3},{4},{5}", Path.GetFileName(item.FileName), item.UploadedDate, item.RowNo, item.Columns, item.ErrorMessage, Environment.NewLine));
        //    }

        //    System.IO.File.AppendAllText(strFileName, csvData.ToString());
        //}
    }
}
