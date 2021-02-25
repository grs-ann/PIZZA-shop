using Microsoft.EntityFrameworkCore;
using PizzaShopApplication.Models.Data.Context;
using PizzaShopApplication.Models.Data.Entities.Review;
using PizzaShopApplication.Models.Domain.Interfaces.Reviews;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public ReviewRepository(ApplicationDataContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// Gets all reviews, contains in DB.
        /// </summary>
        /// <returns>IEnumerable collection of "Reviews"</returns>
        public IEnumerable<Review> GetAllReviews()
        {
            var reviews = _dbContext.Reviews.Include(r => r.User);
            return reviews;
        }
    }
}
