using SodukoSolverOmega.SodukoEngine.Objects;

namespace SodukoSolverOmega.SodukoEngine.Algorithems;

internal class HiddenSingle : Constraint
{

    public override bool Solve(Board board, ValueTuple<int,int> cellCords)
    {
        foreach (var possibility in board[cellCords].Possibilities)
        {
            if (!ExsistInRowPeers(board, cellCords, possibility))
            {
                HelperFuncs.FixCell(board,cellCords, possibility);
                return true; ;
            }
            if (!ExsistInColPeers(board, cellCords, possibility))
            {
                HelperFuncs.FixCell(board,cellCords, possibility);
                return true; ;
            }
            if (!ExsistInBoxPeers(board, cellCords, possibility))
            {
                HelperFuncs.FixCell(board, cellCords, possibility);
                return true; ;
            }
        }
        return false;
    }
}