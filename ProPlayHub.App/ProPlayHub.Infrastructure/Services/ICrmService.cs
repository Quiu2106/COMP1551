namespace ProPlayHub.Infrastructure.Services;

public interface ICrmService
{
    Task<bool> CheckAvailabilityAsync(int packageId, IEnumerable<int> addOnIds);
    Task PushPersonalizedOffersAsync(int userId);
}

public class MockCrmService : ICrmService
{
    public Task<bool> CheckAvailabilityAsync(int packageId, IEnumerable<int> addOnIds)
        => Task.FromResult(true); // luôn sẵn hàng (demo)

    public Task PushPersonalizedOffersAsync(int userId)
        => Task.CompletedTask; // demo không làm gì
}
