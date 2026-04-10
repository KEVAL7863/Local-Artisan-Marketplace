using System.ComponentModel.DataAnnotations;

namespace Local_Artisan_Marketplace.Models
{
    public class ApplicationUser
    {
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [StringLength(256)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [StringLength(50)]
        public string Role { get; set; } = "Collector"; // Collector, Artisan, Admin

        [StringLength(100)]
        public string? StudioName { get; set; }

        [StringLength(50)]
        public string? CraftType { get; set; }

        public bool IsApproved { get; set; } = false;

        public DateTime JoinedDate { get; set; } = DateTime.UtcNow;

        [StringLength(500)]
        public string? ProfileImageUrl { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
