namespace Inventory.BL.CustomExceptions;

public class FileMissingException : ApplicationException
{
    public DateTime ErrorTimeStamp { get; set; }

    public FileMissingException(string message, DateTime time) : this(message, time, null) { }

    public FileMissingException(string message, DateTime time, Exception? inner) : base(message, inner)
    {
        ErrorTimeStamp = time;
    }
}