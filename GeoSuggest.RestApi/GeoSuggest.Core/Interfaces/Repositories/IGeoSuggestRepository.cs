using GeoSuggest.Core.DTO;

namespace GeoSuggest.Core.Interfaces.Repositories;

public interface IGeoSuggestRepository
{
    /// <summary>
    /// Подобрать релевантный адрес по переданной строке
    /// </summary>
    /// <param name="addressString">Адрес в строку</param>
    /// <returns>Разложенный на структуру адрес</returns>
    Task<Address> SuggestRelevantAsync(string addressString);
}