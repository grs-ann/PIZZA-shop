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
        public async Task AddToCartAsync(int id)
        {
            // Получение продукта из базы данных
            Guid ShoppingCartId = GetCartId();
            var cartItem = await _dbContext.Carts.SingleOrDefaultAsync(
                c => c.UserId == ShoppingCartId && c.ProductId == id);
            // Если такого товара еще нет в корзине, то создаем новый.
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
            // В случае, если товар(в данном случае пицца)
            // уже есть в корзине, то увеличиваем количество 
            // товара в корзине.
            else
            {
                cartItem.Quantity++;
            }
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteFromCartAsync(int id)
        {
            // Получение продукта из базы данных
            Guid ShoppingCartId = GetCartId();
            //
            var cartItem = await _dbContext.Carts.SingleOrDefaultAsync(
                c => c.UserId == ShoppingCartId && c.ProductId == id);
            
            if (cartItem.Quantity > 1)
            {
                cartItem.Quantity--;
            }
            // В случае, если количество товара в корзине
            // меньше одного - удаляем запись из БД.
            else
            {
                _dbContext.Remove(cartItem);
            }
            await _dbContext.SaveChangesAsync();
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
        public async Task<List<UserCartInformer>> GetCartItemsAsync()
        {
            var cartInformer = new List<UserCartInformer>();
            Guid ShoppingCartId = GetCartId();
            var userCartItems = _dbContext.Carts.Where(u => u.UserId == ShoppingCartId);
            var tempPizzas = await _dbContext.Products.ToListAsync();
            foreach (var cartItem in userCartItems)
            {
                // Наполняем вспомогательный объект 
                // информацией о выбранном товаре
                var tempoCart = new UserCartInformer();
                tempoCart.PizzaCount = cartItem.Quantity;
                tempoCart.PizzaName = tempPizzas.FirstOrDefault(p => p.Id == cartItem.ProductId).Name;
                tempoCart.PizzaPrice = tempPizzas.FirstOrDefault(p => p.Id == cartItem.ProductId).Price;
                tempoCart.PizzaId = tempPizzas.FirstOrDefault(p => p.Id == cartItem.ProductId).Id;
                tempoCart.ShoppingCartId = ShoppingCartId.ToString();
                cartInformer.Add(tempoCart);
            }
            return cartInformer;
        }
    }
}
