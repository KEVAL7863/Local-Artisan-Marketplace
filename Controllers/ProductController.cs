using Local_Artisan_Marketplace.Data;
using Local_Artisan_Marketplace.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Local_Artisan_Marketplace.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? category, string? search, string? sortBy, decimal? minPrice, decimal? maxPrice)
        {
            var query = _context.Products
                .Where(p => p.IsActive && !p.IsDraft)
                .Include(p => p.Artisan)
                .AsQueryable();

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(p => p.Category == category);
            }

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Title.Contains(search) || (p.Description != null && p.Description.Contains(search)));
            }

            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
            }

            query = sortBy switch
            {
                "price_asc" => query.OrderBy(p => p.Price),
                "price_desc" => query.OrderByDescending(p => p.Price),
                "newest" => query.OrderByDescending(p => p.CreatedAt),
                _ => query.OrderByDescending(p => p.CreatedAt)
            };

            var products = await query.ToListAsync();

            // Get categories for sidebar
            var categories = await _context.Products
                .Where(p => p.IsActive && !p.IsDraft)
                .Select(p => p.Category)
                .Distinct()
                .ToListAsync();

            ViewBag.Categories = categories;
            ViewBag.CurrentCategory = category;
            ViewBag.CurrentSearch = search;
            ViewBag.CurrentSort = sortBy;
            ViewBag.TotalCount = products.Count;

            return View(products);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _context.Products
                .Include(p => p.Artisan)
                .FirstOrDefaultAsync(p => p.Id == id && p.IsActive);

            if (product == null)
                return NotFound();

            // Get related products
            var related = await _context.Products
                .Where(p => p.Category == product.Category && p.Id != product.Id && p.IsActive && !p.IsDraft)
                .Include(p => p.Artisan)
                .Take(4)
                .ToListAsync();

            ViewBag.RelatedProducts = related;

            return View(product);
        }
    }
}
