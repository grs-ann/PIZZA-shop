using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using PizzaShopApplication.Models.Data.Context;
using PizzaShopApplication.Models.Data.Entities.Authentification;
using PizzaShopApplication.Models.Data.Entities.Review;
using PizzaShopApplication.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PizzaShopApplicationTests.ModelsLogicTests
{
    [Collection("Database collection")]
    public class UnitTest_ReviewRepository
    {
        private readonly TestsFixture _fixture;
        private readonly ReviewRepository _reviewRepository;
        public UnitTest_ReviewRepository(TestsFixture fixture)
        {
            _fixture = fixture;
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _reviewRepository = new ReviewRepository(_fixture.db, httpContextAccessorMock.Object);
        }

        [Fact]
        public async void Test_GetAllReviews()
        {
            // Arrange
            AddReviewsToDB(_fixture.db);
            var commentForTest = "Хоть я и директор этой пиццерии, но всегда люблю полакомиться нереально вкуснйо пицце! И это про нас!!!";
            // Act
            var reviews = _reviewRepository.GetAllReviews();
            // Assert
            Assert.NotNull(reviews);
            Assert.True(reviews.Where(r => r.Comment == commentForTest) != null);
            await TestsFixture.ClearDatabase(_fixture.db);
        }
        [Fact(Skip = "specific reason")]
        public async void Test_SetNewReviewAsync()
        {
            // Arrange
            await AddUserToDB(_fixture.db);
            // Act
            var user = await _fixture.db.Users.FirstOrDefaultAsync();
            var comment = "Здравствуйте, меня зовут Макс Алонов, и я хочу сказать спасибо за такую вкусную пиццу.";
            await _reviewRepository.SetNewReviewAsync(comment);
            var addedReview = await _fixture.db.Reviews.FirstOrDefaultAsync();
            // Assert
            Assert.NotNull(addedReview);
            Assert.True(addedReview.User.Name == "Maxim Alonov");
            await TestsFixture.ClearDatabase(_fixture.db);
        }
        private void AddReviewsToDB(ApplicationDataContext context)
        {
            if (!context.Reviews.Any())
            {
                context.Reviews.AddRange(
                    new Review
                    {
                        User = null,
                        Comment = "Очень вкусная пицца! Заказывали в 17:30, в 17-55 уже привезли! Спасибо!",
                        CommentDateTime = DateTime.UtcNow
                    },
                    new Review
                    {
                        User = null,
                        Comment = "Хоть я и директор этой пиццерии, но всегда люблю полакомиться нереально вкуснйо пицце! И это про нас!!!",
                        CommentDateTime = DateTime.UtcNow
                    });
                context.SaveChanges();
            }
        }
        private async Task AddUserToDB(ApplicationDataContext context)
        {
            if (!context.Users.Any())
            {
                await context.AddAsync(
                    new User
                    {
                        Name = "Maxim Alonov",
                        Login = "42ama",
                        Password = "123456",
                        Email = "test@gmail.com",
                    }); 
            }
            await context.SaveChangesAsync();
        }
    }
}
