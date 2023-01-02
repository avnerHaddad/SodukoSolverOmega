using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SodukoSolverOmega.Configuration.Consts;
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
        public void BackUpCells()
        {
            for (int i = 0; i < Consts.BOARD_HEIGHT; i++)
            {
                for (int j = 0; j < Consts.BOARD_WIDTH; j++)
                {
                    BoardToSolve[i, j].backupPossibilities();
                }
            }
        }
        public Board solve(string boardText)
        {
            //get the board in a board format using the lexer
            BoardToSolve = lexer.getBoard(boardText);
            //add some constraints

            //backtracking
            BackUpCells();
            BackTrackSolve(BoardToSolve, 0, 0);

            return BoardToSolve;
        }

        public bool BackTrackSolve(Board board, int row, int col)
        {
            //classic backtracking algorithem

            //we reached the end of the board therfore quit
            if (row == Consts.BOARD_HEIGHT)
            {
                return true;
            }
            //we reached a filled cell therfore skip
            if (board[row, col].isfilled)
            {
                if (col == Consts.BOARD_WIDTH - 1)
                {
                    // move to the next row, since cols are over
                    return BackTrackSolve(board, row + 1, 0);
                }
                else
                {
                    // move to next col
                    return BackTrackSolve(board, row, col + 1);
                }
            }
            //fill in all possibilites until one is solvable
            while (board[row, col].hasPosssibilities)
            {
                if (board[row, col].Guess())
                {
                    if (BackTrackSolve(board, row, col))
                    {
                        return true;
                    }
                    //800000070006010053040600000000080400003000700020005038000000
                    //
                    //430962001900007256006810000040600030012043500058001000100000027000304015500170683
                    //
                }


            }
            //if not solvable reset cell and return false. will return to previous guesser...
            board[row, col].resetVal();
            return false;





        }
    }
}
