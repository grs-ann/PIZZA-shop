using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaShopApplication.Models.Data;
using PizzaShopApplication.Models.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShopApplication.Controllers
{
    [Authorize(Roles = "admin")]
    public class ProductController : Controller
    {
        private readonly PizzaRepository pizzaRepository;
        public ProductController(PizzaRepository pizzaRepository)
        {
            this.pizzaRepository = pizzaRepository;
        }
        [HttpGet]
        
        public async Task<IActionResult> ChangeProducts()
        {
            var pizzas = await pizzaRepository.GetPizzasAsync();
            return View(pizzas);
        }
        [HttpGet]
        public async Task<IActionResult> EditProduct(int itemId)
        {
            var pizza = await pizzaRepository.GetPizzaAsync(itemId);
            return View(pizza);
        }
        [HttpGet]
        // Добавление продукта.
        public async Task<IActionResult> AddProduct()
        {
            return View();
        }
        [HttpPost]
        // Добавление продукта в базу данных.
        public async Task<IActionResult> AddProductToDB(Pizza pizza)
        {
            await pizzaRepository.AddNewPizzaAsync(pizza);
            return RedirectPermanent("~/Product/ChangeProducts");

        }
        // Внесение изменений о товаре в базу данных.
        [HttpPost]
        public async Task<IActionResult> SaveChangesInDB(Pizza pizza, int itemId)
        {
            await pizzaRepository.SaveChangesAsync(pizza, itemId);
            return RedirectPermanent("~/Product/ChangeProducts");
        }
        [HttpPost]
        // Удаление продукта из базы данных.
        public async Task<IActionResult> DeleteFromDB(int itemId)
        {
            await pizzaRepository.DeleteAsync(itemId);
            return RedirectPermanent("~/Product/ChangeProducts");
        }
    }
}
