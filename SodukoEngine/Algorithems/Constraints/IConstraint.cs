using SodukoSolverOmega.SodukoEngine.Objects;

namespace SodukoSolverOmega.SodukoEngine.Algorithems;

internal interface IConstraint
{
    public bool Solve(Board board);
}