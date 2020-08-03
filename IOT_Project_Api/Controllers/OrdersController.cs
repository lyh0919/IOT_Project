using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IOT_Project_BLL;
using IOT_Project_Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IOT_Project_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IOrderBusiness _order;
        public OrdersController(IOrderBusiness order)
        {
            _order = order;
        }

        public List<OrderInfo> ShowOrder(int userId)
        {
            return _order.ShowOrder(userId);
        }

    }
}
