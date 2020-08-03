using IOT_Project_DAL;
using IOT_Project_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IOT_Project_BLL.ShopCar
{
    public class GoodsList : IGoodsList
    {
        private IDataAccess<ProductInfo> _data;
        private IDataAccess<ProductImg> _dataimg;
        public GoodsList(IDataAccess<ProductInfo> data, IDataAccess<ProductImg> dataimg)
        {
            _data = data;
            _dataimg = dataimg;
        }
        //商品列表
        public IEnumerable<ProductInfo> GetProductinfos()
        {
            return _data.ShowAll();
        }
        //一条商品
        public ProductInfo GetProductinfo(int productId)
        {
            return _data.ShowAll().Where(p => p.ProductId == productId).FirstOrDefault();
        }

        public IEnumerable<ProductImg> GetProductImgs(int productId)
        {
            return _dataimg.ShowAll(productId);
        }

        public int AddOrder(OrderInfo order, List<Orderdetail> orderdetails)
        {
            string sql = $"insert into OrderInfo values('{order.OrderId}','{order.UserId}','{order.Orderdate}','{order.Orderamount}','{order.Message}','{order.Postmethod}','{order.Paymethod}','{order.Recevername}','{order.Receveraddr}','{order.Recevertel}','{order.Memo}','{order.Totalprice}') ";
            foreach (var item in orderdetails)
            {
                sql += $"insert into Orderdetail values('{item.OrderDetailId}','{item.OrderId}','{item.ProductId}','{item.Orderamount}','{item.Poststatus}','{item.Recevstatus}','{item.Saletotalprice}') ";
            }

            return _data.AddOrder(sql);
        }

        public int AddOrderOne(OrderInfo order, Orderdetail orderdetail)
        {
            string sql = $"insert into OrderInfo values('{order.OrderId}','{order.UserId}','{order.Orderdate}','{order.Orderamount}','{order.Message}','{order.Postmethod}','{order.Paymethod}','{order.Recevername}','{order.Receveraddr}','{order.Recevertel}','{order.Memo}','{order.Totalprice}') ";
            
            sql += $"insert into Orderdetail values('{orderdetail.OrderDetailId}','{orderdetail.OrderId}','{orderdetail.ProductId}','{orderdetail.Orderamount}','{orderdetail.Poststatus}','{orderdetail.Recevstatus}','{orderdetail.Saletotalprice}') ";
            

            return _data.AddOrder(sql);
        }

        public int UptKuCun(List<ProductInfo> kucun)
        {
            string sql = "";
            foreach (var item in kucun)
            {
                sql += $"update ProductInfo set ProductAmount = ProductAmount - {item.ProductAmount} where ProductId = {item.ProductId} ";
            }
            return _data.UptKuCun(sql);
        }
        public int UptKuCunOne(int goodId,int buycount)
        {
            string sql = "";
            
            sql += $"update ProductInfo set ProductAmount = ProductAmount - {buycount} where ProductId = {goodId} ";
            
            return _data.UptKuCun(sql);
        }
    }
}
