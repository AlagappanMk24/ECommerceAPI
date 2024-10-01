using ECommerce.Application.Contracts.Persistence;
using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, ILogger<CartController> logger) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly ILogger<CartController> _logger = logger;

        [HttpGet("Carts")]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var response = await _unitOfWork.Carts.GetAllCarts();
                if (response.IsSucceeded)
                    return Ok(response.Model);

                return StatusCode(response.StatusCode, response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all carts.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("Cart/{id}")]
        public async Task<IActionResult> GetCartById(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var response = await _unitOfWork.Carts.GetCart(id);
                if (response.IsSucceeded)
                    return Ok(response.Model);

                return StatusCode(response.StatusCode, response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving cart with id: {Id}", id);
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost("AddCart")]
        public async Task<IActionResult> AddCart()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            ApplicationUser currentUser = null;

            try
            {
                currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                    return BadRequest("User is not authenticated.");

                var response = await _unitOfWork.Carts.AddCart(currentUser);
                if (response.IsSucceeded)
                {
                    await _unitOfWork.Save();
                    return Ok(response.Model);
                }

                return StatusCode(response.StatusCode, response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding cart for user: {UserId}", currentUser?.Id);
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpDelete("DeleteCart")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var response = await _unitOfWork.Carts.DeleteCart(id);
                if (response.IsSucceeded)
                {
                    await _unitOfWork.Save();
                    return Ok(response.Model);
                }

                return StatusCode(response.StatusCode, response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting cart with id: {Id}", id);
                return StatusCode(500, "Internal server error.");
            }
        }
    }

}
