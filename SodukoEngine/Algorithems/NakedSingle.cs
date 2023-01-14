using System.Runtime.CompilerServices;
using SodukoSolverOmega.SodukoEngine.Objects;

namespace SodukoSolverOmega.SodukoEngine.Algorithems;

internal class NakedSingle : Constraint
{
    public static bool Solve(Board board, ValueTuple<int, int> Cellcords)
    {
        if (board[Cellcords].Possibilities.Count != 1) return false;
        FixCellHidden(board,Cellcords);
        return true;
    }

}