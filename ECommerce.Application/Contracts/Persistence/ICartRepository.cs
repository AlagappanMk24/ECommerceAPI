using ECommerce.Application.DTOs;
using ECommerce.Domain.Models;

namespace ECommerce.Application.Contracts.Persistence
{
    public interface ICartRepository
    {
        Task<ResponseDto> GetCart(int id);
        Task<ResponseDto> GetAllCarts();
        Task<ResponseDto> AddCart(ApplicationUser currentUser);
        //Task<ResponseDto> UpdateCart(CartDto dto);
        Task<ResponseDto> DeleteCart(int id);
    }
}
