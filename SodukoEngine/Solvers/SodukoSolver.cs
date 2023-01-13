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
            //initialise a lexer and an empty board
            lexer = new Lexer();
            BoardToSolve = new Board();
        }

        public Board Solve(string boardText)
        {
            //get the board in a board format using the lexer
            BoardToSolve = lexer.getBoard(boardText);
            //check if the board is valid
            if (BoardToSolve.IsValidBoard())
            {
                //set up the first constrints
                BoardToSolve.InitialiseConstarints();
                //check if solved without resorting to bruteforcing
                if (BoardToSolve.IsSolved())
                {
                    return BoardToSolve;
                }
                else
                {
                    //start bruteforcing
                    return Solving.BackTrack(BoardToSolve);

                }
            }
            return null;
        }

 
    }
}