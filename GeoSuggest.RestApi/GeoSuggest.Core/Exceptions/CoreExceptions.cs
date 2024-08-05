using GeoSuggest.SharedKernel.Exceptions;

namespace GeoSuggest.Core.Exceptions;

public class CoreExceptions
{
    public static AddressNotFoundException AddressNotFound()
    {
        return new AddressNotFoundException("Адрес не найден");
    }
    
    public static TooManyRequestsException TooManyRequests()
    {
        return new TooManyRequestsException("Превышено количество запросов к сервису геосаджеста");
    }
}