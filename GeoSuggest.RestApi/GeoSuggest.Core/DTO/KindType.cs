using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace GeoSuggest.Core.DTO;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum KindType : byte
{
    /// <summary>
    /// Страна
    /// </summary>
    [EnumMember(Value = "COUNTRY")]
    Country = 1,
    /// <summary>
    /// Регион
    /// </summary>
    [EnumMember(Value = "REGION")]
    Region,
    /// <summary>
    /// Область
    /// </summary>
    [EnumMember(Value = "PROVINCE")]
    Province,
    /// <summary>
    /// Район области, городской совет
    /// </summary>
    [EnumMember(Value = "AREA")]
    Area,
    /// <summary>
    /// Населенный пункт
    /// </summary>
    [EnumMember(Value = "LOCALITY")]
    Locality,
    /// <summary>
    /// Улица
    /// </summary>
    [EnumMember(Value = "STREET")]
    Street,
    /// <summary>
    /// Дом
    /// </summary>
    [EnumMember(Value = "HOUSE")]
    House,
    /// <summary>
    /// Квартира
    /// </summary>
    [EnumMember(Value = "APARTMENT")]
    Apartment,
    /// <summary>
    /// Неопределенный тип компонента адреса
    /// </summary>
    [EnumMember(Value = "UNKNOWN")]
    Unknown
}