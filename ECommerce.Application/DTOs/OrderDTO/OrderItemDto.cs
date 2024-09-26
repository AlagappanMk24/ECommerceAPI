namespace ECommerce.Application.DTOs.OrderDTO
{
    public class OrderItemDto
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
        public double? Discount { get; set; }
    }
}
