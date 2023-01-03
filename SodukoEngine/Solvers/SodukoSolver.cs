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
            //BackUpCells();
            BackTrackSolve(0, 0);
            return BoardToSolve;
        }

        public bool BackTrackSolve(int row, int col)
        {
            Board backUpBoard = new Board(BoardToSolve);
            backUpBoard.setCellPeers();
            //classic backtracking algorithem

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
                    return BackTrackSolve(row + 1, 0);
                }
                else
                {
                    // move to next col
                    return BackTrackSolve(row, col + 1);
                }
            }
            //fill in all possibilites until one is solvable
            while (BoardToSolve[row, col].hasPosssibilities)
            {
                if (BoardToSolve[row, col].Guess())
                {
                    if (BackTrackSolve(row, col))
                    {
                        return true;
                    }

                }


            }
            //if not solvable reset cell and return false. will return to previous guesser...
            //board[row, col].resetVal();
            BoardToSolve = backUpBoard;
            return false;





        }
    }
}
