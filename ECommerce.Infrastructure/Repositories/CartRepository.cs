﻿using AutoMapper;
using ECommerce.Application.Contracts.Persistence;
using ECommerce.Application.DTOs.CartDTO;
using ECommerce.Application.DTOs;
using ECommerce.Domain.Models;
using ECommerce.Domain.Models.Entities;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        private readonly IMapper _mapper;
        public CartRepository(ECommerceDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<ResponseDto> GetAllCarts()
        {
            var carts = await _context.Carts.AsNoTracking().ToListAsync();
            if (carts != null && carts.Count > 0)
            {
                return new ResponseDto
                {
                    StatusCode = 200,
                    IsSucceeded = true,
                    Model = carts
                };
            }

            return new ResponseDto
            {
                StatusCode = 400,
                IsSucceeded = false,
                Message = "There is no Carts!"
            };
        }

        public async Task<ResponseDto> GetCart(int id)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart != null)
            {
                return new ResponseDto
                {
                    StatusCode = 200,
                    IsSucceeded = true,
                    Model = cart
                };
            }

            return new ResponseDto
            {
                StatusCode = 400,
                IsSucceeded = false,
                Message = "This cat is not exist."
            };
        }

        public async Task<ResponseDto> AddCart(ApplicationUser currentUser)
        {
            CartDto dto = new()
            {
                CustomerId = currentUser.Id
            };
            var cart = _mapper.Map<Cart>(dto);

            await _context.Carts.AddAsync(cart);
            var entity = _context.Entry(cart);

            if (entity.State == EntityState.Added)
            {
                return new ResponseDto
                {
                    StatusCode = 200,
                    IsSucceeded = true,
                    Model = cart
                };
            }

            return new ResponseDto
            {
                StatusCode = 400,
                IsSucceeded = false,
                Message = "Cart cant be added."
            };
        }
        public async Task<ResponseDto> DeleteCart(int id)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart != null)
            {
                _context.Remove(cart);
                var entity = _context.Entry(cart);
                if (entity.State == EntityState.Deleted)
                {
                    return new ResponseDto
                    {
                        StatusCode = 200,
                        IsSucceeded = true,
                        Model = cart,
                        Message = "Cart deleted successfully."
                    };
                }

                return new ResponseDto
                {
                    StatusCode = 400,
                    IsSucceeded = false,
                    Message = "Cart cant be deleted, try again."
                };
            }

            return new ResponseDto
            {
                StatusCode = 400,
                IsSucceeded = false,
                Message = "Cart is not exist!"
            };
        }
    }
}
