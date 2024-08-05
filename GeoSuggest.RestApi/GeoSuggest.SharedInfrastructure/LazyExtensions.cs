using Microsoft.Extensions.DependencyInjection;

namespace GeoSuggest.SharedInfrastructure;

public static class LazyExtensions
{
    public static void AddScopedLazy<TService, TImplementation>(this IServiceCollection services)
        where TService : class
        where TImplementation : class, TService
    {
        services.AddScoped<TService, TImplementation>();
        services.AddScoped<Lazy<TService>>(provider => new Lazy<TService>(provider.GetRequiredService<TService>));
    }
}