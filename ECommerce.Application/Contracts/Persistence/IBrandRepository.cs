using ECommerce.Application.DTOs.BrandDTO;
using ECommerce.Application.DTOs;
using ECommerce.Domain.Models.Entities;

namespace ECommerce.Application.Contracts.Persistence
{
    public interface IBrandRepository : IGenericRepository<Brand>
    {
        public Task<ResponseDto> GetAllBrands();
        public Task<ResponseDto> GetBrandById(int id);
        public Task<ResponseDto> GetBrandByName(string name);
        public Task<ResponseDto> AddBrand(BrandDto dto);
        public Task<ResponseDto> UpdateBrand(int id, BrandDto dto);
        public Task<ResponseDto> DeleteBrand(int id);
    }
}
