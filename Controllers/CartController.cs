using Local_Artisan_MarketPlace.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Local_Artisan_MarketPlace.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly CartService _cartService;
        private readonly Data.ApplicationDbContext _context;

        public CartController(CartService cartService, Data.ApplicationDbContext context)
        {
            _cartService = cartService;
            _context = context;
        }

        public IActionResult Index()
        {
            var cart = _cartService.GetCart();
            var subtotal = _cartService.GetCartSubtotal();
            var shipping = subtotal >= 1000 ? 0 : 250;
            var tax = Math.Round((subtotal + shipping) * 0.065m, 2);
            var total = subtotal + shipping + tax;
            ViewBag.Subtotal = subtotal;
            ViewBag.Shipping = shipping;
            ViewBag.Tax = tax;
            ViewBag.Total = total;
            ViewBag.RelatedProducts = _context.Products.Include(p => p.Category).OrderByDescending(p => p.CreatedAt).Take(4).ToList();
            return View(cart);
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int productId, int quantity)
        {
            _cartService.UpdateQuantity(productId, quantity);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Remove(int productId)
        {
            _cartService.RemoveFromCart(productId);
            return RedirectToAction("Index");
        }
    }
}

