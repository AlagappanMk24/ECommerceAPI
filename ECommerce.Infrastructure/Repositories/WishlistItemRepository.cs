using AutoMapper;
using ECommerce.Application.Contracts.Persistence;
using ECommerce.Application.DTOs;
using ECommerce.Application.DTOs.WishlistDTO;
using ECommerce.Domain.Models.Entities;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories
{
    public class WishlistItemsRepository : GenericRepository<WishlistItem>, IWishlistItemRepository
    {
        private readonly IMapper _mapper;
        public WishlistItemsRepository(ECommerceDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<ResponseDto> GetWishlistItem(int id)
        {
            var item = await _context.WishlistItems.Where(i => i.Id == id)
                .Include(i => i.Wishlist).Include(i => i.Product).FirstOrDefaultAsync();

            if (item != null)
            {
                var dto = _mapper.Map<WishlistItemDto>(item);
                return new ResponseDto
                {
                    StatusCode = 200,
                    IsSucceeded = true,
                    Model = dto
                };
            }

            return new ResponseDto
            {
                StatusCode = 404,
                IsSucceeded = false,
                Message = "Item not found."
            };
        }

        public async Task<ResponseDto> GetAllWishlistItems()
        {
            var items = await _context.WishlistItems.AsNoTracking()
                .Include(i => i.Wishlist).Include(i => i.Product)
                .ToListAsync();

            if (items != null && items.Count > 0)
            {
                var dto = _mapper.Map<List<WishlistItemDto>>(items);
                return new ResponseDto
                {
                    StatusCode = 200,
                    IsSucceeded = true,
                    Model = dto
                };
            }

            return new ResponseDto
            {
                StatusCode = 400,
                IsSucceeded = false,
                Message = "There is no Items."
            };
        }

        public async Task<ResponseDto> GetItemsInWishlist(int listId)
        {
            var list = await _context.Wishlists.FindAsync(listId);
            if (list != null)
            {
                var items = await _context.WishlistItems.Where(i => i.WishlistId == listId)
                .Include(i => i.Wishlist).Include(i => i.Product).ToListAsync();

                if (items != null && items.Count > 0)
                {
                    var dto = _mapper.Map<List<WishlistItemDto>>(items);
                    return new ResponseDto
                    {
                        StatusCode = 200,
                        IsSucceeded = true,
                        Model = dto
                    };
                }
                return new ResponseDto
                {
                    StatusCode = 400,
                    IsSucceeded = false,
                    Message = "There is no Items."
                };
            }

            return new ResponseDto
            {
                StatusCode = 404,
                IsSucceeded = false,
                Message = "Wish list not found."
            };
        }

        public async Task<ResponseDto> AddWishlistItem(WishlistItem item)
        {
            if (!_context.Wishlists.Any(w => w.Id == item.WishlistId))
            {
                return new ResponseDto
                {
                    StatusCode = 400,
                    IsSucceeded = false,
                    Message = "Wish list you try to add item to is not found."
                };
            }
            if (!_context.Products.Any(w => w.Id == item.ProductId))
            {
                return new ResponseDto
                {
                    StatusCode = 400,
                    IsSucceeded = false,
                    Message = "Product you try to add is not found."
                };
            }

            item.Wishlist = await _context.Wishlists.FindAsync(item.WishlistId);
            item.Product = await _context.Products.FindAsync(item.ProductId);
            await _context.AddAsync(item);

            var entity = _context.Entry(item);
            if (entity.State == EntityState.Added)
            {
                var dto = _mapper.Map<WishlistItemDto>(item);
                return new ResponseDto
                {
                    StatusCode = 200,
                    IsSucceeded = true,
                    Model = dto
                };
            }
            return new ResponseDto
            {
                StatusCode = 400,
                IsSucceeded = false,
                Message = "Failed to add this item."
            };
        }

        public async Task<ResponseDto> UpdateWishlistItem(int id, WishlistItem item)
        {
            var prevItem = await _context.WishlistItems.FindAsync(id);
            if (prevItem == null)
            {
                return new ResponseDto
                {
                    StatusCode = 400,
                    IsSucceeded = false,
                    Message = "This Item not found."
                };
            }

            if (!_context.Wishlists.Any(w => w.Id == item.WishlistId))
            {
                return new ResponseDto
                {
                    StatusCode = 400,
                    IsSucceeded = false,
                    Message = "Wish list you try to add item to is not found."
                };
            }
            if (!_context.Products.Any(w => w.Id == item.ProductId))
            {
                return new ResponseDto
                {
                    StatusCode = 400,
                    IsSucceeded = false,
                    Message = "Product you try to add is not found."
                };
            }

            item.Id = id;
            item.Wishlist = await _context.Wishlists.FindAsync(item.WishlistId);
            item.Product = await _context.Products.FindAsync(item.ProductId);
            _context.Entry(prevItem).CurrentValues.SetValues(item);

            var entity = _context.Entry(prevItem);
            if (entity.State == EntityState.Modified)
            {
                var dto = _mapper.Map<WishlistItemDto>(item);
                return new ResponseDto
                {
                    StatusCode = 200,
                    IsSucceeded = true,
                    Model = dto
                };
            }
            return new ResponseDto
            {
                StatusCode = 400,
                IsSucceeded = false,
                Message = "Failed to edit this item."
            };
        }

        public async Task<ResponseDto> DeleteWishlistItem(int id)
        {
            var item = await _context.WishlistItems.Where(i => i.Id == id)
                .Include(i => i.Wishlist).Include(i => i.Product).FirstOrDefaultAsync();
            if (item == null)
            {
                return new ResponseDto
                {
                    StatusCode = 404,
                    IsSucceeded = true,
                    Message = "Item not found."
                };
            }

            _context.Remove(item);
            var entity = _context.Entry(item);
            if (entity.State == EntityState.Deleted)
            {
                var dto = _mapper.Map<WishlistItemDto>(item);
                return new ResponseDto
                {
                    StatusCode = 200,
                    IsSucceeded = true,
                    Model = dto
                };
            }
            return new ResponseDto
            {
                StatusCode = 400,
                IsSucceeded = false,
                Message = "Failed to delete this item."
            };
        }
    }
}
