using SodukoSolverOmega.Configuration.Consts;
using SodukoSolverOmega.Configuration.Exceptions;
using SodukoSolverOmega.SodukoEngine.Algorithems;
using SodukoSolverOmega.SodukoEngine.Objects;

namespace SodukoSolverOmega.SodukoEngine.Solvers;

public class SodukoSolver
{
    private static List<IConstraint> Constraints;
    private readonly Board BoardToSolve;
    private readonly Lexer lexer;
    


    public SodukoSolver(string boardText)
    {
        Constraints = new List<IConstraint>();
        Constraints.Add(new NakedSingle());
        Constraints.Add(new HiddenSingle());
        Constraints.Add(new NakedPairs());
        //Constraints.Add(new InterSectionRemoval());
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
        if (!BoardToSolve.IsValidBoard()) throw new UnsolvableSudokuException();;
        //check if solved without resorting to bruteforcing
        PropagateConstraints(BoardToSolve);
        if (BoardToSolve.IsSolved()) return BoardToSolve;
        //start bruteforcing
        Board solved = BackTrack(BoardToSolve);
        if(solved == null){throw new UnsolvableSudokuException();}

        return solved;
    }
    public  Board BackTrack(Board currentState)
    {
        //get the next cell to guess on using smart hueristics
        var NextCell = currentState.GetNextCell();
        foreach (uint possibility in NextCell.possibilities.ListPossibilities())
        {
            //guess and create a new copy of the board after placing it and constraining the new info
            var newState = CreateNextMatrix(currentState, NextCell.Cords, possibility);
            
            //check if solved, and return it. else-  check if solvable and go deeper. check if deeper one is 
            //solved and returns it, if reaches a dead end riase exception null
            if (newState.IsSolved()) return newState;
            if (newState.IsSolvable())
            {
                var deepState = BackTrack(newState);
                if (deepState != null && deepState.IsSolved()) return deepState;
            }
        }

        return null;
    }
    
    private Board CreateNextMatrix(Board board, ValueTuple<int,int> cords, uint Value)
    {
        var NextMat = board.CopyMatrix();
        //add set val and remove from Possibilities to the same func?
        NextMat[cords].SetVal(Value);
        NextMat.RemoveFromPossibilities(NextMat[cords]);
        PropagateConstraints(NextMat);
        return NextMat;
    }
    private void PropagateConstraints(Board board)
    {
        //for each constraint in the list
        //run it a across all cells
        //if no we dont find any special cells.combo than move on to the next constraint
        //if we find a special cell/combo then move back to first constraint
        for (var i = 0; i < Constraints.Count; i++)
            if (Constraints[i].Solve(board))
                i = -1;
    }
}