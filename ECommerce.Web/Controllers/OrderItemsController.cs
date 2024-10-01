using ECommerce.Application.Contracts.Persistence;
using ECommerce.Domain.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController(IUnitOfWork unitOfWork, ILogger<OrderItemsController> logger) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<OrderItemsController> _logger = logger;

        [HttpGet("OrderItem/{id}")]
        public async Task<IActionResult> GetOrderItem(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var orderItem = await _unitOfWork.OrderItems.GetOrderItems(id);
                if (orderItem.IsSucceeded)
                    return Ok(orderItem.Model);

                return StatusCode(orderItem.StatusCode, orderItem.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving order item with id: {Id}", id);
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("Items")]
        public async Task<IActionResult> GetAllItems()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var items = await _unitOfWork.OrderItems.GetAllItems();
                if (items.IsSucceeded)
                    return Ok(items.Model);

                return StatusCode(items.StatusCode, items.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all order items.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("ItemsInOrder/{orderId}")]
        public async Task<IActionResult> GetItemsInOrder(int orderId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var items = await _unitOfWork.OrderItems.GetItemsInOrder(orderId);
                if (items.IsSucceeded)
                    return Ok(items.Model);

                return StatusCode(items.StatusCode, items.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving items in order with id: {OrderId}", orderId);
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost("AddOrderItem")]
        public async Task<IActionResult> AddOrderItem(OrderItem item)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var response = await _unitOfWork.OrderItems.AddOrderItem(item);
                if (response.IsSucceeded)
                {
                    await _unitOfWork.Save();
                    return Ok(response.Model);
                }

                return StatusCode(response.StatusCode, response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding an order item.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPut("EditOrderItem/{id}")]
        public async Task<IActionResult> UpdateOrderItem(int id, OrderItem item)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var response = await _unitOfWork.OrderItems.UpdateOrderItem(id, item);
                if (response.IsSucceeded)
                {
                    await _unitOfWork.Save();
                    return Ok(response.Model);
                }

                return StatusCode(response.StatusCode, response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating order item with id: {Id}", id);
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpDelete("DeleteOrderItem/{id}")]
        public async Task<IActionResult> CancelOrderItem(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var response = await _unitOfWork.OrderItems.DeleteOrderItem(id);
                if (response.IsSucceeded)
                {
                    await _unitOfWork.Save();
                    return Ok(response.Model);
                }

                return StatusCode(response.StatusCode, response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting order item with id: {Id}", id);
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}
