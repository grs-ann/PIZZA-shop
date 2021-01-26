using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PizzaShopApplication.Models;
using PizzaShopApplication.Models.Data;
using PizzaShopApplication.Models.Data.Context;

namespace PizzaShopApplication.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        ApplicationDataContext Context { get; }
        public HomeController(ApplicationDataContext context)
        {
            Context = context;
        }
        public IActionResult Index()
        {
            var pizzas = Context.Pizzas.ToList();
            return View(pizzas);
        }
    }
}
