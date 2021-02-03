using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PizzaShopApplication.Models.Data.Entities;
using PizzaShopApplication.Models.Data.Entities.Authentification;
using PizzaShopApplication.Models.Entities;
using PizzaShopApplication.Models.Secondary;

namespace PizzaShopApplication.Models.Data.Context
{
    public class ApplicationDataContext : DbContext
    {
        IPasswordHasher ph;
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recept> Recepts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Cart> ShoppingCartItems { get; set; }
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options, IPasswordHasher ph) : base(options)
        {
            Database.EnsureCreated();
            this.ph = ph;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleName = "admin";
            string userRoleName = "user";
            string adminEmail = "admin@gmail.ru";
            string adminPassword = "5juceuebok5";
            // Добавление роли администратора, а также роли пользователя.
            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role userRole = new Role { Id = 2, Name = userRoleName };
            User adminUser = new User { Id = Guid.NewGuid(), Email = adminEmail, Password = ph.GenerateHash(adminPassword), RoleId = adminRole.Id };
            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            modelBuilder.Entity<User>().HasData(new User[] { adminUser });
            base.OnModelCreating(modelBuilder);
        }
    }
}
