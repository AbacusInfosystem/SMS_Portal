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
   public class ReceivableManager
    {
       ReceivableRepo _receivableRepo;

       public ReceivableManager()
        {
            _receivableRepo = new ReceivableRepo();
        }

       public List<ReceivableInfo> Get_Receivable_By_Id(int invoice_Id, ref PaginationInfo pager)
       {
           return _receivableRepo.Get_Receivable_By_Id(invoice_Id, ref pager);
       }

       public List<ReceivableInfo> Get_Receivables(ref PaginationInfo pager, int entity_Id, int role_Id)
       {
           return _receivableRepo.Get_Receivables(ref pager, entity_Id, role_Id);
       }

       public int Insert_Receivable(ReceivableInfo receivableInfo, int user_Id, out bool Status)
       {
           return _receivableRepo.Insert_Receivable(receivableInfo, user_Id,out Status);
       }

       public void Insert_ReceivableItems(ReceivableInfo receivableInfo, int user_Id)
       {
           _receivableRepo.Insert_Receivable_Items(receivableInfo, user_Id);
       }

       public void Insert_Receivable_Receipt(ReceivableInfo receivableInfo, int user_Id)
       {
           _receivableRepo.Insert_Receivable_Receipt_Data(receivableInfo, user_Id);
       }

       public ReceivableInfo Get_Receivable_Data_By_Id(int invoice_Id)
       {
           return _receivableRepo.Get_Receivable_Data_By_Id(invoice_Id);
       }

       public List<ReceivableInfo> Get_Receivable_Items(int receivable_Id)
       {
           return _receivableRepo.Get_Receivable_Items_By_Id(receivable_Id);
       }

       //public void Delete_Receivable_Data_Item_By_Id(int receivable_Item_Id)
       //{
       //    _receivableRepo.Delete_Receivable_Data_Item_By_Id(receivable_Item_Id);
       //}

       public List<AutocompleteInfo> Get_Invoice_Autocomplete(string invoice_No)
       {
           return _receivableRepo.Get_Invoice_Autocomplete(invoice_No);
       }

       public void Send_Payment_Receipt(string first_Name,string email_Id, ReceivableInfo receivableInfo, List<ReceivableInfo> receivables,OrdersInfo orderInfo)
       {
           _receivableRepo.Send_Payment_Receipt(first_Name, email_Id, receivableInfo, receivables, orderInfo);
       }

       //public string Get_Receivable_Status(int invoice_Id)
       //{
       //    return _receivableRepo.Get_Receivable_Status(invoice_Id);
       //}

       public decimal Get_Invoice_Amount(int invoice_Id)
       {
           return _receivableRepo.Get_Invoice_Amount(invoice_Id);
       }

       public void Update_Sales_Order_Status(int invoice_Id)
       {
           _receivableRepo.Update_Sales_Order_Status(invoice_Id);
       }
    }
}
