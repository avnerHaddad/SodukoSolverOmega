namespace SodukoSolverOmega.Configuration.Exceptions;

public class UnsolvableSudokuException : SodukoExceptions
{
    public UnsolvableSudokuException() : base("the soduko you entered is unsolvable and therfore cold not be solved")
    {
    }
}