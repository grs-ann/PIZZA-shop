using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaShopApplication.Models.Data;
using PizzaShopApplication.Models.Data.Context;
using PizzaShopApplication.Models.Data.Domain;
using PizzaShopApplication.Models.Data.Entities.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShopApplication.Controllers
{
    [Authorize(Roles = "admin")]
    public class ProductController : Controller
    {
        private ApplicationDataContext dbContext;
        private readonly PizzaRepository pizzaRepository;
        private readonly IWebHostEnvironment appEnvironment;
        public ProductController(ApplicationDataContext dbContext, PizzaRepository pizzaRepository, IWebHostEnvironment appEnvironment)
        {
            this.dbContext = dbContext;
            this.pizzaRepository = pizzaRepository;
            this.appEnvironment = appEnvironment;
        }
        [HttpGet]
        
        public async Task<IActionResult> ChangeProducts()
        {
            var pizzas = await pizzaRepository.GetPizzasForEditAsync();
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
        public IActionResult AddProductToDB()
        {
            return View();
        }
        [HttpPost]
        // Добавление продукта в базу данных.
        public async Task<IActionResult> AddProductToDB(Pizza pizza, IFormFile uploadedFile)
        {
            if (ModelState.IsValid)
            {
                if (uploadedFile == null)
                {
                    ModelState.AddModelError("", "Не загружено изображение");
                    return View(pizza);
                }
                var image = await AddImageFile(uploadedFile);
                await pizzaRepository.AddNewPizzaAsync(pizza, image);
            }
            else
            {
                ModelState.AddModelError("", "некорректные данные");
                return View(pizza);
            }
            
            return RedirectPermanent("~/Product/ChangeProducts");
        }
        // Внесение изменений о товаре в базу данных.
        [HttpPost]
        public async Task<IActionResult> EditProduct(Pizza pizza, int itemId, IFormFile uploadedFile)
        {
            if (ModelState.IsValid)
            {
                await pizzaRepository.SaveChangesAsync(pizza, itemId, uploadedFile);
            }
            else
            {
                ModelState.AddModelError("", "некорректно введены данные");
                return View(pizza);
            }
            return RedirectPermanent("~/Product/ChangeProducts");
        }
        [HttpPost]
        // Удаление продукта из базы данных.
        public async Task<IActionResult> DeleteFromDB(int itemId)
        {
            await pizzaRepository.DeleteAsync(itemId);
            return RedirectPermanent("~/Product/ChangeProducts");
        }
        [HttpPost]
        // Добавление изображения товара в базу данных.
        public async Task<Image> AddImageFile(IFormFile uploadedFile)
        {
            return await pizzaRepository.AddImageFileAsync(uploadedFile);
        }
    }
}
