using ECommerce.Application.DTOs;
using ECommerce.Domain.Models.Entities;

namespace ECommerce.Application.Contracts.Persistence
{
    public interface IWishlistItemRepository : IGenericRepository<WishlistItem>
    {
        public Task<ResponseDto> GetWishlistItem(int id);
        public Task<ResponseDto> GetAllWishlistItems();
        public Task<ResponseDto> GetItemsInWishlist(int listId);
        public Task<ResponseDto> AddWishlistItem(WishlistItem item);
        public Task<ResponseDto> UpdateWishlistItem(int id, WishlistItem item);
        public Task<ResponseDto> DeleteWishlistItem(int id);
    }
}
