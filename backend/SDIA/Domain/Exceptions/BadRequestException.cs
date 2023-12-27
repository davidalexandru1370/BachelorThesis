namespace Domain.Exceptions;

public class BadRequestException : ApplicationException
{
    public BadRequestException(string message) : base(message)
    {
    }

    public BadRequestException(int code) : base(code)
    {
    }
}