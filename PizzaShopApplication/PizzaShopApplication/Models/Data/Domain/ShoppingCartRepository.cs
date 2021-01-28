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
        HttpContext httpContext;
        public ShoppingCartRepository(ApplicationDataContext dbContext, HttpContext httpContext)
        {
            this.dbContext = dbContext;
            this.httpContext = httpContext;
        }
        // Контекст БД.
        private ApplicationDataContext dbContext;
        public string ShoppingCartId { get; set; }
        // Ключ к сессии
        public const string CartSessionKey = "CartId";
        public void AddToCart(int id)
        {
            // Получение продукта из базы данных
            ShoppingCartId = GetCartId();
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
        public string GetCartId()
        {
            if (!httpContext.Session.Keys.Contains(CartSessionKey))
            {
                // Генерация нового гуида.
                Guid tempCartId = Guid.NewGuid();
                httpContext.Session.SetString(CartSessionKey, tempCartId.ToString());
            }
            return httpContext.Session.Get(CartSessionKey).ToString();
        }
        // Получает список товаров в корзине пользователя.
        public List<Cart> GetCartItems()
        {
            ShoppingCartId = GetCartId();
            return dbContext.ShoppingCartItems.Where(c => c.UserId == ShoppingCartId).ToList();
        }
    }
}
