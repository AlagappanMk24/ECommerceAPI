using ECommerce.Application.DTOs;
using ECommerce.Domain.Models;
using ECommerce.Domain.Models.Entities;
using Stripe.Checkout;

namespace ECommerce.Application.Contracts.Persistence
{
    public interface ISessionRepository
    {
        public Task<Session> CreateCheckoutSession();
    }
}
