using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTOs.UserDto
{
    public class PasswordSettingDto
    {
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Compare("NewPassword", ErrorMessage = "Not Matched!")]
        public string ConfirmPassword { get; set; }
    }
}
