using Mapster;

namespace GeoSuggest.RestApi.Mapping.Configurations;

public class AddressMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<Core.DTO.Address, V1.Contracts.AddressDisplay>();
    }
}