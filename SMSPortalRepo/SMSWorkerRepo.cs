using SMSPortalHelper;
using SMSPortalInfo;
using SMSPortalInfo.Common;
using SMSPortalRepo.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortalRepo
{
    public class SMSWorkerRepo
    {
        SQLHelper _smsWorker;

        public SMSWorkerRepo()
        {
            _smsWorker = new SQLHelper();
        }

        // Get email data from email table

        public List<Email_Data> Get_Email_Data(string request_Type)
        {
            List<Email_Data> email_Data_List = new List<Email_Data>();

            List<SqlParameter> sqlParamList = new List<SqlParameter>();

            sqlParamList.Add(new SqlParameter("@Request_Type", request_Type));

            DataTable dt = _smsWorker.ExecuteDataTable(sqlParamList, StoreProcedures.Get_Email_Data_Sp.ToString(), CommandType.StoredProcedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Email_Data list = new Email_Data();

                    if (!dr.IsNull("Request_Id"))
                        list.Request_Id = Convert.ToInt32(dr["Request_Id"]);
                    if (!dr.IsNull("Request_Type"))
                        list.Request_Type = Convert.ToString(dr["Request_Type"]);
                    if (!dr.IsNull("To_Email_Id"))
                        list.Email_Id = Convert.ToString(dr["To_Email_Id"]);
                    if (!dr.IsNull("Body"))
                        list.Email_Body = Convert.ToString(dr["Body"]);
                    if (!dr.IsNull("Subject"))
                        list.Email_Subject = Convert.ToString(dr["Subject"]);

                    email_Data_List.Add(list);
                }
            }

            return email_Data_List;
        }


        // Update email status 

        public void Update_Email_Status(int Request_Id, string Request_Type)
        {
            List<SqlParameter> sqlparam = new List<SqlParameter>();

            sqlparam.Add(new SqlParameter("@Request_Id", Request_Id));
            sqlparam.Add(new SqlParameter("@Request_Type", Request_Type));

            _smsWorker.ExecuteScalerObj(sqlparam, StoreProcedures.Update_Email_Status_sp.ToString(), CommandType.StoredProcedure);
        }
    }
}
