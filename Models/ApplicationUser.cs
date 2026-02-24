using Microsoft.AspNetCore.Identity;

namespace Local_Artisan_MarketPlace.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public string AccountType { get; set; } = "Shopper"; // Shopper or Artisan
        public string? BusinessName { get; set; }
        public string? ArtisanType { get; set; }
    }
}

