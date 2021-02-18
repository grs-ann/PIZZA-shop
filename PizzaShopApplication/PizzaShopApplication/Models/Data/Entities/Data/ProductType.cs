using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShopApplication.Models.Data.Entities.Data
{
    public class ProductType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
