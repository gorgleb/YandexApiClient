using System.Text.Json.Serialization;

namespace GeoSuggest.RestApi.V1.Contracts;

/// <summary>
/// Базовые значения структурированного элемента адреса
/// </summary>
public class AddressItemDisplayBase
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
}