using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolverOmega
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
            

            return BoardToSolve;
        }
    }
}
