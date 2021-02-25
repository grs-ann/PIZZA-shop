using Microsoft.EntityFrameworkCore;
using PizzaShopApplication.Models.Data.Entities.Authentification;
using PizzaShopApplication.Models.Data.Entities.Order;
using PizzaShopApplication.Models.Data.Entities.Products;
using PizzaShopApplication.Models.Data.Entities.Review;
using PizzaShopApplication.Models.Secondary;

namespace PizzaShopApplication.Models.Data.Context
{
    public class ApplicationDataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductProperty> ProductProperties { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Review> Reviews { get; set; }
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

            string adminLogin = "oldsarehere";
            string dispatcherLogin = "dispatcher";

            string adminName = "Администратор";
            string dispatcherName = "Диспетчер";
            // Добавляем роли.
            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role userRole = new Role { Id = 2, Name = userRoleName };
            Role dispatcherRole = new Role { Id = 3, Name = "dispatcher" };
            using (PasswordHasher ph = new PasswordHasher())
            {
                User adminUser = new User
                {
                    Id = 1,
                    Email = adminEmail,
                    Login = adminLogin,
                    Name = adminName,
                    Password = ph.GenerateHash(adminPassword),
                    RoleId = adminRole.Id,
                };
                User dispatcherUser = new User
                {
                    Id = 2,
                    Email = "gera@mail.ru",
                    Login = dispatcherLogin,
                    Name = dispatcherName,
                    Password = ph.GenerateHash("qwerty"),
                    RoleId = dispatcherRole.Id
                };
                modelBuilder.Entity<User>().HasData(new User[] { adminUser, dispatcherUser });
            }
            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole, dispatcherRole });
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
