using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShopApplication.Models.Data.Entities.Data
{
    /// <summary>
    /// Represents database table,
    /// contained different property values 
    /// for products.
    /// </summary>
    public class ProductProperty
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int PropertyId { get; set; }
        public string Value { get; set; }
        // Link to Product table.
        public Product Product { get; set; }
        // Link to Property table.
        public Property Property { get; set; }
    }
}
