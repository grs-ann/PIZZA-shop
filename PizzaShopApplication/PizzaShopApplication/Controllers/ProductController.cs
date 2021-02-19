using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaShopApplication.Models.Data;
using PizzaShopApplication.Models.Data.Context;
using PizzaShopApplication.Models.Data.Domain;
using PizzaShopApplication.Models.Data.Domain.Interfaces;
using PizzaShopApplication.Models.Data.Entities.Data;
using PizzaShopApplication.Models.Secondary.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShopApplication.Controllers
{
    /// <summary>
    /// This controller allows possibility to use CRUD operations
    /// with Products.
    /// </summary>
    [Authorize(Roles = "admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDataContext _dbContext;
        private readonly IProduct _showProductRepository;
        private readonly EditProductDataRepository _editPizzaRepository;
        private readonly IWebHostEnvironment _appEnvironment;
        public ProductController(ApplicationDataContext dbContext, IProduct showProductRepository, 
            IWebHostEnvironment appEnvironment, EditProductDataRepository editPizzaRepository)
        {
            _dbContext = dbContext;
            _showProductRepository = showProductRepository;
            _editPizzaRepository = editPizzaRepository;
            _appEnvironment = appEnvironment;
        }
        /// <summary>
        /// Gets all products to change from database and
        /// displays it in a View.
        /// </summary>
        /// <returns>All products stored in database</returns>
        [HttpGet]
        public IActionResult GetProductsToChange()
        {
            var products = _showProductRepository.GetAllProductsFromDB();
            return View(products);
        }
        /// <summary>
        /// Edits product in database by his <c>Id</c>
        /// </summary>
        /// <param name="itemId">Product Id in database table</param>
        /// <returns>Product</returns>
        [HttpGet]
        public IActionResult EditProduct(int itemId)
        {
            var product = _showProductRepository.GetProductFromDB(itemId);
            return View(product);
        }
        /// <summary>
        /// Fetchs a pizza from the database, populate 
        /// the helper class with data, and pass it to View. 
        /// </summary>
        /// <param name="itemId">Pizza Id in database table.</param>
        /// <returns>PizzaViewModel</returns>
        [HttpGet]
        public IActionResult EditPizza(int itemId)
        {
            var product = _showProductRepository.GetPizzaViewModel(itemId);
            return View(product);
        }
        /// <summary>
        /// Edits product with pizza type in database table.
        /// </summary>
        /// <param name="itemId">Product Id in database table.</param>
        [HttpPost]
        public async Task<IActionResult> EditPizza(PizzaViewModel pizzaModel, IFormFile uploadedFile)
        {
            if (ModelState.IsValid)
            {
                await _editPizzaRepository.EditPizzaAsync(pizzaModel, uploadedFile);
            }
            else
            {
                ModelState.AddModelError("", "некорректно введены данные");
                return View(pizzaModel);
            }
            return RedirectPermanent("~/Product/GetProductsToChange");
        }
        /// <summary>
        /// Fetchs a drink from the database, populate 
        /// the helper class with data, and pass it to View. 
        /// </summary>
        /// <param name="itemId">Drink Id in database table.</param>
        /// <returns>DrinkViewModel</returns>
        [HttpGet]
        public IActionResult EditDrink(int itemId)
        {
            var product = _showProductRepository.GetDrinkViewModel(itemId);
            return View(product);
        }
        /// <summary>
        /// Edits product with drink type in database table.
        /// </summary>
        /// <param name="itemId">Product Id in database table.</param>
        [HttpPost]
        public async Task<IActionResult> EditDrink(DrinkViewModel drinkModel, IFormFile uploadedFile)
        {
            // Почему то приходит продукт без вложенного свойства ProductProperties!!!
            if (ModelState.IsValid)
            {
                await _editPizzaRepository.EditDrinkAsync(drinkModel, uploadedFile);
            }
            else
            {
                ModelState.AddModelError("", "некорректно введены данные");
                return View(drinkModel);
            }
            return RedirectPermanent("~/Product/GetProductsToChange");
        }
        /// <summary>
        /// Returns a View, containing a form to adds new pizza.
        /// </summary>
        [HttpGet]
        public IActionResult AddPizza()
        {
            return View();
        }
        /// <summary>
        /// Adds a new pizza to database table.
        /// </summary>
        /// <param name="viewModel">View model, containing pizza data.</param>
        /// <param name="uploadedFile">Uploaded image.</param>
        [HttpPost]
        public async Task<IActionResult> AddPizza(PizzaViewModel viewModel, IFormFile uploadedFile)
        {
            if (ModelState.IsValid)
            {
                if (uploadedFile == null)
                {
                    ModelState.AddModelError("", "Не загружено изображение");
                    return View(viewModel);
                }
                var image = await AddImageFile(uploadedFile);
                await _editPizzaRepository.AddNewPizzaAsync(viewModel, image);
            }
            else
            {
                ModelState.AddModelError("", "некорректные данные");
                return View(viewModel);
            }
            return RedirectPermanent("~/Product/GetProductsToChange");
        }
        /// <summary>
        /// Returns a View, containing a form to adds new drink.
        /// </summary>
        [HttpGet]
        public IActionResult AddDrink()
        {
            return View();
        }
        /// <summary>
        /// Adds a new drink to database table.
        /// </summary>
        /// <param name="viewModel">View model, containing drink data.</param>
        /// <param name="uploadedFile">Uploaded image.</param>
        [HttpPost]
        public async Task<IActionResult> AddDrink(DrinkViewModel viewModel, IFormFile uploadedFile)
        {
            if (ModelState.IsValid)
            {
                if (uploadedFile == null)
                {
                    ModelState.AddModelError("", "Не загружено изображение");
                    return View(viewModel);
                }
                var image = await AddImageFile(uploadedFile);
                await _editPizzaRepository.AddNewDrinkAsync(viewModel, image);
            }
            else
            {
                ModelState.AddModelError("", "некорректные данные");
                return View(viewModel);
            }
            return RedirectPermanent("~/Product/GetProductsToChange");
        }
        /// <summary>
        /// Deletes product from database table.
        /// </summary>
        /// <param name="itemId">Product Id in database table.</param>
        [HttpPost]
        public async Task<IActionResult> DeleteFromDB(int itemId)
        {
            await _editPizzaRepository.DeleteProductAsync(itemId);
            return RedirectPermanent("~/Product/GetProductsToChange");
        }
        /// <summary>
        /// Adds uploaded image to database table.
        /// </summary>
        /// <param name="uploadedFile">Uploaded image.</param>
        [HttpPost]
        public async Task<Image> AddImageFile(IFormFile uploadedFile)
        {
            return await _editPizzaRepository.AddImageFileAsync(uploadedFile);
        }
    }
}
