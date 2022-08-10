namespace CarLib;

public class InvalidMSRPException : Exception
{
    public InvalidMSRPException() : base("Invalid MSRP!")
    {
    }

    public InvalidMSRPException(string message) : base(message)
    {

    }

    public InvalidMSRPException(string message, Exception inner) : base(message, inner)
    {

    }

}
