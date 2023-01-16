namespace SodukoSolverOmega.Configuration.Exceptions;

internal class BoardSizeMismatchExeption : SodukoExceptions
{
    public BoardSizeMismatchExeption() : base("the string you entered is too long to create a board from")
    {
    }
}