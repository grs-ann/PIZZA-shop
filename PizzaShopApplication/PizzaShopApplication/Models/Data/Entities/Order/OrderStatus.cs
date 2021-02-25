using System.ComponentModel.DataAnnotations;

namespace PizzaShopApplication.Models.Data.Entities.Order
{
    /// <summary>
    /// Represents database table, contained
    /// different order statuses.
    /// </summary>
    public class OrderStatus
    {
        [Key]
        public int Id { get; set; }
        public bool InProcess { get; set; }
        public bool Cancelled { get; set; }
        public string Status { get; set; }
    }
}
