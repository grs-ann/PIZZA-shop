using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PizzaShopApplication.Models.Data.Domain;
using PizzaShopApplication.Models.Data;
using PizzaShopApplication.Models.Secondary.Entities;

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
        public IActionResult AddItemToCart(Guid itemId, string itemName)
        {
            cart.AddToCart(itemId);
            ViewBag.Name = itemName;
            return View();
        }
        //[Route("Cart/GetUserCartInfo")]
        public IActionResult GetUserCartInfo()
        {
            var cartItems = cart.GetCartItems();
            decimal totalSum = 0;
            foreach (var cartItem in cartItems)
            {
                var counter = cartItem.PizzaCount;
                while (counter > 0)
                {
                    totalSum += cartItem.PizzaPrice;
                    counter--;
                }
            }
            ViewBag.TotalSum = totalSum;
            return View(cartItems);
        }
    }
}
