using PizzaShopApplication.Models.Data;
using PizzaShopApplication.Models.Data.Domain.Interfaces;
using PizzaShopApplication.Models.Data.Entities.Data;
using PizzaShopApplication.Models.Filtration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShopApplication.Models.Pagination
{
    public class IndexViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public OrderListViewModel FilterViewModel { get; set; }
    }
}
