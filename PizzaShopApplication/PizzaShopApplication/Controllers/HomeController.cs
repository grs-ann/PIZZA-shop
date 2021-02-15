using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PizzaShopApplication.Models;
using PizzaShopApplication.Models.Data;
using PizzaShopApplication.Models.Data.Context;
using PizzaShopApplication.Models.Data.Domain;

namespace PizzaShopApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ShowPizzaRepository pizzaRepository;
        public HomeController(ShowPizzaRepository pizzaRepository)
        {
            this.pizzaRepository = pizzaRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var pizzas = pizzaRepository.GetProductsFromDB();
            return View(pizzas);
        }
    }
}