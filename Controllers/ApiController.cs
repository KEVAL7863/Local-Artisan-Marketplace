using Local_Artisan_Marketplace.Data;
using Local_Artisan_Marketplace.Helpers;
using Local_Artisan_Marketplace.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Local_Artisan_Marketplace.Controllers
{
    [Route("api")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        private int? GetCurrentUserId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            return claim != null ? int.Parse(claim.Value) : null;
        }

        [HttpPost("account/login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                return BadRequest(new { message = "Email and password are required." });

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null || !PasswordHelper.VerifyPassword(request.Password, user.PasswordHash))
                return Unauthorized(new { message = "Invalid email or password." });

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            return Ok(new { user.Email, user.FullName, user.Role, user.Id });
        }

        [HttpPost("account/register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (string.IsNullOrEmpty(request.FullName) || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                return BadRequest(new { message = "Full name, email, and password are required." });

            var role = request.Role;
            if (string.IsNullOrEmpty(role) || (role != "Collector" && role != "Artisan"))
                role = "Collector";

            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
                return BadRequest(new { message = "An account with this email already exists." });

            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                PasswordHash = PasswordHelper.HashPassword(request.Password),
                FullName = request.FullName,
                Role = role,
                StudioName = request.StudioName,
                CraftType = request.CraftType,
                IsApproved = role != "Artisan",
                JoinedDate = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            return Ok(new { user.Email, user.FullName, user.Role, user.Id, isPending = role == "Artisan" });
        }

        [HttpPost("account/logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok(new { message = "Logged out successfully." });
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetProducts([FromQuery] string? category, [FromQuery] string? search, [FromQuery] string? sortBy)
        {
            var query = _context.Products.Where(p => p.IsActive && !p.IsDraft).Include(p => p.Artisan).AsQueryable();

            if (!string.IsNullOrEmpty(category))
                query = query.Where(p => p.Category == category);
            if (!string.IsNullOrEmpty(search))
                query = query.Where(p => p.Title.Contains(search) || (p.Description != null && p.Description.Contains(search)));

            query = sortBy switch
            {
                "price_asc" => query.OrderBy(p => p.Price),
                "price_desc" => query.OrderByDescending(p => p.Price),
                _ => query.OrderByDescending(p => p.CreatedAt)
            };

            var products = await query.Select(p => new
            {
                p.Id, p.Title, p.Description, p.Price, p.Category, p.StockQuantity, p.ImageUrl,
                ArtisanName = p.Artisan != null ? p.Artisan.FullName : "Unknown"
            }).Take(20).ToListAsync();

            return Ok(products);
        }

        [HttpPost("cart/add")]
        public async Task<IActionResult> AddToCart([FromBody] CartAddRequest request)
        {
            var userId = GetCurrentUserId();
            if (userId == null)
                return Unauthorized(new { message = "Please log in to add items to cart." });

            var existing = await _context.CartItems.FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == request.ProductId);
            if (existing != null)
            {
                existing.Quantity += request.Quantity > 0 ? request.Quantity : 1;
            }
            else
            {
                _context.CartItems.Add(new CartItem
                {
                    UserId = userId.Value,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity > 0 ? request.Quantity : 1,
                    AddedAt = DateTime.UtcNow
                });
            }
            await _context.SaveChangesAsync();
            return Ok(new { message = "Item added to cart." });
        }

        [HttpPost("artisan/products")]
        public async Task<IActionResult> AddProduct([FromBody] AddProductRequest request)
        {
            var userId = GetCurrentUserId();
            if (userId == null)
                return Unauthorized(new { message = "Only artisans can add products." });

            var user = await _context.Users.FindAsync(userId);
            if (user == null || user.Role != "Artisan")
                return Unauthorized(new { message = "Only artisans can add products." });

            var product = new Product
            {
                Title = request.Title,
                Description = request.Description,
                Price = request.Price,
                Category = request.Category ?? "Other",
                StockQuantity = request.StockQuantity,
                ImageUrl = request.ImageUrl,
                IsActive = true,
                IsDraft = false,
                CreatedAt = DateTime.UtcNow,
                ArtisanId = userId.Value
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Product added successfully.", productId = product.Id });
        }

        [HttpGet("admin/approvals")]
        public async Task<IActionResult> GetPendingApprovals()
        {
            var approvals = await _context.ArtisanApplications
                .Where(a => a.Status == "Pending")
                .OrderByDescending(a => a.SubmissionDate)
                .Select(a => new { a.Id, a.ShopName, a.Specialty, a.SubmissionDate, a.Status })
                .ToListAsync();
            return Ok(approvals);
        }

        [HttpPost("admin/approve/{id}")]
        public async Task<IActionResult> ApproveArtisan(int id)
        {
            var application = await _context.ArtisanApplications.Include(a => a.Applicant).FirstOrDefaultAsync(a => a.Id == id);
            if (application == null) return NotFound();

            application.Status = "Approved";
            if (application.Applicant != null)
                application.Applicant.IsApproved = true;

            await _context.SaveChangesAsync();
            return Ok(new { message = "Artisan approved." });
        }

        [HttpPost("admin/reject/{id}")]
        public async Task<IActionResult> RejectArtisan(int id)
        {
            var application = await _context.ArtisanApplications.FindAsync(id);
            if (application == null) return NotFound();

            application.Status = "Rejected";
            await _context.SaveChangesAsync();
            return Ok(new { message = "Artisan rejected." });
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class RegisterRequest
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? Role { get; set; }
        public string? StudioName { get; set; }
        public string? CraftType { get; set; }
    }

    public class CartAddRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; } = 1;
    }

    public class AddProductRequest
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Category { get; set; }
        public int StockQuantity { get; set; }
        public string? ImageUrl { get; set; }
    }
}
