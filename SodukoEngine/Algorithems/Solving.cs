using SodukoSolverOmega.SodukoEngine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolverOmega.SodukoEngine.Algorithems
{
    internal static class Solving
    {
        //backtracking algorithem
        //explanation:
        //1 - get the next cell to guess on using smart hueristics
        //2 - guess and create a new copy of the board after placing it and constraining the new info
        //3 -  check if solved, and return it. else-  check if solvable and go deeper. check if deeper one is 
        //solved and returns it, if reaches a dead end return null
    
        public static Board BackTrack(Board currentState)
        {
            ValueTuple<int, int> NextCell = currentState.GetNextCell();
            foreach (char possibility in currentState[NextCell].Possibilities)
            {
                Board newState = currentState.CreateNextMatrix(NextCell.Item1, NextCell.Item2, possibility);
                if (newState.IsSolved())
                {
                    return newState;
                }
                if (newState.IsSolvable())
                {
                    Board deepState = BackTrack(newState);
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
