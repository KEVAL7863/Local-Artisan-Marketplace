using System.ComponentModel.DataAnnotations;

namespace Local_Artisan_MarketPlace.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? Slug { get; set; }
        public List<Product> Products { get; set; } = new();
    }
}

