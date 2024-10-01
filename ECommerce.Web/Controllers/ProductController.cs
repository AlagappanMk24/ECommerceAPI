using AutoMapper;
using ECommerce.Application.Contracts.Persistence;
using ECommerce.Application.DTOs.ProductDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ProductController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetAllProducts()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var products = await _unitOfWork.Products.GetAllProducts();
                if (products.IsSucceeded)
                    return Ok(products.Model);

                return StatusCode(products.StatusCode, products.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all products.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("Id/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var product = await _unitOfWork.Products.GetProductById(id);
                if (product.IsSucceeded)
                    return Ok(product.Model);

                return StatusCode(product.StatusCode, product.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving product with id: {Id}", id);
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("Name/{name}")]
        public async Task<IActionResult> GetProductByName(string name)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var product = await _unitOfWork.Products.GetProductByName(name);
                if (product.IsSucceeded)
                    return Ok(product.Model);

                return StatusCode(product.StatusCode, product.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving product with name: {Name}", name);
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("ByBrandId/{id}")]
        public async Task<IActionResult> GetProductsByBrandId(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var products = await _unitOfWork.Products.GetProductsByBrandId(id);
                if (products.IsSucceeded)
                    return Ok(products.Model);

                return StatusCode(products.StatusCode, products.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving products by brand id: {Id}", id);
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("ByBrandName/{name}")]
        public async Task<IActionResult> GetProductsByBrandName(string name)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var products = await _unitOfWork.Products.GetProductsByBrandName(name);
                if (products.IsSucceeded)
                    return Ok(products.Model);

                return StatusCode(products.StatusCode, products.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving products by brand name: {Name}", name);
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("ByCategoryId/{id}")]
        public async Task<IActionResult> GetProductsByCategoryId(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var products = await _unitOfWork.Products.GetProductsByCategoryId(id);
                if (products.IsSucceeded)
                    return Ok(products.Model);

                return StatusCode(products.StatusCode, products.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving products by category id: {Id}", id);
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("ByCategoryName/{name}")]
        public async Task<IActionResult> GetProductsByCategoryName(string name)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var products = await _unitOfWork.Products.GetProductsByCategoryName(name);
                if (products.IsSucceeded)
                    return Ok(products.Model);

                return StatusCode(products.StatusCode, products.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving products by category name: {Name}", name);
                return StatusCode(500, "Internal server error.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromForm] AddProductDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var product = await _unitOfWork.Products.AddProductAsync(dto);
                if (product.IsSucceeded)
                {
                    await _unitOfWork.Save();
                    return StatusCode(product.StatusCode, product.Model);
                }

                return StatusCode(product.StatusCode, product.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a product.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var product = await _unitOfWork.Products.DeleteProductAsync(id);
                if (product.IsSucceeded)
                {
                    await _unitOfWork.Save();
                    return StatusCode(product.StatusCode, product.Model);
                }

                return StatusCode(product.StatusCode, product.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting product with id: {Id}", id);
                return StatusCode(500, "Internal server error.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] AddProductDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var response = await _unitOfWork.Products.UpdateProductAsync(id, dto);
                if (response.IsSucceeded)
                {
                    await _unitOfWork.Save();
                    return StatusCode(response.StatusCode, response.Model);
                }

                return StatusCode(response.StatusCode, response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating product with id: {Id}", id);
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}
