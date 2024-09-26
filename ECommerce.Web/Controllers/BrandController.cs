using ECommerce.Application.Contracts.Persistence;
using ECommerce.Application.DTOs.BrandDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController(IUnitOfWork unitOfWork) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [HttpGet("BrandId/{id}")]
        public async Task<IActionResult> GetBrandById(int id)
        {
            if (ModelState.IsValid)
            {
                var response = await _unitOfWork.Brands.GetBrandById(id);
                if (response.IsSucceeded)
                    return Ok(response);
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpGet("BrandName/{name}")]
        public async Task<IActionResult> GetBrandByName(string name)
        {
            if (ModelState.IsValid)
            {
                var response = await _unitOfWork.Brands.GetBrandByName(name);
                if (response.IsSucceeded)
                    return Ok(response);
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpGet("Brands")]
        public async Task<IActionResult> GetAllBrands()
        {
            if (ModelState.IsValid)
            {
                var response = await _unitOfWork.Brands.GetAllBrands();
                if (response.IsSucceeded)
                    return Ok(response);
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("AddBrand")]
        public async Task<IActionResult> AddBrand(BrandDto dto)
        {
            if (ModelState.IsValid)
            {
                var response = await _unitOfWork.Brands.AddBrand(dto);
                if (response.IsSucceeded)
                {
                    await _unitOfWork.Save();
                    return Ok(response);
                }
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("UpdateBrand")]
        public async Task<IActionResult> EditBrand(int id, BrandDto dto)
        {
            if (ModelState.IsValid)
            {
                var response = await _unitOfWork.Brands.UpdateBrand(id, dto);
                if (response.IsSucceeded)
                {
                    await _unitOfWork.Save();
                    return Ok(response);
                }
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("DeleteBrand")]
        public async Task<IActionResult> RemoveBrand(int id)
        {
            if (ModelState.IsValid)
            {
                var response = await _unitOfWork.Brands.DeleteBrand(id);
                if (response.IsSucceeded)
                {
                    await _unitOfWork.Save();
                    return Ok(response);
                }
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }
    }
}
