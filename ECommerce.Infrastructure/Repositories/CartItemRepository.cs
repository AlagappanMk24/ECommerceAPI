﻿using AutoMapper;
using ECommerce.Application.DTOs.CartDTO;
using ECommerce.Application.DTOs;
using ECommerce.Domain.Models.Entities;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ECommerce.Application.Contracts.Persistence;

namespace ECommerce.Infrastructure.Repositories
{
    public class CartItemRepository : GenericRepository<CartItem>, ICartItemRepository
    {
        private readonly IMapper _mapper;

        public CartItemRepository(ECommerceDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<ResponseDto> GetAllCartsItems()
        {
            var items = await _context.CartItems.AsNoTracking()
                .Include(c => c.Product).ToListAsync();
            if (items != null && items.Count > 0)
            {
                var dto = _mapper.Map<List<CartItemDto>>(items);
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
                Message = "There is no items yet."
            };
        }

        public async Task<ResponseDto> GetCartItem(int id)
        {
            var item = await _context.CartItems.Where(c => c.Id == id)
                .Include(c => c.Product).FirstOrDefaultAsync();
            if (item != null)
            {
                var dto = _mapper.Map<CartItemDto>(item);
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

        public async Task<ResponseDto> GetItemsInCart(int cartId)
        {
            var items = await _context.CartItems.Where(c => c.CartId == cartId).ToListAsync();

            if (items != null && items.Count > 0)
            {
                var dto = _mapper.Map<List<CartItemDto>>(items);
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
                Message = "There is no items in this cart."
            };
        }

        public async Task<ResponseDto> AddItemToCart(CartItem item)
        {
            if (!_context.Carts.Any(c => c.Id == item.CartId))
            {
                return new ResponseDto
                {
                    StatusCode = 404,
                    IsSucceeded = false,
                    Message = "The cart you try to add item for, is not exist."
                };
            }

            if (!_context.Products.Any(c => c.Id == item.ProductId))
            {
                return new ResponseDto
                {
                    StatusCode = 404,
                    IsSucceeded = false,
                    Message = "The product you try to add is not exist."
                };
            }

            item.Cart = await _context.Carts.FindAsync(item.CartId);
            item.Product = await _context.Products.FindAsync(item.ProductId);
            await _context.AddAsync(item);
            var entity = _context.Entry(item);

            if (entity.State != EntityState.Added)
            {
                return new ResponseDto
                {
                    StatusCode = 400,
                    IsSucceeded = false,
                    Message = "Failed to add item."
                };
            }

            return new ResponseDto
            {
                StatusCode = 200,
                IsSucceeded = true,
                Model = item
            };
        }

        public async Task<ResponseDto> UpdateCartItem(int id, CartItem item)
        {
            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem == null)
            {
                return new ResponseDto
                {
                    StatusCode = 404,
                    IsSucceeded = false,
                    Message = "Cart Item not found."
                };
            }

            item.Id = id;
            item.CartId = cartItem.CartId;
            item.ProductId = cartItem.ProductId;
            _context.Entry(cartItem).CurrentValues.SetValues(item);

            var entity = _context.Entry(cartItem);
            if (entity.State == EntityState.Modified)
            {
                return new ResponseDto
                {
                    StatusCode = 200,
                    IsSucceeded = true,
                    Model = item
                };
            }

            return new ResponseDto
            {
                StatusCode = 400,
                IsSucceeded = false,
                Message = "Failed to edit this cart item."
            };
        }

        public async Task<ResponseDto> DeleteItemFromCart(int id)
        {
            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem == null)
            {
                return new ResponseDto
                {
                    StatusCode = 404,
                    IsSucceeded = false,
                    Message = "Cart Item not found."
                };
            }

            _context.Remove(cartItem);
            var entity = _context.Entry(cartItem);
            if (entity.State == EntityState.Deleted)
            {
                return new ResponseDto
                {
                    StatusCode = 200,
                    IsSucceeded = true,
                    Model = cartItem
                };
            }

            return new ResponseDto
            {
                StatusCode = 400,
                IsSucceeded = false,
                Message = "Failed to delete this cart item."
            };
        }
    }
}
