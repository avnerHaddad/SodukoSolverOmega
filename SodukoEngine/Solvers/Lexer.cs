using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SodukoSolverOmega.SodukoEngine.Objects;
using SodukoSolverOmega.Configuration.Consts;

namespace SodukoSolverOmega.SodukoEngine.Solvers
{
    internal class Lexer
    {
        private Board board;
        private string boardTxt;
        private int pos;
        private char curVal;


        public Lexer()
        {
            //create a new lexer with an empty board to initialise
            board = new Board();
        }
        //func that initialises the board based on the input, iterates over Boardtxt and creates a board
        private void CreateBoard()
        {
            for (int i = 0; i < Consts.BOARD_SIZE; i++)
            {
                for (int j = 0; j < Consts.BOARD_SIZE; j++)
                {
                    //if value is 0 create an empty cell
                    board[i, j] = curVal == '0' ? new Cell(i, j) : board[i, j] = new Cell(curVal, i, j);
                    Next();
                }
            }
            return;
        }

        //advances our iterator over the string and updates the curVal param
        private void Next()
        {
            try
            {
                //iterate to the next char
                pos++;
                curVal = boardTxt[pos];
            }
            catch (Exception)
            {
                //if reached end of the string then fill the rest of the board with 0
                curVal = '0';
            }


        }
        //public func to get a board from the lexer
        public Board getBoard(string inputTxt)
        {
            boardTxt = inputTxt;
            pos = 0;
            curVal = boardTxt[pos];
            CreateBoard();
            return board;
        }
    }
}
