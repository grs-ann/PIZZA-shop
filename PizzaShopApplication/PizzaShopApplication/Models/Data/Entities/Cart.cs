using PizzaShopApplication.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShopApplication.Models.Entities
{
    public class Cart
    {
        [Key]
        public string ItemId { get; set; }
        // Id пользователя, связанного с приобретаемым элементом.
        // Будет храниться как переменная сеанса.
        public string UserId { get; set; }
        public int Quantity { get; set; }
        public DateTime DateCreated { get; set; }
        public int ProductId { get; set; }
        public virtual Pizza Pizza{ get; set; }
    }
}
