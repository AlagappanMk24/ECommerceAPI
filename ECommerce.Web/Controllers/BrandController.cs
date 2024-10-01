using ECommerce.Application.Contracts.Persistence;
using ECommerce.Application.DTOs.BrandDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController(IUnitOfWork unitOfWork, ILogger<BrandController> logger) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<BrandController> _logger = logger;

        [HttpGet("BrandId/{id}")]
        public async Task<IActionResult> GetBrandById(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var response = await _unitOfWork.Brands.GetBrandById(id);
                if (response.IsSucceeded)
                    return Ok(response);

                return StatusCode(response.StatusCode, response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting brand by id: {Id}", id);
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("BrandName/{name}")]
        public async Task<IActionResult> GetBrandByName(string name)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var response = await _unitOfWork.Brands.GetBrandByName(name);
                if (response.IsSucceeded)
                    return Ok(response);

                return StatusCode(response.StatusCode, response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting brand by name: {Name}", name);
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("Brands")]
        public async Task<IActionResult> GetAllBrands()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var response = await _unitOfWork.Brands.GetAllBrands();
                if (response.IsSucceeded)
                    return Ok(response);

                return StatusCode(response.StatusCode, response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all brands.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost("AddBrand")]
        public async Task<IActionResult> AddBrand(BrandDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var response = await _unitOfWork.Brands.AddBrand(dto);
                if (response.IsSucceeded)
                {
                    await _unitOfWork.Save();
                    return Ok(response);
                }

                return StatusCode(response.StatusCode, response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding brand.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPut("UpdateBrand")]
        public async Task<IActionResult> EditBrand(int id, BrandDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var response = await _unitOfWork.Brands.UpdateBrand(id, dto);
                if (response.IsSucceeded)
                {
                    await _unitOfWork.Save();
                    return Ok(response);
                }

                return StatusCode(response.StatusCode, response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating brand with id: {Id}", id);
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpDelete("DeleteBrand")]
        public async Task<IActionResult> RemoveBrand(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var response = await _unitOfWork.Brands.DeleteBrand(id);
                if (response.IsSucceeded)
                {
                    await _unitOfWork.Save();
                    return Ok(response);
                }

                return StatusCode(response.StatusCode, response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting brand with id: {Id}", id);
                return StatusCode(500, "Internal server error.");
            }
        }
    }

}
