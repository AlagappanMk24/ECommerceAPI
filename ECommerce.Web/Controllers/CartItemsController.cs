using ECommerce.Application.Contracts.Persistence;
using ECommerce.Domain.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemsController(IUnitOfWork unitOfWork, ILogger<CartItemsController> logger) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<CartItemsController> _logger = logger;

        [HttpGet("AllItems")]
        public async Task<IActionResult> GetAllItems()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var response = await _unitOfWork.CartItems.GetAllCartsItems();
                if (response.IsSucceeded)
                    return Ok(response.Model);

                return StatusCode(response.StatusCode, response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all cart items.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("Item/{id}")]
        public async Task<IActionResult> GetItem(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var response = await _unitOfWork.CartItems.GetCartItem(id);
                if (response.IsSucceeded)
                    return Ok(response.Model);

                return StatusCode(response.StatusCode, response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving cart item with id: {Id}", id);
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("ItemsInCart")]
        public async Task<IActionResult> GetItemsInCart(int cartId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var response = await _unitOfWork.CartItems.GetItemsInCart(cartId);
                if (response.IsSucceeded)
                    return Ok(response.Model);

                return StatusCode(response.StatusCode, response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving items in cart with id: {CartId}", cartId);
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost("AddItem")]
        public async Task<IActionResult> AddItemToCart(CartItem item)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var response = await _unitOfWork.CartItems.AddItemToCart(item);
                if (response.IsSucceeded)
                {
                    await _unitOfWork.Save();
                    return Ok(response.Model);
                }

                return StatusCode(response.StatusCode, response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding item to cart.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPut("UpdateItem")]
        public async Task<IActionResult> EditCartItem(int id, CartItem item)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var response = await _unitOfWork.CartItems.UpdateCartItem(id, item);
                if (response.IsSucceeded)
                {
                    await _unitOfWork.Save();
                    return Ok(response.Model);
                }

                return StatusCode(response.StatusCode, response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating cart item with id: {Id}", id);
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpDelete("DeleteItem")]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var response = await _unitOfWork.CartItems.DeleteItemFromCart(id);
                if (response.IsSucceeded)
                {
                    await _unitOfWork.Save();
                    return Ok(response.Model);
                }

                return StatusCode(response.StatusCode, response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting cart item with id: {Id}", id);
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}
