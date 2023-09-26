namespace Inventory.BL.CustomExceptions;

public class ApplicationException : Exception
{
    protected ApplicationException(string message) : base(message) {}
}