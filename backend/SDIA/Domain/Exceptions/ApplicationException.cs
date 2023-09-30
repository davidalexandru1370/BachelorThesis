namespace Domain.Exceptions;

public class ApplicationException : Exception
{
    public ApplicationException(string message) : base(message)
    {
        
    }

    public ApplicationException(int code) : base(code.ToString())
    {
        
    }
}