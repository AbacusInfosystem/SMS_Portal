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
    public class InvoiceRepo
    {
        SQLHelper _sqlRepo;

        public InvoiceRepo()
        {
            _sqlRepo = new SQLHelper();
        }

        public void Insert_Invoice(InvoiceInfo invoice)
        {
            _sqlRepo.ExecuteNonQuery(Set_Values_In_Invoice(invoice), StoreProcedures.Insert_Invoice_Sp.ToString(), CommandType.StoredProcedure);
        }

        //public void Update_Invoice(InvoiceInfo invoice)
        //{
        //    _sqlRepo.ExecuteNonQuery(Set_Values_In_Invoice(invoice), StoreProcedures.Update_Invoice_Sp.ToString(), CommandType.StoredProcedure);
        //}

        private List<SqlParameter> Set_Values_In_Invoice(InvoiceInfo invoice)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            sqlParams.Add(new SqlParameter("@Invoice_Id", invoice.Invoice_Id));
            sqlParams.Add(new SqlParameter("@Order_Id", invoice.Order_Id));
            sqlParams.Add(new SqlParameter("@Invoice_No", invoice.Invoice_No));
            sqlParams.Add(new SqlParameter("@Invoice_Date", invoice.Invoice_Date));
            sqlParams.Add(new SqlParameter("@Created_On", invoice.Created_On));
            sqlParams.Add(new SqlParameter("@Created_By", invoice.Created_By));
            sqlParams.Add(new SqlParameter("@Updated_On", invoice.Updated_On));
            sqlParams.Add(new SqlParameter("@Updated_By", invoice.Updated_By));

            return sqlParams;
        }

        public List<InvoiceInfo> Get_Invoices(ref PaginationInfo Pager)
        {
            List<InvoiceInfo> invoices = new List<InvoiceInfo>();
            DataTable dt = _sqlRepo.ExecuteDataTable(null, StoreProcedures.Get_Invoice_Sp.ToString(), CommandType.StoredProcedure);
            foreach (DataRow dr in CommonMethods.GetRows(dt, ref Pager))
            {
                invoices.Add(Get_Invoice_Values(dr));
            }
            return invoices;
        }
        public InvoiceInfo Get_Invoice_By_Id(int Invoice_Id)
        {
            InvoiceInfo invoice = new InvoiceInfo();
            DataTable dt = _sqlRepo.ExecuteDataTable(null, StoreProcedures.Get_Invoice_Sp.ToString(), CommandType.StoredProcedure);
            foreach (DataRow dr in dt.Rows)
            {
                invoice = Get_Invoice_Values(dr);
            }
            return invoice;
        }

        private InvoiceInfo Get_Invoice_Values(DataRow dr)
        {
            InvoiceInfo invoice = new InvoiceInfo();

            invoice.Invoice_Id = Convert.ToInt32(dr["Invoice_Id"]);
            invoice.Order_Id = Convert.ToInt32(dr["Order_Id"]);
            invoice.Invoice_No = Convert.ToString(dr["Invoice_No"]);
            invoice.Invoice_Date = Convert.ToDateTime(dr["Invoice_Date"]);
            invoice.Created_On = Convert.ToDateTime(dr["Created_On"]);
            invoice.Created_By = Convert.ToInt32(dr["Created_By"]);
            invoice.Updated_On = Convert.ToDateTime(dr["Updated_On"]);
            invoice.Updated_By = Convert.ToInt32(dr["Updated_By"]);
            return invoice;
        }
        //public void Delete_Invoice_By_Id(int invoice_id)
        //{
        //    List<SqlParameter> sqlParams = new List<SqlParameter>();
        //    sqlParams.Add(new SqlParameter("@Invoice_Id", invoice_id));
        //    _sqlRepo.ExecuteNonQuery(sqlParams, StoreProcedures.Delete_Invoice_By_Id.ToString(), CommandType.StoredProcedure);
        //}
    }
}
