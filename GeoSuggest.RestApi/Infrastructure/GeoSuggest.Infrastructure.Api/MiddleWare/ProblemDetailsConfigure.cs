using GeoSuggest.SharedKernel.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace GeoSuggest.Infrastructure.Api.MiddleWare;

public static class ProblemDetailsConfigure
{
    public static void ConfigureProblemDetails(Hellang.Middleware.ProblemDetails.ProblemDetailsOptions options)
    {
        options.IncludeExceptionDetails = (context, exception) => { return true; };
        
        options.Map<TooManyRequestsException>((ctx, exception) =>
            new ProblemDetails
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                Title = ReasonPhrases.GetReasonPhrase(StatusCodes.Status429TooManyRequests),
                Detail = exception.Message,
                Status = StatusCodes.Status429TooManyRequests,
                Instance = ctx.Request.Path
            });
        
        options.Map<ClientException>((ctx, exception) =>
            new ProblemDetails
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                Title = ReasonPhrases.GetReasonPhrase(StatusCodes.Status400BadRequest),
                Detail = exception.Message,
                Status = StatusCodes.Status400BadRequest,
                Instance = ctx.Request.Path
            });
    }
}