using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using PizzaShopApplication.Models.Data.Context;
using PizzaShopApplication.Models.Data.Entities.Order;
using PizzaShopApplication.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PizzaShopApplicationTests.ModelsLogicTests
{
    [Collection("Database collection")]
    public class UnitTest_OrderRepository
    {
        private readonly TestsFixture _fixture;
        private readonly OrderRepository _orderRepository;
        public UnitTest_OrderRepository(TestsFixture fixture)
        {
            _fixture = fixture;
            var IHttpContextAccesosrMock = new Mock<IHttpContextAccessor>();
            _orderRepository = new OrderRepository(_fixture.db, IHttpContextAccesosrMock.Object, new ShoppingCartRepository(_fixture.db, IHttpContextAccesosrMock.Object));
        }
        [Fact]
        public async void Test_GetOrderFromDBAsync()
        {
            // Arrange
            await AddOrderToDBAsync(_fixture.db);
            // Act
            var order = await _orderRepository.GetOrderFromDBAsync(1);
            // Assert
            Assert.NotNull(order);
            Assert.True(order.OrderStatus.Status == "В процессе доставки");
            await TestsFixture.ClearDatabase(_fixture.db);
        }
        private async Task AddOrderToDBAsync(ApplicationDataContext dbContext)
        {
            if (!await dbContext.OrderStatuses.AnyAsync())
            {
                await dbContext.OrderStatuses.AddRangeAsync(
                    // Order in a delivery process.
                    new OrderStatus
                    {
                        InProcess = true,
                        Cancelled = false,
                        Status = "В процессе доставки"
                    },
                    // Order is a deliveried.
                    new OrderStatus
                    {
                        InProcess = false,
                        Cancelled = false,
                        Status = "Заказ доставлен"
                    },
                    // Order is a cancelled.
                    new OrderStatus
                    {
                        InProcess = false,
                        Cancelled = true,
                        Status = "Заказ отменён"
                    });
            }
            if (!await dbContext.Orders.AnyAsync())
            {
                await dbContext.Orders.AddAsync(
                    new Order
                    {
                        OrderDateTime = DateTime.Now,
                        Apartment = "3",
                        ClientName = "Максим",
                        FloorNubmer = 19,
                        Comment = "Хочу кушать!",
                        Email = "42ama@gmail.com",
                        EntranceNubmer = 3,
                        Home = "14",
                        OrderStatusId = 1,
                        Phone = "+79271234567",
                        UserCartForeignKey = Guid.NewGuid(),
                        Street = "Солнченая"
                    });
            }
            await dbContext.SaveChangesAsync();
        }
    }
}
