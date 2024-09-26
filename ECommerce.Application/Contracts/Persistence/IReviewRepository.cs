using ECommerce.Application.DTOs;
using ECommerce.Domain.Models.Entities;
using ECommerce.Domain.Models;

namespace ECommerce.Application.Contracts.Persistence
{
    public interface IReviewRepository : IGenericRepository<Review>
    {
        public Task<ResponseDto> GetReview(int id);
        public Task<ResponseDto> GetAllCustomerReviews(ApplicationUser user);
        public Task<ResponseDto> GetAllProductReviews(int productId);
        public Task<ResponseDto> GetCustomerReviewOnProduct(ApplicationUser user, int prodId);
        public Task<ResponseDto> AddReview(Review review, ApplicationUser user);
        public Task<ResponseDto> UpdateReview(int id, Review review);
        public Task<ResponseDto> DeleteReview(int id);

    }
}
