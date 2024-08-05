using System.Text.Json.Serialization;

namespace GeoSuggest.GeosuggestClient.Contracts;

public class Result
{
    [JsonPropertyName("address")]
    public Address Address { get; set; }
}