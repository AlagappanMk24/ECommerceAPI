using ECommerce.Application.DTOs.CategoryDTO;
using ECommerce.Application.DTOs;
using ECommerce.Domain.Models.Entities;

namespace ECommerce.Application.Contracts.Persistence
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        public Task<ResponseDto> GetAllCategories();
        public Task<ResponseDto> GetCategoryById(int id);
        public Task<ResponseDto> GetCategoryByName(string name);
        public Task<ResponseDto> AddCategory(CategoryDto dto);
        public Task<ResponseDto> UpdateCategory(int id, CategoryDto dto);
        public Task<ResponseDto> DeleteCategory(int id);
    }
}
