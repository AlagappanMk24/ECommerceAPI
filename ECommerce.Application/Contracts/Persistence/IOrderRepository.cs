using ECommerce.Application.DTOs.OrderDTO;
using ECommerce.Application.DTOs;
using ECommerce.Domain.Models.Entities;

namespace ECommerce.Application.Contracts.Persistence
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        public Task<ResponseDto> GetOrderById(int id);
        public Task<ResponseDto> GetCustomerOrders();
        public Task<ResponseDto> AddOrder(OrderDto dto);
        public Task<ResponseDto> UpdateOrder(int id, OrderDto dto);
        public Task<ResponseDto> DeleteOrder(int id);
    }
}
