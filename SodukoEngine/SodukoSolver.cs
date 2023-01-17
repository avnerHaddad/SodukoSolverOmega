using SodukoSolverOmega.Configuration.Exceptions;
using SodukoSolverOmega.SodukoEngine.Algorithems;
using SodukoSolverOmega.SodukoEngine.Objects;

namespace SodukoSolverOmega.SodukoEngine.Solvers;

public class SodukoSolver
{
    public static List<IConstraint> Constraints;
    private readonly Board BoardToSolve;
    private readonly Lexer lexer;
    public int iterations;


    public SodukoSolver(string boardText)
    {
        //initialise constraints list
        Constraints = new List<IConstraint>();
        Constraints.Add(new NakedSingle());
        Constraints.Add(new HiddenSingle());
        Constraints.Add(new NakedPairs());
        Constraints.Add(new NakedTriple());
        Constraints.Add(new HiddenPairs());
        Constraints.Add(new HiddenTriples());
        //initialise a lexer
        lexer = new Lexer();
        //get the board in a board format using the lexer
        BoardToSolve = lexer.getBoard(boardText);
        //set up the first constrints
        BoardToSolve.InitialiseCells();
        BoardToSolve.updatePosssibilities();
    }

    public Board Solve()
    {
        //check if the board is valid
        if (!BoardToSolve.IsValidBoard()) throw new UnsolvableSudokuException();
        //check if solved without resorting to bruteforcing
        PropagateConstraints(BoardToSolve);
        if (BoardToSolve.IsSolved()) return BoardToSolve;
        //start bruteforcing
        var solved = BackTrack(BoardToSolve);
        if (solved == null) throw new UnsolvableSudokuException();

        return solved;
    }
    //backtracking algorithem
    //explanation:
    //1 - get the next cell to guess on using smart hueristics
    //2 - guess and create a new copy of the board after placing it and constraining the new info
    //3 -  check if solved, and return it. else-  check if solvable and go deeper. check if deeper one is 
    //solved and returns it, if reaches a dead end return null

    //creates a deep copy of current matrix, places the new val in it and propagates constraints
    public Board CreateNextMatrix(Board board, ValueTuple<int, int> cords, uint Value)
    {
        var NextMat = board.CopyMatrix();
        //add set val and remove from Possibilities to the same func?
        NextMat[cords].SetVal(Value);
        NextMat.RemoveFromPossibilities(NextMat[cords]);
        PropagateConstraints(NextMat);
        return NextMat;
    }

    public Board BackTrack(Board currentState)
    {
        var NextCell = currentState.GetNextCell();
        foreach (var possibility in NextCell.possibilities.ListPossibilities())
        {
            var newState = CreateNextMatrix(currentState, NextCell.Cords, possibility);
            if (newState.IsSolved()) return newState;
            if (newState.IsSolvable())
            {
                iterations++;
                if (iterations == 10000) Environment.Exit(0);
                var deepState = BackTrack(newState);
                if (deepState != null && deepState.IsSolved()) return deepState;
            }
        }

        return null;
    }

    public void PropagateConstraints(Board board)
    {
        //start with first element of constraints list
        //run it a across all cells
        //if no succces than move on to the next constraint
        //if there is success then move back to first constarint and remove the succcesful cell frrom the queue
        for (var i = 0; i < Constraints.Count; i++)
            if (Constraints[i].Solve(board))
                i = -1;
    }
}