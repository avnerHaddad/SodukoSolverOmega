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
        //func that creates a board based on the input
        private void CreateBoard()
        {

        }
        //external func that return the board that the lexer created
        public Board getBoard()
        {
            CreateBoard();
            return board;
        }
    }
}
