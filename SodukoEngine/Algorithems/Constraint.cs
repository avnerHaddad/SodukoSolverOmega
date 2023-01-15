using SodukoSolverOmega.SodukoEngine.Objects;

namespace SodukoSolverOmega.SodukoEngine.Algorithems;

abstract class Constraint : IConstraint
{
    //helper funcs
    protected static void FixCellHidden(Board board, ValueTuple<int, int> cell)
    {
        board.cells[cell.Item1, cell.Item2].HiddenSet();
        board.RemoveFromPossibilities(board[cell]);
    }

    //checks if a possibility exsists in colPeers of cell

    //checks if a possibility exsists in rowPeers of cell
    //solve func to be overiden
    public virtual bool Solve(Board board, ValueTuple<int, int> Cellcords)
    {
        return true;
    }
}