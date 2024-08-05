using GeoSuggest.GeosuggestClient.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GeoSuggest.GeosuggestClient;

public static class GeosuggestClientConfiguration
{
    public static IServiceCollection AddGeoSuggestClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<GeosuggestClientOptions>(configuration.GetSection(nameof(GeosuggestClientOptions)));
        services.AddHttpClient<GeosuggestClient>();
        return services;
    }
}