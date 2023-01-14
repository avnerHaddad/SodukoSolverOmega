namespace SodukoSolverOmega.SodukoEngine.Algorithems;

public abstract class Constraint : IConstraint
{
    //helper funcs
    
    
    //solve func to be overiden
    public bool Solve()
    {
        return true;
    }
}