using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PizzaShopApplication.Models.Data.Context;
using PizzaShopApplication.Models.Data.Entities.Review;
using PizzaShopApplication.Models.Domain.Interfaces.Reviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PizzaShopApplication.Models.Domain
{
    /// <summary>
    /// This repository represents methods to 
    /// CRUD operations with database "Reviews" table.
    /// </summary>
    public class ReviewRepository : IShowReview, IEditReview
    {
        private readonly ApplicationDataContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ReviewRepository(ApplicationDataContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }
        /// <summary>
        /// Gets all reviews, contains in DB.
        /// </summary>
        /// <returns>IEnumerable collection of "Reviews"</returns>
        public IEnumerable<Review> GetAllReviews()
        {
            var reviews = _dbContext.Reviews.Include(r => r.User).OrderByDescending(r => r.Id);
            return reviews;
        }
        /// <summary>
        /// Adds a new review to database "Reviews" table.
        /// </summary>
        /// <param name="comment">Review comment, sent by user.</param>
        public async Task SetNewReviewAsync(string comment)
        {
            // Getting user Id.
            int.TryParse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimsIdentity.DefaultIssuer), out int userId);
            var user = _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId).Result;
            // Adding a new review to DB.
            await _dbContext.Reviews.AddAsync(
                new Review
                {
                    User = user,
                    Comment = comment,
                    CommentDateTime = DateTime.Now,
                });
            await _dbContext.SaveChangesAsync();
        }
    }
}
