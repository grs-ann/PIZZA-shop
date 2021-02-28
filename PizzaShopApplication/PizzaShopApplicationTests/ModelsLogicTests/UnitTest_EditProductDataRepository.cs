using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using PizzaShopApplication.Models.Data.Entities.Products;
using PizzaShopApplication.Models.Domain;
using PizzaShopApplication.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PizzaShopApplicationTests.ModelsLogicTests
{
    [Collection("Database collection")]
    public class UnitTest_EditProductDataRepository
    {
        private readonly TestsFixture _fixture;
        private readonly EditProductDataRepository _editProductDataRepository;
        private readonly IWebHostEnvironment _appEnvironment;
        public UnitTest_EditProductDataRepository(TestsFixture fixture)
        {
            _fixture = fixture;
            //_appEnvironment = appEnvironment;
            _editProductDataRepository = new EditProductDataRepository(_fixture.db, _appEnvironment);
        }
        [Fact]
        public async void Test_AddNewPizzaAsync()
        {
            // Arrange
            var pizzaViewModel = new PizzaViewModel
            {
                Name = "Куриная",
                Bestseller = true,
                Discount = false,
                Novelty = false,
                Price = 400,
                PizzaIngridients = "Тесто, курица, шампиньоны"
            };
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
            Assert.True(addedPizza.Name == "Куриная");
        }
    }
}
