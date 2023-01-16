namespace SodukoSolverOmega.Configuration.Exceptions;

public class BoardSizeMismatchExeption : SodukoExceptions
{
    public BoardSizeMismatchExeption() : base("the string you entered is too long or does not have an integer square root")
    {
    }
}