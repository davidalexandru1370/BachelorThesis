namespace Domain.Exceptions;

public class ForbiddenException : ApplicationException
{
    public ForbiddenException(string message) : base(message)
    {
    }

    public ForbiddenException(int code) : base(code)
    {
    }
}