namespace ProPlayHub.Core.Entities;

public class Subscription
{
    public int Id { get; set; }
    public int PackageId { get; set; }
    public DateTime StartDate { get; set; }
    public bool AutoRenew { get; set; }
    public string PlanTier { get; set; } = "Standard";
}
