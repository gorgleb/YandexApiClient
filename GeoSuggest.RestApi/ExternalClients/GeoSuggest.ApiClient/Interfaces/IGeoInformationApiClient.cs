using GeoSuggest.RestApi.V1.Contracts;

namespace GeoSuggest.ApiClient.Interfaces
{
    public interface IGeoInformationApiClient
    {
        Task<AddressDisplay> SuggestRelevantAsync(string addressString);
    }
}