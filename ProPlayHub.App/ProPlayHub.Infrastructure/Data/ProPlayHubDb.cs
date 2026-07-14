// ProPlayHub.Infrastructure/Data/ProPlayHubDb.cs
using Microsoft.EntityFrameworkCore;
using ProPlayHub.Core.Entities;

namespace ProPlayHub.Infrastructure.Data
{
    public class ProPlayHubDb : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Package> Packages => Set<Package>();
        public DbSet<AddOn> AddOns => Set<AddOn>();
        public DbSet<DiscountCode> DiscountCodes => Set<DiscountCode>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Subscription> Subscriptions => Set<Subscription>();

        public ProPlayHubDb(DbContextOptions<ProPlayHubDb> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder b)
        {
            // ---- SEED DỮ LIỆU MẪU ----
            b.Entity<Package>().HasData(
                new Package { Id = 1, Name = "Streaming & Multiplayer", Category = "StreamingBundle", BasePrice = 9.99m, Description = "Cloud saves, multiplayer, rentals" },
                new Package { Id = 2, Name = "PC Core", Category = "PlatformSpecific", BasePrice = 7.99m, Description = "PC perks" },
                new Package { Id = 3, Name = "PlayStation Core", Category = "PlatformSpecific", BasePrice = 7.99m, Description = "PS perks" }
            );

            b.Entity<AddOn>().HasData(
                new AddOn { Id = 1, PackageId = 1, Name = "Extra Cloud 100GB", Price = 1.99m },
                new AddOn { Id = 2, PackageId = 1, Name = "In-game Items Pack", Price = 2.99m }
            );

            b.Entity<DiscountCode>().HasData(
                new DiscountCode { Id = 1, Code = "APP15", PercentOff = 15m, ExpiresAt = DateTime.UtcNow.AddMonths(6), IsAppOnly = true }
            );

            // Khai báo ràng buộc đơn giản (nếu cần)
            b.Entity<AddOn>()
             .HasIndex(a => new { a.PackageId, a.Name })
             .IsUnique(false);
        }
    }
}
