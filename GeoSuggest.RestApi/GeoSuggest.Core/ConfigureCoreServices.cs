using GeoSuggest.Core.Interfaces;
using GeoSuggest.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GeoSuggest.Core;

public static class ConfigureCoreServices
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
    {
       services.AddScoped<IGeosuggestService, GeosuggestService>();

        return services;
    }
}