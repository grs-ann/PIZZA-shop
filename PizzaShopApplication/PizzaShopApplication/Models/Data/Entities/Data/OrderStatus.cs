using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShopApplication.Models.Data.Entities.Data
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
