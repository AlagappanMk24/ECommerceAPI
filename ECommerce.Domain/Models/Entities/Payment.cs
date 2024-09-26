using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Domain.Models.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Method { get; set; }
        public string Currency { get; set; }
        public int Quantity { get; set; }
        public string? Description { get; set; }
        public double Amount { get; set; }
        public string CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual ApplicationUser Customer { get; set; }
    }
}
