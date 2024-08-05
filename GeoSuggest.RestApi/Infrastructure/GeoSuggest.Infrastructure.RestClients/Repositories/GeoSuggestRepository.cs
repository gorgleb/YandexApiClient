using GeoSuggest.Core.Exceptions;
using GeoSuggest.Core.Interfaces.Repositories;
using Mapster;

namespace GeoSuggest.Infrastructure.RestClients.Repositories;

public sealed class GeoSuggestRepository(GeosuggestClient.GeosuggestClient geosuggestClient) : IGeoSuggestRepository
{
    public async Task<Core.DTO.Address> SuggestRelevantAsync(string addressString)
    {
        var result = await geosuggestClient.SearchAsync(addressString, take: 1);

        if (result is null || result.Results is null || result.Results.Count == 0)
            throw CoreExceptions.AddressNotFound();
        
        return result.Results.First().Address.Adapt<Core.DTO.Address>();
    }
}