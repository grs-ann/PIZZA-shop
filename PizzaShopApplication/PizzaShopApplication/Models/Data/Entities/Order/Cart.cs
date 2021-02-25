using PizzaShopApplication.Models.Data.Entities.Products;
using System;
using System.ComponentModel.DataAnnotations;

namespace PizzaShopApplication.Models.Data.Entities.Order
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
        public Product Product { get; set; }
    }
}
