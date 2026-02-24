using System.ComponentModel.DataAnnotations;

namespace Local_Artisan_MarketPlace.Models
{
    public class ProductReview
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser? User { get; set; }
        [MaxLength(100)]
        public string ReviewerName { get; set; } = string.Empty;
        public int Rating { get; set; }
        [MaxLength(1000)]
        public string Comment { get; set; } = string.Empty;
        public bool IsVerifiedBuyer { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

