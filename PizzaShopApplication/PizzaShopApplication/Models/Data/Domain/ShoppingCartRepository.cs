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
        // Ключ для кук.
        public const string CookieKey = "CartId";
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
        // Получение ключа из кук пользователя.
        public string GetCartId()
        {
            // В случае, если в куках бразуера пользователя еще не 
            // хранится уникальное значение корзины.
            if (!_httpContextAccessor.HttpContext.Request.Cookies.Keys.Contains(CookieKey))
            {
                // Генерация нового гуида.
                Guid tempCartId = Guid.NewGuid();
                _httpContextAccessor.HttpContext.Response.Cookies.Append(CookieKey, tempCartId.ToString());
                // При первом обращении от пользователя обьект HttpContext
                // еще не обновлен, соответственно кука будет нулевая.
                // Поэтому возвращаем только что сгенерированный гуид.
                return tempCartId.ToString();
            }
            string tempValue;
            _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(CookieKey, out tempValue);
            return tempValue;
        }
        // Получает список товаров в корзине пользователя.
        public List<Cart> GetCartItems()
        {
            ShoppingCartId = GetCartId();
            return dbContext.ShoppingCartItems.Where(c => c.UserId == ShoppingCartId).ToList();
        }
    }
}
