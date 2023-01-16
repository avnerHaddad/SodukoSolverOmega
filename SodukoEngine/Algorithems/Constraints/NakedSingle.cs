using SodukoSolverOmega.Configuration.Consts;
using SodukoSolverOmega.SodukoEngine.Objects;
using SodukoSolverOmega.SodukoEngine.Solvers;

namespace SodukoSolverOmega.SodukoEngine.Algorithems;

public class NakedSingle : IConstraint
{
    public bool Solve(Board board)
    {
        if (board.FilledCells < Consts.NAKED_SINGLE_THRESHOLD)
        {
            board.ClearEffectedCells();
            return false;
        }
        while (board.EffectedQueue.Count > 0)
        {
            var cell = board.EffectedQueue.Dequeue();
            if ((board[cell].possibilities.CountOfSetBits() != 1) || board[cell].Isfilled) return false;
            FixCellHidden(board, cell);
            return true;
        }

        return false;
    }
    public static void FixCellHidden(Board board, ValueTuple<int, int> cell)
    {
        board.cells[cell.Item1, cell.Item2].HiddenSet();
        SodukoSolver.RemoveFromPossibilities(board, board[cell]);
    }

}