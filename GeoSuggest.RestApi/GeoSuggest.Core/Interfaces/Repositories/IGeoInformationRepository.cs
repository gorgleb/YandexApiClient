namespace GeoSuggest.Core.Interfaces.Repositories;

public interface IGeoInformationRepository
{
    IGeoSuggestRepository GeoSuggest { get; }
}