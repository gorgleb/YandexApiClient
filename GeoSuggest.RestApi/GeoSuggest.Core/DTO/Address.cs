namespace GeoSuggest.Core.DTO;

/// <summary>
/// Адрес, разложенный по элементам
/// </summary>
public class Address
{
    /// <summary>
    /// Страна
    /// </summary>
    public Country? Country { get; set; }
    
    /// <summary>
    /// Регион
    /// </summary>
    public Region? Region { get; set; }
    
    /// <summary>
    /// Город
    /// </summary>
    public City? City { get; set; }
    
    /// <summary>
    /// Улица
    /// </summary>
    public string? Street { get; set; }
    
    /// <summary>
    /// Дом
    /// </summary>
    public string? House { get; set; }
    
    /// <summary>
    /// Стандартизированный адрес, соответствующий введенной поисковой строке
    /// </summary>
    public string? FormattedAddress { get; set; }
}