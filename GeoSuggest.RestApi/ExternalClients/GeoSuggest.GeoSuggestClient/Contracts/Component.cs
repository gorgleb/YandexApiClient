using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace GeoSuggest.GeosuggestClient.Contracts;

public class Component
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("kind")]
    public List<string> Kind { get; set; }
}