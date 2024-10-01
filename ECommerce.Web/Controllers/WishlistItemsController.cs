using ECommerce.Application.Contracts.Persistence;
using ECommerce.Domain.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistItemsController(IUnitOfWork unitOfWork, ILogger<WishlistItemsController> logger) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<WishlistItemsController> _logger = logger;

        [HttpGet("Item/{id}")]
        public async Task<IActionResult> GetWishlistItem(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _unitOfWork.WishlistItems.GetWishlistItem(id);
                    if (response.IsSucceeded)
                        return StatusCode(response.StatusCode, response.Model);
                    return StatusCode(response.StatusCode, response.Message);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"An error occurred while retrieving wishlist item with ID {id}");
                    return StatusCode(500, "Internal server error");
                }
            }
            return BadRequest(ModelState);
        }

        [HttpGet("WishlistsItems")]
        public async Task<IActionResult> GetAllWishlistsItems()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _unitOfWork.WishlistItems.GetAllWishlistItems();
                    if (response.IsSucceeded)
                        return StatusCode(response.StatusCode, response.Model);
                    return StatusCode(response.StatusCode, response.Message);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while retrieving all wishlist items");
                    return StatusCode(500, "Internal server error");
                }
            }
            return BadRequest(ModelState);
        }

        [HttpGet("ItemsInList/{listId}")]
        public async Task<IActionResult> GetItemsInList(int listId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _unitOfWork.WishlistItems.GetItemsInWishlist(listId);
                    if (response.IsSucceeded)
                        return StatusCode(response.StatusCode, response.Model);
                    return StatusCode(response.StatusCode, response.Message);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"An error occurred while retrieving items in wishlist with ID {listId}");
                    return StatusCode(500, "Internal server error");
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost("AddItem")]
        public async Task<IActionResult> AddItemToList(WishlistItem item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _unitOfWork.WishlistItems.AddWishlistItem(item);
                    if (response.IsSucceeded)
                    {
                        await _unitOfWork.Save();
                        return StatusCode(response.StatusCode, response.Model);
                    }
                    return StatusCode(response.StatusCode, response.Message);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while adding item to the wishlist");
                    return StatusCode(500, "Internal server error");
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPut("UpdateItem")]
        public async Task<IActionResult> EditCartItem(int id, WishlistItem item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _unitOfWork.WishlistItems.UpdateWishlistItem(id, item);
                    if (response.IsSucceeded)
                    {
                        await _unitOfWork.Save();
                        return StatusCode(response.StatusCode, response.Model);
                    }
                    return StatusCode(response.StatusCode, response.Message);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"An error occurred while updating item with ID {id} in the wishlist");
                    return StatusCode(500, "Internal server error");
                }
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("DeleteItem")]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _unitOfWork.WishlistItems.DeleteWishlistItem(id);
                    if (response.IsSucceeded)
                    {
                        await _unitOfWork.Save();
                        return StatusCode(response.StatusCode, response.Model);
                    }
                    return StatusCode(response.StatusCode, response.Message);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"An error occurred while deleting item with ID {id} from the wishlist");
                    return StatusCode(500, "Internal server error");
                }
            }
            return BadRequest(ModelState);
        }
    }
}
