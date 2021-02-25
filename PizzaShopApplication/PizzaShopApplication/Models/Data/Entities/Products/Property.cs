using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShopApplication.Models.Data.Entities.Products
{
    /// <summary>
    /// Represents a database table,
    /// contained a different product
    /// properties.
    /// </summary>
    public class Property
    {
        public int Id { get; set; }
        public int ProductTypeId { get; set; }
        public string Name { get; set; }
        // Link to ProductProperty table(navigartion property).
        public List<ProductProperty> ProductProperty { get; set; }
    }
}
