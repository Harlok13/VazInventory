namespace Inventory.BL.CustomExceptions;

public class FileMissingException : ApplicationException
{
    public DateTime ErrorTimeStamp { get; set; }

    public FileMissingException(string message, DateTime time) : base(message)
    {
        ErrorTimeStamp = time;
    }
}