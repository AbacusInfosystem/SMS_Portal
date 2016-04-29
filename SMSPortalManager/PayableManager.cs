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
   public class PayableManager
    {

        PayableRepo _payableRepo;

        public PayableManager()
        {
            _payableRepo = new PayableRepo();
        }

        public int Insert_Payable(PayableInfo payableInfo, int user_Id)
        {
            return _payableRepo.Insert_Payable(payableInfo, user_Id);
        }

        public List<PayableInfo> Get_Payable_By_Id(int invoice_Id, ref PaginationInfo pager)
        {
            return _payableRepo.Get_Payable_By_Id(invoice_Id, ref pager);
        }

        public List<PayableInfo> Get_Payables(ref PaginationInfo pager)
        {
            return _payableRepo.Get_Payables(ref pager);
        }

        public void Insert_PayableItems(PayableInfo payableInfo, int user_Id)
        {
            _payableRepo.Insert_PayableItems(payableInfo, user_Id);
        }



        public PayableInfo Get_Payable_Data_By_Id(int payable_Id)
        {
            return _payableRepo.Get_Payable_Data_By_Id(payable_Id);
        }

        public List<PayableInfo> Get_Payable_Items_By_Id(int payable_Id)
        {
            return _payableRepo.Get_Payable_Items_By_Id(payable_Id);
        }
    }
}

