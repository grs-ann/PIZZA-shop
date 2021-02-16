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
using PizzaShopApplication.Models.Pagination;

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
        public IActionResult Index(int page=1)
        {
            int pageSize = 1;
            var pizzas = pizzaRepository.GetProductsFromDB();
            var count = pizzas.Count();
            var items = pizzas.Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Pizzas = items
            };
            return View(viewModel);
        }
    }
}