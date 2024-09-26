using ECommerce.Domain.Models;

namespace ECommerce.Application.Contracts.Persistence
{
    public interface IMailRepository
    {
        Task SendResetPasswordEmailAsync(ApplicationUser user, string subject);
    }
}
