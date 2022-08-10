namespace CarLib;

public class InvalidYearException :Exception
{
    public InvalidYearException() : base("Invalid year!") 
    {
    }

    public InvalidYearException(string message) : base(message)
    {

    }

    public InvalidYearException(string message, Exception inner) : base(message, inner)
    {

    }

}
