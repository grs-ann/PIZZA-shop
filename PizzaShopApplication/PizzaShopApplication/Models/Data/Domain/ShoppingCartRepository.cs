using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PizzaShopApplication.Models.Data.Context;
using PizzaShopApplication.Models.Entities;
using PizzaShopApplication.Models.Secondary.Entities;
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
        private UserCartInformer userCartInformer;
        public ShoppingCartRepository(ApplicationDataContext dbContext,
            IHttpContextAccessor _httpContextAccessor, UserCartInformer userCartInformer)
        {
            this.dbContext = dbContext;
            this._httpContextAccessor = _httpContextAccessor;
            this.userCartInformer = userCartInformer;
        }
        // Ключ для кук.
        public const string CookieKey = "CartId";
        public void AddToCart(Guid id)
        {
            // Получение продукта из базы данных
            Guid ShoppingCartId = GetCartId();
            //
            var cartItem = dbContext.ShoppingCartItems.SingleOrDefault(
                c => c.UserId == ShoppingCartId && c.PizzaId == id);
            // Если такого товара еще нет в корзине, то создаем новый.
            if (cartItem == null)
            {
                cartItem = new Cart
                {
                    ItemId = Guid.NewGuid(),
                    PizzaId = id,
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
        public void DeleteFromCart(Guid id)
        {
            // Получение продукта из базы данных
            Guid ShoppingCartId = GetCartId();
            //
            var cartItem = dbContext.ShoppingCartItems.SingleOrDefault(
                c => c.UserId == ShoppingCartId && c.PizzaId == id);
            if (cartItem.Quantity > 0)
            {
                cartItem.Quantity--;
            }
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
        public Guid GetCartId()
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
                return tempCartId;
            }
            _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(CookieKey, out string tempValue);
            return new Guid(tempValue);
        }
        // Получает список товаров в корзине пользователя.
        public List<UserCartInformer> GetCartItems()
        {
            var cartInformer = new List<UserCartInformer>();
            Guid ShoppingCartId = GetCartId();
            var userCartItems = dbContext.ShoppingCartItems.Where(u => u.UserId == ShoppingCartId);
            var tempPizzas = dbContext.Pizzas;
            foreach (var cartItem in userCartItems)
            {
                // Наполняем вспомогательный объект 
                // информацией о выбранном товаре
                var tempoCart = new UserCartInformer();
                tempoCart.PizzaCount = cartItem.Quantity;
                tempoCart.PizzaName = tempPizzas.FirstOrDefault(p => p.Id == cartItem.PizzaId).Name;
                tempoCart.PizzaPrice = tempPizzas.FirstOrDefault(p => p.Id == cartItem.PizzaId).Price;
                cartInformer.Add(tempoCart);
            }
            return cartInformer;
        }

        
    }
}
