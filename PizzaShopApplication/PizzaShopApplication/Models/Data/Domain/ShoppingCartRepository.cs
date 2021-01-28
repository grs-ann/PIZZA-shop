using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PizzaShopApplication.Models.Data.Context;
using PizzaShopApplication.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShopApplication.Models.Data.Domain
{
    public class ShoppingCartRepository
    {
        // Контекст БД.
        private ApplicationDataContext dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ShoppingCartRepository(ApplicationDataContext dbContext, IHttpContextAccessor _httpContextAccessor)
        {
            this.dbContext = dbContext;
            this._httpContextAccessor = _httpContextAccessor;
        }
        
        public string ShoppingCartId { get; set; }
        // Ключ к сессии
        public const string CartSessionKey = "CartId";
        public void AddToCart(int id)
        {
            // Получение продукта из базы данных
            ShoppingCartId = GetCartId(_httpContextAccessor.HttpContext);
            //
            var cartItem = dbContext.ShoppingCartItems.SingleOrDefault(
                c => c.UserId == ShoppingCartId && c.ProductId == id);
            // Если такого товара еще нет в корзине, то создаем новый.
            if (cartItem == null)
            {
                cartItem = new Cart
                {
                    ItemId = Guid.NewGuid().ToString(),
                    ProductId = id,
                    UserId = ShoppingCartId,
                    Pizza = dbContext.Pizzas.SingleOrDefault(
                        p => p.Id == id),
                    Quantity = 1,
                    DateCreated = DateTime.Now
                };
                dbContext.ShoppingCartItems.Add(cartItem);
            }
            // В случае, если товар(в данном случае пицца)
            // уже есть в корзине, то увеличиваем количество 
            // товара в корзине.
            else
            {
                cartItem.Quantity++;
            }
            dbContext.SaveChanges();
        }
        public void Dispose()
        {
            if (dbContext != null)
            {
                dbContext.Dispose();
                dbContext = null;
            }
        }
        // Получение ключа из сессии пользователя.
        public string GetCartId(HttpContext context)
        {
            if (!context.Session.Keys.Contains(CartSessionKey))
            {
                // Генерация нового гуида.
                Guid tempCartId = Guid.NewGuid();
                context.Session.SetString(CartSessionKey, tempCartId.ToString());
            }
            return context.Session.Get(CartSessionKey).ToString();
        }
        // Получает список товаров в корзине пользователя.
        public List<Cart> GetCartItems()
        {
            ShoppingCartId = GetCartId(_httpContextAccessor.HttpContext);
            return dbContext.ShoppingCartItems.Where(c => c.UserId == ShoppingCartId).ToList();
        }
    }
}
