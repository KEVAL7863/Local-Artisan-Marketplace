using Local_Artisan_Marketplace.Data;
using Local_Artisan_Marketplace.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Local_Artisan_Marketplace.Controllers
{
    [Authorize(Roles = "Artisan")]
    public class ArtisanController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ArtisanController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        private int GetCurrentUserId() => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        public async Task<IActionResult> Dashboard()
        {
            var userId = GetCurrentUserId();
            var user = await _context.Users.FindAsync(userId);

            var totalProducts = await _context.Products.CountAsync(p => p.ArtisanId == userId);

            var totalOrders = await _context.OrderItems
                .Include(oi => oi.Product)
                .CountAsync(oi => oi.Product.ArtisanId == userId);

            var totalRevenue = await _context.OrderItems
                .Include(oi => oi.Product)
                .Include(oi => oi.Order)
                .Where(oi => oi.Product.ArtisanId == userId && oi.Order.Status != "Pending")
                .SumAsync(oi => (decimal?)(oi.UnitPrice * oi.Quantity)) ?? 0;

            ViewBag.ArtisanName = user?.FullName ?? "Artisan";
            ViewBag.TotalProducts = totalProducts;
            ViewBag.TotalOrders = totalOrders;
            ViewBag.TotalRevenue = totalRevenue;

            var recentOrders = await _context.OrderItems
                .Include(oi => oi.Product)
                .Include(oi => oi.Order)
                    .ThenInclude(o => o.Customer)
                .Where(oi => oi.Product.ArtisanId == userId)
                .OrderByDescending(oi => oi.Order.OrderDate)
                .Take(5)
                .ToListAsync();

            ViewBag.RecentOrders = recentOrders;

            return View();
        }

        public async Task<IActionResult> MyProducts()
        {
            var userId = GetCurrentUserId();

            var products = await _context.Products
                .Where(p => p.ArtisanId == userId)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

            return View(products);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct(string title, string? description, decimal price, string category, int stockQuantity, IFormFile? imageFile, string? imageUrl)
        {
            var userId = GetCurrentUserId();

            if (string.IsNullOrEmpty(title) || price <= 0)
            {
                TempData["Error"] = "Product title and a valid price are required.";
                return View();
            }

            string? finalImageUrl = imageUrl;
            if (imageFile != null && imageFile.Length > 0)
            {
                finalImageUrl = await SaveImage(imageFile);
            }

            var product = new Product
            {
                Title = title,
                Description = description,
                Price = price,
                Category = category ?? "Other",
                StockQuantity = stockQuantity,
                ImageUrl = finalImageUrl,
                IsActive = true,
                IsDraft = false,
                CreatedAt = DateTime.UtcNow,
                ArtisanId = userId
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Product added successfully!";
            return RedirectToAction("MyProducts");
        }

        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {
            var userId = GetCurrentUserId();
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id && p.ArtisanId == userId);

            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(int id, string title, string? description, decimal price, string category, int stockQuantity, IFormFile? imageFile, string? imageUrl, bool isActive)
        {
            var userId = GetCurrentUserId();
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id && p.ArtisanId == userId);

            if (product == null)
                return NotFound();

            if (imageFile != null && imageFile.Length > 0)
            {
                // Delete old local image if it exists and was a local file
                if (!string.IsNullOrEmpty(product.ImageUrl) && product.ImageUrl.StartsWith("/images/products/"))
                {
                    var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, product.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                product.ImageUrl = await SaveImage(imageFile);
            }
            else if (!string.IsNullOrEmpty(imageUrl))
            {
                // If a URL is provided, use it
                product.ImageUrl = imageUrl;
            }

            product.Title = title;
            product.Description = description;
            product.Price = price;
            product.Category = category;
            product.StockQuantity = stockQuantity;
            product.IsActive = isActive;

            await _context.SaveChangesAsync();

            TempData["Message"] = "Product updated successfully!";
            return RedirectToAction("MyProducts");
        }

        private async Task<string> SaveImage(IFormFile imageFile)
        {
            var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images", "products");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return "/images/products/" + uniqueFileName;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var userId = GetCurrentUserId();
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id && p.ArtisanId == userId);

            if (product == null)
                return NotFound();

            var hasOrders = await _context.OrderItems.AnyAsync(oi => oi.ProductId == id);
            if (hasOrders)
            {
                product.IsActive = false;
                product.IsDraft = true;
            }
            else
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();

            TempData["Message"] = "Product removed successfully!";
            return RedirectToAction("MyProducts");
        }

        public async Task<IActionResult> Orders(string? status)
        {
            var userId = GetCurrentUserId();

            var query = _context.OrderItems
                .Include(oi => oi.Product)
                .Include(oi => oi.Order)
                    .ThenInclude(o => o.Customer)
                .Where(oi => oi.Product.ArtisanId == userId);

            if (!string.IsNullOrEmpty(status))
                query = query.Where(oi => oi.Order.Status == status);

            var orders = await query
                .OrderByDescending(oi => oi.Order.OrderDate)
                .ToListAsync();

            ViewBag.CurrentStatus = status;
            return View(orders);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, string status)
        {
            var userId = GetCurrentUserId();

            var hasItems = await _context.OrderItems
                .Include(oi => oi.Product)
                .AnyAsync(oi => oi.OrderId == orderId && oi.Product.ArtisanId == userId);

            if (!hasItems)
                return NotFound();

            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.Status = status;
                await _context.SaveChangesAsync();
                TempData["Message"] = $"Order status updated to {status}.";
            }

            return RedirectToAction("Orders");
        }
    }
}
