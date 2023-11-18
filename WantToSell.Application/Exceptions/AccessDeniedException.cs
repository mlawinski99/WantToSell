namespace WantToSell.Application.Exceptions;

public class AccessDeniedException : Exception
{
    private static readonly string DefaultMessage = "You do not have access to this resource!";
    
    public AccessDeniedException(string message) : base(message)
    {

    }
    public AccessDeniedException() : base(DefaultMessage)
    {

    }
}