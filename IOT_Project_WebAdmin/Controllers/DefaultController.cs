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
        public IActionResult Xiang()
        {
            return View();
        }


        //朱佳龙的订单页
        public IActionResult Ding()
        {
            return View();
        }
        //朱佳龙的订单详情页
        public IActionResult Xq()
        {
            return View();
        }

    }
}