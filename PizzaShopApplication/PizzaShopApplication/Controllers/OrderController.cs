using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShopApplication.Controllers
{
    public class OrderController : Controller
    {
        [HttpGet]
        public IActionResult GetOrders()
        {
            return Content("Заказы будут тут.");
        }
    }
}
