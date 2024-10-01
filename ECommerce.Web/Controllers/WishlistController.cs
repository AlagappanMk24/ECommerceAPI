using ECommerce.Application.Contracts.Persistence;
using ECommerce.Application.DTOs.WishlistDTO;
using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, ILogger<WishlistController> logger) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly ILogger<WishlistController> _logger = logger;

        [HttpGet("Wishlist/{id}")]
        public async Task<IActionResult> GetWishlist(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _unitOfWork.Wishlists.GetWishlist(id);
                    if (response.IsSucceeded)
                        return StatusCode(response.StatusCode, response.Model);
                    return StatusCode(response.StatusCode, response.Message);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting wishlist with id {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("Wishlists")]
        public async Task<IActionResult> GetAllWishlists()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _unitOfWork.Wishlists.GetAllWishlists();
                    if (response.IsSucceeded)
                        return StatusCode(response.StatusCode, response.Model);
                    return StatusCode(response.StatusCode, response.Message);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all wishlists");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("AddWishlist")]
        public async Task<IActionResult> AddWishlist(WishlistDto dto)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _unitOfWork.Wishlists.AddWishlist(dto, currentUser);
                    if (response.IsSucceeded)
                    {
                        await _unitOfWork.Save();
                        return StatusCode(response.StatusCode, response.Model);
                    }
                    return StatusCode(response.StatusCode, response.Message);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a wishlist for user {UserId}", currentUser.Id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("UpdateWishlist")]
        public async Task<IActionResult> EditWishlist(int id, WishlistDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _unitOfWork.Wishlists.UpdateWishlist(id, dto);
                    if (response.IsSucceeded)
                    {
                        await _unitOfWork.Save();
                        return StatusCode(response.StatusCode, response.Model);
                    }
                    return StatusCode(response.StatusCode, response.Message);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating wishlist with id {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("DeleteWishlist")]
        public async Task<IActionResult> DeleteWishlist(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _unitOfWork.Wishlists.DeleteWishlist(id);
                    if (response.IsSucceeded)
                    {
                        await _unitOfWork.Save();
                        return StatusCode(response.StatusCode, response.Model);
                    }
                    return StatusCode(response.StatusCode, response.Message);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting wishlist with id {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
