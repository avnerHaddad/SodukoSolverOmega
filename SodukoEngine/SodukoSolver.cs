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


        public SodukoSolver(String boardText)
        {
            //initialise a lexer and an empty board
            lexer = new Lexer();
            BoardToSolve = new Board();
            //get the board in a board format using the lexer
            BoardToSolve = lexer.getBoard(boardText);
            //set up the first constrints
            BoardToSolve.InitialiseConstarints();
        }

        public Board Solve()
        {
            //check if the board is valid
            if (!BoardToSolve.IsValidBoard()) return null;
            //check if solved without resorting to bruteforcing
            if (BoardToSolve.IsSolved())
            {
                return BoardToSolve;
            }
            //start bruteforcing
            return BackTrack(BoardToSolve);
        }
        //backtracking algorithem
        //explanation:
        //1 - get the next cell to guess on using smart hueristics
        //2 - guess and create a new copy of the board after placing it and constraining the new info
        //3 -  check if solved, and return it. else-  check if solvable and go deeper. check if deeper one is 
        //solved and returns it, if reaches a dead end return null
    
        public static Board BackTrack(Board currentState)
        {
            ValueTuple<int, int> NextCell = currentState.GetNextCell();
            foreach (char possibility in BitUtils.ListPossibilities(currentState[NextCell].Possibilities))
            {
                Board newState = currentState.CreateNextMatrix(NextCell.Item1, NextCell.Item2, possibility);
                if (newState.IsSolved())
                {
                    return newState;
                }
                if (newState.IsSolvable())
                {
                    //Console.WriteLine("entering with");
                    //Console.WriteLine(newState.ToString);
                    Board deepState = BackTrack(newState);
                    //Console.WriteLine("exited with");
                    //if(deepState != null){Console.WriteLine(deepState.ToString);}
                    if (deepState != null && deepState.IsSolved())
                    {
                        return deepState;
                    }
                }
            }
            return null;
        }

 
    }
}