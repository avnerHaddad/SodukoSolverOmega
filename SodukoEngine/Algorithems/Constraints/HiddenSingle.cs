using SodukoSolverOmega.Configuration.Consts;
using SodukoSolverOmega.SodukoEngine.Objects;

namespace SodukoSolverOmega.SodukoEngine.Algorithems;

public class HiddenSingle : IConstraint
{
    public bool Solve(Board board)
    {
        if (board.FilledCells < Consts.HIDDEN_SINGLE_THRESHOLD) return false;
        return HiddenSingleLogic(board);
    }

    public bool HiddenSingleLogic(Board board)
    {
        foreach (var cell in board.cells)
            if (!cell.Isfilled)
                foreach (var possibility in cell.possibilities.ListPossibilities())
                {
                    if (!Possibilities.ExsistInRowPeers(board, cell, possibility))
                    {
                        FixCell(board, cell.Cords, possibility);
                        return true;
                    }

                    if (!Possibilities.ExsistInColPeers(board, cell, possibility))
                    {
                        FixCell(board, cell.Cords, possibility);
                        return true;
                    }

                    if (!Possibilities.ExsistInBoxPeers(board, cell, possibility))
                    {
                        FixCell(board, cell.Cords, possibility);
                        return true;
                    }
                }

        return false;
    }

    public static void FixCell(Board board, ValueTuple<int, int> cell, uint val)
    {
        board[cell].SetVal(val);
        board.RemoveFromPossibilities(board[cell]);
    }
}