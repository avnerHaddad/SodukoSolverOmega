using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolverOmega
{
    internal class Lexer
    {
        private Board board;
        private string boardTxt;
        private int pos;
        private int curVal;


        public Lexer(string inputTxt)
        {
            boardTxt = inputTxt; ;
            board = new Board();
            pos = 0;
            curVal = boardTxt[pos];
        }
        //func that creates a board based on the input
        private void CreateBoard()
        {
            Board Createdboard = new Board();
            //change the for to a constant later!
            for(int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    Createdboard[i, j] = new Cell(curVal);
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
            pos++;
            curVal = boardTxt[pos];
        }
        //external func that return the board that the lexer created
        public Board getBoard()
        {
            CreateBoard();
            return board;
        }
    }
}
