namespace Domain.Exceptions;

public class NotAuthenticatedException : ApplicationException
{
    public NotAuthenticatedException(string message) : base(message)
    {
    }

    public NotAuthenticatedException(int code) : base(code)
    {
    }
}