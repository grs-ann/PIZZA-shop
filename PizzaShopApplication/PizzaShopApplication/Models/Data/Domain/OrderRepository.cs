using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PizzaShopApplication.Models.Data.Context;
using PizzaShopApplication.Models.Data.Domain.Interfaces;
using PizzaShopApplication.Models.Data.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShopApplication.Models.Data.Domain
{
    public class OrderRepository : IOrder
    {
        private readonly ApplicationDataContext _dbContext;
        private readonly IHttpContextAccessor _httpContext;
        private readonly ShoppingCartRepository _cartRepository;
        public OrderRepository(ApplicationDataContext dbContext,
            IHttpContextAccessor httpContext, ShoppingCartRepository cartRepository)
        {
            _dbContext = dbContext;
            _httpContext = httpContext;
            _cartRepository = cartRepository;
        }
        public IEnumerable<Order> GetOrders()
        {
            if (_httpContext.HttpContext.User.IsInRole("admin"))
            {
                return _dbContext.Orders.Include(o => o.OrderStatus);
            }
            else
            {
                return _dbContext.Orders.Include(o => o.OrderStatus).Where(o => o.OrderDateTime >= DateTime.UtcNow.AddDays(-7));
            }
        }
        // Добавляет заказ в базу данных.
        public async Task AddOrderToDBAsync(Order order)
        {
            // Устанавливает для заказа статус - в процессе доставки.
            order.OrderStatusId = 1;
            order.OrderDateTime = DateTime.UtcNow;
            var cartGuid = _httpContext.HttpContext.Request.Cookies["CartId"];
            order.UserCartForeignKey = Guid.Parse(cartGuid);
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();
        }
        // Получает заказ, сохраненный в базе данных по обьекту.
        public async Task<Order> GetOrderFromDBAsync(Order order)
        {
            order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.OrderDateTime == order.OrderDateTime && o.Phone == order.Phone);
            return order;
        }
        // Получает заказ, сохраненный в базе данных по Id.
        public async Task<Order> GetOrderFromDBAsync(int orderId)
        {
            return await _dbContext.Orders.Include(o => o.OrderStatus).FirstOrDefaultAsync(o => o.Id == orderId);
        }
        // Находит в базе данных все продукты(пиццы) по id заказа.
        public IEnumerable<Cart> GetConcreteCartFromOrder(string cartId)
        {
            var conreteCart = _cartRepository.GetConcreteCartAsync(cartId);
            return conreteCart;
        }
        public IEnumerable<OrderStatus> GetOrderStatuses()
        {
            return _dbContext.OrderStatuses;
        }

        public async Task ChangeOrderStatusAsync(int orderId, int orderStatusId)
        {
            var orderToChange = _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == orderId).Result;
            orderToChange.OrderStatusId = orderStatusId;
            await _dbContext.SaveChangesAsync();
        }
    }
}
