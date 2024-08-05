using Asp.Versioning;
using GeoSuggest.Core.Interfaces;
using GeoSuggest.RestApi.V1.Contracts;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace GeoSuggest.RestApi.V1.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/geo-information")]
public class GeoInformationController : ControllerBase
{
    private readonly IGeosuggestService _geoSuggest;

    public GeoInformationController(IGeosuggestService geoSuggest)
    {
        _geoSuggest = geoSuggest;
    }

    /// <summary>
    /// Найти релевантный адрес и структурировать его
    /// </summary>
    /// <param name="addressString">Произвольно введенный адрес</param>
    /// <returns>Структуру релевантного адреса</returns>
    [HttpGet("suggest-relevant")]
    [ProducesResponseType(typeof(AddressDisplay), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status429TooManyRequests)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SuggestAsync(string addressString)
    {
        return Ok((await _geoSuggest.SuggestRelevantAsync(addressString)).Adapt<AddressDisplay>());
    }
}