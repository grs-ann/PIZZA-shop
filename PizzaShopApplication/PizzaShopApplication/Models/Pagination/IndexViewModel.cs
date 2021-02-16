using PizzaShopApplication.Models.Data;
using PizzaShopApplication.Models.Data.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShopApplication.Models.Pagination
{
    public class IndexViewModel
    {
        public IEnumerable<Pizza> Pizzas { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
