using AutoMapper.Configuration.Annotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ECommerce.Domain.Models.Entities
{
    public class OrderItem
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int Quantity { get; set; }
        [Ignore]
        public double TotalPrice { get; set; }
        public double? Discount { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }

        [JsonIgnore]
        [ForeignKey("OrderId")]
        public virtual Order? Order { get; set; }
        [JsonIgnore]
        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }
    }
}
