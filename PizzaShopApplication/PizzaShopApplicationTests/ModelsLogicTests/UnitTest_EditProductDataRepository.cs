using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Moq;
using PizzaShopApplication.Models.Data.Context;
using PizzaShopApplication.Models.Data.Entities.Products;
using PizzaShopApplication.Models.Domain;
using PizzaShopApplication.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PizzaShopApplicationTests.ModelsLogicTests
{
    [Collection("Database collection")]
    public class UnitTest_EditProductDataRepository
    {
        private readonly TestsFixture _fixture;
        private readonly EditProductDataRepository _editProductDataRepository;
        public UnitTest_EditProductDataRepository(TestsFixture fixture)
        {
            _fixture = fixture;
            var appEnvMock = new Mock<IWebHostEnvironment>();
            _editProductDataRepository = new EditProductDataRepository(_fixture.db, appEnvMock.Object);
        }
        [Fact]
        public async void Test_AddNewPizzaAsync()
        {
            // Arrange
            var pizzaViewModel = GetPizzaViewModel();
            var image = new Image
            {
                Id = 1
            };
            // Act
            await _editProductDataRepository.AddNewPizzaAsync(pizzaViewModel, image);
            await _fixture.db.SaveChangesAsync();
            var addedPizza = await _fixture.db.Products.FirstOrDefaultAsync();
            // Assert
            Assert.NotNull(addedPizza);
            Assert.True(addedPizza.ImageId == 1);
            Assert.True(addedPizza.Name == "Куриная после изменения");
            await TestsFixture.ClearDatabase(_fixture.db);
        }
        [Fact]
        public async void Test_AddNewDrinkAsync()
        {
            // Arrange
            var drinkViewModel = GetDrinkViewModel();
            var image = new Image
            {
                Id = 2
            };
            // Act
            await _editProductDataRepository.AddNewDrinkAsync(drinkViewModel, image);
            await _fixture.db.SaveChangesAsync();
            var addedDrink = await _fixture.db.Products.FirstOrDefaultAsync(p => p.Name == "BonAqua");
            // Assert
            Assert.NotNull(addedDrink);
            Assert.True(addedDrink.ImageId == 2);
            Assert.True(addedDrink.Name == "BonAqua" && addedDrink.Price == 60);
            await TestsFixture.ClearDatabase(_fixture.db);
        }
        [Fact]
        public async void Test_EditPizzaAsync()
        {
            // Arrange
            await AddDataToDBAsync(_fixture.db);
            var pizzaViewModel = GetPizzaViewModel();
            // Act
            var pizzaUntilChange = await _fixture.db.Products.Include(i => i.Image).FirstOrDefaultAsync(p => p.Name == "Куриная");
            var pizzaPriceUntilChange = pizzaUntilChange.Price;
            await _editProductDataRepository.EditPizzaAsync(pizzaViewModel, null);
            var pizzaAfterChange = await _fixture.db.Products.FirstOrDefaultAsync(p => p.Name == "Куриная после изменения");
            // Assert
            Assert.NotNull(pizzaUntilChange);
            Assert.True(pizzaPriceUntilChange == 349);
            Assert.True(pizzaAfterChange.Price == 400);
            await TestsFixture.ClearDatabase(_fixture.db);
        }
        [Fact]
        public async void Test_DeleteProductAsync()
        {
            // Arrange
            await AddDataToDBAsync(_fixture.db);
            // Act
            var itemToDelete = await _fixture.db.Products.FirstOrDefaultAsync(p => p.Name == "BonAqua");
            var nameValueUntilDelete = itemToDelete.Name;
            await _editProductDataRepository.DeleteProductAsync(itemToDelete.Id);
            itemToDelete = await _fixture.db.Products.FirstOrDefaultAsync(p => p.Name == "BonAqua");
            // Assert
            Assert.True(nameValueUntilDelete == "BonAqua");
            Assert.Null(itemToDelete);
            await TestsFixture.ClearDatabase(_fixture.db);
        }
        private PizzaViewModel GetPizzaViewModel()
        {
            return new PizzaViewModel
            {
                Name = "Куриная после изменения",
                Bestseller = true,
                Discount = false,
                Novelty = false,
                Price = 400,
                PizzaIngridients = "Тесто, курица, шампиньоны",
                Id = 2
            };
        }
        private DrinkViewModel GetDrinkViewModel()
        {
            return new DrinkViewModel
            {
                Name = "BonAqua",
                Bestseller = true,
                Discount = false,
                Novelty = false,
                Price = 60
            };
        }
        private async Task AddDataToDBAsync(ApplicationDataContext dbContext)
        {
            if (!await dbContext.Images.AnyAsync())
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
            if (!await dbContext.Products.AnyAsync())
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
                if (!await dbContext.Properties.AnyAsync())
                {
                    dbContext.Properties.AddRange(
                        new Property
                        {
                            ProductTypeId = 1,
                            Name = "Ингредиенты",
                        });
                    dbContext.SaveChanges();
                }
                if (!await dbContext.ProductProperties.AnyAsync())
                {
                    dbContext.ProductProperties.AddRange(
                        new ProductProperty
                        {
                            Value = "белый соус, курица, лук, моцарелла, орегано, томаты",
                            ProductId = 2,
                            PropertyId = 1
                        },
                        new ProductProperty
                        {
                            Value = "базилик, дорблю, моцарелла, пармезан, сливочный сыр, сырный соус",
                            ProductId = 1,
                            PropertyId = 1
                        });
                    dbContext.SaveChanges();
                }
            }
            await dbContext.SaveChangesAsync();
        }
    }
}
