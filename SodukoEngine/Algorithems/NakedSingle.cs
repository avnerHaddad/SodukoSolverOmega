using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using SodukoSolverOmega.SodukoEngine.Objects;

namespace SodukoSolverOmega.SodukoEngine.Algorithems;

internal class NakedSingle : IConstraint
{
    public bool Solve(Board board)
    {
        while(board.EffectedQueue.Count > 0)
        {
            var cell = board.EffectedQueue.Dequeue();
            if (BitUtils.CountOfSetBits(board[cell].Possibilities) != 1 || board[cell].Isfilled) return false;
            HelperFuncs.FixCellHidden(board,cell);
            return true;
        }

        return false;
    }

}