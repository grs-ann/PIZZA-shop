using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShopApplication.Models.Domain.Interfaces.Reviews
{
    public interface IShowReview
    {
        public Task<Review> GetAllReviews();
    }
}
