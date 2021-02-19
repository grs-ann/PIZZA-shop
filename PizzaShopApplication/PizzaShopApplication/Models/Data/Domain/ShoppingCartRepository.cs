using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PizzaShopApplication.Models.Data.Context;
using PizzaShopApplication.Models.Data.Entities.Data;
using PizzaShopApplication.Models.Secondary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShopApplication.Models.Data.Domain
{
    /// <summary>
    /// This class represents the ability to use 
    /// CRUD opertaions in "Carts" table in DB.
    /// </summary>
    public class ShoppingCartRepository
    {
        private ApplicationDataContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ShoppingCartRepository(ApplicationDataContext dbContext,
            IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }
        /// <summary>
        /// Cookie key for search user cart
        /// </summary>
        public const string CookieKey = "CartId";
        /// <summary>
        /// Gets user cart by cart GUID.
        /// </summary>
        /// <param name="id">Represents CartId(GUID), coming from Request.</param>
        /// <returns>User cart</returns>
        public IEnumerable<Cart> GetConcreteCartAsync(string id)
        {
            return _dbContext.Carts.Include(s => s.Product).Where(s => s.UserId.ToString() == id);
        }
        /// <summary>
        /// Adds product to user cart.
        /// </summary>
        /// <param name="id">Product Id in database.</param>
        public async Task AddToCartAsync(int id)
        {
            Guid ShoppingCartId = GetCartId();
            // Getting product from DB by his Id.
            var cartItem = await _dbContext.Carts.SingleOrDefaultAsync(
                c => c.UserId == ShoppingCartId && c.ProductId == id);
            // If product not in cart yet, adds a new product to cart.
            if (cartItem == null)
            {
                cartItem = new Cart
                {
                    ItemId = Guid.NewGuid(),
                    ProductId = id,
                    UserId = ShoppingCartId,
                    Product = await _dbContext.Products.SingleOrDefaultAsync(
                        p => p.Id == id),
                    Quantity = 1,
                    DateCreated = DateTime.Now
                };
                await _dbContext.Carts.AddAsync(cartItem);
            }
            // If the product is already in the cart,
            // just increases thit product count.
            else
            {
                cartItem.Quantity++;
            }
            await _dbContext.SaveChangesAsync();
        }
        /// <summary>
        /// Deletes product from user cart.
        /// </summary>
        /// <param name="id">Product Id in database.</param>
        /// <returns></returns>
        public async Task DeleteFromCartAsync(int id)
        {
            Guid ShoppingCartId = GetCartId();
            // Gets a product by his id.
            var cartItem = await _dbContext.Carts.SingleOrDefaultAsync(
                c => c.UserId == ShoppingCartId && c.ProductId == id);
            if (cartItem.Quantity > 1)
            {
                cartItem.Quantity--;
            }
            // If product count < 1, delete this product
            // from DB table "Carts".
            else
            {
                _dbContext.Remove(cartItem);
            }
            await _dbContext.SaveChangesAsync();
        }
        /// <summary>
        /// Gets key, represented user cart guid from user Cookies.
        /// </summary>
        /// <returns>User cart guid</returns>
        public Guid GetCartId()
        {
            // If the unique value of the shopping cart is not 
            // yet stored in the user's browser cookies. 
            if (!_httpContextAccessor.HttpContext.Request.Cookies.Keys.Contains(CookieKey))
            {
                // New Guid generating.
                Guid tempCartId = Guid.NewGuid();
                _httpContextAccessor.HttpContext.Response.Cookies.Append(CookieKey, tempCartId.ToString());
                // At the first request from the user, the HttpContext object has
                // not yet been updated, so the cookie will be zero.
                // Therefore, we return the newly generated guide. 
                return tempCartId;
            }
            _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(CookieKey, out string tempValue);
            return new Guid(tempValue);
        }
        /// <summary>
        /// Gets user cart.
        /// </summary>
        /// <returns>User cart collection</returns>
        public IEnumerable<Cart> GetCartItems()
        {
            Guid ShoppingCartId = GetCartId();
            var userCartItems = _dbContext.Carts.Include(c => c.Product).Where(u => u.UserId == ShoppingCartId);
            return userCartItems;
        }
    }
}
