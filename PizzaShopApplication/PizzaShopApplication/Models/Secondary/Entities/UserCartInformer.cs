using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShopApplication.Models.Secondary.Entities
{
    public class UserCartInformer
    {
        public string PizzaName { get; set; }
        public decimal PizzaPrice { get; set; }
        public int PizzaCount { get; set; }
        public Guid PizzaId { get; set; }
    }
}
