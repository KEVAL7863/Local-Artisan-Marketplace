using Local_Artisan_Marketplace.Data;
using Local_Artisan_Marketplace.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Local_Artisan_Marketplace.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        private int GetCurrentUserId() => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        public async Task<IActionResult> Dashboard()
        {
            ViewBag.TotalUsers = await _context.Users.CountAsync();
            ViewBag.TotalArtisans = await _context.Users.CountAsync(u => u.Role == "Artisan");
            ViewBag.TotalProducts = await _context.Products.CountAsync(p => p.IsActive);
            ViewBag.TotalOrders = await _context.Orders.CountAsync();

            var pendingApps = await _context.ArtisanApplications.CountAsync(a => a.Status == "Pending");
            var pendingArtisanUsers = await _context.Users.CountAsync(u => u.Role == "Artisan" && !u.IsApproved);
            ViewBag.PendingApprovals = pendingApps + pendingArtisanUsers;

            ViewBag.TotalRevenue = await _context.Orders
                .Where(o => o.Status != "Pending")
                .SumAsync(o => (decimal?)o.TotalAmount) ?? 0;

            var recentOrders = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .OrderByDescending(o => o.OrderDate)
                .Take(10)
                .ToListAsync();

            ViewBag.RecentOrders = recentOrders;

            return View();
        }

        public async Task<IActionResult> ArtisanApproval()
        {
            var applications = await _context.ArtisanApplications
                .Include(a => a.Applicant)
                .OrderByDescending(a => a.SubmissionDate)
                .ToListAsync();

            var pendingArtisans = await _context.Users
                .Where(u => u.Role == "Artisan" && !u.IsApproved)
                .ToListAsync();

            ViewBag.PendingArtisans = pendingArtisans;
            ViewBag.PendingCount = applications.Count(a => a.Status == "Pending") + pendingArtisans.Count;
            ViewBag.ApprovedCount = applications.Count(a => a.Status == "Approved");

            return View(applications);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveArtisan(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null && user.Role == "Artisan")
            {
                user.IsApproved = true;

                var app = await _context.ArtisanApplications
                    .FirstOrDefaultAsync(a => a.ApplicantId == userId);
                if (app != null)
                    app.Status = "Approved";

                await _context.SaveChangesAsync();
            }

            TempData["Message"] = "Artisan approved successfully!";
            return RedirectToAction("ArtisanApproval");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectArtisan(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                var app = await _context.ArtisanApplications
                    .FirstOrDefaultAsync(a => a.ApplicantId == userId);
                if (app != null)
                    app.Status = "Rejected";

                user.IsApproved = false;
                await _context.SaveChangesAsync();
            }

            TempData["Message"] = "Artisan application rejected.";
            return RedirectToAction("ArtisanApproval");
        }

        public async Task<IActionResult> Orders(string? status, string? search)
        {
            var query = _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .AsQueryable();

            if (!string.IsNullOrEmpty(status))
                query = query.Where(o => o.Status == status);

            if (!string.IsNullOrEmpty(search))
                query = query.Where(o => o.OrderNumber.Contains(search) ||
                    (o.Customer != null && o.Customer.FullName.Contains(search)));

            var orders = await query
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            ViewBag.CurrentStatus = status;
            ViewBag.CurrentSearch = search;
            ViewBag.PendingCount = await _context.Orders.CountAsync(o => o.Status == "Pending");
            ViewBag.ShippedCount = await _context.Orders.CountAsync(o => o.Status == "Shipped");
            ViewBag.DeliveredCount = await _context.Orders.CountAsync(o => o.Status == "Delivered");
            ViewBag.TotalOrderCount = await _context.Orders.CountAsync();

            return View(orders);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, string status)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.Status = status;
                await _context.SaveChangesAsync();
                TempData["Message"] = $"Order #{order.OrderNumber} updated to {status}.";
            }
            return RedirectToAction("Orders");
        }

        public async Task<IActionResult> UserManagement(string? role, string? search)
        {
            var query = _context.Users.AsQueryable();

            if (role == "Suspended")
            {
                query = query.Where(u => !u.IsApproved);
            }
            else if (!string.IsNullOrEmpty(role))
            {
                query = query.Where(u => u.Role == role);
            }

            if (!string.IsNullOrEmpty(search))
                query = query.Where(u => u.FullName.Contains(search) || u.Email.Contains(search));

            var users = await query
                .OrderByDescending(u => u.JoinedDate)
                .ToListAsync();

            var userIds = users.Select(u => u.Id).ToList();
            var orderCounts = await _context.Orders
                .Where(o => userIds.Contains(o.CustomerId))
                .GroupBy(o => o.CustomerId)
                .Select(g => new { UserId = g.Key, Count = g.Count(), Total = g.Sum(o => o.TotalAmount) })
                .ToDictionaryAsync(x => x.UserId, x => new { x.Count, x.Total });

            var productCounts = await _context.Products
                .Where(p => userIds.Contains(p.ArtisanId))
                .GroupBy(p => p.ArtisanId)
                .Select(g => new { UserId = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.UserId, x => x.Count);

            ViewBag.OrderCounts = orderCounts;
            ViewBag.ProductCounts = productCounts;
            ViewBag.CurrentRole = role;
            ViewBag.CurrentSearch = search;
            ViewBag.TotalCount = await _context.Users.CountAsync();
            ViewBag.ArtisanCount = await _context.Users.CountAsync(u => u.Role == "Artisan");
            ViewBag.CollectorCount = await _context.Users.CountAsync(u => u.Role == "Collector");
            ViewBag.SuspendedCount = await _context.Users.CountAsync(u => !u.IsApproved);

            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleUserStatus(int userId)
        {
            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                if (isAjax)
                    return NotFound(new { success = false, message = "User not found." });
                return RedirectToAction("UserManagement");
            }

            user.IsApproved = !user.IsApproved;
            await _context.SaveChangesAsync();
            TempData["Message"] = $"User {user.FullName} status updated to {(user.IsApproved ? "Active" : "Suspended")}.";

            if (isAjax)
            {
                return Json(new
                {
                    success = true,
                    message = TempData["Message"]?.ToString(),
                    userId = user.Id,
                    isActive = user.IsApproved
                });
            }

            return RedirectToAction("UserManagement");
        }
    }
}
