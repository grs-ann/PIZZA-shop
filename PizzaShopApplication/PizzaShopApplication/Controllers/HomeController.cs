using Microsoft.AspNetCore.Mvc;
using PizzaShopApplication.Models.Domain.Interfaces;
using PizzaShopApplication.Models.ProductModels;

namespace PizzaShopApplication.Controllers
{
    /// <summary>
    /// This controller used for products presentation for buyers.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly IProduct _showProductRepository;
        public HomeController(IProduct showProductRepository)
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