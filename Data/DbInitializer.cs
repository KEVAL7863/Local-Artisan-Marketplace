using Local_Artisan_MarketPlace.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Local_Artisan_MarketPlace.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            await context.Database.MigrateAsync();

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            var adminEmail = "admin@artisanmarket.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FullName = "Administrator",
                    AccountType = "Admin",
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(adminUser, "Admin@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category { Name = "Ceramics", Slug = "ceramics" },
                    new Category { Name = "Drinkware", Slug = "drinkware" },
                    new Category { Name = "Lifestyle", Slug = "lifestyle" },
                    new Category { Name = "Tableware", Slug = "tableware" }
                );
                await context.SaveChangesAsync();
            }

            if (!context.Products.Any())
            {
                var ceramicsId = context.Categories.First(c => c.Name == "Ceramics").Id;
                var drinkwareId = context.Categories.First(c => c.Name == "Drinkware").Id;
                var lifestyleId = context.Categories.First(c => c.Name == "Lifestyle").Id;
                var tablewareId = context.Categories.First(c => c.Name == "Tableware").Id;

                context.Products.AddRange(
                    new Product
                    {
                        Name = "Hand-Thrown Matte Ceramic Vase",
                        Description = "Crafted using traditional pottery techniques, this vase showcases sustainable stoneware with a sophisticated matte finish. Each piece features a unique, tactile surface that adds character to any space. Perfect for floral arrangements or as a sculptural statement piece.",
                        ShortDescription = "Traditional stoneware with a sophisticated matte finish.",
                        Price = 450,
                        ImageUrl = "https://images.unsplash.com/photo-1578749556568-bc2c40e68b61?w=400",
                        ThumbnailUrls = "https://images.unsplash.com/photo-1578749556568-bc2c40e68b61?w=100,https://images.unsplash.com/photo-1610701596007-11502861dcfa?w=100,https://images.unsplash.com/photo-1565193566173-7a0ee3dbe261?w=100",
                        StockQuantity = 4,
                        Dimensions = "8\" Height x 5\" Diameter",
                        Collection = "Limited Collection",
                        CategoryId = ceramicsId,
                        Rating = 4.6,
                        ReviewCount = 34,
                        IsSustainable = true,
                        IsHandSigned = true
                    },
                    new Product
                    {
                        Name = "Hand-Glazed Ceramic Mug",
                        Description = "Each piece features a unique, tactile surface glaze that makes every mug one-of-a-kind. Perfect for your morning coffee or tea.",
                        ShortDescription = "Each piece features a unique, tactile surface glaze.",
                        Price = 315,
                        ImageUrl = "https://images.unsplash.com/photo-1514228742587-6b1558fcca3d?w=400",
                        StockQuantity = 12,
                        Collection = "Earth Tones",
                        CategoryId = drinkwareId,
                        Rating = 4.8,
                        ReviewCount = 28,
                        IsSustainable = true,
                        IsHandSigned = true
                    },
                    new Product
                    {
                        Name = "Minimalist Incense Burner",
                        Description = "Sculptural stoneware designed for modern interiors. A perfect blend of functionality and art.",
                        ShortDescription = "Sculptural stoneware designed for modern interiors.",
                        Price = 536,
                        ImageUrl = "https://images.unsplash.com/photo-1602874801006-4e6e28c0bfe8?w=400",
                        StockQuantity = 8,
                        Collection = "Wellness",
                        CategoryId = lifestyleId,
                        Rating = 4.7,
                        ReviewCount = 19,
                        IsSustainable = true,
                        IsHandSigned = true
                    },
                    new Product
                    {
                        Name = "Speckled Breakfast Bowl",
                        Description = "Handcrafted ceramic bowl with natural speckles. Perfect for breakfast or snacks.",
                        ShortDescription = "Handcrafted ceramic bowl with natural speckles.",
                        Price = 450,
                        ImageUrl = "https://images.unsplash.com/photo-1544787219-7f47ccb76574?w=400",
                        StockQuantity = 15,
                        CategoryId = tablewareId,
                        Rating = 4.5,
                        ReviewCount = 22,
                        IsSustainable = true
                    },
                    new Product
                    {
                        Name = "Earth Tones Mug",
                        Description = "Warm earth-toned ceramic mug for your daily brew.",
                        ShortDescription = "Warm earth-toned ceramic mug.",
                        Price = 950,
                        ImageUrl = "https://images.unsplash.com/photo-1509042239860-f550ce710b93?w=400",
                        StockQuantity = 10,
                        CategoryId = drinkwareId,
                        Rating = 4.6,
                        ReviewCount = 15
                    },
                    new Product
                    {
                        Name = "Tuscan Plate Set",
                        Description = "A set of three handcrafted plates inspired by Tuscan ceramics.",
                        ShortDescription = "Handcrafted Tuscan-inspired plate set.",
                        Price = 2200,
                        ImageUrl = "https://images.unsplash.com/photo-1556909114-f6e7ad7d3136?w=400",
                        StockQuantity = 6,
                        CategoryId = tablewareId,
                        Rating = 4.9,
                        ReviewCount = 31
                    },
                    new Product
                    {
                        Name = "Wooden Serving Set",
                        Description = "Elegant wooden serving utensils for your table.",
                        ShortDescription = "Elegant wooden serving utensils.",
                        Price = 1200,
                        ImageUrl = "https://images.unsplash.com/photo-1556909212-d5b604d0c90d?w=400",
                        StockQuantity = 8,
                        CategoryId = tablewareId,
                        Rating = 4.4,
                        ReviewCount = 12
                    },
                    new Product
                    {
                        Name = "Natural Linen Napkins",
                        Description = "Set of four handwoven linen napkins in natural tones.",
                        ShortDescription = "Handwoven linen napkins in natural tones.",
                        Price = 850,
                        ImageUrl = "https://images.unsplash.com/photo-1582735689369-4fe89db7114c?w=400",
                        StockQuantity = 20,
                        CategoryId = tablewareId,
                        Rating = 4.7,
                        ReviewCount = 18
                    }
                );
                await context.SaveChangesAsync();
            }

            if (!context.ProductReviews.Any() && context.Products.Any() && adminUser != null)
            {
                var vase = context.Products.First(p => p.Name.Contains("Vase"));
                context.ProductReviews.AddRange(
                    new ProductReview { ProductId = vase.Id, UserId = adminUser.Id, ReviewerName = "Sarah Jenkins", Rating = 5, Comment = "Absolutely stunning! The matte finish is even more beautiful in person. It has a lovely weight to it and the packaging was impeccable. Will definitely order again.", IsVerifiedBuyer = true },
                    new ProductReview { ProductId = vase.Id, UserId = adminUser.Id, ReviewerName = "Marcus Thorne", Rating = 5, Comment = "A true work of art. I appreciate that each piece is unique - the slight variations make it feel special. Worth every rupee.", IsVerifiedBuyer = true }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}

