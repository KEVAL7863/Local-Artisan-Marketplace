using Local_Artisan_MarketPlace.Data;
using Local_Artisan_MarketPlace.Models;
using Local_Artisan_MarketPlace.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Local_Artisan_MarketPlace.Controllers
{
    [Authorize]
    public class ShopController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CartService _cartService;

        public ShopController(ApplicationDbContext context, CartService cartService)
        {
            _context = context;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index(string? search, int? categoryId)
        {
            var query = _context.Products.Include(p => p.Category).AsQueryable();
            if (!string.IsNullOrEmpty(search))
                query = query.Where(p => p.Name.Contains(search) || (p.Description != null && p.Description.Contains(search)));
            if (categoryId.HasValue)
                query = query.Where(p => p.CategoryId == categoryId.Value);
            var products = await query.ToListAsync();
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.CartCount = _cartService.GetCartCount();
            return View(products);
        }

        public async Task<IActionResult> Product(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Artisan)
                .Include(p => p.Reviews)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return NotFound();
            var related = await _context.Products.Include(p => p.Category)
                .Where(p => p.Id != id && (p.CategoryId == product.CategoryId || p.Collection == product.Collection))
                .Take(4).ToListAsync();
            ViewBag.RelatedProducts = related;
            ViewBag.CartCount = _cartService.GetCartCount();
            return View(product);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity = 1)
        {
            _cartService.AddToCart(productId, quantity);
            return RedirectToAction("Product", new { id = productId });
        }
    }
}

