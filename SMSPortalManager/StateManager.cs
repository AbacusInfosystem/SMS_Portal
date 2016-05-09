using SMSPortalRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortalManager
{
    public class StateManager
    {
        public StateRepo _stateRepo;
        public StateManager()
        {
            _stateRepo = new StateRepo();
        }

        public StateInfo Get_State_By_Id(int stateId)
        {
            return _stateRepo.Get_State_By_Id(stateId);
        }

        public List<StateInfo> Get_States()
        {
            return _stateRepo.Get_States();
        }

    }
}
