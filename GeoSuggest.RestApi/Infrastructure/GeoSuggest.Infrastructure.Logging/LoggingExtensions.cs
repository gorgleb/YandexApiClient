using Elastic.Apm.DiagnosticSource;
using Elastic.Apm.Elasticsearch;
using Elastic.Apm.Extensions.Hosting;
using Elastic.Apm.GrpcClient;
using Elastic.Apm.MongoDb;
using Elastic.Apm.NetCoreAll;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
namespace GeoSuggest.Infrastructure.Logging;

public static class LoggingExtensions
{
    public static IApplicationBuilder UseWebAppMetrics(this IApplicationBuilder app, IConfiguration configuration)
    {
        app.UseAllElasticApm(configuration);
        return app;
    }
		
    public static IHostBuilder UseConsoleAppMetrics(this IHostBuilder hostBuilder)
    {
        hostBuilder.UseElasticApm(
            new HttpDiagnosticsSubscriber(),
            new ElasticsearchDiagnosticsSubscriber(),
            new GrpcClientDiagnosticSubscriber(),
            new MongoDbDiagnosticsSubscriber()
        );

        return hostBuilder;
    }
}