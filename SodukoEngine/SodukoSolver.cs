using SodukoSolverOmega.Configuration.Consts;
using SodukoSolverOmega.SodukoEngine.Algorithems;
using SodukoSolverOmega.SodukoEngine.Objects;

namespace SodukoSolverOmega.SodukoEngine.Solvers;

public class SodukoSolver
{
    private readonly Board BoardToSolve;
    private readonly Lexer lexer;


    public SodukoSolver(string boardText)
    {
        //initialise a lexer
        lexer = new Lexer();
        //get the board in a board format using the lexer
        BoardToSolve = lexer.getBoard(boardText);
        //set up the first constrints
        BoardToSolve.InitialiseCells();
        updatePosssibilities(BoardToSolve);
    }

    public Board Solve()
    {        
  
        //check if the board is valid
        if (!BoardToSolve.IsValidBoard()) return null;
        //check if solved without resorting to bruteforcing
        BoardToSolve.PropagateConstraints();
        if (BoardToSolve.IsSolved()) return BoardToSolve;
        //start bruteforcing
        return BackTrack(BoardToSolve);
    }
    //backtracking algorithem
    //explanation:
    //1 - get the next cell to guess on using smart hueristics
    //2 - guess and create a new copy of the board after placing it and constraining the new info
    //3 -  check if solved, and return it. else-  check if solvable and go deeper. check if deeper one is 
    //solved and returns it, if reaches a dead end return null
    public static  void RemoveFromPossibilities(Board board, Cell cell)
    {
        foreach (var cords in cell.getAllPeers())
            if (!board[cords].Isfilled)
            {
                //hide this bit functonality in a func
                board[cords].possibilities.RemovePossibility(Possibilities.ValueToPossibility(cell.Value));
                board.EffectedQueue.Enqueue(board[cords.Item1, cords.Item2].Cords);
            }
    }
    
    public void updatePosssibilities(Board board)
    {
        //remove every fixed cell from Possibilities of others
        for (var i = 0; i < Consts.BOARD_SIZE; i++)
        for (var j = 0; j < Consts.BOARD_SIZE; j++)
            if (board.cells[i, j].Isfilled)
                RemoveFromPossibilities(board, board.cells[i, j]);
    }
    public Board CreateNextMatrix(Board board, ValueTuple<int,int> cords, char Value)
    {
        var NextMat = board.CopyMatrix();
        //add set val and remove from Possibilities to the same func?
        NextMat[cords].SetVal(Value);
        RemoveFromPossibilities(NextMat, NextMat[cords]);
        NextMat.PropagateConstraints();
        return NextMat;
    }
    public  Board BackTrack(Board currentState)
    {
        var NextCell = currentState.GetNextCell();
        foreach (char possibility in NextCell.possibilities.ListPossibilities())
        {
            var newState = CreateNextMatrix(currentState, NextCell.Cords, possibility);
            if (newState.IsSolved()) return newState;
            if (newState.IsSolvable())
            {
                //Console.WriteLine("entering with");
                //Console.WriteLine(newState.ToString);
                Console.WriteLine(newState.ToString);
                var deepState = BackTrack(newState);
                //Console.WriteLine("exited with");
                //if(deepState != null){Console.WriteLine(deepState.ToString);}
                if (deepState != null && deepState.IsSolved()) return deepState;
            }
        }

        return null;
    }
}