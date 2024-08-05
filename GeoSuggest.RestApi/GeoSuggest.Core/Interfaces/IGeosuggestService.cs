using GeoSuggest.Core.DTO;

namespace GeoSuggest.Core.Interfaces;

public interface IGeosuggestService
{
    Task<Address> SuggestRelevantAsync(string addressString);
}