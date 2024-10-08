﻿using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Domain.Models.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string? Status { get; set; }
        public int? TrackNumber { get; set; }
        public string? ShippingAddress { get; set; }
        public string? ShippingMethod { get; set; }
        public DateTime? ShippingDate { get; set; }
        public DateTime? DeliverDate { get; set; }
        public double ShippingCost { get; set; }
        public string? CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual ApplicationUser Customer { get; set; }
        public virtual ICollection<OrderItem>? orderItems { get; set; } = new List<OrderItem>();

    }
}
