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

namespace PizzaShopApplicationTests.ModelsLogicTests
{
    [Collection("Database collection")]
    public class UnitTest_ShowProductRepository
    {
        private readonly TestsFixture _fixture;
        private readonly ShowProductRepository _showProductRepository;
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
            _fixture.Dispose();
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
            _fixture.Dispose();
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
            _fixture.Dispose();
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
            Assert.True(allDrinks.FirstOrDefault().ProductType.Name == "�������");
            _fixture.Dispose();
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
            _fixture.Dispose();
        }
        [Fact]
        public void Test_GetPizzaViewModel()
        {
            // Arrange
            AddDataToDB(_fixture.db);
            // Act
            var correctPizzaViewModel = _showProductRepository.GetPizzaViewModel(1);
            var notCorrectPizzaViewModel = _showProductRepository.GetPizzaViewModel(3);
            // Assert
            Assert.NotNull(correctPizzaViewModel);
            Assert.True(correctPizzaViewModel.Name == "4 ����");
            Assert.Null(notCorrectPizzaViewModel);
            _fixture.Dispose();
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
                        Name = "4 ����",
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
                        Name = "�������",
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
                        Name = "4 ����",
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
                        Name = "�������",
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
                        Name = "�����"
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
                        Name = "�����"
                    },
                    new ProductType
                    {
                        Name = "�������"
                    });
            await dbContext.SaveChangesAsync();
        }

    }
}
