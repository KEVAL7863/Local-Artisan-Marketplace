# Local Artisan Marketplace - 7 Question Answers

## 1) Dynamic ketla generate thay che?
Aa project ma dynamic data mainly Controller mathi View ma moklay che ane Razor (`.cshtml`) ma render thay che.

- `AdminController.Dashboard()` ma `ViewBag.TotalUsers`, `ViewBag.TotalProducts`, `ViewBag.RecentOrders` jevu dynamic data set thay che.
- `ArtisanController.Dashboard()` ma logged-in artisan pramane total products/orders/revenue dynamic ave che.
- Views ma `@if`, `@foreach`, `@ViewBag`, `@model` thi UI dynamic generate thay che.

Simple rite kehie to:
- Dashboard cards dynamic
- Table/list records dynamic
- Logged-in user pramane content dynamic

## 2) Async await kyare ane kem use thay che?
Project ma database operations non-blocking banava `async/await` no use thay che.

Examples:
- `CountAsync()`, `ToListAsync()`, `FirstOrDefaultAsync()`, `SaveChangesAsync()`
- Controllers ma methods jeva ke:
  - `AdminController.Dashboard()`
  - `AccountController.Login()`
  - `CartController.Index()`
  - `ArtisanController.Orders()`

Faaydo:
- Server thread block thato nathi
- Concurrent users ma better performance male che

## 3) Database kai rite create thay che?
Project Entity Framework Core (SQL Server provider) use kare che.

Flow:
1. Connection string `appsettings.json` ma define che (`DefaultConnection`).
2. `Program.cs` ma `UseSqlServer(...)` thi DbContext register thay che.
3. App startup time par:
   - `db.Database.Migrate();`
   - Aa pending migrations apply kare che
4. Pachhi `SeedData.SeedAllAsync(...)` thi initial data insert thay che.

Etle app run karta database/table automatically create/update thai shake che.

## 4) API call kem thay che?
Aa project mostly MVC pattern follow kare che (controller actions + Razor views).

Request types:
- Browser GET request:
  - `/Product/Index`
  - `/Admin/Dashboard`
- Form POST request:
  - Login/Register/Logout
  - Order status update
  - User status toggle

Form ma Anti-forgery token pan use thay che (`@Html.AntiForgeryToken()`).

## 5) Database connectivity kai rite thay che?
Connectivity setup:

- File: `appsettings.json`
  - `ConnectionStrings:DefaultConnection`
- File: `Program.cs`
  - `builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(...))`
- File: `Data/ApplicationDbContext.cs`
  - `DbSet<>` properties through tables map thay che

Ahi thi EF Core SQL Server sathe connect thai ne query execute kare che.

## 6) Session kai rite handle thay che?
Traditional ASP.NET Session use nathi, pan cookie-based authentication use thay che.

Flow:
- Login success pachi `AccountController.SignInUser()` claims banave che.
- `HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal)` call thay che.
- Cookie thi user identity maintain thay che.
- `[Authorize]` and `[Authorize(Roles = "Admin")]` thi protected routes control thay che.
- Logout ma `SignOutAsync(...)` thi auth cookie clear thay che.

## 7) Server ma data fetch kai rite thay che?
Server-side fetch Controller ma Entity Framework query thi thay che.

Common pattern:
- `_context.TableName...`
- `.Where(...)`
- `.Include(...)` / `.ThenInclude(...)` relation load karva
- `.OrderByDescending(...)`
- `.ToListAsync()` / `.CountAsync()`

Examples:
- `AdminController.Orders()` ma filtered order list fetch thay che.
- `CartController.Index()` ma user-specific cart items fetch thay che.
- `ArtisanController.MyProducts()` ma artisan-specific products fetch thay che.

---

## Quick Reference Files
- `Program.cs`
- `Data/ApplicationDbContext.cs`
- `Data/SeedData.cs`
- `Controllers/AccountController.cs`
- `Controllers/AdminController.cs`
- `Controllers/ArtisanController.cs`
- `Controllers/CartController.cs`
