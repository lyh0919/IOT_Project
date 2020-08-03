using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using IOT_Project_Api.DTO;
using IOT_Project_BLL.Login;
using IOT_Project_BLL.ShopCar;
using IOT_Project_Common;
using IOT_Project_Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IOT_Project_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ShopCarController : ControllerBase
    {
        CSRedis.CSRedisClient redis = null;
        private IGoodsList _goodsList;
        private ILogin _loginBll;
        public ShopCarController(IGoodsList goodsList, ILogin loginBll)
        {
            redis = new CSRedis.CSRedisClient("127.0.0.1:6379");

            RedisHelper.Initialization(redis);

            _goodsList = goodsList;
            _loginBll = loginBll;
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
            redis.Expire("shopcar", 3600);
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
        [HttpGet]
        public int DeleteShopCarOne(int goodId)
        {
            List<ProductInfo> list = redis.Get<List<ProductInfo>>("shopcar");

            if (list.Remove(list.Where(s => s.ProductId == goodId).FirstOrDefault()))
            {
                return 1;
            }
            else
            {
                return 0;
            }
            
        }

        //清空购物车
        [HttpGet]
        public int ClearCar(string token,int totalcount,decimal price)
        {
            int userId = 0;
            JWTHelper jWT = new JWTHelper();
            string json = jWT.GetPayload(token);
            Customer model = JsonConvert.DeserializeObject<Customer>(json);
            if (model != null)
            {
                 userId = model.UserId;
            }
            else
            {
                return 0;
            }


            Random random = new Random();

            List<ProductInfo> list = redis.Get<List<ProductInfo>>("shopcar");

            OrderInfo order = new OrderInfo();
            order.OrderId = random.Next(1, 1000000).ToString();
            order.UserId = userId;
            order.Orderdate = DateTime.Now;
            order.Orderamount = totalcount;
            order.Message = "很满意";
            order.Postmethod = "中通";
            order.Paymethod = "支付宝";
            order.Recevername = model.UserName;
            order.Receveraddr = model.UserAddress;
            order.Recevertel = model.UserTel;
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
        [HttpGet]
        public void ClearShopCar()
        {
            if (redis.Get<List<ProductInfo>>("shopcar") != null)
            {
                redis.Del("shopcar");
            }
            
        }
        [HttpGet]
        public int BuyShop(string token,int goodId,int buycount)
        {
            int userId = 0;
            JWTHelper jWT = new JWTHelper();
            string json = jWT.GetPayload(token);
            Customer model = JsonConvert.DeserializeObject<Customer>(json);
            if (model != null)
            {
                userId = model.UserId;
            }
            else
            {
                return 0;
            }


            Random random = new Random();

            ProductInfo list = _goodsList.GetProductinfo(goodId);

            OrderInfo order = new OrderInfo();
            order.OrderId = random.Next(1, 1000000).ToString();
            order.UserId = userId;
            order.Orderdate = DateTime.Now;
            order.Orderamount = buycount;
            order.Message = "很满意";
            order.Postmethod = "中通";
            order.Paymethod = "支付宝";
            order.Recevername = model.UserName;
            order.Receveraddr = model.UserAddress;
            order.Recevertel = model.UserTel;
            order.Memo = "_";
            order.Totalprice = list.ProductDPrice * buycount;
            
            Orderdetail orderdetail = new Orderdetail();
            orderdetail.OrderDetailId = random.Next(1, 100000);
            orderdetail.OrderId = order.OrderId;
            orderdetail.ProductId = list.ProductId;
            orderdetail.Orderamount = buycount;
            orderdetail.Poststatus = "未发货";
            orderdetail.Recevstatus = "未收货";
            orderdetail.Saletotalprice = (list.ProductDPrice * list.ProductAmount).ToString();
                



            

            _goodsList.UptKuCunOne(goodId,buycount);

            if (_goodsList.AddOrderOne(order, orderdetail) > 0)
            {
                return 1;
            }
            return 0;
        }

    }
}
