using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace IOT_Project_WebAdmin.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        //登录页面
        public IActionResult ShopIndex()
        {
            return View();
        }


        //注册用户信息
        public IActionResult RegisterIndex()
        {
            return View();
        }
        //重置密码通过邮箱
        public IActionResult LookInfoPwd()
        {
            return View();
        }

        //修改用户信息
        public IActionResult UpdateRegister()
        {
            return View();
        }

        //购买商品记录
        public IActionResult BuyedProduct()
        {
            return View();
        }


        //查看到自己的相关信息
        public IActionResult LookAboutUserInfo()
        {
            return View();
        }


        //消费记录
        //public IActionResult ExpenceTracker()
        //{
        //    return View();
        //}

      
       

        //个人消费记录


    }
}
