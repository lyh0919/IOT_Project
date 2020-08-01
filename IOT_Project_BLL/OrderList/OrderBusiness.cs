using IOT_Project_Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IOT_Project_BLL
{
    public class OrderBusiness : IOrderBusiness
    {
        public IOrderBusiness _Ib;

        public OrderBusiness(IOrderBusiness Ib)
        {
            _Ib = Ib;
        }
        public int DelOrderInfo(object id)
        {
           return _Ib.DelOrderInfo(id);
        }

        public List<Orderdetail> DetailShow()
        {
            return _Ib.DetailShow();
        }

        public List<OrderInfo> OrderShow(string OrderId, string ReceverName)
        {
            return _Ib.OrderShow(OrderId, ReceverName);
        }

        public List<OrderInfo> ShowOrder(int id)
        {
            return _Ib.ShowOrder(id);
        }

        public int UptOrderInfo(OrderInfo model)
        {
            return _Ib.UptOrderInfo(model);
        }
    }
}
