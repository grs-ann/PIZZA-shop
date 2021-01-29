using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PizzaShopApplication.Models.Data.Domain;

namespace PizzaShopApplication.Controllers
{
    public class CartConroller : Controller
    {
        private readonly ShoppingCartRepository cart;
        public CartConroller(ShoppingCartRepository cart)
        {
            this.cart = cart;
        }
        [Route("Cart/AddItemToCart")]
        public IActionResult AddItemToCart(int itemId)
        {
            cart.AddToCart(itemId);
            return Content("добавлено");
        }
    }
}
