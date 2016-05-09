using SMSPortalHelper;
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
    public class StateRepo
    {
        SQLHelper _sqlRepo;

        public StateRepo()
        {
            _sqlRepo = new SQLHelper();
        }
        public List<StateInfo> Get_States()
        {
            List<StateInfo> states = new List<StateInfo>();
            DataTable dt = _sqlRepo.ExecuteDataTable(null, StoreProcedures.Get_State_Sp.ToString(), CommandType.StoredProcedure);
            foreach (DataRow dr in dt.Rows)
            {
                states.Add(Get_State_Values(dr));
            }
            return states;
        }

        public StateInfo Get_State_By_Id(int stateId)
        {
            StateInfo state = new StateInfo();
            List<SqlParameter> sqlParam = new List<SqlParameter>();

            sqlParam.Add(new SqlParameter("@stateId", stateId));

            DataTable dt = _sqlRepo.ExecuteDataTable(sqlParam, StoreProcedures.Get_State_By_Id_Sp.ToString(), CommandType.StoredProcedure);
            foreach (DataRow dr in dt.Rows)
            {
                state = Get_State_Values(dr);
            }
            return state;
        }

        private StateInfo Get_State_Values(DataRow dr)
        {
            StateInfo state = new StateInfo();

            state.State_Id = Convert.ToInt32(dr["State_Id"]);
            state.State_Name = Convert.ToString(dr["State_Name"]);
            state.Country_Id = Convert.ToInt32(dr["Country_Id"]);
            return state;
        }


         
    }
}
