using Local_Artisan_MarketPlace.Data;
using Local_Artisan_MarketPlace.Models;
using Local_Artisan_MarketPlace.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Local_Artisan_MarketPlace.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly CartService _cartService;
        private readonly ApplicationDbContext _context;

        public CheckoutController(CartService cartService, ApplicationDbContext context)
        {
            _cartService = cartService;
            _context = context;
        }

        public IActionResult Shipping()
        {
            var cart = _cartService.GetCart();
            if (cart.Count == 0) return RedirectToAction("Index", "Cart");
            ViewBag.Cart = cart;
            ViewBag.Subtotal = _cartService.GetCartSubtotal();
            ViewBag.Shipping = 0;
            ViewBag.Tax = 0;
            ViewBag.Total = _cartService.GetCartSubtotal();
            return View(new ShippingViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Shipping(ShippingViewModel model)
        {
            var cart = _cartService.GetCart();
            if (cart.Count == 0) return RedirectToAction("Index", "Cart");
            if (ModelState.IsValid)
            {
                TempData["Shipping"] = System.Text.Json.JsonSerializer.Serialize(model);
                return RedirectToAction("Payment");
            }
            ViewBag.Cart = cart;
            ViewBag.Subtotal = _cartService.GetCartSubtotal();
            return View(model);
        }

        public IActionResult Payment()
        {
            if (TempData["Shipping"] == null) return RedirectToAction("Shipping");
            TempData.Keep("Shipping");
            var shippingData = System.Text.Json.JsonSerializer.Deserialize<ShippingViewModel>(TempData["Shipping"]!.ToString()!);
            var cart = _cartService.GetCart();
            if (cart.Count == 0) return RedirectToAction("Index", "Cart");
            var subtotal = _cartService.GetCartSubtotal();
            var shipping = shippingData?.ShippingMethod == "Express" ? 250 : 0;
            var tax = Math.Round((subtotal + shipping) * 0.065m, 2);
            ViewBag.Cart = cart;
            ViewBag.Subtotal = subtotal;
            ViewBag.Shipping = shipping;
            ViewBag.Tax = tax;
            ViewBag.Total = subtotal + shipping + tax;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceOrder(PaymentViewModel model)
        {
            if (TempData["Shipping"] == null) return RedirectToAction("Shipping");
            var shippingData = System.Text.Json.JsonSerializer.Deserialize<ShippingViewModel>(TempData["Shipping"]!.ToString()!);
            if (shippingData == null) return RedirectToAction("Shipping");
            var cart = _cartService.GetCart();
            if (cart.Count == 0) return RedirectToAction("Index", "Cart");
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var subtotal = _cartService.GetCartSubtotal();
            var shippingCost = shippingData.ShippingMethod == "Express" ? 250 : 0;
            var tax = Math.Round((subtotal + shippingCost) * 0.065m, 2);
            var total = subtotal + shippingCost + tax;
            var order = new Order
            {
                OrderNumber = "AM" + DateTime.UtcNow.Ticks,
                UserId = userId,
                Subtotal = subtotal,
                ShippingCost = shippingCost,
                Tax = tax,
                Total = total,
                Status = "Confirmed",
                ShippingMethod = shippingData.ShippingMethod,
                PaymentMethod = model.PaymentMethod,
                FirstName = shippingData.FirstName,
                LastName = shippingData.LastName,
                Address = shippingData.Address,
                City = shippingData.City,
                State = shippingData.State,
                ZipCode = shippingData.ZipCode,
                Phone = shippingData.Phone
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            foreach (var item in cart)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                _context.OrderItems.Add(new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    ProductImageUrl = item.ProductImageUrl,
                    Quantity = item.Quantity,
                    UnitPrice = item.Price,
                    TotalPrice = item.TotalPrice
                });
                if (product != null)
                {
                    product.StockQuantity -= item.Quantity;
                }
            }
            await _context.SaveChangesAsync();
            _cartService.ClearCart();
            TempData.Remove("Shipping");
            return RedirectToAction("OrderConfirmation", new { id = order.Id });
        }

        public IActionResult OrderConfirmation(int id)
        {
            return View(id);
        }
    }

    public class ShippingViewModel
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string ShippingMethod { get; set; } = "Standard";
    }

    public class PaymentViewModel
    {
        public string PaymentMethod { get; set; } = "Card";
    }
}

