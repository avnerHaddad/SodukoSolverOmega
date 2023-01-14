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
    public static bool ExsistInBoxPeers(Board board, ValueTuple<int, int> cell, char possibility)
    {
        foreach (ValueTuple<int, int> peer in Board.rowPeers[cell])
        {
            if (board[peer].Possibilities.Contains(possibility) && !board[peer].Isfilled)
            {
                return true;
            }
        }
        return false;

    }

    //checks if a possibility exsists in colPeers of cell
    public static bool ExsistInColPeers(Board board, ValueTuple<int, int> cell, char possibility)
    {
        foreach (ValueTuple<int, int> peer in Board.colPeers[cell])
        {
            if (board[peer].Possibilities.Contains(possibility) && !board[peer].Isfilled)
            {
                return true;
            }
        }
        return false;

    }

    //checks if a possibility exsists in rowPeers of cell
    public static bool ExsistInRowPeers(Board board, ValueTuple<int, int> cell, char possibility)
    {
        foreach (ValueTuple<int, int> peer in Board.rowPeers[cell])
        {
            if (board[peer].Possibilities.Contains(possibility) && !board[peer].Isfilled)
            {
                return true;
            }
        }
        return false;

    }
    //solve func to be overiden
    public virtual bool Solve(Board board, ValueTuple<int, int> Cellcords)
    {
        return true;
    }
}