namespace ProPlayHub.Core.Entities;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int PackageId { get; set; }
    public List<int> AddOnIds { get; set; } = new();
    public decimal Subtotal { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
    public string Status { get; set; } = "Pending"; // Paid / Failed
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
