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

        //public void Insert_Orders(OrdersInfo orders)
        //{
        //    _ordersRepo.Insert_Orders(orders);
        //}

        //public void Update_Orders(OrdersInfo orders)
        //{
        //    _ordersRepo.Update_Orders(orders);
        //}

        public List<OrdersInfo> Get_Orders(ref PaginationInfo Pager)
        {
            return _ordersRepo.Get_Orders(ref Pager);
        }

        public OrdersInfo Get_Orders_By_Id(int Order_Id)
        {
            ProductManager _productManager= new ProductManager();
            OrdersInfo Order = _ordersRepo.Get_Orders_By_Id(Order_Id);
            foreach (OrderItemInfo OrderItemInfo in Order.OrderItems)
            {
                OrderItemInfo.Product = _productManager.Get_Product_By_Id(OrderItemInfo.Product_Id);
            }
            return Order;
        }
         
    }
}
