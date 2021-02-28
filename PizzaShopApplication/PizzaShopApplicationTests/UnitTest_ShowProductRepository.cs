using Microsoft.EntityFrameworkCore;
using PizzaShopApplication.Models.Data.Context;
using PizzaShopApplication.Models.Data.Entities.Products;
using PizzaShopApplication.Models.Domain;
using PizzaShopApplicationTests.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PizzaShopApplicationTests
{
    [Collection("Database collection")]
    public class UnitTest_ShowProductRepository
    {
        //ApplicationDataContext dbContext, IWebHostEnvironment appEnvironment
        private TestsFixture _fixture;
        private ShowProductRepository _showProductRepository { get; }
        public UnitTest_ShowProductRepository(TestsFixture fixture)
        {
            _fixture = fixture;
            _showProductRepository = new ShowProductRepository(fixture.db);
        }
        [Fact]
        public void Test_GetAllProductsFromDB()
        {
            // Arrange
            AddDataToDB(_fixture.db);
            // Act
            var allProducts = _showProductRepository.GetAllProductsFromDB();
            var imageName = allProducts.FirstOrDefault().Image.Name;
            // Assert
            Assert.NotNull(allProducts);
            Assert.NotEmpty(allProducts);
            Assert.Equal("cheese.png", imageName);
        }
        [Fact]
        public void Test_GetProductFromDB()
        {
            // Arrange
            AddDataToDB(_fixture.db);
            // Act
            var nullProductById = _showProductRepository.GetProductFromDB(10);
            var notNullProductById = _showProductRepository.GetProductFromDB(3);
            // Assert
            Assert.Null(nullProductById);
            Assert.NotNull(notNullProductById);
            Assert.True(notNullProductById.Name.Equals("BonAqua"));
        }
        [Fact]
        public void Test_GetAllPizzasFromDB()
        {
            // Arrange
            AddPizzasToDB(_fixture.db);
            // Act
            var allPizzas = _showProductRepository.GetAllPizzasFromDB();
            // Assert
            Assert.NotNull(allPizzas);
            Assert.NotEmpty(allPizzas);
            Assert.Null(allPizzas.FirstOrDefault(p => p.ProductType.Id == 2));
        }
        [Fact]
        public void Test_GetAllDrinksFromDB()
        {
            // Arrange
            AddDrinksToDB(_fixture.db);
            // Act
            var allDrinks = _showProductRepository.GetAllDrinksFromDB();
            // Assert
            Assert.NotNull(allDrinks);
            Assert.NotEmpty(allDrinks);
            Assert.NotNull(allDrinks.FirstOrDefault(p => p.ProductType.Id == 2));
            Assert.True(allDrinks.FirstOrDefault().ProductType.Name == "Напиток");
        }
        [Fact]
        public void Test_GetDrinkViewModel()
        {
            // Arrange
            AddDataToDB(_fixture.db);
            // Act
            var correctDrinkViewModel = _showProductRepository.GetDrinkViewModel(3);
            var notCorrectDrinkViewModel = _showProductRepository.GetDrinkViewModel(1);
            // Assert
            Assert.NotNull(correctDrinkViewModel);
            Assert.True(correctDrinkViewModel.Name == "BonAqua");
            Assert.Null(notCorrectDrinkViewModel);
        }
        [Fact]
        public void Test_GetPizzaViewModel()
        {
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
        private async void AddPizzasToDB(ApplicationDataContext dbContext)
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
                    });
            dbContext.ProductTypes.Add(
                    new ProductType
                    {
                        Name = "Пицца"
                    });
            await dbContext.SaveChangesAsync();
        }
        private async void AddDrinksToDB(ApplicationDataContext dbContext)
        {
            dbContext.Images.AddRange(
                    new Image
                    {
                        Name = "bonaqua.png",
                        Path = "/images/"
                    });
            dbContext.Products.AddRange(
                    new Product
                    {
                        Name = "BonAqua",
                        Price = 60,
                        Novelty = false,
                        Bestseller = false,
                        Discount = false,
                        ImageId = 1,
                        ProductTypeId = 2,
                    });
            dbContext.ProductTypes.AddRange(
                    new ProductType
                    {
                        Name = "Пицца"
                    },
                    new ProductType
                    {
                        Name = "Напиток"
                    });
            await dbContext.SaveChangesAsync();
        }

    }
}
