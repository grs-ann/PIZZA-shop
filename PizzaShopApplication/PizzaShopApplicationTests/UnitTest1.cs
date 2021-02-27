using Microsoft.EntityFrameworkCore;
using PizzaShopApplication.Models.Data.Context;
using PizzaShopApplication.Models.Data.Entities.Products;
using PizzaShopApplication.Models.Domain;
using PizzaShopApplicationTests.Extensions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace PizzaShopApplicationTests
{
    [Collection("Database collection")]
    public class UnitTest1
    {
        //ApplicationDataContext dbContext, IWebHostEnvironment appEnvironment
        private TestsFixture _fixture;
        private ShowProductRepository _showProductRepository { get; }
        public UnitTest1(TestsFixture fixture)
        {
            _fixture = fixture;
            _showProductRepository = new ShowProductRepository(fixture.db);
        }
        [Fact]
        public void Test1()
        {
            // Arrange
            AddDataToDB(_fixture.db);
            // Act
            var allProducts = _showProductRepository.GetAllProductsFromDB();
            // Assert
            Assert.NotNull(allProducts);
            Assert.NotEmpty(allProducts);
        }
        private async void AddDataToDB(ApplicationDataContext dbContext)
        {
            if (! await dbContext.Images.AnyAsync())
            {
                dbContext.Images.AddRange(
                    new Image
                    {
                        Name = "cheese.png",
                        Path = "/images/"
                    },
                    new Image
                    {
                        Name = "chicken.png",
                        Path = "/images/"
                    },
                    new Image
                    {
                        Name = "bonaqua.png",
                        Path = "/images/"
                    });
            }
            if (! await dbContext.Products.AnyAsync())
            {
                dbContext.Products.AddRange(
                    new Product
                    {
                        Name = "4 сыра",
                        Price = 389,
                        Novelty = false,
                        Bestseller = false,
                        Discount = false,
                        ImageId = 1,
                        ProductTypeId = 1,
                        ProductPropertyId = 1
                    },
                    new Product
                    {
                        Name = "Куриная",
                        Price = 349,
                        Novelty = false,
                        Bestseller = false,
                        Discount = false,
                        ImageId = 2,
                        ProductTypeId = 1,
                        ProductPropertyId = 1
                    },
                    new Product
                    {
                        Name = "BonAqua",
                        Price = 70,
                        Novelty = false,
                        Bestseller = false,
                        Discount = false,
                        ImageId = 3,
                        ProductTypeId = 2
                    });
            }
            await dbContext.SaveChangesAsync();
        }

    }
}
