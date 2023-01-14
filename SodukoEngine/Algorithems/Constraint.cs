using SodukoSolverOmega.SodukoEngine.Objects;

namespace SodukoSolverOmega.SodukoEngine.Algorithems;

abstract class Constraint : IConstraint
{
    //helper funcs
    public static void FixCellHidden(Board board, ValueTuple<int, int> cell)
    {
        if (board.cells[cell.Item1, cell.Item2].Isfilled) return;
        board.cells[cell.Item1, cell.Item2].HiddenSet();
        board.RemoveFromPossibilities(board.cells[cell.Item1, cell.Item2]);
    }
    
    //solve func to be overiden
    public virtual bool Solve()
    {
        return true;
    }
}