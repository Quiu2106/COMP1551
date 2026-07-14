namespace ProPlayHub.Core.Entities;

public class Package
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Category { get; set; } = ""; // PlatformSpecific / StreamingBundle
    public decimal BasePrice { get; set; }
    public string Description { get; set; } = "";
}
