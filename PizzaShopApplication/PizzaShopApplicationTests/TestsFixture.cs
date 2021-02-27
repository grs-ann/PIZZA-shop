using Microsoft.EntityFrameworkCore;
using PizzaShopApplication.Models.Data.Context;
using PizzaShopApplicationTests.Extensions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace PizzaShopApplicationTests
{
    public class TestsFixture : IDisposable
    {
        public ApplicationDataContext db;
        public TestsFixture()
        {

            var options = new DbContextOptionsBuilder<ApplicationDataContext>()
                .UseInMemoryDatabase(databaseName: "CursedTestingDB")
                .Options;
            db = new ApplicationDataContext(options);
        }
        public async void Dispose()
        {
            await db.Database.EnsureDeletedAsync();
        }
        public static async Task ClearDatabase(ApplicationDataContext context)
        {
            await context.Products.ClearIfAny();
            await context.Carts.ClearIfAny();
            await context.Images.ClearIfAny();
            await context.Orders.ClearIfAny();
            await context.OrderStatuses.ClearIfAny();
            await context.ProductProperties.ClearIfAny();
            await context.Products.ClearIfAny();
            await context.ProductTypes.ClearIfAny();
            await context.Properties.ClearIfAny();
            await context.Reviews.ClearIfAny();
            await context.Roles.ClearIfAny();
            await context.Users.ClearIfAny();

            await context.SaveChangesAsync();
        }
    }
    [CollectionDefinition("Database collection")]
    public class DatabaseCollection : ICollectionFixture<TestsFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
