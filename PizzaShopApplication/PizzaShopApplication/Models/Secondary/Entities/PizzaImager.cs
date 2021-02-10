using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShopApplication.Models.Secondary.Entities
{
    public class PizzaImager
    {
        public int PizzaId { get; set; }
        public string PizzaName { get; set; }
        public decimal PizzaPrice { get; set; }
        // Изображение пиццы
        public string ImagePath { get; set; }
    }
}
