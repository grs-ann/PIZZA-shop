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
    /// <summary>
    /// This controller provides the ability to manage a basket of orders. 
    /// </summary>
    public class CartController : Controller
    {
        private readonly ShoppingCartRepository cart;
        public CartController(ShoppingCartRepository cart)
        {
            this.cart = cart;
        }
        /// <summary>
        /// Adds a new product to cart
        /// </summary>
        /// <param name="itemId">Product Id in database table named "Products"</param>
        /// <param name="itemName">Product name</param>
        public async Task<IActionResult> AddItemToCart(int itemId, string itemName)
        {
            await cart.AddToCartAsync(itemId);
            ViewBag.Name = itemName;
            return View();
        }
        /// <summary>
        /// Add another product with <c>itemId</c>
        /// </summary>
        /// <param name="itemId">Product Id in database table named "Products"</param>
        public async Task<IActionResult> AddItemToCartChangeEvent(int itemId)
        {
            await cart.AddToCartAsync(itemId);
            return RedirectPermanent("~/Cart/GetUserCartInfo");
        }
        /// <summary>
        /// Deletes item from cart
        /// </summary>
        /// <param name="itemId">Product Id in database table named "Products"</param>
        public async Task<IActionResult> DeleteItemFromCart(int itemId)
        {
            await cart.DeleteFromCartAsync(itemId);
            return RedirectPermanent("~/Cart/GetUserCartInfo");
        }
        /// <summary>
        /// Gets user's cart with all ordered products.
        /// </summary>
        public async Task<IActionResult> GetUserCartInfo()
        {
            var cartItems = await cart.GetCartItemsAsync();
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
