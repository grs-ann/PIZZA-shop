using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaShopApplication.Models.Data.Context;
using PizzaShopApplication.Models.Domain.Interfaces.Reviews;
using System.Threading.Tasks;

namespace PizzaShopApplication.Controllers
{
    /// <summary>
    /// This controller allows possibility
    /// to work with users reviews.
    /// </summary>
    public class ReviewController : Controller
    {
        private readonly ApplicationDataContext _dbContext;
        private readonly IShowReview _showReviewRepository;
        private readonly IEditReview _editReviewRepository;
        public ReviewController(ApplicationDataContext dbContext, IShowReview reviewRepository
            , IEditReview editReviewRepository)
        {
            _dbContext = dbContext;
            _showReviewRepository = reviewRepository;
            _editReviewRepository = editReviewRepository;
        }
        /// <summary>
        /// Gets all comments from database "Reviews" table.
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAllComments()
        {
            var reviews = _showReviewRepository.GetAllReviews();
            return View(reviews);
        }
        /// <summary>
        /// Gets possibility to user to add review.
        /// </summary>
        /// <param name="reviewId">Review id</param>
        [Authorize]
        [HttpGet]
        public IActionResult CommentReview()
        {
            return View();
        }
        /// <summary>
        /// Adds review, sent by user.
        /// </summary>
        /// <param name="review">Review data, filled by user.</param>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CommentReview(string comment)
        {
            await _editReviewRepository.SetNewReviewAsync(comment);
            return RedirectToAction("GetAllComments", "Review");
        }
    }
}
