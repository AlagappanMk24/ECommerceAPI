using ECommerce.Application.Contracts.Persistence;
using ECommerce.Application.DTOs.CategoryDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(IUnitOfWork unitOfWork, ILogger<CategoryController> logger) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<CategoryController> _logger = logger;

        [HttpGet("Categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var response = await _unitOfWork.Categories.GetAllCategories();
                if (response.IsSucceeded)
                    return Ok(response.Model);

                return StatusCode(response.StatusCode, response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all categories.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("Id/{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var response = await _unitOfWork.Categories.GetCategoryById(id);
                if (response.IsSucceeded)
                    return Ok(response.Model);

                return StatusCode(response.StatusCode, response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving category with id: {Id}", id);
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("Name/{name}")]
        public async Task<IActionResult> GetCategoryByName(string name)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var response = await _unitOfWork.Categories.GetCategoryByName(name);
                if (response.IsSucceeded)
                    return Ok(response.Model);

                return StatusCode(response.StatusCode, response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving category with name: {Name}", name);
                return StatusCode(500, "Internal server error.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory(CategoryDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var response = await _unitOfWork.Categories.AddCategory(dto);
                if (response.IsSucceeded)
                {
                    await _unitOfWork.Save();
                    return Ok(response.Model);
                }

                return StatusCode(response.StatusCode, response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new category.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateCategory")]
        public async Task<IActionResult> EditCategory(int id, CategoryDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var response = await _unitOfWork.Categories.UpdateCategory(id, dto);
                if (response.IsSucceeded)
                {
                    await _unitOfWork.Save();
                    return Ok(response.Model);
                }

                return StatusCode(response.StatusCode, response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating category with id: {Id}", id);
                return StatusCode(500, "Internal server error.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var response = await _unitOfWork.Categories.DeleteCategory(id);
                if (response.IsSucceeded)
                {
                    await _unitOfWork.Save();
                    return Ok(response.Model);
                }

                return StatusCode(response.StatusCode, response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting category with id: {Id}", id);
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}
