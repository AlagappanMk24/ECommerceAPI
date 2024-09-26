using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Domain.Models.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }

        [ForeignKey("ParentCategoryId")]
        public Category? ParentCategory { get; set; }
        public virtual ICollection<Product>? Products { get; set; } = new List<Product>();
    }
}
