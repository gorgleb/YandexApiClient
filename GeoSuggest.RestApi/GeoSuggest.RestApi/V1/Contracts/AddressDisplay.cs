using System.Text.Json.Serialization;

namespace GeoSuggest.RestApi.V1.Contracts;

/// <summary>
/// Адрес, разложенный по элементам
/// </summary>
public class AddressDisplay
{
    /// <summary>
    /// Страна
    /// </summary>

    [JsonPropertyName("country")]
    public CountryDisplay Country { get; set; }

    /// <summary>
    /// Регион
    /// </summary>

    [JsonPropertyName("region")]
    public RegionDisplay Region { get; set; }

    /// <summary>
    /// Город
    /// </summary>

    [JsonPropertyName("city")]
    public CityDisplay City { get; set; }

    /// <summary>
    /// Улица
    /// </summary>

    [JsonPropertyName("street")]
    public string? Street { get; set; }

    /// <summary>
    /// Дом
    /// </summary>

    [JsonPropertyName("house")]
    public string? House { get; set; }

    /// <summary>
    /// Стандартизированный адрес, соответствующий введенной поисковой строке
    /// </summary>

    [JsonPropertyName("formattedAddress")]
    public string FormattedAddress { get; set; }
}