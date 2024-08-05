namespace GeoSuggest.SharedKernel.Exceptions;

public class TooManyRequestsException : ClientException
{
    public TooManyRequestsException(string? message) : base(message)
    {
    }
}