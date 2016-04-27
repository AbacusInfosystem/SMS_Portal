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
    public class PurchaseOrderManager
    {
        PurchaseOrderRepo _purchaseorderRepo;

        public PurchaseOrderManager()
        {
            _purchaseorderRepo = new PurchaseOrderRepo();
        }

        public void Insert_Purchase_Order(PurchaseOrderInfo purchaseorder)
        {
            _purchaseorderRepo.Insert_Purchase_Order(purchaseorder);
        }

        //public void Update_Purchase_Order(PurchaseOrderInfo purchaseorder)
        //{
        //    _purchaseorderRepo.Update_Purchase_Order(purchaseorder);
        //}

        public List<PurchaseOrderInfo> Get_Purchase_Orders(ref PaginationInfo Pager)
        {
            return _purchaseorderRepo.Get_Purchase_Orders(ref Pager);
        }

        public List<PurchaseOrderInfo> Get_Purchase_Orders_By_Id(int Purchase_Order_Id,ref PaginationInfo Pager)
        {
            return _purchaseorderRepo.Get_Purchase_Orders_By_Id(Purchase_Order_Id,ref Pager);
        }       

        public PurchaseOrderInfo Get_Purchase_Order_By_Id(int Purchase_Order_Id)
        {
            return _purchaseorderRepo.Get_Purchase_Order_By_Id(Purchase_Order_Id);
        }

        public List<AutocompleteInfo> Get_Purchase_Order_Autocomplete(string Purchase_Order_No)
        {
            return _purchaseorderRepo.Get_Purchase_Order_Autocomplete(Purchase_Order_No);
        }
        
    }
}
