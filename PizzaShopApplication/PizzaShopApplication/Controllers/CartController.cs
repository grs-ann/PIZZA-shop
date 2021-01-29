using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PizzaShopApplication.Models.Data.Domain;

namespace PizzaShopApplication.Controllers
{
    public class CartController : Controller
    {
        private readonly ShoppingCartRepository cart;
        public CartController(ShoppingCartRepository cart)
        {
            this.cart = cart;
        }
        //[Route("Cart/AddItemToCart")]
        public IActionResult AddItemToCart(int itemId, string itemName)
        {
            cart.AddToCart(itemId);
            ViewBag.Name = itemName;
            return View();
        }
        //[Route("Cart/GetUserCartInfo")]
        public IActionResult GetUserCartInfo()
        {
            return Content("Это пользовательская корзина");
        }
    }
}
