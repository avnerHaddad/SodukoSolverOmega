using System.Runtime.CompilerServices;
using SodukoSolverOmega.SodukoEngine.Objects;

namespace SodukoSolverOmega.SodukoEngine.Algorithems;

internal class NakedSingle : Constraint
{
    public static int CountBits(long value)
    {
        int count = 0;
        while (value != 0)
        {
            count++;
            value &= value - 1;
        }
        return count;
    }
    public override bool Solve(Board board, ValueTuple<int, int> Cellcords)
    {
        if (CountBits(board[Cellcords].Possibilities) != 1) return false;
        FixCellHidden(board,Cellcords);
        return true;
    }

}