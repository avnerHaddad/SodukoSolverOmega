using SodukoSolverOmega.SodukoEngine.Objects;

namespace SodukoSolverOmega.SodukoEngine.Algorithems;

public interface IConstraint
{
    public bool Solve(Board board);
}