using GeoSuggest.ApiClient.Interfaces;
using GeoSuggest.ApiClient.Settings;
using GeoSuggest.GeosuggestClient.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GeoSuggest.ApiClient
{
    public static class GeoInformationApiClientConfiguration
    {
        public static IServiceCollection AddGeoInformationApiClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<GeoInformationApiClientOptions>(configuration.GetSection(nameof(GeosuggestClientOptions)));
            services.AddHttpClient<IGeoInformationApiClient, GeoInformationApiClient>();
            return services;
        }
    }
}