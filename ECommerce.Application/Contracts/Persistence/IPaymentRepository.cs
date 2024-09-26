using ECommerce.Application.DTOs.PaymentDTO;
using ECommerce.Application.DTOs;
using ECommerce.Domain.Models;
using ECommerce.Domain.Models.Entities;

namespace ECommerce.Application.Contracts.Persistence
{
    public interface IPaymentRepository : IGenericRepository<Payment>
    {
        Task<ResponseDto> CreateCheckoutSession(PaymentDto dto, ApplicationUser user);
        public Task<ResponseDto> GetAllPayments();
        public Task<ResponseDto> GetPayment(int id);
        public Task<ResponseDto> DeletePayment(int id);
    }
}
