using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShopApplication.Models.Domain.Interfaces.Reviews
{
    public interface IEditReview
    {
        /// <summary>
        /// Should add a new review to DB.
        /// </summary>
        /// <param name="comment">Review comment.</param>
        /// <returns></returns>
        public Task SetNewReviewAsync(string comment);
    }
}
