using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ECommerce.Domain.Models.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }

        [JsonIgnore]
        [ForeignKey("CustomerId")]
        public virtual ApplicationUser Customer { get; set; }
        [JsonIgnore]
        public virtual ICollection<CartItem>? cartItems { get; set; } = new List<CartItem>();
    }
}
