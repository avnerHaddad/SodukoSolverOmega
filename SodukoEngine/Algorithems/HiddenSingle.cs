using SodukoSolverOmega.SodukoEngine.Objects;

namespace SodukoSolverOmega.SodukoEngine.Algorithems;

internal class HiddenSingle : Constraint
{

    public static bool Solve(Board board, ValueTuple<int,int> cellCords)
    {
        foreach (var possibility in board.cells[cellCords.Item1, cellCords.Item2].Possibilities)
        {
            if (ExsistInRowPeers(board, cellCords, possibility))
            {
                HelperFuncs.FixCell(board,cellCords, possibility);
                return true; ;
            }
            if (ExsistInColPeers(board, cellCords, possibility))
            {
                HelperFuncs.FixCell(board,cellCords, possibility);
                return true; ;
            }
            if (ExsistInBoxPeers(board, cellCords, possibility))
            {
                HelperFuncs.FixCell(board, cellCords, possibility);
                return true; ;
            }
        }
        return false;
    }
}