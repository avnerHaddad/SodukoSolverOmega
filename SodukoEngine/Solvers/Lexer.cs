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
            board = new Board();
        }
        //func that creates a board based on the input
        private void CreateBoard()
        {
            Board Createdboard = new Board();
            //change the for to a constant later!
            for (int i = 0; i < Consts.BOARD_HEIGHT; i++)
            {
                for (int j = 0; j < Consts.BOARD_WIDTH; j++)
                {
                    if (curVal != '0')
                    {
                        Createdboard[i, j] = new Cell(curVal,i,j);
                    }
                    else
                    {
                        Createdboard[i, j] = new Cell(i,j);

                    }

                    Next();
                }
            }
            Createdboard.setCellPeers();
            board = Createdboard;
            return;

        }

        //advances our iterator over the string and updates the curVal param
        private void Next()
        {
            try
            {
                pos++;
                curVal = boardTxt[pos];
            }
            catch (Exception)
            {
                curVal = '0';
            }


        }
        //external func that return the board that the lexer created

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
