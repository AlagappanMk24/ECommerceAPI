using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ECommerce.Domain.Models.Entities
{
    public class WishlistItem
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int? WishlistId { get; set; }
        public int? ProductId { get; set; }

        [JsonIgnore]
        [ForeignKey("WishlistId")]
        public virtual Wishlist? Wishlist { get; set; }
        [JsonIgnore]
        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }
    }
}
