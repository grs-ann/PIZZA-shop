using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShopApplication.Models.Data.Entities.Data
{
    public class ProductProperty
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int PropertyId { get; set; }
        public string Value { get; set; }
        // Ссылка на продукт.
        public Product Product { get; set; }
        // Ссылка на свойство.
        public Property Property { get; set; }
    }
}
