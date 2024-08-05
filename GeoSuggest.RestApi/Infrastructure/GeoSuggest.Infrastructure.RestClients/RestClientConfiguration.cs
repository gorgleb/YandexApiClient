using GeoSuggest.Core.Interfaces.Repositories;
using GeoSuggest.Infrastructure.RestClients.Repositories;
using Microsoft.Extensions.DependencyInjection;
using GeoSuggest.SharedInfrastructure;

namespace GeoSuggest.Infrastructure.RestClients;

public static class RestClientConfiguration
{
    public static void AddRestClientRepositories(this IServiceCollection services)
    {
        services.AddScopedLazy<IGeoSuggestRepository, GeoSuggestRepository>();
    }
}