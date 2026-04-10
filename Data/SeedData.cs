using Local_Artisan_Marketplace.Helpers;
using Local_Artisan_Marketplace.Models;
using Microsoft.EntityFrameworkCore;

namespace Local_Artisan_Marketplace.Data
{
    public static class SeedData
    {
        public static async Task SeedAllAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            // Check if data already seeded
            if (await context.Products.AnyAsync())
                return;

            // ===================== ADMIN USER =====================
            var adminUser = new ApplicationUser
            {
                UserName = "admin@artisanmarket.com",
                Email = "admin@artisanmarket.com",
                PasswordHash = PasswordHelper.HashPassword("admin123"),
                FullName = "Keval Gelani",
                Role = "Admin",
                IsApproved = true,
                JoinedDate = new DateTime(2023, 1, 1),
                ProfileImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuDQ-Lq_9UPGr5SKkmIzZT3XJJ3jzdWXQ9FH5FEOdnlQW0M39Hmz99HOMH74XuJcoMoIJRsoSgDAll1r4Wwn0_rJH1VAgo2SmIOzX_wQQEDsMcluEj6nRtOmHs5Of1kmYqoosGIeGsvtACvPoqqUhAmIFDA0hjhcBcScT_oT5IUHyTLElMQxGJenb7R4KEb1GBvHIfvfCqDGeSwv-rEAOILiHp3WfmSGjzuFO80pBswksOrsldYijSRS6On0-rIt6Xfaqsz7vZyn1ww"
            };
            context.Users.Add(adminUser);

            // ===================== ARTISAN USERS =====================
            var keval = new ApplicationUser
            {
                UserName = "keval@artisanmarket.com",
                Email = "keval@artisanmarket.com",
                PasswordHash = PasswordHelper.HashPassword("artisan123"),
                FullName = "Keval Patel",
                Role = "Artisan",
                StudioName = "Keval Ceramics",
                CraftType = "Ceramics",
                IsApproved = true,
                JoinedDate = new DateTime(2023, 3, 15),
                ProfileImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuDnWIeNh2y2x6DILcW_B63S1HKp9zNlSEfikn3LYbD_khejYdpbz1bzqU1IK12ht2j4h8Sg5DdpjGikRLa-8wOvbEtPAApT0ilgVSIe-bnjuFdjbedxJu7BORjFjI-Nl7S2Q0ehNG-dPrsuoNOWsQH9RlpJYbuPZk6zhvus2N20yANC6jyrwHbgDqtTQPHCzk-DPAzBLdEA2V9yMXaH5HZQHSdDGO-QYzMWgtMmrOaaRzp0W290plrbKVz6Pc5GJpOIG7IwRvVy2KY"
            };
            context.Users.Add(keval);

            var raj = new ApplicationUser
            {
                UserName = "raj@artisanmarket.com",
                Email = "raj@artisanmarket.com",
                PasswordHash = PasswordHelper.HashPassword("artisan123"),
                FullName = "Raj Sharma",
                Role = "Artisan",
                StudioName = "Raj Woodworks",
                CraftType = "Woodworking",
                IsApproved = true,
                JoinedDate = new DateTime(2023, 4, 10),
                ProfileImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuB0lYvfGlS1SWWMXe4b2LjNqFYyJkBN92aLfA_XJ5ujFPX4mWNF1q0qfaPMDYsesYryYq91PHl8_myaqjHHPmVY9_LDuzj8US1C_Xlsr0U3IHm-6IDqmgvFhGm_MC_zXKwhHaleNRAex57iqgmh1oKrzwF3uEXaAc_NmPNcONCZB7yIc3K9STMbHX3bEndO1d5tpRO4RbsuJwIXJbuUvpYpFENjnqEyhIlcJP7Xo5OaetxYgFLgUDBCuSTXnb9U5H-khC8t76OUfU4"
            };
            context.Users.Add(raj);

            var parth = new ApplicationUser
            {
                UserName = "parth@artisanmarket.com",
                Email = "parth@artisanmarket.com",
                PasswordHash = PasswordHelper.HashPassword("artisan123"),
                FullName = "Parth Modi",
                Role = "Artisan",
                StudioName = "Parth Silver",
                CraftType = "Jewelry",
                IsApproved = true,
                JoinedDate = new DateTime(2023, 5, 20),
                ProfileImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuBkuo2nTRAeLtbItJm2a7ahT8qRezwFjusibU19Z-JHMlh_9AGkUOonjLRNzOEvKzLSDpnDVGT48uRm_Q6vCnWIhM0VM-1St778bcyvrtUskrvrW8s42JmXozHS78cI1I-cgy0-pWSC1K4DpkZK0k9kQsPyuGMjU4G9iHttbKUzuho9XVpC1O8tHMtML_NQ6t27o2I_9PKPDPtvcOtLAknJjB-84QXsidaOpk5QnGKZfXeodxIt1Ek-rAdTfojA8As-iHi4H-Qbv7c"
            };
            context.Users.Add(parth);

            var saddleArtisan = new ApplicationUser
            {
                UserName = "saddle@artisanmarket.com",
                Email = "saddle@artisanmarket.com",
                PasswordHash = PasswordHelper.HashPassword("artisan123"),
                FullName = "Arjun Patel",
                Role = "Artisan",
                StudioName = "Saddle Co.",
                CraftType = "Leather",
                IsApproved = true,
                JoinedDate = new DateTime(2023, 6, 5),
                ProfileImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuBtP6CScouM5TFBq-RmDs7UI3-tjCIbK-2-reYbMnHG_uDI-bSUI_1jxwgm_EBgPEaV-k6ClJ3MCiSvIHBUo-ITjl5BfvZ3i3HJU5Ir3cadjwALFh6XCpHriHwJJlcSX9ZIrbLeAVdPezEiv7UVA0LOVbk9Zfsod7yh2wJ15JDE0avILKQtmFPwL-o2yCj_OrYA-jY7_4RARUYRzhWRBcRcgLnEetrNLyRT-G2bpObxYBSQWbjuCUf49HjRaXkjMfb9BbVVa1N_BGI"
            };
            context.Users.Add(saddleArtisan);

            var ravi = new ApplicationUser
            {
                UserName = "ravi@artisanmarket.com",
                Email = "ravi@artisanmarket.com",
                PasswordHash = PasswordHelper.HashPassword("artisan123"),
                FullName = "Ravi Joshi",
                Role = "Artisan",
                StudioName = "Ravi Textiles",
                CraftType = "Textiles",
                IsApproved = true,
                JoinedDate = new DateTime(2023, 7, 12),
                ProfileImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuA7wjeefORozMN3d4kHJBNiJekK9rsR_3Wv6k-L_ibydJ6lOok-DOIUc_klKo_XPU-ua7rPUkxRmZQX0ctJ9WH5uGmNe7uf3EnExamFMSX9HgKrX5fqtcbghDIgFJaEt1hLvhzfBLyZLPRum12C7rpM1Ji5Ulpgaaj0f_yU0sRkK62reWmBkcLlruELj7I4963JzJeigZT42eU7SODj9R8F5MDYSSh7Lcx0WTb0iOpxijzvJ6p4o-dFXoYT6aINY8zXfa3KjkEmzng"
            };
            context.Users.Add(ravi);

            var ananya = new ApplicationUser
            {
                UserName = "ananya.crafts@email.com",
                Email = "ananya.crafts@email.com",
                PasswordHash = PasswordHelper.HashPassword("artisan123"),
                FullName = "Ananya Sharma",
                Role = "Artisan",
                StudioName = "Ananya Crafts",
                CraftType = "Ceramics",
                IsApproved = true,
                JoinedDate = new DateTime(2023, 10, 12),
                ProfileImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuCN7_jHiz4HcdvsNLk8myHCEfyHhYAtDIlXiW7DefAfsQdT2-wN5Li8jZ0_XetMVOyskGGDhQkM8GJGEE-U_6aSc66CjKMgaOTS4ZgcKiBRDi05uSJIeXeq-wxCWZDjzplPGKezOCbLz50X1zcCRXbm99zV-6o6cJTu-m1P1SDpOwbpkZx9XBwtoMuMqUhL3xzJPhG5kvHoW49g8McWA3_VAnS386IhbXcOtmogYBk-0mR1I3LMLNLSV4w1rV9iv2hQW7-eiYhSh9E"
            };
            context.Users.Add(ananya);

            var rohan = new ApplicationUser
            {
                UserName = "singh.pottery@email.com",
                Email = "singh.pottery@email.com",
                PasswordHash = PasswordHelper.HashPassword("artisan123"),
                FullName = "Rohan Singh",
                Role = "Artisan",
                StudioName = "Singh Pottery",
                CraftType = "Ceramics",
                IsApproved = false,
                JoinedDate = new DateTime(2023, 9, 28),
                ProfileImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuBUX-R03dbLvenvPOQnR5YqNzsV1s7Hq7faOb5V_5XnpVSe2iVBujMXKKDvOHCKxrpFzsYAcNolXMqKXvP36pRwzal6tG005zytBOG79kPAxXsULpMfR4MylMvR1NHCqhEvEXtYsz2QVylrNzYKSHp9WTtzuxKbr_q5AMhXClRNPlxOJjwMDyOzK7maI4LBdjEQazd3mJep1UCGTM7ZbA4Xsfn99RxioFl2mvRDXl8DsaTW83tpEFIOME4T90jdVqM0f8O69WKhPPg"
            };
            context.Users.Add(rohan);

            // ===================== COLLECTOR USERS =====================
            var rajesh = new ApplicationUser { UserName = "rajesh@email.com", Email = "rajesh@email.com", PasswordHash = PasswordHelper.HashPassword("collector123"), FullName = "Keval Gelani", Role = "Collector", IsApproved = true, JoinedDate = new DateTime(2023, 8, 1) };
            var anita = new ApplicationUser { UserName = "anita@email.com", Email = "anita@email.com", PasswordHash = PasswordHelper.HashPassword("collector123"), FullName = "Anita Desai", Role = "Collector", IsApproved = true, JoinedDate = new DateTime(2023, 8, 15) };
            var suresh = new ApplicationUser { UserName = "suresh@email.com", Email = "suresh@email.com", PasswordHash = PasswordHelper.HashPassword("collector123"), FullName = "Suresh Raina", Role = "Collector", IsApproved = true, JoinedDate = new DateTime(2023, 9, 1) };
            var priya = new ApplicationUser { UserName = "priya.sharma@email.com", Email = "priya.sharma@email.com", PasswordHash = PasswordHelper.HashPassword("collector123"), FullName = "Priya Sharma", Role = "Collector", IsApproved = true, JoinedDate = new DateTime(2023, 9, 10) };
            var vikram = new ApplicationUser { UserName = "v.malhotra@email.com", Email = "v.malhotra@email.com", PasswordHash = PasswordHelper.HashPassword("collector123"), FullName = "Vikram Malhotra", Role = "Collector", IsApproved = true, JoinedDate = new DateTime(2023, 11, 5) };
            var priyaDas = new ApplicationUser { UserName = "priya.das@email.com", Email = "priya.das@email.com", PasswordHash = PasswordHelper.HashPassword("collector123"), FullName = "Priya Das", Role = "Collector", IsApproved = true, JoinedDate = new DateTime(2023, 12, 1) };
            var meera = new ApplicationUser { UserName = "meera@email.com", Email = "meera@email.com", PasswordHash = PasswordHelper.HashPassword("collector123"), FullName = "Meera Shah", Role = "Collector", IsApproved = true, JoinedDate = new DateTime(2023, 10, 1) };
            var vikramSingh = new ApplicationUser { UserName = "vikram.singh@email.com", Email = "vikram.singh@email.com", PasswordHash = PasswordHelper.HashPassword("collector123"), FullName = "Vikram Singh", Role = "Collector", IsApproved = true, JoinedDate = new DateTime(2023, 10, 5) };
            var sureshPrabhu = new ApplicationUser { UserName = "suresh.prabhu@email.com", Email = "suresh.prabhu@email.com", PasswordHash = PasswordHelper.HashPassword("collector123"), FullName = "Suresh Prabhu", Role = "Collector", IsApproved = true, JoinedDate = new DateTime(2023, 10, 10) };
            var priyaDevi = new ApplicationUser { UserName = "priya.devi@email.com", Email = "priya.devi@email.com", PasswordHash = PasswordHelper.HashPassword("collector123"), FullName = "Priya Devi", Role = "Collector", IsApproved = true, JoinedDate = new DateTime(2023, 10, 15) };

            context.Users.AddRange(rajesh, anita, suresh, priya, vikram, priyaDas, meera, vikramSingh, sureshPrabhu, priyaDevi);
            await context.SaveChangesAsync();

            // ===================== PRODUCTS =====================
            var products = new List<Product>
            {
                new Product { Title = "Spotted Earth Bowl", Description = "Minimalist handmade ceramic bowl with speckled glaze. Perfect for everyday use or as a decorative piece.", Price = 45, Category = "Ceramics", StockQuantity = 15, ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuBHjoCRQb8WVNAhuYRWmU-FLZDhzPPVGfWvxShwfO6kJAoXX2jwegmOo0AGabWI82B5MnojQHY1n8gJDEGejP8E8gWSsdR48krUYDZbH8lBd3YDXrhUK6LZXT0LBJgL16fJ6LykMtfdipSa-s9lUKK-lc_9GJjLidffJbxL3bk6tyoMoPxT8bD_CGF_QtLBnF7TJQjNPlXsdwot3QtoZXmQzlHqE-I3k_6ZP9ZugqemuUnePqCPzD8MSo94OPEHYHnOtawNl2kh5Uo", IsActive = true, ArtisanId = keval.Id, CreatedAt = new DateTime(2023, 10, 1) },
                new Product { Title = "Rustic Clay Vase", Description = "Hand-thrown rustic clay vase with natural earth tones. A stunning centerpiece for any home.", Price = 45, Category = "Ceramics", StockQuantity = 8, ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuD5pkzuX5g6qwYQkcZNNOPqCrdqp9c3X1nAGMLNWISBVmotZs2cdZcrd9A9JHF8NTwE7Uz7g1zhn2hTFpU_4qaaShrxS5T8UkN75Js4Sq74LAoj7KCG_DS9dRAzml1jS1G3lSMIPGNW0fCmMY0isUaa-RF1_0T0QQZXZrVqIfHxd0vquZfIwOLawihXwdy1waR5IDNzqS0Dqls8iyE3N_5WFmWo8B3ydBnMIxWmfmFaP_e0aJ_180oxrFCiXJQfViX_KalCUp3qubo", IsActive = true, ArtisanId = keval.Id, CreatedAt = new DateTime(2023, 10, 5) },
                new Product { Title = "Indigo Speckled Bowl", Description = "Handcrafted speckled ceramic breakfast bowl with indigo glaze. Food-safe and microwave friendly.", Price = 32, Category = "Ceramics", StockQuantity = 20, ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuDTSWEA6sQkiFyxml0BwEf1KuKWSNEOJtq_AyhAGsSrdQoXMGs3yRsUx4Z41EnjDiwMQC3XacK0dT3CpPcVFcIspeql7-caj5Nz72PPrK2QrmF6Udeh2FSHT_j4saI2D6wGG-QCQcz6-VKE1PY6M9WNI7voEdd_MaRZJq0SUlrTDC1vBkesh2U0W3vZmEFI3YAkerilQvZrguZF5AmS_qyeV5MlCzzlWitp2-Vx9xdHGuVTKLtUlJViHXiP3cBTzMpQ2E4mkX9yfqY", IsActive = true, ArtisanId = keval.Id, CreatedAt = new DateTime(2023, 10, 10) },
                new Product { Title = "Hand-thrown Ceramic Bowl", Description = "Elegant hand-thrown ceramic bowl with smooth finish. Each piece is unique.", Price = 45, Category = "Ceramics", StockQuantity = 12, ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuAMAitYgJRduJPxxt0n1_rwzqMCg-ndn9176dy6SldC7kspasui3sL_OeYSstw79zDKf4LTS9ZtRUP-zgTozciJntbz1ib856P46DZ1oCpcjli0aBVuJtZ_ALFBOq_V_Fd0JAD-vbXpyftoHl7uynPDk8vnG3raRdtzrop8CfACCeGcUvzpGARykFqV-rMT-q94gRwhK6nHo6P9kHO_6-80_Zkun7E-ksIv9bO1ab-ZAoj1QKS3F96rfscml8t6R3D8Mur2WUMBe-s", IsActive = true, ArtisanId = keval.Id, CreatedAt = new DateTime(2023, 10, 15) },
                new Product { Title = "Hand-carved Walnut Bowl", Description = "Natural finish, 12-inch diameter hand-carved walnut bowl. Perfect for serving or display.", Price = 85, Category = "Woodwork", StockQuantity = 6, ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuABHctyerCmuTdF0oNaKtO50ZzBTymBYLpXBT3tcBYY_ziKMnDdY9Sx9x6deCu55eT4sFLEyEYwaDducuvgF22YXrQcNu5cprfirx4G4Z5b3t16ExXMEe4XNeTrO30l223SgbQf0oiVzYGcKzNw1QCYzW3mNOmT2aYm4y4VJ3bA9ODfoPnGpW38dlWtvS-xSGD7d6bx_KBeH67KlxpDStu-EtGcOEz8jSk2bIB0qs9TGq1ZHP0LlwlzGaI9hPLkmCVowtw7PNV9nqE", IsActive = true, ArtisanId = keval.Id, CreatedAt = new DateTime(2023, 10, 18) },
                new Product { Title = "Live Edge Board", Description = "Handcrafted walnut wood cutting board with natural live edges. Each piece is one-of-a-kind.", Price = 89, Category = "Woodwork", StockQuantity = 10, ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuB9gaAKkoLD6yN2Wo6yTnCjuLQ61Qh1cl1-ibuSdz2NrONAkfh8XojRTMTdpXORoaXCFqMTn1caqiVUulSjDa4KHkLdKW10vtevs4lnGrvBQvA_zrQoQErEyCF2-q0IGgoXtFlwIyr8mDNjih6Lvp6jqcl9i79H808kO4O8nf3_CeDqpL3dvfU6cgV-TxWJhmZ4SZRUp8qoDOhfXtPR3Sc5jUND6Cy9-PhYi2cBFqyNY8mN-SOJhwuORTtA4zlerOU0UGi0CUu2WRA", IsActive = true, ArtisanId = raj.Id, CreatedAt = new DateTime(2023, 10, 2) },
                new Product { Title = "Hand-Carved Servers", Description = "Polished cherry wood hand-carved salad serving spoons. Set of 2.", Price = 54, Category = "Woodwork", StockQuantity = 14, ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuDpitWZ5dlBe6Snbg66C6zAP6c_AUhV2I9gpNmZ5dgCpiOapxQPgOrYU7raOING07452P-opbRWg1Ai00HrfFKmwBlQrVDvDpQSpvPifwr7A8aYL2AYeEnR6BU5QV4Qv83dGnpnlcAmLIAvPvPcQm9jFFSLJqDfb5RuRizFUx1_rdtwFkCWyauZCoOnQXwqEcHiydigimVDkYXPNn9llb7gHnVX7r8QouIjRTyeS7Su6VXGBAek7S0NlBEzoJT_JmbruHdsd5iLIhc", IsActive = true, ArtisanId = raj.Id, CreatedAt = new DateTime(2023, 10, 8) },
                new Product { Title = "Moonstone Silver Ring", Description = "Delicate handmade silver ring with a natural moonstone setting. Adjustable size.", Price = 89, Category = "Jewelry", StockQuantity = 7, ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuBPiX_uSulpoFIVH6z7PPdiXxRrLEQlphDe3BfGMW6R7og4BZbH3zTpWt27ohdiLNUexvVe15uzhpbfy2KsohA21OKJ6uTj60Tw6QHmNLcBLK2fYMr-cqNxyNx37B7ZZZ6y7Wei35o_myVIxd7hoHekDVCIA75xweoRtucBMNw03ug0SazpvsKz4uyu06XI80-NgkZdvQljhYNuGOKktYIny5CeWMaLemX6Vts5i3L4rqLP7S32lEJmJtNtk5XEuxpOKYcgPlP6ga8", IsActive = true, ArtisanId = parth.Id, CreatedAt = new DateTime(2023, 10, 3) },
                new Product { Title = "Silver Moon Drops", Description = "Artisanal hand-hammered sterling silver crescent earrings. Lightweight and elegant.", Price = 120, Category = "Jewelry", StockQuantity = 5, ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuDljRrC5AO5Ir3t2WNBbS_HffWwrSkpYfW3ag--N-yWvL3vxMRbnI5hJdFfBVPd-Po9UvMqNKG_EOSN8_5U_-8h3bYZc4fuoRpDdW718PWnc4KXI11J6J_N_ifMZXYpE1JHn2sj4loxaPvAivw_qsIQhsxfOP8EyfDY2PBWgEZ3X-_aJBxOKrYy1D88y1SYESTj5YbTkk36n8-D5QuUSE2DTO5sjY2VcTw2jIn7qPs2AnU1WjM19zX3BXFV7akv6_tYpm943ukJ92g", IsActive = true, ArtisanId = parth.Id, CreatedAt = new DateTime(2023, 10, 12) },
                new Product { Title = "Artisan Messenger Bag", Description = "Rich brown leather messenger bag with brass buckles. Handstitched with premium full-grain leather.", Price = 210, Category = "Leather", StockQuantity = 4, ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuBnoztqAG2S3K3P3sNAbIS6tPxxQb7kTf9gRxYw3_WjxpVJX6dgLHp7jMuNE3wkOGjvKyRMavvRJA5PSId20ms_QtkCYAArGWeWbq2BFbsP1fX9u-EEO7cIDA6xoW1ow7DvpHXH5Se7C5jszOZMeBtvXkWb4xGysbh2XdbYrvdXiA9v4eVMx71L7f1uVxayodfwAH7jTSS2aJDkg9hVvsYEIpqRNeuZCUXmoO15vp04IYudOg-iWLBYg3jIVZql6KVCgBHTiQ6gMpE", IsActive = true, ArtisanId = saddleArtisan.Id, CreatedAt = new DateTime(2023, 10, 4) },
                new Product { Title = "Geometric Throw Blanket", Description = "Hand-woven textile blanket with geometric patterns. Made from 100% organic cotton.", Price = 125, Category = "Textiles", StockQuantity = 9, ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuBUEjNRicLLZ1jaBcDyECKTwwLqlOWVO4iEpYV0LZ0n2Z2A0EFR4wQI6OKjEWzJEKeg_YJIToJwkDlrn2b2ksxo3onVtYMRdTMRJnTcYRMDbtJvM9Ky9aQUVAQ5J40F4avzD72QCf3Q2eXvxo8OmDuEDl5Q86gPEuHRRRCSIl10cRvRgNTPlW5sMcENi0El_CqXE2Ilv93YdHl95sVl_3IRhu1yVcRO8xuD8tzVTsvS7Ger2xn9IKaz2_sg4YYrWjuDu13rbHHe0r8", IsActive = true, ArtisanId = ravi.Id, CreatedAt = new DateTime(2023, 10, 6) },
                new Product { Title = "Linen Woven Throw", Description = "Textured hand-woven linen throw blanket in soft beige. Perfect for cozy evenings.", Price = 65, Category = "Textiles", StockQuantity = 11, ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuDShUf-cc0W-W2Pk8yE5UU4v5UXen-3EIhvFTOQEPYDJJNmAbpSdf3tZlyqBh7xRcRQdQGeyyIgVul0rUaeD0nT29zSYAzI-9JqQeTZw-OeiBetlxCfxnOy2BDYBWwuECbCjCymkideSTb_teU9wpFKv67EQyS4lmh3UmXvHlF1MnFma36RxaEAXJv0Qwov4-rrOrGIpxjWf5ikzdnlPfj99tDj5ReG66j5gEprixhaV5L6f0-nkHwzPF5mTFpDqq61czuGisFFJDY", IsActive = true, ArtisanId = ravi.Id, CreatedAt = new DateTime(2023, 10, 9) },
                new Product { Title = "Natural Linen Hand Towel", Description = "100% organic linen hand towel in Sand color. Soft and absorbent.", Price = 24, Category = "Textiles", StockQuantity = 25, ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuDKuiUJxdxRjHvjCbsIkAWzDrQ-iRmT227mGDsq9Y7APyjPQljWmE70TLOYYdVx18mbwsnv3ChUpEiNx7qtr1QqKtrxKoKM2rWqxhtohANfMNXz8gmGBH-C3vwzM7ocM1Zv_MnkLs2GnApuuDjcnh-DP2FTFKLfB_habDx0LSyw16KfGZYATuTXZJnOd3Upk1QGsJBGeCPNeNUxYL_QYi62A2epkESJjHH-LxrTrWoa45VDuavN1gYeJTaX_VsitkvQeI06F8xY_Q8", IsActive = true, ArtisanId = ravi.Id, CreatedAt = new DateTime(2023, 10, 14) },
                new Product { Title = "Artisan Linen Tea Towel", Description = "Artisanal woven linen tea towel with stripes. Set of 2 towels.", Price = 18, Category = "Textiles", StockQuantity = 30, ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuBxhKd8csx4Gn4vfsmf_L0sHmpRFC0hsqlLFHe4qQxqUvuMLylErV5YKgL1-58reWeFLL5liq_qkTyXmu_070ZcGiQMYP8PGS3UgNn60VvV-htKSZSTbebe6Sk2gVVfZItGwsrM6AdiJjK1kbnsU89kTS58enMTVRhfPMbK_BW7F_yMNXWINeE9fZLfCy-YRpsrE8RfhIXjFL7MxnE9n-bEjgTSljH50hAzCdJXHoykf62AqEU6bwF2u6FWt0HOvBFiOHSTWLL5pWk", IsActive = true, ArtisanId = ravi.Id, CreatedAt = new DateTime(2023, 10, 16) },
                new Product { Title = "Hand-painted Vase", Description = "Beautiful hand-painted ceramic vase with blue patterns. A statement piece for any room.", Price = 1200, Category = "Ceramics", StockQuantity = 10, ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuDJTRRNemrtPunoOBHylnPWCD9MGXa-aPCOjazrR2oUGy8hRLsPvTwuZlL9uhn8V-FzAqZEeSz4pYtsVh2eM0oRJcJHPXuMratshr-vYlBMrBsHrKaPIGmKAzeb2JXrTkJ0HDx_kjeDRMdUNpJ3HNlzHPSTCnyMysZZR_P2ynnYFWTEQ3OcTzdu3FH3-_w6T5icMnfrqkJLSrW-OHrXiLIoENqv-061GazrjlljlNbswFb_6A5X4UYN4Gy_1gXDs2y2rN-Vg1iOCvQ", IsActive = true, ArtisanId = ananya.Id, CreatedAt = new DateTime(2023, 10, 20) },
                new Product { Title = "Hand-loomed Scarf", Description = "Traditional hand-loomed wool scarf in multicolor design. Warm and stylish.", Price = 2500, Category = "Textiles", StockQuantity = 2, ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuCgwL8oWhbHuIHbiv95vLz2-Sl1ZKyNf7HElScSRKvaYYV5mPkuIe_jXhI4hukPzsCljYc7E2pGA0MQNmAIAQfokF8kLtcti9n7t2V6GW4ggq3aTtLvUhxM2fgskoAfPfdVkJ5T8hGPS9wf2fXXykUb7mF5JmqrQXORv4LyBdwpl1UGsqGR8qT9rQa8yNcZYL3i7hYpsbsjxMwAcDW-c-MapV9_VVBkFvy6ILOqniO0o7KJ1LjP0YzJEZPh9hMqHAqS7HdmNGCrWIo", IsActive = true, ArtisanId = ananya.Id, CreatedAt = new DateTime(2023, 10, 22) },
                new Product { Title = "Carved Walnut Bowl", Description = "Exquisitely carved walnut wood serving bowl. Perfect for salads and fruits.", Price = 850, Category = "Woodwork", StockQuantity = 0, ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuCs2mlF5bdOTBVKSHBWoxtFJ4rY5OH6VAWLZK4_cADr01jquv2q0MEoyv7byIPF2ecBu82qOkolhDNHeJWpfcaSJqULm99cAw5dPEa_rDuAbizR4f6l1Ha3QMK_So6CaEX7waMBKMxwhzZGq0LcECb5MXyXlo4GxMrM-FLEJpWzZUPubhzu1lMgqLpkTEM3c9eCM2DchhjzxyYuEOtL0ufyFOXmU1zU1PwNF8ZsMgtBb5KyRrk457bjTLli2VG936kFspD94ASG8j0", IsActive = true, ArtisanId = ananya.Id, CreatedAt = new DateTime(2023, 10, 24) },
                new Product { Title = "Vintage Oil Lamp", Description = "Vintage copper and glass oil lamp. A beautiful decorative piece with a rustic charm.", Price = 3200, Category = "Home Decor", StockQuantity = 8, ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuA7ELl7R0bafXmglq7_XahfMMtebbUuUlA1BCw31splsnrXWSqBT9opvI115gZ6sGQwnZXCEPVHqlllCeJablzU7D_hDRXCsTgl0Mi7M7jTJHHW7owBE9NumoDYadJ8s_jDXCjpNCUiPYwlGg7sJtEEKPC4N8jKsDdv91h9STQC_tUjZe9Wn12GErX18DUZsMM-NoN7DHXk2yfEQQVsjDbKu7bsVMoVcocL6uTZYCUEGTd8rnITgg29SZA9CdQ25EXHMkebQ3ZMTmI", IsActive = true, ArtisanId = ananya.Id, CreatedAt = new DateTime(2023, 10, 25) },
            };

            context.Products.AddRange(products);
            await context.SaveChangesAsync();

            // ===================== ORDERS =====================
            var order1 = new Order { OrderNumber = "ORD-99210001", CustomerId = rajesh.Id, OrderDate = new DateTime(2023, 10, 24), Subtotal = 4500, ShippingCost = 0, Tax = 810, TotalAmount = 5310, Status = "Delivered", FirstName = "Rajesh", LastName = "Kumar", Address = "123 MG Road", City = "Mumbai", State = "MH", ZipCode = "400001", PaymentMethod = "Credit Card" };
            var order2 = new Order { OrderNumber = "ORD-99220002", CustomerId = anita.Id, OrderDate = new DateTime(2023, 10, 25), Subtotal = 1200, ShippingCost = 0, Tax = 216, TotalAmount = 1416, Status = "Shipped", FirstName = "Anita", LastName = "Desai", Address = "45 Park Street", City = "Kolkata", State = "WB", ZipCode = "700016", PaymentMethod = "Credit Card" };
            var order3 = new Order { OrderNumber = "ORD-99230003", CustomerId = sureshPrabhu.Id, OrderDate = new DateTime(2023, 10, 25), Subtotal = 850, ShippingCost = 0, Tax = 153, TotalAmount = 1003, Status = "Pending", FirstName = "Suresh", LastName = "Prabhu", Address = "78 Laxmi Nagar", City = "Delhi", State = "DL", ZipCode = "110092", PaymentMethod = "PayPal" };
            var order4 = new Order { OrderNumber = "ORD-99240004", CustomerId = meera.Id, OrderDate = new DateTime(2023, 10, 26), Subtotal = 2100, ShippingCost = 0, Tax = 378, TotalAmount = 2478, Status = "Shipped", FirstName = "Meera", LastName = "Shah", Address = "22 CG Road", City = "Ahmedabad", State = "GJ", ZipCode = "380006", PaymentMethod = "Credit Card" };
            var order5 = new Order { OrderNumber = "ORD-99250005", CustomerId = vikramSingh.Id, OrderDate = new DateTime(2023, 10, 26), Subtotal = 3400, ShippingCost = 0, Tax = 612, TotalAmount = 4012, Status = "Pending", FirstName = "Vikram", LastName = "Singh", Address = "56 Civil Lines", City = "Jaipur", State = "RJ", ZipCode = "302001", PaymentMethod = "Credit Card" };
            var order6 = new Order { OrderNumber = "ORD-77210006", CustomerId = rajesh.Id, OrderDate = new DateTime(2023, 10, 12), Subtotal = 2450, ShippingCost = 0, Tax = 441, TotalAmount = 2891, Status = "Pending", FirstName = "Rajesh", LastName = "Kumar", Address = "123 MG Road", City = "Mumbai", State = "MH", ZipCode = "400001", PaymentMethod = "Credit Card" };
            var order7 = new Order { OrderNumber = "ORD-77180007", CustomerId = anita.Id, OrderDate = new DateTime(2023, 10, 10), Subtotal = 1200, ShippingCost = 0, Tax = 216, TotalAmount = 1416, Status = "Shipped", FirstName = "Anita", LastName = "Desai", Address = "45 Park Street", City = "Kolkata", State = "WB", ZipCode = "700016", PaymentMethod = "Credit Card" };
            var order8 = new Order { OrderNumber = "ORD-77150008", CustomerId = vikramSingh.Id, OrderDate = new DateTime(2023, 10, 8), Subtotal = 4800, ShippingCost = 0, Tax = 864, TotalAmount = 5664, Status = "Delivered", FirstName = "Vikram", LastName = "Singh", Address = "56 Civil Lines", City = "Jaipur", State = "RJ", ZipCode = "302001", PaymentMethod = "Credit Card" };
            var order9 = new Order { OrderNumber = "ORD-77120009", CustomerId = priyaDevi.Id, OrderDate = new DateTime(2023, 10, 5), Subtotal = 950, ShippingCost = 0, Tax = 171, TotalAmount = 1121, Status = "Delivered", FirstName = "Priya", LastName = "Devi", Address = "89 Ashok Marg", City = "Lucknow", State = "UP", ZipCode = "226001", PaymentMethod = "PayPal" };
            var order10 = new Order { OrderNumber = "ORD-77090010", CustomerId = suresh.Id, OrderDate = new DateTime(2023, 10, 1), Subtotal = 3100, ShippingCost = 0, Tax = 558, TotalAmount = 3658, Status = "Shipped", FirstName = "Suresh", LastName = "Raina", Address = "34 Station Road", City = "Pune", State = "MH", ZipCode = "411001", PaymentMethod = "Credit Card" };

            context.Orders.AddRange(order1, order2, order3, order4, order5, order6, order7, order8, order9, order10);
            await context.SaveChangesAsync();

            // ===================== ORDER ITEMS =====================
            var orderItems = new List<OrderItem>
            {
                new OrderItem { OrderId = order1.Id, ProductId = products[0].Id, Quantity = 2, UnitPrice = 45 },
                new OrderItem { OrderId = order1.Id, ProductId = products[9].Id, Quantity = 1, UnitPrice = 210 },
                new OrderItem { OrderId = order1.Id, ProductId = products[14].Id, Quantity = 1, UnitPrice = 1200 },
                new OrderItem { OrderId = order2.Id, ProductId = products[14].Id, Quantity = 1, UnitPrice = 1200 },
                new OrderItem { OrderId = order3.Id, ProductId = products[16].Id, Quantity = 1, UnitPrice = 850 },
                new OrderItem { OrderId = order4.Id, ProductId = products[7].Id, Quantity = 1, UnitPrice = 89 },
                new OrderItem { OrderId = order4.Id, ProductId = products[9].Id, Quantity = 1, UnitPrice = 210 },
                new OrderItem { OrderId = order5.Id, ProductId = products[17].Id, Quantity = 1, UnitPrice = 3200 },
                new OrderItem { OrderId = order6.Id, ProductId = products[15].Id, Quantity = 1, UnitPrice = 2500 },
                new OrderItem { OrderId = order7.Id, ProductId = products[14].Id, Quantity = 1, UnitPrice = 1200 },
                new OrderItem { OrderId = order8.Id, ProductId = products[17].Id, Quantity = 1, UnitPrice = 3200 },
                new OrderItem { OrderId = order8.Id, ProductId = products[10].Id, Quantity = 1, UnitPrice = 125 },
                new OrderItem { OrderId = order9.Id, ProductId = products[16].Id, Quantity = 1, UnitPrice = 850 },
                new OrderItem { OrderId = order10.Id, ProductId = products[17].Id, Quantity = 1, UnitPrice = 3200 },
            };

            context.OrderItems.AddRange(orderItems);
            await context.SaveChangesAsync();

            // ===================== ARTISAN APPLICATIONS =====================
            var pendingArtisan1 = new ApplicationUser { UserName = "pottery.co@email.com", Email = "pottery.co@email.com", PasswordHash = PasswordHelper.HashPassword("artisan123"), FullName = "Handmade Pottery Co.", Role = "Artisan", StudioName = "Handmade Pottery Co.", CraftType = "Ceramics", IsApproved = false, JoinedDate = new DateTime(2023, 10, 24) };
            var pendingArtisan2 = new ApplicationUser { UserName = "rusticleather@email.com", Email = "rusticleather@email.com", PasswordHash = PasswordHelper.HashPassword("artisan123"), FullName = "Rustic Leather Goods", Role = "Artisan", StudioName = "Rustic Leather Goods", CraftType = "Leather", IsApproved = false, JoinedDate = new DateTime(2023, 10, 22) };
            var pendingArtisan3 = new ApplicationUser { UserName = "greenleaf@email.com", Email = "greenleaf@email.com", PasswordHash = PasswordHelper.HashPassword("artisan123"), FullName = "Green Leaf Botanicals", Role = "Artisan", StudioName = "Green Leaf Botanicals", CraftType = "Organic Skincare", IsApproved = false, JoinedDate = new DateTime(2023, 10, 21) };
            var pendingArtisan4 = new ApplicationUser { UserName = "urbanwood@email.com", Email = "urbanwood@email.com", PasswordHash = PasswordHelper.HashPassword("artisan123"), FullName = "Urban Woodcarvers", Role = "Artisan", StudioName = "Urban Woodcarvers", CraftType = "Woodworking", IsApproved = false, JoinedDate = new DateTime(2023, 10, 20) };

            context.Users.AddRange(pendingArtisan1, pendingArtisan2, pendingArtisan3, pendingArtisan4);
            await context.SaveChangesAsync();

            var applications = new List<ArtisanApplication>
            {
                new ArtisanApplication { ShopName = "Handmade Pottery Co.", Specialty = "Ceramics", SubmissionDate = new DateTime(2023, 10, 24), Status = "Pending", ApplicantId = pendingArtisan1.Id, ShopImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuA418DdbBmE4M7Y8cn0WsOuC51hVHybk-hprxv5BK3kbi6TFaqX9j2eWYqdlj4pm1bjke-FQSpVkwFZ4VnHvJabvMPcDQEbJWTs_KJwsC5GIN6mWgjcmJ2NuNswqUI_DCjxPlMSG7N1RBp2_zsj8tEYXliHSlwYz6BySsKTzKmfebvCv6buRNf64lgtqy5Hclgz2qWZm3iXGyi5vOtBS8pVgcYExY42VeAyG6a2P6ctVT9byO1Cq49M4HcrATwQNsxJGNIpLBTR2kU" },
                new ArtisanApplication { ShopName = "Rustic Leather Goods", Specialty = "Leatherwork", SubmissionDate = new DateTime(2023, 10, 22), Status = "Pending", ApplicantId = pendingArtisan2.Id, ShopImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuDR1OG6vZitUS_9ksy-EwFPF-Ka4M_DmB1P31y0WMtswR-D3_uFROQs4nhxQGrxGX8dXcqgc8VXePAEcmqLSV8rr5thWkbnLXi5fulZqIMW02r8279ls-DuctcSb1h8g25ZqVev6i4EWy837Yu5vHWHHFKOZn0YaXWXbI3mzL31294doETrF2nyNBsHntPfpcukWAuokYcDToMNshcMJIyd1FrWjhwzZ9ndXYRL-MzwgezvFvo3IayCv0-xHA4Aab5LMtKXVQ7JOk0" },
                new ArtisanApplication { ShopName = "Green Leaf Botanicals", Specialty = "Organic Skincare", SubmissionDate = new DateTime(2023, 10, 21), Status = "Pending", ApplicantId = pendingArtisan3.Id, ShopImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuCTi4DwhWMfroiGh2EtTgtlbTeekOUTgcorid5MP7CCqt4IuicD9yvdImKKgwUNni3i-8KjpxPHgnnab1lz8Gr9ubPuo_QaCnRzgjqCJREtHemfQ6UBi6fm3jZ-ZfLMcSM_APkAa9Vdb9AfwDTt4er114IvVzykOjz1lRtHQwiDBazNGi91p5vCp08WHPLm1FW9T60RT6s-Dbsw4uRqTfRMbxKBroHJseBUr9SyAxMemHirdYrIZJycUpvPhhI6JPJQp187Kw5yU8A" },
                new ArtisanApplication { ShopName = "Urban Woodcarvers", Specialty = "Woodworking", SubmissionDate = new DateTime(2023, 10, 20), Status = "Pending", ApplicantId = pendingArtisan4.Id, ShopImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuDTdlexUr3NjgJrP0ISLD4d5gxEG8C38fWfTiUla5ggWQ5-b0U4y0txB8tEI1KmfLFZtWooe1gijkZS0-oNpxN3tF7InIqTfmxoQCLH8CNPFC5OvpDGnoFJWj5KiecX3ImEqn694EIPPFI-jBU1ilWoQeSNLmc-gbFK3pE24yXMacyYWa8F1lTDE_cMURFgat7Wtgb2FHcLrMyQO2WUqbfB0jXwyhPSNWbl3Yh0LVk4UR-eeyZberEMVPqf744jSGn3diDcGyOdeY0" },
            };

            context.ArtisanApplications.AddRange(applications);
            await context.SaveChangesAsync();
        }
    }
}
