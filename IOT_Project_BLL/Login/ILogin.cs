using IOT_Project_Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IOT_Project_BLL.Login
{
   public interface ILogin
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        Customer ShowCustomer(Customer m);
        //用户信息添加
        int Add(Customer m);
        //用户信息修改
        int Update(Customer m);

        List<Customer> ShowCustome();
        //显示出所有的订单信息
        List<OrderInfo> ShowOrder();

        //查看订单详情

        List<Orderdetail> ShowOrderdetial();

        List<ProductInfo> ShowProductinfo();

        List<producttypeinfo> ShowProducttype();

        List<ProductImg> ShowPicture();
    }
}
