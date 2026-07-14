namespace ProPlayHub.Core.Entities;

public class DiscountCode
{
    public int Id { get; set; }
    public string Code { get; set; } = "";
    public decimal PercentOff { get; set; } // 15 = 15%
    public DateTime ExpiresAt { get; set; }
    public bool IsAppOnly { get; set; } = true;
}
