using System.Text.Json;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Writers;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace GeoSuggest.RestApi.Filters;

public class NSwagEnumOpenApiExtension : IOpenApiExtension
{
    private readonly Type _enumType;
    public NSwagEnumOpenApiExtension(SchemaFilterContext context)
    {
        _enumType = context.Type;
    }

    public NSwagEnumOpenApiExtension(Type enumType)
    {
        _enumType = enumType;
    }

    public void Write(IOpenApiWriter writer, OpenApiSpecVersion specVersion)
    {
        string[] enums = Enum.GetNames(_enumType);
        JsonSerializerOptions options = new() { WriteIndented = true };
        string value = JsonSerializer.Serialize(enums, options);
        writer.WriteRaw(value);
    }
}