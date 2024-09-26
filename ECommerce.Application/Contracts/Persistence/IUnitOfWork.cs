namespace ECommerce.Application.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepository Customers { get; }
        IProductRepository Products { get; }
        IOrderRepository Orders { get; }
        IOrderItemRepository OrderItems { get; }
        ICategoryRepository Categories { get; }
        ICartRepository Carts { get; }
        ICartItemRepository CartItems { get; }
        IReviewRepository Reviews { get; }
        IWishlistRepository Wishlists { get; }
        IWishlistItemRepository WishlistItems { get; }
        ISessionRepository Sessions { get; }
        IMailRepository Mails { get; }
        IPaymentRepository Payments { get; }
        IBrandRepository Brands { get; }
        Task<int> Save();
    }
}
