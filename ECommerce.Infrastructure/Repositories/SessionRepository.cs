using ECommerce.Application.Contracts.Persistence;
using Stripe.Checkout;

namespace ECommerce.Infrastructure.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        public SessionRepository()
        {

        }
        public Task<Session> CreateCheckoutSession()
        {
            throw new NotImplementedException();
        }
    }
}
