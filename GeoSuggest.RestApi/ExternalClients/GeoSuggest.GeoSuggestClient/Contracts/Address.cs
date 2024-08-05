using System.Text.Json.Serialization;

namespace GeoSuggest.GeosuggestClient.Contracts;

public class Address
{
    [JsonPropertyName("formatted_address")]
    public string FormattedAddress { get; set; }
    
    [JsonPropertyName("component")]
    public List<Component> Component { get; set; }
}