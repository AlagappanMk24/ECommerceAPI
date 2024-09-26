using ECommerce.Application.DTOs;
using ECommerce.Domain.Models.Entities;

namespace ECommerce.Application.Contracts.Persistence
{
    public interface IOrderItemRepository : IGenericRepository<OrderItem>
    {
        public Task<ResponseDto> GetOrderItems(int id);
        public Task<ResponseDto> GetAllItems();
        public Task<ResponseDto> GetItemsInOrder(int orderId);
        public Task<ResponseDto> AddOrderItem(OrderItem item);
        public Task<ResponseDto> UpdateOrderItem(int id, OrderItem item);
        public Task<ResponseDto> DeleteOrderItem(int id);
    }
}
