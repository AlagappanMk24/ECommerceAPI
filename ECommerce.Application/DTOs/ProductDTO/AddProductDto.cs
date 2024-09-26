using Microsoft.AspNetCore.Http;

namespace ECommerce.Application.DTOs.ProductDTO
{
    public class AddProductDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public int StockAmount { get; set; }
        public IFormFile? ImageFile { get; set; }
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }

        public double? DiscountPercentage { get; set; }
        public double? AfterDiscount { get; }
    }
}
