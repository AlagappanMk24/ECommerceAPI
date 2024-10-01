using ECommerce.Application.Contracts.Persistence;
using ECommerce.Application.DTOs.UserDto;
using ECommerce.Application.DTOs.UserDTO;
using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, ILogger<AccountController> logger) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly ILogger<AccountController> _logger = logger;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var account = await _unitOfWork.Customers.RegisterAsync(dto);
                if (account != null)
                {
                    if (account.IsSucceeded)
                        return Ok(account);

                    return StatusCode(account.StatusCode, account.Message);
                }
                return BadRequest("Registration failed.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during registration.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var account = await _unitOfWork.Customers.LoginAsync(dto);
                if (account != null)
                {
                    return StatusCode(account.StatusCode, account.Model);
                }
                return BadRequest("Login failed.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during login.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword(PasswordSettingDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var response = await _unitOfWork.Customers.ChangePassword(dto);
                return StatusCode(response.StatusCode, response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while changing password.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPut("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var response = await _unitOfWork.Customers.ResetPassword(dto);
                return StatusCode(response.StatusCode, response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while resetting password.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost("SendResetPasswordEmail")]
        public async Task<IActionResult> SendResetPasswordEmail(string subject = "Reset Password")
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
                return BadRequest("Not authenticated user.");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                await _unitOfWork.Mails.SendResetPasswordEmailAsync(currentUser, subject);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while sending reset password email.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost("refreshToken")]
        public async Task<IActionResult> NewRefreshToken([FromBody] string email)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var token = await _unitOfWork.Customers.GetRefreshToken(email);
                if (token != null)
                {
                    return StatusCode(token.StatusCode, token.Model);
                }
                return BadRequest("Token generation failed.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while generating refresh token.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPut("EditProfile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UserDto dto, [FromHeader] string currentEmail)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var account = await _unitOfWork.Customers.UpdateProfile(dto, currentEmail);
                return StatusCode(account.StatusCode, account.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating profile.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpDelete("DeleteAccount")]
        public async Task<IActionResult> DeleteAccount(LoginDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var response = await _unitOfWork.Customers.DeleteAccountAsync(dto);
                return StatusCode(response.StatusCode, response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting account.");
                return StatusCode(500, "Internal server error.");
            }
        }
    }

}
