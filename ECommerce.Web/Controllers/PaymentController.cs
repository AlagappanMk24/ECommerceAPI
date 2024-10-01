using ECommerce.Application.Contracts.Persistence;
using ECommerce.Application.DTOs.PaymentDTO;
using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, ILogger<PaymentController> logger) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly ILogger<PaymentController> _logger = logger;

        [HttpPost("Checkout")]
        public async Task<IActionResult> CreateCheckOutSession(PaymentDto dto)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                _logger.LogWarning("User not authenticated.");
                return BadRequest("User not authenticated");
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var response = await _unitOfWork.Payments.CreateCheckoutSession(dto, currentUser);
                if (response.IsSucceeded)
                {
                    await _unitOfWork.Save();
                    return StatusCode(response.StatusCode, response.Model);
                }

                return StatusCode(response.StatusCode, response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating checkout session.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("Payment/{id}")]
        public async Task<IActionResult> GetPayment(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var response = await _unitOfWork.Payments.GetPayment(id);
                if (response.IsSucceeded)
                {
                    return StatusCode(response.StatusCode, response.Model);
                }

                return StatusCode(response.StatusCode, response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving payment with id: {Id}", id);
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("AllPayment")]
        public async Task<IActionResult> GetAllPayments()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var response = await _unitOfWork.Payments.GetAllPayments();
                if (response.IsSucceeded)
                {
                    return StatusCode(response.StatusCode, response.Model);
                }

                return StatusCode(response.StatusCode, response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all payments.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpDelete("DeletePayment/{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var response = await _unitOfWork.Payments.DeletePayment(id);
                if (response.IsSucceeded)
                {
                    await _unitOfWork.Save();
                    return StatusCode(response.StatusCode, response.Model);
                }

                return StatusCode(response.StatusCode, response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting payment with id: {Id}", id);
                return StatusCode(500, "Internal server error.");
            }
        }

        // Uncomment if you want to implement success and cancel endpoints
        // [HttpGet("success")]
        // public IActionResult Success()
        // {
        //     return Ok("Succeeded");
        // }

        // [HttpGet("cancel")]
        // public IActionResult Cancel()
        // {
        //     return Ok("Canceled");
        // }
    }
}
