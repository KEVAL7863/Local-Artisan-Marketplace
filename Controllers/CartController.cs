using Local_Artisan_Marketplace.Data;
using Local_Artisan_Marketplace.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Local_Artisan_Marketplace.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        private int GetCurrentUserId() => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        public async Task<IActionResult> Index()
        {
            var userId = GetCurrentUserId();

            var cartItems = await _context.CartItems
                .Where(c => c.UserId == userId)
                .Include(c => c.Product)
                    .ThenInclude(p => p.Artisan)
                .ToListAsync();

            return View(cartItems);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
        {
            var userId = GetCurrentUserId();

            var product = await _context.Products.FindAsync(productId);
            if (product == null || !product.IsActive)
                return NotFound();

            var existing = await _context.CartItems
                .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);

            if (existing != null)
            {
                existing.Quantity += quantity;
            }
            else
            {
                _context.CartItems.Add(new CartItem
                {
                    UserId = userId,
                    ProductId = productId,
                    Quantity = quantity,
                    AddedAt = DateTime.UtcNow
                });
            }

            await _context.SaveChangesAsync();
            TempData["Message"] = "Item added to cart!";

            var referer = Request.Headers["Referer"].ToString();
            if (!string.IsNullOrEmpty(referer))
                return Redirect(referer);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            var userId = GetCurrentUserId();
            var item = await _context.CartItems
                .FirstOrDefaultAsync(c => c.Id == cartItemId && c.UserId == userId);

            if (item != null)
            {
                _context.CartItems.Remove(item);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateQuantity(int cartItemId, int quantity)
        {
            var userId = GetCurrentUserId();
            var item = await _context.CartItems
                .FirstOrDefaultAsync(c => c.Id == cartItemId && c.UserId == userId);

            if (item != null)
            {
                if (quantity <= 0)
                {
                    _context.CartItems.Remove(item);
                }
                else
                {
                    item.Quantity = quantity;
                }
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Checkout()
        {
            var userId = GetCurrentUserId();

            var cartItems = await _context.CartItems
                .Where(c => c.UserId == userId)
                .Include(c => c.Product)
                    .ThenInclude(p => p.Artisan)
                .ToListAsync();

            if (!cartItems.Any())
                return RedirectToAction("Index");

            ViewBag.CartItems = cartItems;
            ViewBag.Subtotal = cartItems.Sum(c => c.Product.Price * c.Quantity);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceOrder(string firstName, string lastName, string address, string city, string state, string zipCode, string paymentMethod, string? paypalTransactionId)
        {
            var userId = GetCurrentUserId();

            var cartItems = await _context.CartItems
                .Where(c => c.UserId == userId)
                .Include(c => c.Product)
                .ToListAsync();

            if (!cartItems.Any())
            {
                TempData["Error"] = "Your cart is empty.";
                return RedirectToAction("Index");
            }

            var subtotal = cartItems.Sum(c => c.Product.Price * c.Quantity);
            var shipping = subtotal >= 500 ? 0m : 50m;
            var tax = subtotal * 0.18m;
            var total = subtotal + shipping + tax;

            var order = new Order
            {
                OrderNumber = "ORD-" + DateTime.UtcNow.Ticks.ToString()[^8..],
                CustomerId = userId,
                OrderDate = DateTime.UtcNow,
                Subtotal = subtotal,
                ShippingCost = shipping,
                Tax = Math.Round(tax, 2),
                TotalAmount = Math.Round(total, 2),
                Status = paymentMethod == "PayPal" ? "Paid" : "Pending",
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                City = city,
                State = state,
                ZipCode = zipCode,
                PaymentMethod = paymentMethod ?? "Credit Card"
            };

            // In a real scenario, you'd verify the paypalTransactionId here
            if (paymentMethod == "PayPal" && !string.IsNullOrEmpty(paypalTransactionId))
            {
                // Verify payment with PayPal API
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            foreach (var cartItem in cartItems)
            {
                _context.OrderItems.Add(new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    UnitPrice = cartItem.Product.Price
                });

                cartItem.Product.StockQuantity = Math.Max(0, cartItem.Product.StockQuantity - cartItem.Quantity);
            }

            _context.CartItems.RemoveRange(cartItems);
            await _context.SaveChangesAsync();

            return RedirectToAction("OrderConfirmation", new { orderId = order.Id });
        }

        public async Task<IActionResult> OrderConfirmation(int orderId)
        {
            var userId = GetCurrentUserId();
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId && o.CustomerId == userId);

            if (order == null)
                return NotFound();

            return View(order);
        }
    }
}
