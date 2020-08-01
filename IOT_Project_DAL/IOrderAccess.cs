using IOT_Project_Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IOT_Project_DAL
{
    public interface IOrderAccess
    {
        /// <summary>
        /// 订单信息表
        /// </summary>
        /// <typeparam name="OrderInfo"></typeparam>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>

        List<OrderInfo>  OrderShow( string OrderId, string ReceverName);
        List<Orderdetail> DetailShow();
     
        int DelOrderInfo(object id);

        int UptOrderInfo(OrderInfo model);

        List<OrderInfo> ShowOrder(int id);

    }
}
