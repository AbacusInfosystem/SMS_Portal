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

        public OrdersInfo Get_Order_Data_By_Id(int order_Id)
        {
            return _ordersRepo.Get_Orders_By_Id(order_Id);
        }
        public int Insert_Orders(OrdersInfo orders)
        {
          return  _ordersRepo.Insert_Orders(orders);
        }

        public List<OrdersInfo> Get_Orders_Data_By_Id(int order_Id,ref PaginationInfo Pager)
        {
            return _ordersRepo.Get_Orders_Data_By_Id(order_Id, ref Pager);
        }

        public List<OrderItemInfo> Get_Orders_Item_By_Id(int order_Id)
        {
            return _ordersRepo.Get_Order_Items_By_Order_Id(order_Id);
        }

        public List<OrdersInfo> Get_Orders_Data_By_Dates(DateTime fromDate, DateTime toDate, ref PaginationInfo pager)
        {
            return _ordersRepo.Get_Orders_Data_By_Dates(fromDate, toDate,ref pager);
        }
        public List<OrdersInfo> Get_Orders_Data_By_Status(int Status, ref PaginationInfo pager)
        {
            return _ordersRepo.Get_Orders_Data_By_Status(Status,ref pager);
        }
        public DealerInfo Get_Dealer_Data_By_Id(int dealer_Id)
        {
            return _ordersRepo.Get_Dealer_Data(dealer_Id);
        }

        public void Update_Order_Status(OrdersInfo order)
            {
            _ordersRepo.Update_Order_Status(order);
            }

        public void Send_Order_Status_Notification(string email_Id,OrdersInfo order)
        {
            _ordersRepo.Send_Order_Status_Notification(email_Id,order);
        }
         
        public List<AutocompleteInfo> Get_Order_No_Autocomplete(string order_No)
        {
            return _ordersRepo.Get_Order_No_Autocomplete(order_No);
        }
    }
}
