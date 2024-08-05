using GeoSuggest.Core.DTO;
using GeoSuggest.Core.Interfaces;
using GeoSuggest.Core.Interfaces.Repositories;

namespace GeoSuggest.Core.Services;

public class GeosuggestService : IGeosuggestService
{
    private readonly IGeoInformationRepository _repository;

    public GeosuggestService(IGeoInformationRepository repository)
    {
        _repository = repository;
    }

    public async Task<Address> SuggestRelevantAsync(string addressString)
    {
        return await _repository.GeoSuggest.SuggestRelevantAsync(addressString);
    }
}