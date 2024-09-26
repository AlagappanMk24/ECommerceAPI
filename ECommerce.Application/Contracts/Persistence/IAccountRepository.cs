using ECommerce.Application.DTOs.UserDto;
using ECommerce.Application.DTOs.UserDTO;
using ECommerce.Application.DTOs;
using ECommerce.Domain.Models;

namespace ECommerce.Application.Contracts.Persistence
{
    public interface IAccountRepository : IGenericRepository<ApplicationUser>
    {
        public Task<ResponseDto> LoginAsync(LoginDto login);
        public Task<ResponseDto> GetRefreshToken(string email);
        public Task<ResponseDto> RegisterAsync(RegisterDto register);
        public Task<ResponseDto> DeleteAccountAsync(LoginDto dto);
        public Task<ResponseDto> ChangePassword(PasswordSettingDto password);
        public Task<ResponseDto> UpdateProfile(UserDto user, string currentEmail);
        public Task<ResponseDto> ResetPassword(ResetPasswordDto dto);
    }
}
