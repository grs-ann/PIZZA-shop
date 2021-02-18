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
using PizzaShopApplication.Models.Secondary.Entities;

namespace PizzaShopApplication.Controllers
{
    /// <summary>
    /// This controller used for products presentation for buyers.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ShowProductRepository _showProductRepository;
        public HomeController(ShowProductRepository showProductRepository)
        {
            _showProductRepository = showProductRepository;
        }
        /// <summary>
        /// Collects different products to one collection and gets it to View.
        /// </summary>
        [HttpGet]
        public IActionResult Index()
        {
            var pizzas = _showProductRepository.GetAllPizzasFromDB();
            var drinks = _showProductRepository.GetAllDrinksFromDB();
            var productViewModel = new ProductViewModel(pizzas, drinks);
            return View(productViewModel);
        }
    }
}