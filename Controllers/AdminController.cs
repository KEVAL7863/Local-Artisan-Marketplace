using Local_Artisan_MarketPlace.Data;
using Local_Artisan_MarketPlace.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Local_Artisan_MarketPlace.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.OrderByDescending(u => u.Id).ToListAsync();
            var userList = new List<AdminUserViewModel>();
            foreach (var user in users)
            {
                userList.Add(new AdminUserViewModel
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email ?? "",
                    AccountType = user.AccountType,
                    BusinessName = user.BusinessName,
                    ArtisanType = user.ArtisanType,
                    IsAdmin = await _userManager.IsInRoleAsync(user, "Admin")
                });
            }

            ViewBag.TotalUsers = userList.Count;
            ViewBag.ShopperCount = userList.Count(u => u.AccountType == "Shopper");
            ViewBag.ArtisanCount = userList.Count(u => u.AccountType == "Artisan");
            ViewBag.AdminCount = userList.Count(u => u.IsAdmin);

            return View(userList);
        }
    }

    public class AdminUserViewModel
    {
        public string Id { get; set; } = "";
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public string AccountType { get; set; } = "";
        public string? BusinessName { get; set; }
        public string? ArtisanType { get; set; }
        public bool IsAdmin { get; set; }
    }
}

