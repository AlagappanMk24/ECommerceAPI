using ECommerce.Application.Contracts.Persistence;
using ECommerce.Domain.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderItemsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("OrderItem/{id}")]
        public async Task<IActionResult> GetOrderItem(int id)
        {
            if (ModelState.IsValid)
            {
                var order = await _unitOfWork.OrderItems.GetOrderItems(id);
                if (order.IsSucceeded)
                    return StatusCode(order.StatusCode, order.Model);
                return StatusCode(order.StatusCode, order.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpGet("Items")]
        public async Task<IActionResult> GetAllItems()
        {
            if (ModelState.IsValid)
            {
                var orders = await _unitOfWork.OrderItems.GetAllItems();
                if (orders.IsSucceeded)
                    return StatusCode(orders.StatusCode, orders.Model);
                return StatusCode(orders.StatusCode, orders.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpGet("ItemsInOrder/{orderId}")]
        public async Task<IActionResult> GetItemsInOrder(int orderId)
        {
            if (ModelState.IsValid)
            {
                var Items = await _unitOfWork.OrderItems.GetItemsInOrder(orderId);
                if (Items.IsSucceeded)
                    return StatusCode(Items.StatusCode, Items.Model);
                return StatusCode(Items.StatusCode, Items.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("AddOrderItem")]
        public async Task<IActionResult> AddOrderItem(OrderItem item)
        {
            if (ModelState.IsValid)
            {
                var response = await _unitOfWork.OrderItems.AddOrderItem(item);
                if (response.IsSucceeded)
                {
                    await _unitOfWork.Save();
                    return StatusCode(response.StatusCode, response.Model);
                }
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("EditOrderItem/{id}")]
        public async Task<IActionResult> UpdateOrderItem(int id, OrderItem item)
        {
            if (ModelState.IsValid)
            {
                var response = await _unitOfWork.OrderItems.UpdateOrderItem(id, item);
                if (response.IsSucceeded)
                {
                    await _unitOfWork.Save();
                    return StatusCode(response.StatusCode, response.Model);
                }
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("DeleteOrderItem/{id}")]
        public async Task<IActionResult> CancelOrderItem(int id)
        {
            if (ModelState.IsValid)
            {
                var response = await _unitOfWork.OrderItems.DeleteOrderItem(id);
                if (response.IsSucceeded)
                {
                    await _unitOfWork.Save();
                    return StatusCode(response.StatusCode, response.Model);
                }
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }
    }
}
