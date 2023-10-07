namespace Domain.Exceptions;

public class DuplicateEntryException : ApplicationException
{
    public DuplicateEntryException(string message) : base(message)
    {
    }

    public DuplicateEntryException(int code) : base(code)
    {
    }
}