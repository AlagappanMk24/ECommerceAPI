using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTOs.ReviewDTO
{
    public class ReviewDto
    {
        public int Rate { get; set; }
        [MaxLength(250)]
        public string Comment { get; set; }
        public string ProductName { get; set; }
    }
}
