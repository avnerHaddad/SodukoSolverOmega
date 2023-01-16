namespace SodukoSolverOmega.Configuration.Exceptions;

internal class InvalidCharException : SodukoExceptions
{
    public InvalidCharException() : base(
        "the string you entered contains character that do not allighn with the soduko format")
    {
    }
}