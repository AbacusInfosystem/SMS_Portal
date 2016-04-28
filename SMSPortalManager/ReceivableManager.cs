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

       public List<ReceivableInfo> Get_Receivable_By_Name(string Invoice_No, ref PaginationInfo pager)
       {
           return _receivableRepo.Get_Receivable_By_Id(Invoice_No, ref pager);
       }

       public List<ReceivableInfo> Get_Receivables(ref PaginationInfo pager)
       {
           return _receivableRepo.Get_Receivables(ref pager);
       }

       public List<AutocompleteInfo> Load_Receivable_InvoiceNo(string txtInvoice_No)
       {
           return _receivableRepo.Load_Receivable_InvoiceNo(txtInvoice_No);
       }

       public List<ReceivableInfo> Get_InvoiceNo()
       {
           return _receivableRepo.Get_InvoiceNo();
       }

       public void Insert_Receivable(ReceivableInfo receivableInfo,int user_Id)
       {
           _receivableRepo.Insert_Receivable(receivableInfo, user_Id);
       }

       //public List<ReceivableInfo> Get_Receivable_Items(int receivable_Id)
       //{
       //    return _receivableRepo.(receivable_Id);
       //}
    }
}
