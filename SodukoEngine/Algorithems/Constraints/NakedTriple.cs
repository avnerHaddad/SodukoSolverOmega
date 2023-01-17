using SodukoSolverOmega.Configuration.Consts;
using SodukoSolverOmega.SodukoEngine.Objects;

namespace SodukoSolverOmega.SodukoEngine.Algorithems;

public class NakedTriple : NakedTuples, IConstraint
{
    public bool Solve(Board board)
    {
        if (board.FilledCells < Consts.NAKED_TRIPLE_THRESHOLD) return false;
        return DoNakedTuples(board, 3);
    }
}