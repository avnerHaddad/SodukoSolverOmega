namespace SodukoSolverOmega.Configuration.Exceptions;

internal class UnsolvableSudokuException : Exception
{
    public UnsolvableSudokuException() : base("the soduko you entered is unsolvable and therfore cold not be solved")
    {
    }
}