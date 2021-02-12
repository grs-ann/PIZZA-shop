using PizzaShopApplication.Models.Data.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShopApplication.Models.Data.Domain.Interfaces
{
    public interface IOrder
    {
        public Task AddOrderToDBAsync(Order order);
        public Task<Order> GetOrderFromDBAsync(Order order);
        public Task<Order> GetOrderFromDBAsync(int orderId);
        public Task ChangeOrderStatusAsync(int order, int orderStatusId);
        public IEnumerable<Order> GetOrders();
        public IEnumerable<Cart> GetConcreteCartFromOrder(string id);
        public IEnumerable<OrderStatus> GetOrderStatuses();

    }
}
