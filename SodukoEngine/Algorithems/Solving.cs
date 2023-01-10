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
        public static Board BackTrack(Board currentState)
        {
            ValueTuple<int, int> NextCell = currentState.GetNextCell();
            foreach (char possibility in currentState[NextCell.Item1, NextCell.Item2].Possibilities)
            {
                Board newState = currentState.CreateNextMatrix(NextCell.Item1, NextCell.Item2, possibility);
                if (newState.isSolved())
                {
                    return newState;
                }
                if (newState.isSolvable())
                {
                    Board deepState = BackTrack(newState);
                    if (deepState != null && deepState.isSolved())
                    {
                        return deepState;
                    }
                }
            }
            return null;
        }


    }
}
