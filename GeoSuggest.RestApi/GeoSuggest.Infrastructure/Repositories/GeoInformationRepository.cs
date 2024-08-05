using GeoSuggest.Core.Interfaces.Repositories;

namespace GeoSuggest.Infrastructure.Repositories;

public partial class GeoInformationRepository : IGeoInformationRepository
{
    public GeoInformationRepository(IServiceProvider serviceProvider)
    {
        ClientsInit(serviceProvider);
    }
}