using ECommerce.Application.Contracts.Persistence;
using ECommerce.Domain.Models;
using ECommerce.Domain.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, ILogger<ReviewsController> logger) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly ILogger<ReviewsController> _logger = logger; // Add logger

        // GET: api/reviews/Review/{id}
        [HttpGet("Review/{id}")]
        public async Task<IActionResult> GetReview(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _unitOfWork.Reviews.GetReview(id);
                    return response.IsSucceeded
                        ? StatusCode(response.StatusCode, response.Model)
                        : StatusCode(response.StatusCode, response.Message);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting review with ID {ReviewId}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/reviews/CustomerReviews
        [HttpGet("CustomerReviews")]
        public async Task<IActionResult> GetCustomerReviews()
        {
            ApplicationUser? currentUser = null;
            try
            {
                currentUser = await _userManager.GetUserAsync(User);
                if (ModelState.IsValid)
                {
                    var response = await _unitOfWork.Reviews.GetAllCustomerReviews(currentUser);
                    return response.IsSucceeded
                        ? StatusCode(response.StatusCode, response.Model)
                        : StatusCode(response.StatusCode, response.Message);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting customer reviews for user {UserId}", currentUser?.Id);
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/reviews/Product/{productId}/Reviews
        [HttpGet("Product/{productId}/Reviews")]
        public async Task<IActionResult> GetProductReviews(int productId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _unitOfWork.Reviews.GetAllProductReviews(productId);
                    return response.IsSucceeded
                        ? StatusCode(response.StatusCode, response.Model)
                        : StatusCode(response.StatusCode, response.Message);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting reviews for product with ID {ProductId}", productId);
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/reviews/CustomerReviewOnProduct/{prodId}
        [HttpGet("CustomerReviewOnProduct/{prodId}")]
        public async Task<IActionResult> GetCustomerReviewOnProduct(int prodId)
        {
            ApplicationUser? currentUser = null;
            try
            {
                currentUser = await _userManager.GetUserAsync(User);
                if (ModelState.IsValid)
                {
                    var response = await _unitOfWork.Reviews.GetCustomerReviewOnProduct(currentUser, prodId);
                    return response.IsSucceeded
                        ? StatusCode(response.StatusCode, response.Model)
                        : StatusCode(response.StatusCode, response.Message);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting customer review on product with ID {ProductId} for user {UserId}", prodId, currentUser?.Id);
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/reviews/AddReview
        [HttpPost("AddReview")]
        public async Task<IActionResult> AddReview(Review review)
        {
            ApplicationUser? currentUser = null;
            try
            {
                currentUser = await _userManager.GetUserAsync(User);
                if (ModelState.IsValid)
                {
                    var response = await _unitOfWork.Reviews.AddReview(review, currentUser);
                    if (response.IsSucceeded)
                    {
                        await _unitOfWork.Save();
                        return StatusCode(response.StatusCode, response.Model);
                    }
                    return StatusCode(response.StatusCode, response.Message);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a review for user {UserId}", currentUser?.Id);
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/reviews/UpdateReview
        [HttpPut("UpdateReview")]
        public async Task<IActionResult> EditReview(int id, Review review)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _unitOfWork.Reviews.UpdateReview(id, review);
                    if (response.IsSucceeded)
                    {
                        await _unitOfWork.Save();
                        return StatusCode(response.StatusCode, response.Model);
                    }
                    return StatusCode(response.StatusCode, response.Message);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating review with ID {ReviewId}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: api/reviews/DeleteReview
        [HttpDelete("DeleteReview")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _unitOfWork.Reviews.DeleteReview(id);
                    if (response.IsSucceeded)
                    {
                        await _unitOfWork.Save();
                        return StatusCode(response.StatusCode, response.Model);
                    }
                    return StatusCode(response.StatusCode, response.Message);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting review with ID {ReviewId}", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }

}
