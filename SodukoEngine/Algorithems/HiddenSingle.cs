using SodukoSolverOmega.SodukoEngine.Objects;

namespace SodukoSolverOmega.SodukoEngine.Algorithems;

internal class HiddenSingle : Constraint
{

    public override bool Solve(Board board, ValueTuple<int,int> cellCords)
    {
        
        
        foreach (var possibility in BitUtils.ListPossibilities(board[cellCords].Possibilities))
        {
            if (!BitUtils.ExsistInRowPeers(board, cellCords, possibility))
            {
                HelperFuncs.FixCell(board,cellCords, possibility);
                return true;
            }
            if (!BitUtils.ExsistInColPeers(board, cellCords, possibility))
            {
                HelperFuncs.FixCell(board,cellCords, possibility);
                return true;
            }
            if (!BitUtils.ExsistInBoxPeers(board, cellCords, possibility))
            {
                HelperFuncs.FixCell(board, cellCords, possibility);
                return true;
            }
        }
        return false;
    }
}