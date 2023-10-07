namespace Domain.Exceptions;

public class NotFoundException : ApplicationException
{
    public NotFoundException(string message) : base(message)
    {
        
    }
    
    public NotFoundException(int code) : base(code.ToString())
    {
        
    }
}