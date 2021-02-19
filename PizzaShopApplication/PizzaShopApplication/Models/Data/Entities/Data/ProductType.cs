using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShopApplication.Models.Data.Entities.Data
{
    /// <summary>
    /// Represents table, contained
    /// different product types.
    /// For example, pizza, drink, etc. 
    /// </summary>
    public class ProductType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
