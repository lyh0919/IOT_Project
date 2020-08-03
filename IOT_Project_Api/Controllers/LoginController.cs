using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IOT_Project_BLL.Login;
using IOT_Project_Common;
using IOT_Project_Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IOT_Project_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILogin _loginBll;
        public LoginController(ILogin loginBll)
        {
            this._loginBll = loginBll;

        }

        //dengluchaxun

        [HttpPost]
        public async Task<string> LoginCustomer(Customer obj)
        {
            string token = "";
            Md5Helper md = new Md5Helper();
            obj.UserPwd = md.ToMd5(obj.UserPwd);
            Customer model= _loginBll.ShowCustomer(obj);
            if (model != null)
            {
            JWTHelper jwt = new JWTHelper();
            Dictionary<string, object> pairs = new Dictionary<string, object>();
            pairs.Add("UserId", model.UserId);
            pairs.Add("UserName", model.UserName);
            pairs.Add("UserPwd", model.UserPwd);
             token= jwt.GetToken(pairs,30000);
            return await Task.Run(()=> { return token; });
            }
            else
            {
                return null;
            }
           
        }

        //查看顾客的相关信息 自己的相关信息
        public List<Customer> ShowCustromer(string token="")
        {
            JWTHelper jWT = new JWTHelper();
            string json = jWT.GetPayload(token);
            Customer model = JsonConvert.DeserializeObject<Customer>(json);
            List<Customer> list = null;
            if (model != null)
            {
                list = _loginBll.ShowCustome();
                list = list.Where(s => s.UserName.Equals(model.UserName)).ToList();
                return list;
            }
            else
            {
                return null;
            }
        }
        [HttpPost]       
        public int RegisterUser([FromBody]Customer m)
        {
            
                List<Customer> list = _loginBll.ShowCustome().ToList();
                list = list.Where(s => s.UserName.Equals(m.UserName)).ToList();
                if (list.Count!=0)
                {
                    var name = "";
                for (int i = 0; i < list.Count; i++)
                {
                    name = list[i].UserName;
                }
                    if (m.UserName == name)
                    {
                        return -1;//用户名相同
                    }
                else
                {
                    return 0;
                }    
                }
              else
                {
                    Md5Helper md = new Md5Helper();
                    m.UserPwd = md.ToMd5(m.UserPwd);
                    m.UserEmail = m.UserEmail + "@qq.com";
                    m.UserLevel = "1";
                    int flag = _loginBll.Add(m);
                    return flag;
                }
                
               
                                
        }

        /// <summary>
        /// 查询个人的详细信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public List<Customer> DetialCustomer(string token)
        {
            JWTHelper jWT = new JWTHelper();
            string json = jWT.GetPayload(token);
            Customer model = JsonConvert.DeserializeObject<Customer>(json);
            if (model!=null)
            {
                List<Customer> list = _loginBll.ShowCustome();
                list = list.Where(s => s.UserId.Equals(model.UserId)).ToList();
                return list;
            }
            else
            {
                return null;
            }

        }


        /// <summary>
        /// 修改个人信息
        /// </summary>
        /// <param name="m"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]

        public int UpdateUser([FromBody]Customer m,string token)
        {
            JWTHelper jWT = new JWTHelper();
            string json = jWT.GetPayload(token);
            Customer model = JsonConvert.DeserializeObject<Customer>(json);
            if (model != null)
            {
                Md5Helper md = new Md5Helper();
                m.UserPwd = md.ToMd5(m.UserPwd);
                m.UserId = model.UserId;//修改 勿忘修改哪条数据
                int flag = _loginBll.Update(m);
                return flag;
            }
            else
            {
                return -1;
            }
        }



        //找回密码通过邮箱发送验证码
        public string LookUpPwd(string email)
        {
            List<Customer> list = _loginBll.ShowCustome().ToList();
            list = list.Where(s => s.UserEmail.Equals(email)).ToList();
            var em = "";
            var pwd = "";
            for (int i = 0; i < list.Count; i++)
            {
                em = list[i].UserEmail;
                pwd = list[i].UserPwd;
            }
            List<string> FromMial = new List<string>();
            FromMial.Add("2424940988@qq.com");
            List<string> ToMial = new List<string>();
            ToMial.Add(em);
            EmailHelper emhelper = new EmailHelper(FromMial,"找回的密码",pwd,ToMial);
            emhelper.Send();
            return "成功";
            
        }


        //显示订单
        [HttpGet]
        public List<OrderInfo> ShowOrder()
        {
            List<OrderInfo> list = _loginBll.ShowOrder();
            return list;
        }

        [HttpGet]
        public List<Orderdetail> ShowDetial()
        {
            List<Orderdetail> list = _loginBll.ShowOrderdetial();
            return list;
        }

        [HttpGet]
        public List<ProductImg> ShowPicture()
        {
            List<ProductImg> list = _loginBll.ShowPicture();
            return list;
        }

        [HttpGet]
        public List<ProductInfo> ShowProductInfo()
        {
            List<ProductInfo> list = _loginBll.ShowProductinfo();
            return list;
        }

        [HttpGet]
        public List<producttypeinfo> ShowProducttype()
        {
            List<producttypeinfo> list = _loginBll.ShowProducttype();
          
            return list;
        }

        //个人订单通过名字进行查询并显示
        [HttpGet]
        public List<CustomerAndOrder> ShowCustomerAndOrder(string token)
        {
        JWTHelper jWT = new JWTHelper();
        string json = jWT.GetPayload(token);
        Customer model = JsonConvert.DeserializeObject<Customer>(json);
        if (model != null)
        {
            var slist = (from s in ShowCustromer(token)
                         join a in ShowOrder()
                         on s.UserId equals a.UserId
                         select new CustomerAndOrder
                         {
                             UserName=s.UserName,
                             UserTel=s.UserTel,
                             UserAddress=s.UserAddress,
                             Orderdate=a.Orderdate,
                             Orderamount=a.Orderamount,
                             Message=a.Message,
                             Postmethod=a.Postmethod

                         }).ToList();

            slist = slist.Where(s=>s.UserName.Contains(model.UserName)).ToList();
            return slist;
            }
            else
            {
                return null;
            }
        }
  
        //查询出会员信息表中的订单及订单详情
        public List<CustAndOrderAndPro> ShowAll(string token)
        {
            if (token!=null)
            {
                JWTHelper jWT = new JWTHelper();
                string json = jWT.GetPayload(token);
                Customer model = JsonConvert.DeserializeObject<Customer>(json);
                if (model != null)
                {

                    List<CustAndOrderAndPro> list = (from s in ShowCustromer(token)
                                                     join a in ShowOrder()
                                                     on s.UserId equals a.UserId
                                                     join b in ShowDetial()
                                                     on a.OrderId equals b.OrderId
                                                     join c in ShowProductInfo()
                                                     on b.ProductId equals c.ProductId
                                                     join d in ShowProducttype() on
                                                     c.ProductTypeId equals d.ProductTypeId
                                                     join e in ShowPicture() on
                                                     c.ProductId equals e.ProId
                                                     select new CustAndOrderAndPro
                                                     {
                                                         Uid = s.UserId,
                                                         UserName = s.UserName,
                                                         UserTel = s.UserTel,
                                                         UserAddress = s.UserAddress,
                                                         Orderdate = a.Orderdate,
                                                         Orderamount = a.Orderamount,
                                                         OrderId = a.OrderId,
                                                         Message = a.Message,
                                                         Paymethod = a.Paymethod,
                                                         Recevername = a.Recevername,
                                                         Recevertel = a.Recevertel,
                                                         Totalprice = a.Totalprice,
                                                         Postmethod = a.Postmethod,
                                                         Receveraddr = a.Receveraddr,
                                                         Memo = a.Memo,
                                                         Poststatus = b.Poststatus,
                                                         Saletotalprice = b.Saletotalprice,
                                                         ProductName = c.ProductName,
                                                         Storage = c.Storage,
                                                         ProductPrice = c.ProductPrice,
                                                         ProductDealamount = c.ProductDealamount,
                                                         ProductLookamount = c.ProductLookamount,
                                                         ProductStoretime = c.ProductStoretime,
                                                         ProductTypeName = d.ProductTypeName,
                                                         Picture=e.Picture
                                                     }).ToList();
                    return list;
                }
                else
                {
                    return null;
                         
                         
                }
            }
          else{
                return null;
            }

        }






     
    }
  public  class CustomerAndOrder
    {
        public int Uid { get; set; }
        public string UserName { get; set; }
        
        public string UserTel { get; set; }
        public string UserBirthday { get; set; }
        public string UserLevel { get; set; }
        public string UserAddress { get; set; }


        //订单表中的数据
        public int Id { get; set; }
        public string OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime Orderdate { get; set; }
        public int Orderamount { get; set; }
        public string Message { get; set; }
        public string Postmethod { get; set; }
        public string Paymethod { get; set; }
        public string Recevername { get; set; }
        public string Receveraddr { get; set; }
        public string Recevertel { get; set; }
        public string Memo { get; set; }
        public string Totalprice { get; set; }
    }



    //查询出所有的数据
    public class CustAndOrderAndPro
    {
        //会员信息表中的数据
        public int Uid { get; set; }
        public string UserName { get; set; }

        public string UserTel { get; set; }
        public string UserBirthday { get; set; }
        public string UserLevel { get; set; }
        public string UserAddress { get; set; }


        //订单表中的数据
        public int Id { get; set; }
        public string OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime Orderdate { get; set; }
        public int Orderamount { get; set; }
        public string Message { get; set; }
        public string Postmethod { get; set; }
        public string Paymethod { get; set; }
        public string Recevername { get; set; }
        public string Receveraddr { get; set; }
        public string Recevertel { get; set; }
        public string Memo { get; set; }
        public decimal Totalprice { get; set; }

        //订单详情
        public int Odid{ get; set; }
        public int OrderDetailId { get; set; }
        public string FOrderId { get; set; }  //外键Id
        public int FProductId { get; set; }
    
        public string Poststatus { get; set; }
        public string Recevstatus { get; set; }
        public string Saletotalprice { get; set; }

        //商品表中的数据
        public int ProductId { get; set; }
        public int FProductTypeId { get; set; }
        public string ProductName { get; set; }
        public int Storage { get; set; }
        public int ProductAmount { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductDPrice { get; set; }
        public decimal ProductDiscount { get; set; }
        public int ProductDealamount { get; set; }
        public string ProductOutline { get; set; }
        public DateTime ProductStoretime { get; set; }
        public int ProductLookamount { get; set; }

        public int ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }

        public string Picture { get; set; }

    }
}