using Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuoChe.Models
{
    public class OrderInfo
    {
        public List<OrderEntity> OrderList;

        public OrderInfo(List<OrderEntity> orderlist)
        {
            OrderList = orderlist;
        }
    }
}