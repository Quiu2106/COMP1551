namespace ProPlayHub.Core.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = "";
    public string Email { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    public int Age { get; set; }
    public string Location { get; set; } = "";
    public string PlatformPref { get; set; } = ""; // PC / Xbox / PlayStation
    public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
