using SodukoSolverOmega.Configuration.Consts;
using SodukoSolverOmega.SodukoEngine.Objects;

namespace SodukoSolverOmega.SodukoEngine.Algorithems;

public class HiddenPairs : HiddenTupple, IConstraint
{
    public bool Solve(Board board)
    {
        if (board.FilledCells < Consts.HIDDEN_PAIR_THRESHOLD) return false;
        return HiddenTuples(board, 2);
    }
}