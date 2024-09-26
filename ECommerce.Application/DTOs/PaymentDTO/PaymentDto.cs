namespace ECommerce.Application.DTOs.PaymentDTO
{
    public class PaymentDto
    {
        public string Currency { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public double Amount { get; set; }
    }
}
