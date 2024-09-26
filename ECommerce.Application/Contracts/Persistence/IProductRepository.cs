using ECommerce.Application.DTOs.ProductDTO;
using ECommerce.Application.DTOs;
using ECommerce.Domain.Models.Entities;

namespace ECommerce.Application.Contracts.Persistence
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public Task<ResponseDto> GetAllProducts();
        public Task<ResponseDto> GetProductById(int id);
        public Task<ResponseDto> GetProductByName(string name);
        public Task<ResponseDto> GetProductsByCategoryId(int idd);
        public Task<ResponseDto> GetProductsByCategoryName(string name);
        public Task<ResponseDto> GetProductsByBrandId(int id);
        public Task<ResponseDto> GetProductsByBrandName(string name);
        public Task<ResponseDto> AddProductAsync(AddProductDto dto);
        public Task<ResponseDto> DeleteProductAsync(int id);
        public Task<ResponseDto> UpdateProductAsync(int id, AddProductDto dto);
    }
}
