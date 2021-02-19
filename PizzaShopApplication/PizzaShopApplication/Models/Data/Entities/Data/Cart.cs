using PizzaShopApplication.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShopApplication.Models.Data.Entities.Data
{
    /// <summary>
    /// Represents user cart entity.
    /// </summary>
    public class Cart
    {
        [Key]
        public Guid ItemId { get; set; }
        // Unique user Id, stored in cookies.
        public Guid UserId { get; set; }
        public int Quantity { get; set; }
        public DateTime DateCreated { get; set; }
        public int ProductId { get; set; }
        public Product Product{ get; set; }
    }
}
