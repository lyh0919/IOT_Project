using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IOT_Project_BLL.ShopCar;
using IOT_Project_DAL;
using IOT_Project_Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IOT_Project_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        private IGoodsList _goodsList;
        public DefaultController(IGoodsList goodsList)
        {
            _goodsList = goodsList;
        }

        
    }
}
