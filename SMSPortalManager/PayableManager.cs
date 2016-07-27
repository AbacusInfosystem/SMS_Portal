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

        public int Insert_Payable(PayableInfo payableInfo, int user_Id,int role_Id,int entity_Id)
        {
            return _payableRepo.Insert_Payable(payableInfo, user_Id,role_Id,entity_Id);
        }

        public List<PayableInfo> Get_Payable_By_Id(int Purchase_Order_Id, ref PaginationInfo pager)
        {
            return _payableRepo.Get_Payable_By_Id(Purchase_Order_Id, ref pager);
        }

        public List<PayableInfo> Get_Payables(ref PaginationInfo pager, int Vendor_Id,int entity_Id)
        {
            return _payableRepo.Get_Payables(ref pager, Vendor_Id, entity_Id);
        }

        public void Insert_PayableItems(PayableInfo payableInfo, int user_Id)
        {
            _payableRepo.Insert_PayableItems(payableInfo, user_Id);
        }

        public PayableInfo Get_Payable_Data_By_Id(int purchase_order_id,int entity_Id)
        {
            return _payableRepo.Get_Payable_Data_By_Id(purchase_order_id, entity_Id);
        }

        public List<PayableInfo> Get_Payable_Items_By_Id(int payable_Id)
        {
            return _payableRepo.Get_Payable_Items_By_Id(payable_Id);
        }

        public void Delete_Payable_Data_Item_By_Id(int payable_Item_Id)
        {
            _payableRepo.Delete_Payable_Data_Item_By_Id(payable_Item_Id);
        }

        public decimal Get_Purchase_Order_Amount(int purchase_order_Id)
        {
            return _payableRepo.Get_Purchase_Order_Amount(purchase_order_Id);
        }

        //public string Get_Payable_Status(int purchase_order_id)
        //{
        //    return _payableRepo.Get_Payable_Status(purchase_order_id);
        //}

        public List<AutocompleteInfo> Get_Payable_Purchase_Order_Autocomplete(string purchase_order_no,int Vendor_Id)
        {
            return _payableRepo.Get_Payable_Purchase_Order_Autocomplete(purchase_order_no, Vendor_Id);
        }

    }
}

