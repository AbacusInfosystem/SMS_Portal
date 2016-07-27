using SMSPortalHelper;
using SMSPortalInfo;
using SMSPortalInfo.Common;
using SMSPortalRepo.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortalRepo
{
    public class VendorRepo
    {
         SQLHelper _sqlHelper;

         public VendorRepo()
        {
            _sqlHelper = new SQLHelper();
        }

         public void Insert_Vendor(VendorInfo vendor, int user_Id)
         {
             _sqlHelper.ExecuteNonQuery(Set_Values_In_Vendor(vendor, user_Id), StoreProcedures.Insert_Data_New_Vendor_Sp.ToString(), CommandType.StoredProcedure);
         }

         public void Update_Vendor(VendorInfo vendor, int user_Id)
         {
             _sqlHelper.ExecuteNonQuery(Set_Values_In_Vendor(vendor, user_Id), StoreProcedures.Update_Data_New_Vendor_Sp.ToString(), CommandType.StoredProcedure);
         }

         private List<SqlParameter> Set_Values_In_Vendor(VendorInfo vendor, int user_Id)
         {
             List<SqlParameter> sqlParams = new List<SqlParameter>();
             if (vendor.Vendor_Id != 0)
             {
                 sqlParams.Add(new SqlParameter("@Vendor_Id", vendor.Vendor_Id));
             }

             sqlParams.Add(new SqlParameter("@Vendor_Name", vendor.Vendor_Name));
             sqlParams.Add(new SqlParameter("@Address", vendor.Address));
             sqlParams.Add(new SqlParameter("@City", vendor.City));
             sqlParams.Add(new SqlParameter("@State", vendor.State));
             sqlParams.Add(new SqlParameter("@Pincode", vendor.Pincode));
             sqlParams.Add(new SqlParameter("@Contact_No_1", vendor.Contact_No_1));
             sqlParams.Add(new SqlParameter("@Contact_No_2", vendor.Contact_No_2));
             sqlParams.Add(new SqlParameter("@Email", vendor.Email));
             sqlParams.Add(new SqlParameter("@Is_Active", vendor.Is_Active));
             if (vendor.Vendor_Id == 0)
             {
                 sqlParams.Add(new SqlParameter("@Created_On", DateTime.Now));
                 sqlParams.Add(new SqlParameter("@Created_By", user_Id));
             }
             sqlParams.Add(new SqlParameter("@Updated_On", DateTime.Now));
             sqlParams.Add(new SqlParameter("@Updated_By", user_Id));
             return sqlParams;
         }

         public VendorInfo Get_Vendor_By_Id(int vendor_Id)
         {
             List<SqlParameter> sqlParamList = new List<SqlParameter>();
             sqlParamList.Add(new SqlParameter("@Vendor_Id", vendor_Id));

             VendorInfo Vendor = new VendorInfo();
             DataTable dt = _sqlHelper.ExecuteDataTable(sqlParamList, StoreProcedures.Get_New_Vendor_By_Id_Sp.ToString(), CommandType.StoredProcedure);
             foreach (DataRow dr in dt.Rows)
             {
                 Vendor = Get_Vendor_Values(dr);
             }
             return Vendor;
         }

         public List<AutocompleteInfo> Get_Vendor_Autocomplete(string vendor)
         {
             List<AutocompleteInfo> autoList = new List<AutocompleteInfo>();
             List<SqlParameter> sqlparam = new List<SqlParameter>();
             sqlparam.Add(new SqlParameter("@Description", vendor == null ? System.String.Empty : vendor.Trim()));
             DataTable dt = _sqlHelper.ExecuteDataTable(sqlparam, StoreProcedures.Get_New_Vendor_Autocomplete_Sp.ToString(), CommandType.StoredProcedure);
             if (dt != null && dt.Rows.Count > 0)
             {
                 List<DataRow> drList = new List<DataRow>();
                 drList = dt.AsEnumerable().ToList();
                 foreach (DataRow dr in drList)
                 {
                     AutocompleteInfo auto = new AutocompleteInfo();
                     auto.Label = Convert.ToString(dr["Label"]);
                     auto.Value = Convert.ToInt32(dr["Value"]);
                     autoList.Add(auto);
                 }
             }
             return autoList;
         }

         public List<VendorInfo> Get_Vendors(ref PaginationInfo pager)
         {
             List<VendorInfo> Vendors = new List<VendorInfo>();
             DataTable dt = _sqlHelper.ExecuteDataTable(null, StoreProcedures.Get_New_Vendor_Sp.ToString(), CommandType.StoredProcedure);
             foreach (DataRow dr in CommonMethods.GetRows(dt, ref pager))
             {
                 Vendors.Add(Get_Vendor_Values(dr));
             }
             return Vendors;
         }

         public List<VendorInfo> Get_Vendor_By_Id_List(int vendor_Id, ref PaginationInfo pager)
         {
             List<SqlParameter> sqlParamList = new List<SqlParameter>();
             sqlParamList.Add(new SqlParameter("@Vendor_Id", vendor_Id));

             List<VendorInfo> Vendor = new List<VendorInfo>();
             DataTable dt = _sqlHelper.ExecuteDataTable(sqlParamList, StoreProcedures.Get_New_Vendor_Data_By_Id_Sp.ToString(), CommandType.StoredProcedure);
             foreach (DataRow dr in CommonMethods.GetRows(dt, ref pager))
             {
                 Vendor.Add(Get_Vendor_Values(dr));
             }
             return Vendor;
         }

         private VendorInfo Get_Vendor_Values(DataRow dr)
         {
             VendorInfo Vendor = new VendorInfo();

             Vendor.Vendor_Id = Convert.ToInt32(dr["Vendor_Id"]);
             Vendor.Vendor_Name = Convert.ToString(dr["Vendor_Name"]);
             Vendor.Address = Convert.ToString(dr["Address"]);
             Vendor.City = Convert.ToString(dr["City"]);
             Vendor.State = Convert.ToInt32(dr["State"]);
             Vendor.State_Name = Convert.ToString(dr["State_Name"]);
             Vendor.Pincode = Convert.ToInt32(dr["Pincode"]);
             Vendor.Contact_No_1 = Convert.ToString(dr["Contact_No_1"]);
             Vendor.Contact_No_2 = Convert.ToString(dr["Contact_No_2"]);
             Vendor.Email = Convert.ToString(dr["Email"]);
             Vendor.Is_Active = Convert.ToBoolean(dr["Is_Active"]);
             Vendor.Vendor_Logo = Convert.ToString(dr["Vendor_Logo"]);
             Vendor.Created_On = Convert.ToDateTime(dr["Created_On"]);
             Vendor.Created_By = Convert.ToInt32(dr["Created_By"]);
             Vendor.Updated_On = Convert.ToDateTime(dr["Updated_On"]);
             Vendor.Updated_By = Convert.ToInt32(dr["Updated_By"]);

             return Vendor;
         }

         public bool Check_Existing_Vendor(string vendor_Name)
         {
             bool check = false;
             List<SqlParameter> sqlParam = new List<SqlParameter>();
             sqlParam.Add(new SqlParameter("@Vendor_Name", vendor_Name));

             DataTable dt = _sqlHelper.ExecuteDataTable(sqlParam, StoreProcedures.Check_Existing_New_Vendor.ToString(), CommandType.StoredProcedure);

             if (dt != null && dt.Rows.Count > 0)
             {
                 foreach (DataRow dr in dt.Rows)
                 {
                     check = Convert.ToBoolean(dr["Check_Vendor"]);
                 }
             }
             return check;
         }

         public void Send_Registration_Email(string vendor_Email_Id, string vendor_Name)
         {

             StringBuilder html = new StringBuilder();
             string subject = "Registration Confirmation Message";

             #region Main Table

             html.Append("<p><h1>Registration Successful.</h1></p>");
             html.Append("<p>Hi " + vendor_Name + ",</p>");
             html.Append("<p>            Thank you for registering with SMS.</p>");
             html.Append("<br>");
             html.Append("<br>");
             html.Append("<br>");
             html.Append("<p>This is automated email.Please do not reply to this message.</p>");

             #endregion

             MailAddress fromMail = new MailAddress(ConfigurationManager.AppSettings["fromMailAddress"].ToString(), ConfigurationManager.AppSettings["fromMailName"].ToString());
             MailMessage message = new MailMessage();
             message.From = fromMail;
             message.Subject = subject;
             message.IsBodyHtml = true;
             message.Body = html.ToString();
             MailAddress To = new MailAddress(vendor_Email_Id);
             message.To.Add(To);
             SmtpClient client = new SmtpClient();
             client.Send(message);
         }

         public VendorInfo Get_Vendor_Profile_Data_By_User_Id(int vendor_Id)
         {
             List<SqlParameter> sqlParamList = new List<SqlParameter>();
             sqlParamList.Add(new SqlParameter("@Vendor_Id", vendor_Id));

             VendorInfo Vendor = new VendorInfo();
             DataTable dt = _sqlHelper.ExecuteDataTable(sqlParamList, StoreProcedures.Get_New_Vendor_Profile_Data_Sp.ToString(), CommandType.StoredProcedure);
             foreach (DataRow dr in dt.Rows)
             {
                 Vendor = Get_Vendor_Values(dr);
             }
             return Vendor;
         }

         public void Update_Vendor_Profile(VendorInfo vendor, int user_Id)
         {
             _sqlHelper.ExecuteNonQuery(Set_Values_In_Vendor_Profile(vendor, user_Id), StoreProcedures.Update_Vendor_Profile_Data_Sp.ToString(), CommandType.StoredProcedure);
         }

         private List<SqlParameter> Set_Values_In_Vendor_Profile(VendorInfo vendor, int user_Id)
         {
             List<SqlParameter> sqlParams = new List<SqlParameter>();

             sqlParams.Add(new SqlParameter("@Vendor_Id", vendor.Vendor_Id));

             sqlParams.Add(new SqlParameter("@Vendor_Name", vendor.Vendor_Name));
             sqlParams.Add(new SqlParameter("@Address", vendor.Address));
             sqlParams.Add(new SqlParameter("@City", vendor.City));
             sqlParams.Add(new SqlParameter("@State", vendor.State));
             sqlParams.Add(new SqlParameter("@Pincode", vendor.Pincode));
             sqlParams.Add(new SqlParameter("@Contact_No_1", vendor.Contact_No_1));
             sqlParams.Add(new SqlParameter("@Contact_No_2", vendor.Contact_No_2));
             sqlParams.Add(new SqlParameter("@Email", vendor.Email));

             sqlParams.Add(new SqlParameter("@Updated_On", DateTime.Now));
             sqlParams.Add(new SqlParameter("@Updated_By", user_Id));

             return sqlParams;
         }

         public void Update_Vendor_FileName(int vendor_Id, string File_Name)
         {
             List<SqlParameter> sqlParam = new List<SqlParameter>();
             sqlParam.Add(new SqlParameter("@Vendor_Id", vendor_Id));
             sqlParam.Add(new SqlParameter("@File_Name", File_Name));
             _sqlHelper.ExecuteNonQuery(sqlParam, StoreProcedures.Update_Vendor_Image.ToString(), CommandType.StoredProcedure);

         }

         public VendorInfo Get_Vendor_Logo_By_Id(int vendor_Id)
         {
             List<SqlParameter> sqlParamList = new List<SqlParameter>();
             sqlParamList.Add(new SqlParameter("@Vendor_Id", vendor_Id));

             VendorInfo Vendor = new VendorInfo();
             DataTable dt = _sqlHelper.ExecuteDataTable(sqlParamList, StoreProcedures.Get_New_Vendor_By_Id_Sp.ToString(), CommandType.StoredProcedure);
             foreach (DataRow dr in dt.Rows)
             {
                 Vendor = Get_Vendor_Logo_Values(dr);
             }
             return Vendor;
         }

         private VendorInfo Get_Vendor_Logo_Values(DataRow dr)
         {
             VendorInfo Vendor = new VendorInfo();

             Vendor.Vendor_Id = Convert.ToInt32(dr["Vendor_Id"]);
             Vendor.Vendor_Name = Convert.ToString(dr["Vendor_Name"]);
             Vendor.Address = Convert.ToString(dr["Address"]);
             Vendor.City = Convert.ToString(dr["City"]);
             Vendor.State = Convert.ToInt32(dr["State"]);
             Vendor.State_Name = Convert.ToString(dr["State_Name"]);
             Vendor.Pincode = Convert.ToInt32(dr["Pincode"]);
             Vendor.Contact_No_1 = Convert.ToString(dr["Contact_No_1"]);
             Vendor.Contact_No_2 = Convert.ToString(dr["Contact_No_2"]);
             Vendor.Email = Convert.ToString(dr["Email"]);
             Vendor.Vendor_Logo = Convert.ToString(dr["Vendor_Logo"]);
             Vendor.Is_Active = Convert.ToBoolean(dr["Is_Active"]);
             Vendor.Created_On = Convert.ToDateTime(dr["Created_On"]);
             Vendor.Created_By = Convert.ToInt32(dr["Created_By"]);
             Vendor.Updated_On = Convert.ToDateTime(dr["Updated_On"]);
             Vendor.Updated_By = Convert.ToInt32(dr["Updated_By"]);

             return Vendor;
         }

    }
}
