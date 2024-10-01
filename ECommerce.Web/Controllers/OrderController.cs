using ECommerce.Application.Contracts.Persistence;
using ECommerce.Application.DTOs.OrderDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IUnitOfWork unitOfWork, ILogger<OrderController> logger) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<OrderController> _logger = logger;

        [HttpGet("Orders")]
        public async Task<IActionResult> GetAllOrders()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var orders = await _unitOfWork.Orders.GetCustomerOrders();
                if (orders.IsSucceeded)
                    return Ok(orders.Model);

                return StatusCode(orders.StatusCode, orders.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all orders.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("Order/{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var order = await _unitOfWork.Orders.GetOrderById(id);
                if (order.IsSucceeded)
                    return Ok(order.Model);

                return StatusCode(order.StatusCode, order.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving order with id: {Id}", id);
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost("AddOrder")]
        public async Task<IActionResult> AddOrder(OrderDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var response = await _unitOfWork.Orders.AddOrder(dto);
                if (response.IsSucceeded)
                {
                    await _unitOfWork.Save();
                    return Ok(response.Model);
                }

                return StatusCode(response.StatusCode, response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new order.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPut("EditOrder/{id}")]
        public async Task<IActionResult> UpdateOrder(int id, OrderDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var response = await _unitOfWork.Orders.UpdateOrder(id, dto);
                if (response.IsSucceeded)
                {
                    await _unitOfWork.Save();
                    return Ok(response.Model);
                }

                return StatusCode(response.StatusCode, response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating order with id: {Id}", id);
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpDelete("DeleteOrder/{id}")]
        public async Task<IActionResult> CancelOrder(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var response = await _unitOfWork.Orders.DeleteOrder(id);
                if (response.IsSucceeded)
                {
                    await _unitOfWork.Save();
                    return Ok(response.Model);
                }

                return StatusCode(response.StatusCode, response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while canceling order with id: {Id}", id);
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}
