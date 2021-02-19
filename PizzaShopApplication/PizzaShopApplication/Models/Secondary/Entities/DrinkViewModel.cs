using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShopApplication.Models.Secondary.Entities
{
    /// <summary>
    /// The purpose of this class is to pass drink data to the View.
    /// </summary>
    public class DrinkViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool Novelty { get; set; }
        public bool Bestseller { get; set; }
        public bool Discount { get; set; }
    }
}
