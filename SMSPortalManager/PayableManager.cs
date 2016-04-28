using SMSPortalInfo;
using SMSPortalRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortalManager
{
   public class PayableManager
    {

        PayableRepo _payableRepo;

        public PayableManager()
        {
            _payableRepo = new PayableRepo();
        }

        public void Insert_Payable(PayableInfo payableInfo, int user_Id)
        {
            _payableRepo.Insert_Payable(payableInfo, user_Id);
        }
    }
}
