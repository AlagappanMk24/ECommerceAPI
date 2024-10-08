﻿namespace ECommerce.Domain.Models.Entities
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public ICollection<Product>? Products { get; set; } = new List<Product>();
    }
}
