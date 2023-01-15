using SodukoSolverOmega.SodukoEngine.Objects;

namespace SodukoSolverOmega.SodukoEngine.Algorithems;

internal class HiddenSingle : IConstraint
{

    public  bool Solve(Board board)
    {

        foreach (var cell in board.cells)
        {
            if (!cell.Isfilled)
            {
                foreach (var possibility in BitUtils.ListPossibilities(cell.Possibilities))
                {
                    if (!BitUtils.ExsistInRowPeers(board, cell.Cords, possibility))
                    {
                        HelperFuncs.FixCell(board,cell.Cords, possibility);
                        return true;
                    }
                    if (!BitUtils.ExsistInColPeers(board, cell.Cords, possibility))
                    {
                        HelperFuncs.FixCell(board,cell.Cords, possibility);
                        return true;
                    }
                    if (!BitUtils.ExsistInBoxPeers(board, cell.Cords, possibility))
                    {
                        HelperFuncs.FixCell(board, cell.Cords, possibility);
                        return true;
                    }
                }

            }
        }
        return false;
    }
}