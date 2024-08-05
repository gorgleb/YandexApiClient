using GeoSuggest.Core.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GeoSuggest.Infrastructure.Repositories;

public partial class GeoInformationRepository : IGeoInformationRepository
{
    private IServiceProvider _serviceProvider = null!;
    
    private Lazy<IGeoSuggestRepository> _geoSuggest = null!;

    private void ClientsInit(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _geoSuggest = _serviceProvider.GetRequiredService<Lazy<IGeoSuggestRepository>>();
    }
    
    public IGeoSuggestRepository GeoSuggest => _geoSuggest.Value;
}