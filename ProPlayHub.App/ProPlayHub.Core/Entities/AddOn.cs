namespace ProPlayHub.Core.Entities;

public class AddOn
{
    public int Id { get; set; }
    public int PackageId { get; set; }
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
}
