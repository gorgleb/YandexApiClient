using System.Text.Json.Serialization;

namespace GeoSuggest.GeosuggestClient.Contracts;

public class SuggestResponse
{
    [JsonPropertyName("suggest_reqid")]
    public string SuggestRegId { get; set; }
 
    [JsonPropertyName("results")]
    public List<Result> Results { get; set; }
}