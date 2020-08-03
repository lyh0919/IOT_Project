using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace IOT_Project_WebAdmin.Controllers
{
    public class DefaultController : Controller
    {
        //首页显示
        public IActionResult Index()
        {
            return View();
        }
        //详情页
        public IActionResult Show()
        {
            return View();
        }

        public IActionResult ShopCar()
        {
            return View();
        }
        public IActionResult BuyShop()
        {
            return View();
        }

        //订单
        public IActionResult Ding()
        {
            return View();
        }

        public IActionResult Xq()
        {
            return View();
        }
        
    }
}