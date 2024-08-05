using GeoSuggest.Core.Interfaces.Repositories;
using GeoSuggest.GeosuggestClient;
using GeoSuggest.Infrastructure.Health;
using GeoSuggest.Infrastructure.Mapping;
using GeoSuggest.Infrastructure.Repositories;
using GeoSuggest.Infrastructure.RestClients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace GeoSuggest.Infrastructure;

public static class ConfigureInfrastructure
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransient<IGeoInformationRepository, GeoInformationRepository>();

        services.AddExternalClientsInfrastructure(configuration);
        
        services.AddMappings();

        services.AddHealthChecks()
            .AddCheck<RestClientsHealth>("suggest-maps.yandex", HealthStatus.Unhealthy);
        
        return services;
    }

    private static void AddExternalClientsInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddGeoSuggestClient(configuration);

        services.AddRestClientRepositories();
    }
}