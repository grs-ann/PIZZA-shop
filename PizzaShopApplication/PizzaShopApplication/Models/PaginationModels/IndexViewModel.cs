using PizzaShopApplication.Models.Data.Entities.Order;
using PizzaShopApplication.Models.FiltrationModels;
using System.Collections.Generic;

namespace PizzaShopApplication.Models.PaginationModels
{
    public class IndexViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public OrderListViewModel FilterViewModel { get; set; }
    }
}
