using SMSPortalInfo;
using SMSPortalInfo.Common;
using SMSPortalRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortalManager
{
    public class ConsolidatedOrderManager
    {
        public ConsolidatedOrderRepo _consolidatedOrerRepo;

        public ConsolidatedOrderManager()
        {
            _consolidatedOrerRepo = new ConsolidatedOrderRepo();
        }

        public List<ConsolidatedOrderInfo> Get_Orders(int entity_Id, DateTime from_Date, DateTime to_Date)
        {
           return _consolidatedOrerRepo.Get_Consolidated_Orders(entity_Id,from_Date,to_Date);
        }

        public void Update_Order(List<ConsolidatedOrderInfo> consolidated_Orders,DateTime frm_Date,DateTime to_Date,int entity_Id)
        {
            _consolidatedOrerRepo.Update_Order(consolidated_Orders, frm_Date, to_Date, entity_Id);
        }

        public string Get_Order_No_By_Dealer_Id(int entity_Id)
        {
            return _consolidatedOrerRepo.Get_Order_No_By_Dealer_Id(entity_Id);
        }
    }
}
