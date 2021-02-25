using PizzaShopApplication.Models.Data.Entities.Order;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaShopApplication.Models.Domain.Interfaces
{
    /// <summary>
    /// This interface defines base methods, which
    /// that should be in class-inheritor for
    /// work with contained in database table.
    /// </summary>
    public interface IOrder
    {
        public Task AddOrderToDBAsync(Order order);
        public Task<Order> GetOrderFromDBAsync(Order order);
        public Task<Order> GetOrderFromDBAsync(int orderId);
        public Task ChangeOrderStatusAsync(int order, int orderStatusId);
        public List<Order> GetOrdersWithFiltration(int? orderStatusId, int? orderId, DateTime date);
        public IEnumerable<Cart> GetConcreteCartFromOrder(string id);
        public IEnumerable<OrderStatus> GetOrderStatuses();
    }
}
