namespace GeoSuggest.SharedKernel.Exceptions;

public class AddressNotFoundException : ClientException
{
    public AddressNotFoundException(string? message) : base(message)
    {
    }
}