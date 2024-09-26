using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ECommerce.Domain.Models.Entities
{
    public class Review
    {

        [JsonIgnore]
        public int Id { get; set; }
        public int Rate { get; set; }
        [MaxLength(250)]
        public string Comment { get; set; }
        [JsonIgnore]
        public DateTime Date { get; set; } = DateTime.UtcNow;
        [JsonIgnore]
        public string? CustomerId { get; set; }
        public int? ProductId { get; set; }

        [JsonIgnore]
        [ForeignKey("CustomerId")]
        public virtual ApplicationUser? Customer { get; set; }
        [JsonIgnore]
        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }
    }
}
