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
    public class InvoiceManager
    {
        public InvoiceRepo _invoiceRepo;

        public InvoiceManager()
        {
            _invoiceRepo = new InvoiceRepo();
        }

        public int Insert_Invoice(InvoiceInfo invoice,int user_id)
        {
           return _invoiceRepo.Insert_Invoice(invoice, user_id);
        }

        public int Insert_Vendor_Invoice(InvoiceInfo invoice, int user_id,int entity_Id)
        {
            return _invoiceRepo.Insert_Vendor_Invoice(invoice, user_id, entity_Id);
        }

        //public void Update_Invoice(InvoiceInfo invoice)
        //{
        //    _invoiceRepo.Update_Invoice(invoice);
        //}

        public List<InvoiceInfo> Get_Invoices(ref PaginationInfo Pager)
        {
            return _invoiceRepo.Get_Invoices(ref Pager);
        }
        public List<InvoiceInfo> Get_Vendor_Invoices(ref PaginationInfo Pager,int entity_id)
        {
            return _invoiceRepo.Get_Vendor_Invoices(ref Pager, entity_id);
        }
        public List<InvoiceInfo> Get_Invoices_By_Id(int Invoice_Id, ref PaginationInfo Pager)
        {
            return _invoiceRepo.Get_Invoices_By_Id(Invoice_Id, ref Pager);
        }

        public List<InvoiceInfo> Get_Vendor_Invoices_By_Id(int Invoice_Id,int entity_Id, ref PaginationInfo Pager)
        {
            return _invoiceRepo.Get_Vendor_Invoices_By_Id(Invoice_Id,entity_Id, ref Pager);
        }

        public InvoiceInfo Get_Invoice_By_Id(int Invoice_Id)
        {
            return _invoiceRepo.Get_Invoice_By_Id(Invoice_Id);
        }

        public InvoiceInfo Get_Vendor_Invoice_By_Id(int Invoice_Id,int entity_Id)
        {
            return _invoiceRepo.Get_Vendor_Invoice_By_Id(Invoice_Id, entity_Id);
        }

        public InvoiceInfo Get_Dealer_Invoice_By_Id(int Invoice_Id, int entity_Id)
        {
            return _invoiceRepo.Get_Dealer_Invoice_By_Id(Invoice_Id, entity_Id);
        }
         
        public List<AutocompleteInfo> Get_Invoice_Autocomplete(string InvoiceNo,int role_id,int entity_id)
        {
            return _invoiceRepo.Get_Invoice_Autocomplete(InvoiceNo,role_id,entity_id);
        }

        public DealerInfo Get_Dealer_By_Id(int Dealer_Id)
        {
            return _invoiceRepo.Get_Dealer_By_Id(Dealer_Id);
        }

        public ProductInfo Get_Product_By_Id(int Product_Id)
        {
            return _invoiceRepo.Get_Product_By_Id(Product_Id);
        }

        public TaxInfo Get_Tax_By_Product_By_Id(int Product_Id)
        {
            return _invoiceRepo.Get_Tax_Product_By_Id(Product_Id);
        }

        public void Send_Invoice_Email(string Email_Id, InvoiceInfo invoice, OrdersInfo Order,DealerInfo Dealer)
        {
            _invoiceRepo.Send_Invoice_Email(Email_Id, invoice, Order, Dealer);
        }

        public OrdersInfo Get_Orders_By_Id(int Order_Id)
        {
            return _invoiceRepo.Get_Orders_By_Id(Order_Id);
        }

        public List<OrderItemInfo> Get_Order_Items_By_Order_Id(int Order_Id)
        {
            return _invoiceRepo.Get_Order_Items_By_Order_Id(Order_Id);
        }

        #region Brand Invoice

        public List<InvoiceInfo> Get_Brand_Invoices(int Entity_Id, ref PaginationInfo Pager)
        {
            return _invoiceRepo.Get_Brand_Invoices(Entity_Id, ref Pager);
        }

        public List<AutocompleteInfo> Get_Brand_Invoice_Autocomplete(string InvoiceNo, int Brand_Id)
        {
            return _invoiceRepo.Get_Brand_Invoice_Autocomplete(InvoiceNo, Brand_Id);
        }
        #endregion
    }
}
