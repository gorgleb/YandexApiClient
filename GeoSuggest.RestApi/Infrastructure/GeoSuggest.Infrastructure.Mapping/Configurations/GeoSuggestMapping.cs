using GeoSuggest.Core.DTO;
using GeoSuggest.GeosuggestClient.Contracts;
using Mapster;

namespace GeoSuggest.Infrastructure.Mapping.Configurations;

public class GeoSuggestMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<GeosuggestClient.Contracts.Address, Core.DTO.Address>()
            .Ignore(d => d.Country!)
            .Ignore(d => d.Region!)
            .Ignore(d => d.City!)
            .Map(d => d.Street, s => GetFirstAddressComponentName(s.Component, KindType.Street))
            .Map(d => d.House, s => GetFirstAddressComponentName(s.Component, KindType.House))
            .AfterMapping((s, d) =>
            {
                var country = GetFirstAddressComponentName(s.Component, KindType.Country);
                
                var region = GetFirstAddressComponentName(s.Component, KindType.Area) ??
                             GetFirstAddressComponentName(s.Component, KindType.Province);
                
                var city = GetFirstAddressComponentName(s.Component, KindType.Locality);

                d.Country = SetAddressItem<Country>(country);
                d.Region = SetAddressItem<Region>(region);
                d.City = SetAddressItem<City>(city);
                
            });
    }

    private static T? SetAddressItem<T>(string? country) where T: AddressItemBase, new()
    {
        if (string.IsNullOrWhiteSpace(country))
            return null;
            
        return new T
        {
            Name = country
        };
    }

    private string? GetFirstAddressComponentName(List<Component> components, KindType kindType)
    {
        switch (kindType)
        {
            case KindType.Country:
                return components.FirstOrDefault(c => FindKindByName(c, KindType.Country))?.Name;
            case KindType.Region:
                return components.FirstOrDefault(c => FindKindByName(c, KindType.Region))?.Name;
            case KindType.Area:
                return components.FirstOrDefault(c => FindKindByName(c, KindType.Area))?.Name;
            case KindType.Province:
                return components.FirstOrDefault(c => FindKindByName(c, KindType.Province))?.Name;
            case KindType.Locality:
                return components.FirstOrDefault(c => FindKindByName(c, KindType.Locality))?.Name;
            case KindType.Street:
                return components.FirstOrDefault(c => FindKindByName(c, KindType.Street))?.Name;
            case KindType.House:
                return components.FirstOrDefault(c => FindKindByName(c, KindType.House))?.Name;
            default:
                return null;
        }
    }

    private static bool FindKindByName(Component c, KindType kindType)
    {
        return c.Kind.Any(k => string.Equals(k, kindType.ToString(), StringComparison.InvariantCultureIgnoreCase));
    }
}