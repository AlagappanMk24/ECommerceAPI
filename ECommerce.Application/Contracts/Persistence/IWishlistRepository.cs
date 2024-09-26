using ECommerce.Application.DTOs.WishlistDTO;
using ECommerce.Application.DTOs;
using ECommerce.Domain.Models;
using ECommerce.Domain.Models.Entities;

namespace ECommerce.Application.Contracts.Persistence
{
    public interface IWishlistRepository : IGenericRepository<Wishlist>
    {
        public Task<ResponseDto> GetWishlist(int id);
        public Task<ResponseDto> GetAllWishlists();
        public Task<ResponseDto> AddWishlist(WishlistDto dto, ApplicationUser user);
        public Task<ResponseDto> UpdateWishlist(int id, WishlistDto dto);
        public Task<ResponseDto> DeleteWishlist(int id);
    }
}
