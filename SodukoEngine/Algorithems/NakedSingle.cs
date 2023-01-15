using System.Runtime.CompilerServices;
using SodukoSolverOmega.SodukoEngine.Objects;

namespace SodukoSolverOmega.SodukoEngine.Algorithems;

internal class NakedSingle : Constraint
{
    public override bool Solve(Board board, ValueTuple<int, int> Cellcords)
    {
        if (HelperFuncs.CountOfSetBits(board[Cellcords].Possibilities) != 1 || board[Cellcords].Isfilled) return false;
        FixCellHidden(board,Cellcords);
        return true;
    }

}