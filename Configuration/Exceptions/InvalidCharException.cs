namespace SodukoSolverOmega.Configuration.Exceptions;

public class InvalidCharException : SodukoExceptions
{
    public InvalidCharException() : base(
        "the string you entered contains character that do not align with the Soduko's size")
    {
    }
}