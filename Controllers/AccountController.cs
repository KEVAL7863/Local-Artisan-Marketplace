using Local_Artisan_Marketplace.Data;
using Local_Artisan_Marketplace.Helpers;
using Local_Artisan_Marketplace.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Local_Artisan_Marketplace.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError(string.Empty, "Email and password are required.");
                return View();
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null || !PasswordHelper.VerifyPassword(password, user.PasswordHash))
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View();
            }

            if (user.Role == "Admin" && user.FullName != "Keval Gelani")
            {
                user.FullName = "Keval Gelani";
                await _context.SaveChangesAsync();
            }

            await SignInUser(user);

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            if (user.Role == "Admin")
                return RedirectToAction("Dashboard", "Admin");
            if (user.Role == "Artisan")
                return RedirectToAction("Dashboard", "Artisan");

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string fullName, string email, string password, string role, string? studioName, string? craftType)
        {
            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError(string.Empty, "Full name, email, and password are required.");
                return View();
            }

            if (string.IsNullOrEmpty(role) || (role != "Collector" && role != "Artisan"))
                role = "Collector";

            // Check if email already exists
            if (await _context.Users.AnyAsync(u => u.Email == email))
            {
                ModelState.AddModelError(string.Empty, "An account with this email already exists.");
                return View();
            }

            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                PasswordHash = PasswordHelper.HashPassword(password),
                FullName = fullName,
                Role = role,
                StudioName = studioName,
                CraftType = craftType,
                IsApproved = role != "Artisan",
                JoinedDate = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            await SignInUser(user);

            if (role == "Artisan")
                TempData["Message"] = "Your artisan account has been created and is pending approval.";

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        private async Task SignInUser(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}
