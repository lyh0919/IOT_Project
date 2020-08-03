using IOT_Project_Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IOT_Project_BLL.ShopCar
{
    public interface IGoodsList
    {
        IEnumerable<ProductInfo> GetProductinfos();
        ProductInfo GetProductinfo(int productId);
        IEnumerable<ProductImg> GetProductImgs(int productId);


        int AddOrder(OrderInfo order,List<Orderdetail> orderdetails);
        int AddOrderOne(OrderInfo order, Orderdetail orderdetail);
        int UptKuCun(List<ProductInfo> kucun);
        int UptKuCunOne(int goodId, int buycount);
    }
}
