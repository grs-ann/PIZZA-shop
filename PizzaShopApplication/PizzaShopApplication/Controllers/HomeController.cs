using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly PizzaRepository pizzaRepository;
        public HomeController(PizzaRepository pizzaRepository)
        {
            this.pizzaRepository = pizzaRepository;
        }
        [HttpGet]
        [Route("/")]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> Index()
        {
            var pizzas = await pizzaRepository.GetPizzasAsync();
            return View(pizzas);
        }
    }
}