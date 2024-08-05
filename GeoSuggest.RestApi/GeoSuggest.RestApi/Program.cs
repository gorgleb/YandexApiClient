using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Asp.Versioning.ApiExplorer;
using DateOnlyTimeOnly.AspNet.Converters;
using Hellang.Middleware.ProblemDetails;
using GeoSuggest.Core;
using GeoSuggest.Infrastructure;
using GeoSuggest.Infrastructure.Api;
using GeoSuggest.Infrastructure.Api.Extensions;
using GeoSuggest.Infrastructure.Api.MiddleWare;
using GeoSuggest.Infrastructure.Api.Swagger;
using GeoSuggest.Infrastructure.Logging;
using GeoSuggest.RestApi.Filters;
using GeoSuggest.RestApi.Mapping;
using GeoSuggest.SharedInfrastructure;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);
builder.Host.WithCommonConfig();
 
builder.Services
    .AddControllers()
    .AddJsonOptions(s =>
    {
        s.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        s.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
        s.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
        s.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
    
builder.Services.AddHttpContextAccessor();
builder.Services.AddCoreServices(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddRestApiMappings();

builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

builder.Services.AddSwagger(builder.Configuration, options => {
    options.SchemaFilter<NSwagEnumExtensionSchemaFilter>();
});

builder.Services.AddProblemDetails(ProblemDetailsConfigure.ConfigureProblemDetails);

builder.Services.AddCorsPolicies(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUi(app.Services.GetRequiredService<IApiVersionDescriptionProvider>());

app.UseWebAppMetrics(builder.Configuration);
app.UseProblemDetails();
app.UseRouting();

#pragma warning disable ASP0014
app.UseEndpoints(config =>
{
    config.MapControllers();
    config.MapHealthChecks("/health/liveness", new HealthCheckOptions
    {
        Predicate = _ => true,
        ResponseWriter = (context, report) =>
        {
            context.Response.ContentType = "application/json; charset=utf-8";

            using var memoryStream = new MemoryStream();
            var options = new JsonWriterOptions { Indented = true };
            using (var jsonWriter = new Utf8JsonWriter(memoryStream, options))
            {
                jsonWriter.WriteStartObject();
                jsonWriter.WriteString("status", report.Status.ToString());
                jsonWriter.WriteStartObject("results");

                foreach (var healthReportEntry in report.Entries)
                {
                    jsonWriter.WriteStartObject(healthReportEntry.Key);
                    jsonWriter.WriteString("status",
                        healthReportEntry.Value.Status.ToString());
                    jsonWriter.WriteString("description",
                        healthReportEntry.Value.Description);
                    jsonWriter.WriteStartObject("data");

                    foreach (var item in healthReportEntry.Value.Data)
                    {
                        jsonWriter.WritePropertyName(item.Key);

                        JsonSerializer.Serialize(jsonWriter, item.Value,
                            item.Value?.GetType() ?? typeof(object));
                    }

                    jsonWriter.WriteEndObject();
                    jsonWriter.WriteEndObject();
                }

                jsonWriter.WriteEndObject();
                jsonWriter.WriteEndObject();
            }

            return context.Response.WriteAsync(Encoding.UTF8.GetString(memoryStream.ToArray()));
        }
    });
});
#pragma warning restore ASP0014
app.UseApiVersionEndpoint();
app.Run();