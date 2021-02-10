using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PizzaShopApplication.Models.Data.Entities;
using PizzaShopApplication.Models.Data.Entities.Authentification;
using PizzaShopApplication.Models.Data.Entities.Data;
using PizzaShopApplication.Models.Entities;
using PizzaShopApplication.Models.Secondary;

namespace PizzaShopApplication.Models.Data.Context
{
    public class ApplicationDataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recept> Recepts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Cart> ShoppingCartItems { get; set; }
        public DbSet<Image> Images { get; set; }
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleName = "admin";
            string userRoleName = "user";

            string adminEmail = "42ama@gmail.com";
            string adminPassword = "123456";
            // Добавляем роли.
            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role userRole = new Role { Id = 2, Name = userRoleName };
            using (PasswordHasher ph = new PasswordHasher())
            {
                User adminUser = new User { Id = 1, Email = adminEmail, Password = ph.GenerateHash(adminPassword), RoleId = adminRole.Id };
                modelBuilder.Entity<User>().HasData(new User[] { adminUser });
            }
            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
