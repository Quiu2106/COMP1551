using Microsoft.EntityFrameworkCore;
using ProPlayHub.Core.Entities;
using ProPlayHub.Infrastructure.Data;

namespace ProPlayHub.Infrastructure.Services;

public interface ICheckoutService
{
    Task<Order> CreateOrderAsync(
        int userId, int packageId, IEnumerable<int> addOnIds,
        string? discountCode, bool viaApp = true);
}

public class CheckoutService : ICheckoutService
{
    private readonly ProPlayHubDb _db;
    private readonly ICrmService _crm;
    private readonly IPaymentGateway _pay;

    public CheckoutService(ProPlayHubDb db, ICrmService crm, IPaymentGateway pay)
    {
        _db = db; _crm = crm; _pay = pay;
    }

    public async Task<Order> CreateOrderAsync(
        int userId, int packageId, IEnumerable<int> addOnIds,
        string? discountCode, bool viaApp = true)
    {
        var pkg = await _db.Packages.FindAsync(packageId)
                  ?? throw new Exception("Package not found");
        var addOns = await _db.AddOns
                              .Where(a => addOnIds.Contains(a.Id))
                              .ToListAsync();

        if (!await _crm.CheckAvailabilityAsync(packageId, addOnIds))
            throw new Exception("Package unavailable");

        var subtotal = pkg.BasePrice + addOns.Sum(a => a.Price);
        decimal appDisc = viaApp ? Math.Round(subtotal * 0.15m, 2) : 0m;
        decimal codeDisc = 0m;

        if (!string.IsNullOrWhiteSpace(discountCode))
        {
            var dc = await _db.DiscountCodes
                              .FirstOrDefaultAsync(x => x.Code == discountCode && x.ExpiresAt > DateTime.UtcNow);
            if (dc != null) codeDisc = Math.Round(subtotal * (dc.PercentOff / 100m), 2);
        }

        var total = Math.Max(0, subtotal - appDisc - codeDisc);

        var verified = await _pay.VerifyCardAsync("411111111111", "123", DateTime.UtcNow.AddYears(2));
        if (!verified) throw new Exception("Card verification failed");
        var txn = await _pay.ChargeAsync(total, "tok_demo");

        var order = new Order
        {
            UserId = userId,
            PackageId = packageId,
            AddOnIds = addOns.Select(a => a.Id).ToList(),
            Subtotal = subtotal,
            Discount = appDisc + codeDisc,
            Total = total,
            Status = "Paid",
            CreatedAt = DateTime.UtcNow
        };

        _db.Orders.Add(order);
        await _db.SaveChangesAsync();
        return order;
    }
}
