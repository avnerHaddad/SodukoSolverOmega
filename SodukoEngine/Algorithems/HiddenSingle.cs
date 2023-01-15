using SodukoSolverOmega.SodukoEngine.Objects;

namespace SodukoSolverOmega.SodukoEngine.Algorithems;

internal class HiddenSingle : Constraint
{

    public override bool Solve(Board board, ValueTuple<int,int> cellCords)
    {
        
        
        foreach (var possibility in HelperFuncs.ListPossibilities(board[cellCords].Possibilities))
        {
            if (!HelperFuncs.ExsistInRowPeers(board, cellCords, possibility))
            {
                HelperFuncs.FixCell(board,cellCords, possibility);
                return true;
            }
            if (!HelperFuncs.ExsistInColPeers(board, cellCords, possibility))
            {
                HelperFuncs.FixCell(board,cellCords, possibility);
                return true;
            }
            if (!HelperFuncs.ExsistInBoxPeers(board, cellCords, possibility))
            {
                HelperFuncs.FixCell(board, cellCords, possibility);
                return true;
            }
        }
        return false;
    }
}