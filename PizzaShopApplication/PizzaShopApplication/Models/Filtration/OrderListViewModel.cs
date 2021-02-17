using Microsoft.AspNetCore.Mvc.Rendering;
using PizzaShopApplication.Models.Data.Context;
using PizzaShopApplication.Models.Data.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShopApplication.Models.Filtration
{
    public class OrderListViewModel
    {
        public OrderListViewModel(List<Order> orders, int? orderStatusId, int? orderId, DateTime date, ApplicationDataContext dbContext)
        {
            Orders = orders;
            var orderStatuses = dbContext.OrderStatuses.ToList();
            orderStatuses.Insert(0, new OrderStatus { Id = 0, Status = "Все" });
            OrderStatuses = new SelectList(orderStatuses, "Id", "Status");
            OrderId = orderId;
            Date = date;
        }
        // Коллекция заказов, которую получит View.
        public IEnumerable<Order> Orders { get; set; }
        // Статусы заказов для фильтрации по ним.
        public SelectList OrderStatuses { get; set; }
        // Id заказа для быстрого поиска по нему.
        public int? OrderId { get; set; }
        // Дата для фильтрации по дате заказа(hh:mm:ss).
        public DateTime Date { get; set; }
    }
}
