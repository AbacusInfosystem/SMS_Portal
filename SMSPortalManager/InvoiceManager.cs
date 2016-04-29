﻿using SMSPortalInfo;
using SMSPortalInfo.Common;
using SMSPortalRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortalManager
{
    public class InvoiceManager
    {
        public InvoiceRepo _invoiceRepo;

        public InvoiceManager()
        {
            _invoiceRepo = new InvoiceRepo();
        }

        public void Insert_Invoice(InvoiceInfo invoice)
        {
            _invoiceRepo.Insert_Invoice(invoice);
        }

        //public void Update_Invoice(InvoiceInfo invoice)
        //{
        //    _invoiceRepo.Update_Invoice(invoice);
        //}

        public List<InvoiceInfo> Get_Invoices(ref PaginationInfo Pager)
        {
            return _invoiceRepo.Get_Invoices(ref Pager);
        }

        public InvoiceInfo Get_Invoice_By_Id(int Invoice_Id)
        {
            return _invoiceRepo.Get_Invoice_By_Id(Invoice_Id);
        }
        //public void Delete_Invoice_By_Id(int Invoice_Id)
        //{
        //    _enquiryRepo.Delete_Invoice_By_Id(Invoice_Id);
        //}
    }
}
