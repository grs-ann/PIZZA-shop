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
        private readonly ApplicationDataContext _dbContext;
        private readonly ShowPizzaRepository _showPizzaRepository;
        private readonly EditPizzaDataRepository _editPizzaRepository;
        private readonly IWebHostEnvironment _appEnvironment;
        public ProductController(ApplicationDataContext dbContext, ShowPizzaRepository showPizzaRepository, 
            IWebHostEnvironment appEnvironment, EditPizzaDataRepository editPizzaRepository)
        {
            _dbContext = dbContext;
            _showPizzaRepository = showPizzaRepository;
            _editPizzaRepository = editPizzaRepository;
            _appEnvironment = appEnvironment;
        }
        [HttpGet]
        
        public IActionResult ChangeProducts()
        {
            var pizzas = _showPizzaRepository.GetProductsFromDB();
            return View(pizzas);
        }
        [HttpGet]
        public async Task<IActionResult> EditProduct(int itemId)
        {
            var pizza = await _showPizzaRepository.GetProductFromDBAsync(itemId);
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
                await _editPizzaRepository.AddNewPizzaAsync(pizza, image);
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
                await _editPizzaRepository.EditPizzaAsync(pizza, itemId, uploadedFile);
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
            await _editPizzaRepository.DeletePizzaAsync(itemId);
            return RedirectPermanent("~/Product/ChangeProducts");
        }
        [HttpPost]
        // Добавление изображения товара в базу данных.
        public async Task<Image> AddImageFile(IFormFile uploadedFile)
        {
            return await _editPizzaRepository.AddImageFileAsync(uploadedFile);
        }
    }
}
