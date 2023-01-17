using SodukoSolverOmega.Configuration.Consts;
using SodukoSolverOmega.SodukoEngine.Objects;

namespace SodukoSolverOmega.SodukoEngine.Algorithems;

public class NakedSingle : IConstraint
{
    public bool Solve(Board board)
    {
        //if smaller then threshold dont bother checking
        if (board.FilledCells < Consts.NAKED_SINGLE_THRESHOLD)
        {
            board.ClearEffectedCells();
            return false;
        }

        //check all the cells that were effected by prev sets  and see if they have only one possibility, if yes then set it
        while (board.EffectedQueue.Count > 0)
        {
            var cell = board.EffectedQueue.Dequeue();
            if (board[cell].possibilities.CountOfSetBits() != 1 || board[cell].Isfilled) return false;
            FixCellHidden(board, cell);
            return true;
        }

        return false;
    }

    public static void FixCellHidden(Board board, ValueTuple<int, int> cell)
    {
        //sets the cell to its only posssibility and removes that possibility from its peers
        board.cells[cell.Item1, cell.Item2].HiddenSet();
        board.RemoveFromPossibilities(board[cell]);
    }
}