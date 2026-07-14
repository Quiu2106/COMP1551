using ProPlayHub.Infrastructure.Services;
using ProPlayHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ProPlayHub.App;

public partial class TestCheckoutPage : ContentPage
{
    private readonly ICheckoutService _checkout;
    private readonly ProPlayHubDb _db;

    public TestCheckoutPage(ICheckoutService checkout, ProPlayHubDb db)
    {
        InitializeComponent();
        _checkout = checkout;
        _db = db;
    }

    private async void OnTestClicked(object sender, EventArgs e)
    {
        try
        {
            // 🔹 Kiểm tra trước xem đã có Package seed chưa
            var package = await _db.Packages.FirstOrDefaultAsync();
            if (package == null)
            {
                await DisplayAlert("Lỗi", "Chưa có package nào trong DB.", "OK");
                return;
            }

            // 🔹 Tạo đơn hàng mẫu (userId=1, packageId=1, addOnIds=[1,2], code=APP15)
            var order = await _checkout.CreateOrderAsync(1, 1, new[] { 1, 2 }, "APP15", viaApp: true);

            ResultLabel.Text = $"✅ Tạo đơn hàng thành công!\n" +
                               $"Subtotal: {order.Subtotal:C}\n" +
                               $"Discount: {order.Discount:C}\n" +
                               $"Total: {order.Total:C}\n" +
                               $"Status: {order.Status}";
        }
        catch (Exception ex)
        {
            ResultLabel.Text = $"❌ Lỗi: {ex.Message}";
        }
    }
}
