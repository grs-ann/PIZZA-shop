using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PizzaShopApplication.Models.Entities;

namespace PizzaShopApplication.Models.Data.Context
{
    public class ApplicationDataContext : DbContext
    {
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recept> Recepts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Cart> ShoppingCartItems { get; set; }
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
