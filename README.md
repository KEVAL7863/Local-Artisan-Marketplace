
# Local Artisan Marketplace

A full-stack e-commerce web application connecting local artisans with collectors. Built with ASP.NET Core 8.0 MVC.



---

## Tech Stack Overview

### Backend
| Technology | Version | Use Case |
|-----------|---------|----------|
| .NET | 8.0 (LTS) | Runtime framework |
| C# | 12 | Programming language |
| ASP.NET Core MVC | 8.0 | Web framework (Model-View-Controller pattern) |
| Entity Framework Core | 8.0.0 | ORM (Object-Relational Mapper) for database operations |
| ASP.NET Core Identity | 8.0.0 | Authentication & authorization (login, register, roles) |
| SQL Server LocalDB | 15.x | Database engine (development) |


### Frontend
| Technology | Version | Use Case |
|-----------|---------|----------|
| Tailwind CSS | CDN (latest) | Utility-first CSS framework for all styling |
| Google Material Symbols | Outlined | Icon library (400+ icons used throughout) |
| Google Fonts - Work Sans | 300-900 weights | Primary typography font |
| jQuery | 3.x | DOM manipulation & validation support |
| jQuery Validation | 1.x | Client-side form validation |
| Vanilla JavaScript | ES6+ | Custom interactivity (image preview, toast, toggles) |

---

## Step-by-Step: What is Used & Where

### Step 1: CSS Framework - Tailwind CSS (CDN)

**Source:** `https://cdn.tailwindcss.com?plugins=forms,container-queries`

**Where Used:** Every single `.cshtml` view file

**Plugins Enabled:**
- `forms` - Better default styling for form elements (inputs, selects, textareas)
- `container-queries` - Responsive layouts based on container size

**Custom Configuration (in `_Layout.cshtml`):**
```javascript
tailwind.config = {
    darkMode: "class",
    theme: {
        extend: {
            colors: {
                "primary": "#8b5e3c",           // Brown - main brand color
                "background-light": "#f7f7f6",  // Light mode background
                "background-dark": "#1d1815",   // Dark mode background
            },
            fontFamily: {
                "display": ["Work Sans", "sans-serif"]
            },
            borderRadius: {
                "DEFAULT": "0.25rem",
                "lg": "0.5rem",
                "xl": "0.75rem",
                "full": "9999px"
            },
        },
    },
}
```

**Use Cases:**
- Responsive grid layouts (`grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4`)
- Flexbox layouts (`flex items-center justify-between`)
- Spacing & sizing (`p-6`, `m-4`, `h-12`, `w-full`)
- Colors (`bg-primary`, `text-slate-900`, `border-primary/10`)
- Dark mode (`dark:bg-background-dark`, `dark:text-slate-100`)
- Hover/focus states (`hover:bg-primary/90`, `focus:ring-primary`)
- Transitions (`transition-colors`, `transition-all`)
- Typography (`text-3xl`, `font-bold`, `tracking-tight`)
- Shadows (`shadow-lg`, `shadow-primary/20`)
- Rounded corners (`rounded-xl`, `rounded-full`)

---

### Step 2: Icon Library - Google Material Symbols Outlined

**Source:** `https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined`

**Where Used:** Navigation, buttons, status badges, cards, sidebars

**Configuration:**
```css
.material-symbols-outlined {
    font-variation-settings: 'FILL' 0, 'wght' 400, 'GRAD' 0, 'opsz' 24;
}
```

**Icons Used in the Project:**
| Icon Name | Where Used |
|-----------|-----------|
| `storefront` | Logo/branding in sidebar |
| `dashboard` | Dashboard nav link |
| `shopping_cart` | Cart icon, Orders nav |
| `inventory_2` | Products nav link |
| `group` | Users management |
| `brush` | Artisan section |
| `search` | Search bars |
| `person` | User profile |
| `logout` | Logout buttons |
| `add_circle` | Add product |
| `cloud_upload` | Image upload area |
| `check_circle` | Success/approved status |
| `error` | Error messages |
| `notifications` | Notification bell |
| `local_shipping` | Shipping info |
| `payments` | Payment section |
| `pending_actions` | Pending status |
| `trending_up` | Growth indicators |
| `delete` | Remove items |
| `edit` | Edit actions |
| `favorite` | Wishlist hearts |
| `add_shopping_cart` | Add to cart |
| `arrow_forward` | Navigation arrows |
| `chevron_right` | Breadcrumbs |
| `settings` | Settings nav |
| `analytics` | Analytics nav |
| `verified_user` | Security badge |
| `package_2` | Shipping badge |
| `shield` | Security badge |
| `currency_rupee` | Revenue display |
| `image` | Image placeholder |
| `close` | Close/dismiss |

---

### Step 3: Typography - Google Fonts (Work Sans)

**Source:** `https://fonts.googleapis.com/css2?family=Work+Sans:wght@300;400;500;600;700;900`

**Where Used:** Entire website - body text, headings, buttons, navigation

**Weights Used:**
| Weight | Name | Use Case |
|--------|------|----------|
| 300 | Light | Subtle descriptions |
| 400 | Regular | Body text, form labels |
| 500 | Medium | Navigation links |
| 600 | SemiBold | Table headers, labels |
| 700 | Bold | Headings, button text, prices |
| 900 | Black | Hero text, page titles |

**Applied via:**
```css
body { font-family: 'Work Sans', sans-serif; }
```

---

### Step 4: JavaScript Libraries

#### 4.1 jQuery (in `wwwroot/lib/jquery/`)
- **Version:** 3.x
- **Use Case:** DOM manipulation support, validation integration
- **Files:** `jquery.js`, `jquery.min.js`

#### 4.2 jQuery Validation (in `wwwroot/lib/jquery-validation/`)
- **Use Case:** Client-side form validation
- **Files:** `jquery.validate.js`, `additional-methods.js`

#### 4.3 jQuery Validation Unobtrusive (in `wwwroot/lib/jquery-validation-unobtrusive/`)
- **Use Case:** ASP.NET Core MVC server-side validation attributes mapped to client-side
- **Files:** `jquery.validate.unobtrusive.js`

#### 4.4 Bootstrap JS (in `wwwroot/lib/bootstrap/`)
- **Version:** Legacy (from project template)
- **Use Case:** Not actively used - kept from default template
- **Files:** `bootstrap.bundle.js`

#### 4.5 Custom Vanilla JavaScript (inline in views)
- **Use Cases:**
  - Image URL preview in Add/Edit Product
  - Password visibility toggle in Login
  - Artisan fields show/hide in Register
  - Toast notification auto-dismiss (4 seconds)
  - Header "Publish Product" button triggers form submit

---

### Step 5: Backend Framework - ASP.NET Core 8.0 MVC

**Pattern:** Model-View-Controller (MVC)

**Controllers (7 total):**
| Controller | Route | Use Case |
|-----------|-------|----------|
| `HomeController` | `/` | Home page, Privacy page |
| `AccountController` | `/Account/*` | Login, Register, Logout |
| `ProductController` | `/Product/*` | Product listing, Details, Filtering |
| `CartController` | `/Cart/*` | Shopping cart, Checkout, Place order |
| `ArtisanController` | `/Artisan/*` | Artisan dashboard, Products CRUD, Orders |
| `AdminController` | `/Admin/*` | Admin dashboard, User/Order/Artisan management |
| `ApiController` | `/api/*` | REST API endpoints (JSON) |

**Routing Pattern:**
```csharp
pattern: "{controller=Home}/{action=Index}/{id?}"
```

---

### Step 6: Authentication & Authorization - ASP.NET Core Identity

**NuGet Package:** `Microsoft.AspNetCore.Identity.EntityFrameworkCore`

**Use Cases:**
- User registration with email/password
- User login with cookie-based sessions
- Role-based authorization (Admin, Artisan, Collector)
- Password hashing (built-in security)
- AntiForgeryToken for CSRF protection on all POST forms

**Authorization Attributes:**
```csharp
[Authorize]                    // CartController - requires login
[Authorize(Roles = "Artisan")] // ArtisanController - artisan only
[Authorize(Roles = "Admin")]   // AdminController - admin only
```

**Password Configuration:**
```csharp
options.Password.RequireDigit = false;
options.Password.RequireLowercase = false;
options.Password.RequireUppercase = false;
options.Password.RequireNonAlphanumeric = false;
options.Password.RequiredLength = 6;
```

### Step 7: Database & ORM - Entity Framework Core + SQL Server

**NuGet Packages:**
- `Microsoft.EntityFrameworkCore` (8.0.0)
- `Microsoft.EntityFrameworkCore.SqlServer` (8.0.0)
- `Microsoft.EntityFrameworkCore.Tools` (8.0.0)

**Connection String:**
```
Server=(localdb)\mssqllocaldb;Database=LocalArtisanMarketplace;Trusted_Connection=True
```

**Database Tables (13 total):**
| Table | Use Case |
|-------|----------|
| `AspNetUsers` | User accounts (extended with custom fields) |
| `AspNetRoles` | Role definitions (Admin, Artisan, Collector) |
| `AspNetUserRoles` | User-role assignments |
| `AspNetRoleClaims` | Role-based claims |
| `AspNetUserClaims` | User-specific claims |
| `AspNetUserLogins` | External login providers |
| `AspNetUserTokens` | Authentication tokens |
| `Products` | Product catalog |
| `Orders` | Customer orders |
| `OrderItems` | Individual items in orders |
| `CartItems` | Shopping cart items |
| `ArtisanApplications` | Artisan approval applications |
| `__EFMigrationsHistory` | Migration tracking |

**EF Core Patterns Used:**
- Fluent API configuration (in `ApplicationDbContext`)
- Eager loading with `.Include()` and `.ThenInclude()`
- Async queries (`ToListAsync()`, `FirstOrDefaultAsync()`)
- LINQ filtering, sorting, grouping
- Cascade/Restrict delete behaviors
- Unique index constraints

---

### Step 8: Razor View Engine

**Template Engine:** ASP.NET Core Razor (.cshtml files)

**Features Used:**
| Feature | Use Case |
|---------|----------|
| `@model` | Strongly-typed view models |
| `@ViewBag` | Dynamic data passing from controller |
| `@inject` | Dependency injection in views (UserManager, SignInManager) |
| `@Html.AntiForgeryToken()` | CSRF protection on forms |
| `asp-controller` / `asp-action` | Tag helpers for URL generation |
| `asp-route-*` | Route parameter binding |
| `@if` / `@foreach` | Conditional rendering & loops |
| `@{ }` | C# code blocks in views |
| `_Layout.cshtml` | Master layout page |
| `_ViewStart.cshtml` | Default layout assignment |
| `_ViewImports.cshtml` | Global using statements & tag helpers |

**View Files (23 total):**
```
Views/
├── _ViewImports.cshtml          (Global imports)
├── _ViewStart.cshtml            (Layout config)
├── Home/Index.cshtml            (Landing page)
├── Home/Privacy.cshtml          (Privacy policy)
├── Account/Login.cshtml         (Login form)
├── Account/Register.cshtml      (Registration form)
├── Product/Index.cshtml         (Product catalog)
├── Product/Details.cshtml       (Product detail)
├── Cart/Index.cshtml            (Shopping cart)
├── Cart/Checkout.cshtml         (Checkout form)
├── Cart/OrderConfirmation.cshtml (Order success)
├── Artisan/Dashboard.cshtml     (Artisan stats)
├── Artisan/MyProducts.cshtml    (Product management)
├── Artisan/AddProduct.cshtml    (Add new product)
├── Artisan/EditProduct.cshtml   (Edit product)
├── Artisan/Orders.cshtml        (Artisan orders)
├── Admin/Dashboard.cshtml       (Admin overview)
├── Admin/ArtisanApproval.cshtml (Artisan approvals)
├── Admin/Orders.cshtml          (Order management)
├── Admin/UserManagement.cshtml  (User management)
├── Shared/_Layout.cshtml        (Master layout)
├── Shared/_ValidationScriptsPartial.cshtml
└── Shared/Error.cshtml          (Error page)
```

---

### Step 9: Data Models (7 models)

| Model | Fields | Use Case |
|-------|--------|----------|
| `ApplicationUser` | FullName, Role, StudioName, CraftType, IsApproved, JoinedDate, ProfileImageUrl | Extended Identity user |
| `Product` | Title, Description, Price, Category, StockQuantity, ImageUrl, IsActive, IsDraft | Product catalog item |
| `Order` | OrderNumber, Subtotal, ShippingCost, Tax, TotalAmount, Status, Shipping address, PaymentMethod | Purchase order |
| `OrderItem` | OrderId, ProductId, Quantity, UnitPrice | Line item in an order |
| `CartItem` | UserId, ProductId, Quantity, AddedAt | Shopping cart item |
| `ArtisanApplication` | ShopName, Specialty, SubmissionDate, Status, ShopImageUrl | Artisan approval request |
| `ErrorViewModel` | RequestId | Error page display |

---

### Step 10: Middleware Pipeline (Program.cs)

**Order of middleware execution:**
```
1. HTTPS Redirection
2. Static Files (wwwroot/)
3. Routing
4. CORS (Cross-Origin Resource Sharing)
5. Authentication (Cookie-based Identity)
6. Authorization (Role-based access control)
7. MVC Controller Mapping
```

**Services Registered:**
```
1. ApplicationDbContext (SQL Server)
2. ASP.NET Core Identity (UserManager, SignInManager, RoleManager)
3. Cookie Authentication Configuration
4. CORS Policy (AllowAnyOrigin)
5. MVC Controllers with Views
```

---

### Step 11: Design System

**Color Palette:**
| Color | Hex | Use Case |
|-------|-----|----------|
| Primary (Brown) | `#8b5e3c` | Buttons, links, accents, branding |
| Background Light | `#f7f7f6` | Light mode page background |
| Background Dark | `#1d1815` | Dark mode page background |
| Slate 900 | `#0f172a` | Primary text (light mode) |
| Slate 100 | `#f1f5f9` | Primary text (dark mode) |
| Green | Various | Success states, active status |
| Amber | Various | Pending/warning states |
| Red | Various | Error states, delete actions |
| Blue | Various | Shipped status, info states |

**UI Components Used:**
- Sidebar navigation (fixed, scrollable)
- Top header bars with search
- Data tables with sorting/filtering
- Card layouts for stats/products
- Form inputs with floating labels
- Badge/pill components for status
- Toast notifications (success/error)
- Breadcrumb navigation
- Responsive grid product cards
- Avatar/initials components
- Progress bars for insights
- Modal-free inline actions

---

### Step 12: Security Features

| Feature | Implementation |
|---------|---------------|
| CSRF Protection | `@Html.AntiForgeryToken()` on every POST form |
| Password Hashing | ASP.NET Core Identity (bcrypt-based) |
| Role Authorization | `[Authorize(Roles = "Admin")]` attributes |
| Cookie Authentication | Secure HTTP-only cookies |
| Input Validation | Required attributes, type checking |
| SQL Injection Prevention | Entity Framework parameterized queries |
| XSS Prevention | Razor auto-encoding of output |

---

### Step 13: Responsive Breakpoints

| Breakpoint | Width | Layout Changes |
|-----------|-------|----------------|
| Default | < 640px | Single column, hidden sidebar |
| `sm:` | >= 640px | Two-column grids |
| `md:` | >= 768px | Sidebar visible, multi-column |
| `lg:` | >= 1024px | Full sidebar, wide content |
| `xl:` | >= 1280px | Max content width applied |

---

### Step 14: Currency & Localization

| Feature | Value |
|---------|-------|
| Currency | Indian Rupee (₹) |
| Price Format | `₹XX.XX` or `₹X,XXX` |
| Tax Rate | 18% GST |
| Free Shipping | Orders >= ₹500 |
| Default Shipping | ₹50 |

---

## How to Run

```bash
# 1. Restore packages
dotnet restore

# 2. Create/update database
dotnet ef database update

# 3. Run the application
dotnet run
```

## Default Login Credentials

| Role | Email | Password |
|------|-------|----------|
| Admin | admin@artisanmarket.com | admin123 |
| Artisan | elena@artisanmarket.com | artisan123 |
| Collector | rajesh@email.com | collector123 |

---

## Project Structure

```
Local-Artisan-Marketplace/
├── Art-ui/                  # Original HTML design mockups (14 files)
├── Controllers/             # 7 MVC controllers
├── Models/                  # 7 data models
├── Views/                   # 23 Razor view files
├── Data/
│   ├── ApplicationDbContext.cs    # EF Core database context
│   └── SeedData.cs               # Sample data seeder
├── Migrations/              # EF Core database migrations
├── wwwroot/
│   ├── css/site.css         # Minimal custom CSS
│   ├── js/site.js           # Custom JavaScript placeholder
│   └── lib/                 # jQuery, Bootstrap, Validation
├── Properties/
├── Program.cs               # Application entry point & configuration
├── appsettings.json         # Database connection string & config
├── Local-Artisan-Marketplace.csproj  # Project file with NuGet packages
└── Local-Artisan-Marketplace.sln     # Solution file
```
