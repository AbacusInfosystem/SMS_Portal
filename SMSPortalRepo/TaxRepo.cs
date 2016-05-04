using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMSPortalHelper;
using SMSPortalInfo;
using SMSPortalInfo.Common;
using SMSPortalRepo.Common;

namespace SMSPortalRepo
{
    public class TaxRepo
    {
        SQLHelper _sqlRepo = null;

        public TaxRepo()
        {
            _sqlRepo = new SQLHelper();
        }

        public int Insert_Tax(TaxInfo tax)
        {
            int tax_id = 0;

            tax_id=Convert.ToInt32(_sqlRepo.ExecuteScalerObj(Set_Values_In_Tax(tax), StoreProcedures.Insert_Tax_Sp.ToString(), CommandType.StoredProcedure));

            return tax_id;
        }

        public void Update_Tax(TaxInfo tax)
        {
            _sqlRepo.ExecuteNonQuery(Set_Values_In_Tax(tax), StoreProcedures.Update_Tax_Sp.ToString(), CommandType.StoredProcedure);
        }

        public TaxInfo Get_Tax_By_Id()
        {
      
            TaxInfo tax = new TaxInfo();

            DataTable dt = _sqlRepo.ExecuteDataTable(null, StoreProcedures.Get_Tax_By_Id_Sp.ToString(), CommandType.StoredProcedure);
            
            List<DataRow> drList = new List<DataRow>();
            
            drList = dt.AsEnumerable().ToList();
            
            foreach (DataRow dr in drList)
            {
                tax = Get_Tax_Values(dr);
            }

            return tax;
        }

        private TaxInfo Get_Tax_Values(DataRow dr)
       
        {

            TaxInfo tax = new TaxInfo();

            tax.Tax_Id = Convert.ToInt32(dr["Tax_Id"]);

            if (!dr.IsNull("Tax_Id"))

            tax.Local_Tax = Convert.ToDecimal(dr["Local_Tax"]);

            tax.Export_Tax = Convert.ToDecimal(dr["Export_Tax"]);

            tax.Created_On = Convert.ToDateTime(dr["Created_On"]);

            tax.Created_By = Convert.ToInt32(dr["Created_By"]);

            tax.Updated_On = Convert.ToDateTime(dr["Updated_On"]);

            tax.Updated_By = Convert.ToInt32(dr["Updated_By"]);

            return tax;
        }

        private List<SqlParameter> Set_Values_In_Tax(TaxInfo tax)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            if (tax.Tax_Id != 0)
            
            {
                sqlParams.Add(new SqlParameter("@Tax_Id", tax.Tax_Id));
            }

            //sqlParams.Add(new SqlParameter("@Tax_Id", tax.Tax_Id));

            sqlParams.Add(new SqlParameter("@Local_Tax", tax.Local_Tax));

            sqlParams.Add(new SqlParameter("@Export_Tax", tax.Export_Tax));

            if (tax.Tax_Id == 0)
           
            {
                sqlParams.Add(new SqlParameter("@Created_On", tax.Created_On));

                sqlParams.Add(new SqlParameter("@Created_By", tax.Created_By));
            }

            sqlParams.Add(new SqlParameter("@Updated_On", tax.Updated_On));

            sqlParams.Add(new SqlParameter("@Updated_By", tax.Updated_By));

            return sqlParams;
        }

    }
}
