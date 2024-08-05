using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace GeoSuggest.RestApi.Filters;

public class NSwagEnumExtensionSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema is null)
            throw new ArgumentNullException(nameof(schema));

        if (context is null)
            throw new ArgumentNullException(nameof(context));

        if (context.Type.IsEnum)
        {
            schema.Extensions.Add("x-enum-varnames", new NSwagEnumOpenApiExtension(context));
        }
    }
}