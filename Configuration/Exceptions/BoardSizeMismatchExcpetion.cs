namespace SodukoSolverOmega.Configuration.Exceptions;

public class BoardSizeMismatchExeption : SodukoExceptions
{
    public BoardSizeMismatchExeption() : base(
        "the string you entered is too long to create a board from or does not have an integer square root")
    {
    }
}