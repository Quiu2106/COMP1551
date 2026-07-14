// ProPlayHub.Infrastructure/Data/DbBootstrap.cs
using Microsoft.EntityFrameworkCore;

namespace ProPlayHub.Infrastructure.Data
{
    public static class DbBootstrap
    {
        public static async Task EnsureCreatedAsync(ProPlayHubDb db)
        {
            // Tạo DB nếu chưa có, áp dụng seed trong OnModelCreating
            await db.Database.EnsureCreatedAsync();
        }
    }
}
