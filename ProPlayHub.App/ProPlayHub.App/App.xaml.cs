using ProPlayHub.Infrastructure.Data;

namespace ProPlayHub.App;

public partial class App : Application
{
    public App(ProPlayHubDb db)   // DbContext được DI vào
    {
        InitializeComponent();    // 🔴 BẮT BUỘC: khớp với x:Class ở App.xaml
        db.Database.EnsureCreated();

        // (tạm) mở AppShell, trong Shell có tab Test
        MainPage = new AppShell();
    }
}
