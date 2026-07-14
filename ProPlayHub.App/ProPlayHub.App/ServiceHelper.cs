namespace ProPlayHub.App;

public static class ServiceHelper
{
    public static T GetService<T>() where T : notnull
        => Current.GetService<T>() ?? throw new InvalidOperationException($"Service {typeof(T)} not found.");

    public static IServiceProvider Current
        => Application.Current?.Handler?.MauiContext?.Services
           ?? throw new InvalidOperationException("Maui services not available.");
}
