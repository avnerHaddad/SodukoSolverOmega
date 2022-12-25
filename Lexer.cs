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
        private iterator iterator;
        //create an iterator for the string

        public Lexer(string inputTxt)
        {
            boardTxt = inputTxt; ;
            board = new Board();
            //initialise iterator later
        }

        private void CreateBoard()
        {

        }
        public Board getBoard()
        {
            CreateBoard();
            return board;
        }
    }
}
