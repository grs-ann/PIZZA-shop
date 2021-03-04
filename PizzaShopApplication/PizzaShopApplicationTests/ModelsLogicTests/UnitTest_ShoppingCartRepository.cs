using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using PizzaShopApplication.Models.Data.Context;
using PizzaShopApplication.Models.Data.Entities.Order;
using PizzaShopApplication.Models.Data.Entities.Products;
using PizzaShopApplication.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PizzaShopApplicationTests.ModelsLogicTests
{
    [Collection("Database collection")]
    public class UnitTest_ShoppingCartRepository
    {
        private readonly TestsFixture _fixture;
        private readonly ShoppingCartRepository _shoppingCartRepository;
        private const string CookieKey = "CartId";
        // UserId from cookies.
        private Guid cartGuid = Guid.NewGuid();
        public UnitTest_ShoppingCartRepository(TestsFixture fixture)
        {
            _fixture = fixture;
            var IHttpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _shoppingCartRepository = new ShoppingCartRepository(_fixture.db, IHttpContextAccessorMock.Object);
        }
        [Fact]
        public async void Test_GetConcreteCartAsync()
        {
            // Arrange
            var guid = Guid.NewGuid();
            await AddUserCartToDB(guid);
            // Act
            var concreteUserCart = _shoppingCartRepository.GetConcreteCartAsync(guid.ToString());
            // Assert
            Assert.NotNull(concreteUserCart);
            Assert.True(concreteUserCart.FirstOrDefault().Product.Name == "Ананасовая");
            await TestsFixture.ClearDatabase(_fixture.db);
        }
        private async Task AddUserCartToDB(Guid guid)
        {
            if (!await _fixture.db.Products.AnyAsync())
            {
                await _fixture.db.Products.AddAsync(
                    new Product
                    {
                        Name = "Ананасовая"
                    });
            }
            if (!await _fixture.db.Carts.AnyAsync())
            {
                await _fixture.db.Carts.AddAsync(
                    new Cart
                    {
                        UserId = guid,
                        DateCreated = DateTime.Now,
                        Quantity = 1,
                        ProductId = 1
                    });
            }
            await _fixture.db.SaveChangesAsync();
        }
    }
}
