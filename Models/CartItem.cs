using System.Text.Json.Serialization;

namespace Local_Artisan_MarketPlace.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string? ProductImageUrl { get; set; }
        public string? ShortDescription { get; set; }
        public string? CategoryName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        [JsonIgnore]
        public decimal TotalPrice => Price * Quantity;
    }
}

