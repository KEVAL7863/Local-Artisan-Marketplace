using System.ComponentModel.DataAnnotations;

namespace Local_Artisan_MarketPlace.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;
        [MaxLength(500)]
        public string? ShortDescription { get; set; }
        public decimal Price { get; set; }
        [MaxLength(200)]
        public string? ImageUrl { get; set; }
        [MaxLength(500)]
        public string? ThumbnailUrls { get; set; }
        public int StockQuantity { get; set; } = 0;
        [MaxLength(100)]
        public string? Dimensions { get; set; }
        [MaxLength(50)]
        public string? Collection { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public string? ArtisanId { get; set; }
        public ApplicationUser? Artisan { get; set; }
        public double Rating { get; set; } = 0;
        public int ReviewCount { get; set; } = 0;
        public bool IsSustainable { get; set; }
        public bool IsHandSigned { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<ProductReview> Reviews { get; set; } = new();
    }
}

