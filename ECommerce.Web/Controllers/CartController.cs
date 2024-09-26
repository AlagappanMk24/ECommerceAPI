using ECommerce.Application.Contracts.Persistence;
using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        [HttpGet("Carts")]
        public async Task<IActionResult> GetAll()
        {
            if (ModelState.IsValid)
            {
                var response = await _unitOfWork.Carts.GetAllCarts();
                if (response.IsSucceeded)
                {
                    return StatusCode(response.StatusCode, response.Model);
                }
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpGet("Cart/{id}")]
        public async Task<IActionResult> GetCartById(int id)
        {
            if (ModelState.IsValid)
            {
                var response = await _unitOfWork.Carts.GetCart(id);
                if (response.IsSucceeded)
                    return StatusCode(response.StatusCode, response.Model);
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("AddCart")]
        public async Task<IActionResult> AddCart()
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser != null)
                {
                    var response = await _unitOfWork.Carts.AddCart(currentUser);
                    if (response.IsSucceeded)
                    {
                        await _unitOfWork.Save();
                        return StatusCode(response.StatusCode, response.Model);
                    }
                    return StatusCode(response.StatusCode, response.Message);
                }
                return BadRequest("User is not authenticated.");
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("DeleteCart")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            if (ModelState.IsValid)
            {
                var response = await _unitOfWork.Carts.DeleteCart(id);
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
