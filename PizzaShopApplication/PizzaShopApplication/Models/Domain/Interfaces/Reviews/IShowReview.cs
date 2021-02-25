using PizzaShopApplication.Models.Data.Entities.Review;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaShopApplication.Models.Domain.Interfaces.Reviews
{
    public interface IShowReview
    {
        public IEnumerable<Review> GetAllReviews();
    }
}
