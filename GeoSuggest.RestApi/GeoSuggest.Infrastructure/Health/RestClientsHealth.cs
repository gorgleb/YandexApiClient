using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace GeoSuggest.Infrastructure.Health;

public sealed class RestClientsHealth(GeosuggestClient.GeosuggestClient client) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            var healthResult = await  client.SearchAsync("Москва", 1);
            
            return HealthCheckResult.Healthy();
        }
        catch (Exception e)
        {
            return HealthCheckResult.Unhealthy(exception: e);
        }
    }
}