using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using IOT_Project_Api.DTO;
using IOT_Project_BLL.ShopCar;
using IOT_Project_Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IOT_Project_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ShopCarController : ControllerBase
    {
        CSRedis.CSRedisClient redis = null;
        private IGoodsList _goodsList;
        public ShopCarController(IGoodsList goodsList)
        {
            redis = new CSRedis.CSRedisClient("127.0.0.1:6379");

            RedisHelper.Initialization(redis);

            _goodsList = goodsList;
        }
        [HttpGet]
        public string AddCar(int goodId,int goodNum)
        {
            ProductInfo info = _goodsList.GetProductinfo(goodId);
            if (info.ProductAmount < goodNum)
            {
                return "库存不足";
            }
            info.ProductAmount = goodNum;

            List<ProductInfo> list = redis.Get<List<ProductInfo>>("shopcar");
            //商品购物车是不是空的
            if (list != null)
            {

                var m = list.Where(x => x.ProductId == goodId).FirstOrDefault();
                if (m != null)
                {
                    return "已加入该商品";
                }
                else
                {
                    list.Add(info);
                    //return "加入成功";
                }

            }
            else
            {
                list = new List<ProductInfo>();
                list.Add(info);
                
            }
            redis.Set("shopcar", list);
            return "加入成功";
        }
        //显示购物车
        [HttpGet]
        public IActionResult ShopCarShow()
        {
            if (redis.Get<List<ProductInfo>>("shopcar") == null)
            {
                return Ok("购物车空空如也");
            }
            return Ok(redis.Get<List<ProductInfo>>("shopcar"));
        }
        //public int ShopCarCount()
        //{
        //    return redis.Get<List<ProductInfo>>("shopcar").Count;
        //}

        //清空购物车
        [HttpGet]
        public int ClearCar(int userId,int totalcount,decimal price)
        {
            Random random = new Random();

            List<ProductInfo> list = redis.Get<List<ProductInfo>>("shopcar");

            OrderInfo order = new OrderInfo();
            order.OrderId = random.Next(1, 1000000).ToString();
            order.UserId = userId;
            order.Orderdate = DateTime.Now;
            order.Orderamount = totalcount;
            order.Message = "_";
            order.Postmethod = "随便";
            order.Paymethod = "支付宝";
            order.Recevername = "123";
            order.Receveraddr = "上海市奉贤区";
            order.Recevertel = "13994984291";
            order.Memo = "_";
            order.Totalprice = price;
            List<Orderdetail> orderdetails = new List<Orderdetail>();


            foreach (var item in list)
            {
                Orderdetail orderdetail = new Orderdetail();
                
                
                orderdetail.OrderDetailId = random.Next(1, 100000);
                orderdetail.OrderId = order.OrderId;
                orderdetail.ProductId = item.ProductId;
                orderdetail.Orderamount = item.ProductAmount;
                orderdetail.Poststatus = "未发货";
                orderdetail.Recevstatus = "未收货";
                orderdetail.Saletotalprice = (item.ProductDPrice * item.ProductAmount).ToString();
                orderdetails.Add(orderdetail);



            }


            _goodsList.UptKuCun(redis.Get<List<ProductInfo>>("shopcar"));

            

            if (_goodsList.AddOrder(order, orderdetails) > 0)
            {
                redis.Del("shopcar");
                return 1;
            }
            return 0;

        }

    }
}
