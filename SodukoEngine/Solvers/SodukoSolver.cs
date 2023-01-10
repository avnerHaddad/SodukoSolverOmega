using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SodukoSolverOmega.Configuration.Consts;
using SodukoSolverOmega.IO;
using SodukoSolverOmega.SodukoEngine.Algorithems;
using SodukoSolverOmega.SodukoEngine.Objects;


namespace SodukoSolverOmega.SodukoEngine.Solvers
{
    internal class SodukoSolver
    {
        private Lexer lexer;
        private Board BoardToSolve;


        public SodukoSolver()
        {
            lexer = new Lexer();
            BoardToSolve = new Board();
        }

        public Board solve(string boardText)
        {
            //get the board in a board format using the lexer
            BoardToSolve = lexer.getBoard(boardText);
            //BoardToSolve.setCellPeers();
            if (BoardToSolve.IsValidBoard())
            {
                BoardToSolve.InitialiseConstarints();
                return Solving.BackTrack(BoardToSolve);
            }
            return null;

            //add some constraints

            //backtracking
            //BackUpCells();
            //BackTrackSolve(0, 0,BoardToSolve);
        }

 
    }
}



/*
public bool BackTrackSolve(int row, int col, Board current)
{

    //classic backtracking algorithem
    Board newBoard = new Board(current);
    //we reached the end of the board therfore quit
    if (row == Consts.BOARD_HEIGHT)
    {
        return true;
    }
    //we reached a filled cell therfore skip
    if (BoardToSolve[row, col].isfilled)
    {
        if (col == Consts.BOARD_WIDTH - 1)
        {
            // move to the next row, since cols are over

            return BackTrackSolve(row + 1, 0, newBoard);
        }
        else
        {
            // move to next col
            return BackTrackSolve(row, col + 1, newBoard);
        }
    }
    //fill in all possibilites until one is solvable
    while (newBoard.Cells[row, col].hasPosssibilities)
    {
        //try guessing and if the guess succeeds,go to the next layer

        if (newBoard.Cells[row, col].Guess())
        {

            if (BackTrackSolve(row, col, newBoard))
            {
                return true;
            }
        }


    }

    //if not solvable reset cell and return false. will return to previous guesser...
    //board[row, col].resetVal();
    return false;





}
*/