using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PizzaShopApplication.Models.Data.Context;
using PizzaShopApplication.Models.Domain;
using PizzaShopApplication.Models.Domain.Interfaces.Reviews;

namespace PizzaShopApplication.Controllers
{
    /// <summary>
    /// This controller allows possibility
    /// to work with users reviews.
    /// </summary>
    public class ReviewController : Controller
    {
        private readonly ApplicationDataContext _dbContext;
        private readonly IShowReview _reviewRepository;
        public ReviewController(ApplicationDataContext dbContext, IShowReview reviewRepository)
        {
            _dbContext = dbContext;
            _reviewRepository = reviewRepository;
        }
        /// <summary>
        /// Gets all comments from database "Reviews" table.
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAllComments()
        {
            var reviews = _reviewRepository.GetAllReviews();
            return View(reviews);
        }
        /// <summary>
        /// Comments a review by its Id.
        /// </summary>
        /// <param name="reviewId">Review id</param>
        public IActionResult CommentReview(int reviewId)
        {
            return PartialView("_CommentReview");
        }
    }
}
