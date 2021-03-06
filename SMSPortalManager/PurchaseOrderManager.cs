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
    public class PurchaseOrderManager
    {
        PurchaseOrderRepo _purchaseorderRepo;

        public PurchaseOrderManager()
        {
            _purchaseorderRepo = new PurchaseOrderRepo();
        }

        public int Insert_Purchase_Order(PurchaseOrderInfo purchaseorder,int user_id,int entity_Id)
        {
            return _purchaseorderRepo.Insert_Purchase_Order(purchaseorder, user_id, entity_Id);
        }

        public void Update_Purchase_Order(PurchaseOrderInfo purchaseorder, int user_id)
        {
            _purchaseorderRepo.Update_Purchase_Order(purchaseorder,user_id);
        }

        public List<PurchaseOrderInfo> Get_Purchase_Orders(ref PaginationInfo Pager,int entity_Id)
        {
            return _purchaseorderRepo.Get_Purchase_Orders(ref Pager, entity_Id);
        }

        public List<PurchaseOrderInfo> Get_Purchase_Orders_By_Id(int Purchase_Order_Id,ref PaginationInfo Pager)
        {
            return _purchaseorderRepo.Get_Purchase_Orders_By_Id(Purchase_Order_Id,ref Pager);
        }       

        public PurchaseOrderInfo Get_Purchase_Order_By_Id(int Purchase_Order_Id)
        {
            return _purchaseorderRepo.Get_Purchase_Order_By_Id(Purchase_Order_Id);
        }

        public List<AutocompleteInfo> Get_Purchase_Order_Autocomplete(string Purchase_Order_No,int entity_Id)
        {
            return _purchaseorderRepo.Get_Purchase_Order_Autocomplete(Purchase_Order_No, entity_Id);
        }

        public List<PurchaseOrderItemInfo> Get_Purchase_Order_Items_By_Id(int Purchase_Order_Id)
        {
            return _purchaseorderRepo.Get_Purchase_Order_Items_By_Id(Purchase_Order_Id);
        }

        public void Insert_Purchase_Order_Item(PurchaseOrderItemInfo purchaseOrderItemInfo, int user_id)
        {
             _purchaseorderRepo.Insert_Purchase_Order_Item(purchaseOrderItemInfo, user_id);
        }

        public void Update_Purchase_Order_Item(PurchaseOrderItemInfo purchaseOrderItemInfo, int user_id)
        {
            _purchaseorderRepo.Update_Purchase_Order_Item(purchaseOrderItemInfo ,user_id);
        }

        public void Delete_Purchase_Order_Item_By_Id(int purchase_order_item_id)
        {
            _purchaseorderRepo.Delete_Purchase_Order_Item_By_Id(purchase_order_item_id);
        }

        public bool Check_Duplicate_Product_PurchaseOrder(int Product_Id, int Purchase_Order_Id)
        {
            return _purchaseorderRepo.Check_Duplicate_Product_PurchaseOrder(Product_Id, Purchase_Order_Id);
        }

        public string Generate_Ref_No(string initialCharacter, string columnName, string substringStartIndex, string substringEndIndex, string tableName)
        {
            return _purchaseorderRepo.Generate_Ref_No(initialCharacter, columnName, substringStartIndex, substringEndIndex, tableName);
        }

        public string Generate_Ven_Ref_No(string initialCharacter, string columnName, string substringStartIndex, string substringEndIndex, string tableName,int vendor_Id)
        {
            return _purchaseorderRepo.Generate_Ven_Ref_No(initialCharacter, columnName, substringStartIndex, substringEndIndex, tableName, vendor_Id);
        }

        public void Update_Purchase_Order_Gross_Amount(int Purchaser_Order_Id, decimal Gross_Amount)
        {
            _purchaseorderRepo.Update_Purchase_Order_Gross_Amount(Purchaser_Order_Id, Gross_Amount);
        }

        public ProductInfo Get_Vendor_Product_Price_Id(int Product_Id, int Vendor_Id)
        {
            return _purchaseorderRepo.Get_Vendor_Product_Price_Id(Product_Id, Vendor_Id);
        }

        public void Send_Purchase_Order_Email(string Vendor_Email_Id, PurchaseOrderInfo PurchaseOrder, List<PurchaseOrderItemInfo> purchaseOrderItems)
        {
             _purchaseorderRepo.Send_Purchase_Order_Email(Vendor_Email_Id, PurchaseOrder, purchaseOrderItems);
        }

        public ThirdPartyVendorInfo Get_Vendor_By_Id(int Vendor_Id)
        {
            return _purchaseorderRepo.Get_Vendor_By_Id(Vendor_Id);
        }

        public List<PurchaseOrderInfo> Get_Purchase_Orders_By_Vendor_Id(int vendor_Id)
        {
            return _purchaseorderRepo.Get_Purchase_Orders_By_Vendor(vendor_Id);
        }
        
    }
}
