using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShopApplication.Models.Data.Entities.Data
{
    public class Property
    {
        public int Id { get; set; }
        public int ProductTypeId { get; set; }
        public string Name { get; set; }
        // Связь с таблицей ProductProperty(навигационное свойство).
        public List<ProductProperty> ProductProperty { get; set; }
    }
}
