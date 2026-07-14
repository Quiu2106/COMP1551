using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ProPlayHub.Infrastructure.Data;
using ProPlayHub.Infrastructure.Services;


namespace ProPlayHub.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            // 🔹 ĐĂNG KÝ DATABASE SQLITE CHO APP
            builder.Services.AddDbContext<ProPlayHubDb>(options =>
            {
                var dbPath = Path.Combine(FileSystem.AppDataDirectory, "proplayhub.db");
                options.UseSqlite($"Filename={dbPath}");
            });
            builder.Services.AddSingleton<ICrmService, MockCrmService>();
            builder.Services.AddSingleton<IPaymentGateway, MockVisaCheck>();
            builder.Services.AddScoped<ICheckoutService, CheckoutService>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}