namespace ProPlayHub.Infrastructure.Services;

public interface IPaymentGateway
{
    Task<bool> VerifyCardAsync(string cardNumber, string ccv, DateTime exp);
    Task<string> ChargeAsync(decimal amount, string cardToken);
}

public class MockVisaCheck : IPaymentGateway
{
    public Task<bool> VerifyCardAsync(string card, string ccv, DateTime exp)
        => Task.FromResult(card.Length >= 12 && ccv.Length == 3);

    public Task<string> ChargeAsync(decimal amount, string token)
        => Task.FromResult($"TXN-{Guid.NewGuid():N}".ToUpper());
}
