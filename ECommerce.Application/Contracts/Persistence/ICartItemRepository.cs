using ECommerce.Application.DTOs;
using ECommerce.Domain.Models.Entities;

namespace ECommerce.Application.Contracts.Persistence
{
    public interface ICartItemRepository : IGenericRepository<CartItem> 
    {
        Task<ResponseDto> GetAllCartsItems();
        public Task<ResponseDto> GetCartItem(int id);
        Task<ResponseDto> GetItemsInCart(int cartId);
        Task<ResponseDto> AddItemToCart(CartItem item);
        Task<ResponseDto> UpdateCartItem(int id, CartItem item);
        Task<ResponseDto> DeleteItemFromCart(int id);
    }
}
