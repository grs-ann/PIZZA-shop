using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PizzaShopApplication.Models.Data.Context;
using PizzaShopApplication.Models.Domain.Interfaces;
using PizzaShopApplication.Models.Data.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShopApplication.Models.Domain
{
    /// <summary>
    /// This class represents the ability to use 
    /// CRUD opertaions in "Orders" table in DB.
    /// </summary>
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
        /// <summary>
        /// Gets filtered order list.
        /// </summary>
        /// <param name="orderStatusId">Order status Id.</param>
        /// <param name="orderId">Order Id.</param>
        /// <param name="date">DateTime for filtering by it. </param>
        /// <returns></returns>
        public List<Order> GetOrdersWithFiltration(int? orderStatusId, int? orderId, DateTime date)
        {
            var orders = _dbContext.Orders.Include(o => o.OrderStatus).ToListAsync().Result;
            if (orderStatusId != null && orderStatusId != 0)
            {
                orders = orders.Where(o => o.OrderStatusId == orderStatusId).ToList();
            }
            if (orderId != null && orderId != 0)
            {
                orders = orders.Where(o => o.Id == orderId).ToList();
            }
            // If DateTime field in View is not filled,
            // datetime must be equal DateTime.MinValue.
            if (date != null && date != DateTime.MinValue)
            {
                orders = orders.Where(o => o.OrderDateTime.Date == date.Date).ToList();
            }
            return orders;
        }
        /// <summary>
        /// Adds order to database "Orders" table.
        /// </summary>
        /// <param name="order">User order data.</param>
        /// <returns></returns>
        public async Task AddOrderToDBAsync(Order order)
        {
            // By default, order status is equal "In delivery process".
            order.OrderStatusId = 1;
            order.OrderDateTime = DateTime.UtcNow;
            var cartGuid = _httpContext.HttpContext.Request.Cookies["CartId"];
            order.UserCartForeignKey = Guid.Parse(cartGuid);
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();
        }
        /// <summary>
        /// Gets a order contains in database.
        /// </summary>
        /// <param name="order">User order data.</param>
        /// <returns></returns>
        public async Task<Order> GetOrderFromDBAsync(Order order)
        {
            order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.OrderDateTime == order.OrderDateTime && o.Phone == order.Phone);
            return order;
        }
        /// <summary>
        /// Gets order, contains in database by his Id.
        /// </summary>
        /// <param name="orderId">Order Id</param>
        public async Task<Order> GetOrderFromDBAsync(int orderId)
        {
            return await _dbContext.Orders.Include(o => o.OrderStatus).FirstOrDefaultAsync(o => o.Id == orderId);
        }
        /// <summary>
        /// Gets all products from user cart Id.
        /// </summary>
        /// <param name="cartId">User cart Id.</param>
        /// <returns></returns>
        public IEnumerable<Cart> GetConcreteCartFromOrder(string cartId)
        {
            var conreteCart = _cartRepository.GetConcreteCartAsync(cartId);
            return conreteCart;
        }
        /// <summary>
        /// Gets all order statuses from database "OrderStatuses" table.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OrderStatus> GetOrderStatuses()
        {
            return _dbContext.OrderStatuses;
        }
        /// <summary>
        ///  Changes order status.
        /// </summary>
        /// <param name="orderId">Order Id.</param>
        /// <param name="orderStatusId">Order status Id.</param>
        /// <returns></returns>
        public async Task ChangeOrderStatusAsync(int orderId, int orderStatusId)
        {
            var orderToChange = _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == orderId).Result;
            orderToChange.OrderStatusId = orderStatusId;
            await _dbContext.SaveChangesAsync();
        }
    }
}
