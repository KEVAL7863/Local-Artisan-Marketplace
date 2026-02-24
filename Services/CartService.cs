using Local_Artisan_MarketPlace.Data;
using Local_Artisan_MarketPlace.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Local_Artisan_MarketPlace.Services
{
    public class CartService
    {
        private const string CartSessionKey = "ShoppingCart";
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;

        public CartService(IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        private List<CartItem> GetCartFromSession()
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            if (session == null) return new List<CartItem>();
            var cartJson = session.GetString(CartSessionKey);
            return string.IsNullOrEmpty(cartJson) ? new List<CartItem>() : JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new List<CartItem>();
        }

        private void SaveCartToSession(List<CartItem> cart)
        {
            _httpContextAccessor.HttpContext?.Session.SetString(CartSessionKey, JsonSerializer.Serialize(cart));
        }

        public List<CartItem> GetCart()
        {
            var cart = GetCartFromSession();
            foreach (var item in cart)
            {
                var product = _context.Products.Find(item.ProductId);
                if (product != null)
                {
                    item.ProductName = product.Name;
                    item.ProductImageUrl = product.ImageUrl;
                    item.ShortDescription = product.ShortDescription;
                    item.Price = product.Price;
                    item.CategoryName = product.Category?.Name;
                }
            }
            return cart;
        }

        public void AddToCart(int productId, int quantity = 1)
        {
            var product = _context.Products.Include(p => p.Category).FirstOrDefault(p => p.Id == productId);
            if (product == null) return;
            var cart = GetCartFromSession();
            var existing = cart.FirstOrDefault(c => c.ProductId == productId);
            if (existing != null)
                existing.Quantity += quantity;
            else
                cart.Add(new CartItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    ProductImageUrl = product.ImageUrl,
                    ShortDescription = product.ShortDescription,
                    CategoryName = product.Category?.Name,
                    Price = product.Price,
                    Quantity = quantity
                });
            SaveCartToSession(cart);
        }

        public void UpdateQuantity(int productId, int quantity)
        {
            var cart = GetCartFromSession();
            var item = cart.FirstOrDefault(c => c.ProductId == productId);
            if (item != null)
            {
                if (quantity <= 0)
                    cart.Remove(item);
                else
                    item.Quantity = quantity;
                SaveCartToSession(cart);
            }
        }

        public void RemoveFromCart(int productId)
        {
            var cart = GetCartFromSession();
            cart.RemoveAll(c => c.ProductId == productId);
            SaveCartToSession(cart);
        }

        public int GetCartCount()
        {
            return GetCartFromSession().Sum(c => c.Quantity);
        }

        public decimal GetCartSubtotal()
        {
            return GetCart().Sum(c => c.TotalPrice);
        }

        public void ClearCart()
        {
            SaveCartToSession(new List<CartItem>());
        }
    }
}

