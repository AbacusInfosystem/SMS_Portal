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
    public class OrdersManager
    {
        OrdersRepo _ordersRepo;

        public OrdersManager()
        {
            _ordersRepo = new OrdersRepo();
        }

        public List<OrdersInfo> Get_Orders(ref PaginationInfo Pager,int dealer_Id)
        {
            return _ordersRepo.Get_Orders(ref Pager, dealer_Id);
        }

        public List<OrdersInfo> Get_All_Orders(ref PaginationInfo Pager)
        {
            return _ordersRepo.Get_All_Orders(ref Pager);
        }

        public List<OrdersInfo> Get_Vendor_Orders(ref PaginationInfo Pager, int vendor_Id)
        {
            return _ordersRepo.Get_Vendor_Orders(ref Pager, vendor_Id);
        }

        public OrdersInfo Get_Order_Data_By_Id(int order_Id)
        {
            return _ordersRepo.Get_Orders_By_Id(order_Id);
        }

        public OrdersInfo Get_Dealer_Order_Data_By_Id(int order_Id)
        {
            return _ordersRepo.Get_Dealer_Orders_By_Id(order_Id);
        }

        public OrdersInfo Get_Vendor_Order_Data_By_Id(int vendor_Order_Id)
        {
            return _ordersRepo.Get_Vendor_Orders_By_Id(vendor_Order_Id);
        }

        public int Insert_Orders(OrdersInfo orders, out string order_Ids, out decimal order_Amount)
        {
            return _ordersRepo.Insert_Orders(orders, out order_Ids, out order_Amount);
        }

        public List<OrdersInfo> Get_Orders_Data_By_Id(int order_Id,ref PaginationInfo Pager)
        {
            return _ordersRepo.Get_Orders_Data_By_Id(order_Id, ref Pager);
        }

        public List<OrderItemInfo> Get_Orders_Item_By_Id(int order_Id)
        {
            return _ordersRepo.Get_Order_Items_By_Order_Id(order_Id);
        }

        public List<OrderItemInfo> Get_Dealer_Order_Items_By_Order_Id(int order_Id)
        {
            return _ordersRepo.Get_Dealer_Order_Items_By_Order_Id(order_Id);
        }

        public List<OrderItemInfo> Get_Vendor_Orders_Item_By_Id(int order_Id)
        {
            return _ordersRepo.Get_Vendor_Order_Items_By_Order_Id(order_Id);
        }

        public List<OrdersInfo> Get_Orders_Data_By_Dates(DateTime fromDate, DateTime toDate, ref PaginationInfo pager)
        {
            return _ordersRepo.Get_Orders_Data_By_Dates(fromDate, toDate,ref pager);
        }

        public List<OrdersInfo> Get_Orders_Data_By_Status(int Status, ref PaginationInfo pager,int role_Id,int entity_Id)
        {
            return _ordersRepo.Get_Orders_Data_By_Status(Status, ref pager, role_Id, entity_Id);
        }
        public DealerInfo Get_Dealer_Data_By_Id(int dealer_Id)
        {
            return _ordersRepo.Get_Dealer_Data(dealer_Id);
        }

        public void Update_Order_Status(OrdersInfo order)
            {
            _ordersRepo.Update_Order_Status(order);
            }

        public void Update_Vendor_Order_Status(OrdersInfo order)
        {
            _ordersRepo.Update_Vendor_Order_Status(order);
        }

        public void Send_Order_Status_Notification(string first_Name, string email_Id, OrdersInfo order,bool confirmed_Status)
        {
            _ordersRepo.Send_Order_Status_Notification(first_Name, email_Id, order, confirmed_Status);
        }
         
        public List<AutocompleteInfo> Get_Order_No_Autocomplete(string order_No)
        {
            return _ordersRepo.Get_Order_No_Autocomplete(order_No);
        }

        public List<AutocompleteInfo> Get_Orders_No_Autocomplete_By_Dealer(string order_No, int Dealer_Id)
        {
            return _ordersRepo.Get_Orders_No_Autocomplete_By_Dealer(order_No, Dealer_Id);
        }

        public void Set_Order_Balanace_Amount(int Order_Id, decimal Amount)
        {
            _ordersRepo.Set_Order_Balanace_Amount(Order_Id, Amount);
        }

        public void Set_Order_Status(int Order_Id, int Status)
        {
            _ordersRepo.Set_Order_Status(Order_Id, Status);
        }

        public List<OrdersInfo> Get_Vendor_Consolidated_Orders_Data_By_Dates(DateTime fromDate, DateTime toDate,int entity_Id)
        {
            return _ordersRepo.Get_Consolidated_Sales_Orders_Data_By_Dates(fromDate, toDate, entity_Id);
        }

        public void Set_Vendor_Order_Balanace_Amount(int Order_Id, decimal Amount)
        {
            _ordersRepo.Set_Vendor_Order_Balanace_Amount(Order_Id, Amount);
        }

        public void Set_Vendor_Order_Status(int Order_Id, int Status)
        {
            _ordersRepo.Set_Vendor_Order_Status(Order_Id, Status);
        }

    }
}
