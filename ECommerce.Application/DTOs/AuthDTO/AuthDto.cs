using System.Text.Json.Serialization;

namespace ECommerce.Application.DTOs.AuthDto
{
    public class AuthDto
    {
        public string Token { get; set; }
        public string UserName { get; set; }
        public string? Message { get; set; }
        public string Email { get; set; }
        public bool IsAuthenticated { get; set; }
        [JsonIgnore]
        public string? Role { get; set; }

        [JsonIgnore]
        public string RefreshToken { get; set; }
        [JsonIgnore]
        public DateTime RefreshTokenExpiration { get; set; }
    }
}
