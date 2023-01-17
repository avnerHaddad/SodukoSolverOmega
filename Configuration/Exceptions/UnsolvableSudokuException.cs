namespace SodukoSolverOmega.Configuration.Exceptions;

public class UnsolvableSudokuException : SodukoExceptions
{
    public UnsolvableSudokuException() : base("the soduko you entered is unsolvable and therefore could not be solved")
    {
    }
}