﻿using System;
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
        //评论
        public IActionResult Show()
        {
            return View();
        }

    }
}