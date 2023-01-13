using SodukoSolverOmega.Configuration.Consts;
using SodukoSolverOmega.SodukoEngine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolverOmega.SodukoEngine.Algorithems
{
    internal static class BacktrackingHueristics
    {
        //todo
        //add a hueristic that chooses the possibility with the most influence to guess with?

        //return the number of empty cells the current cell has in its peers,
        //used to get the cell that has the most influence when picking a next cell
        public static int GetDegreeHueristic(Board board, ValueTuple<int, int> cords)
        {
            return HelperFuncs.GetUnfilledCells(board, Board.cellPeers[cords]).Count;
        }
        //return a list of cells with the minimum amount of posssiblities,
        //used to pick the least risky next cell
        public static List<ValueTuple<int, int>> GetMinPossibilityHueristic(Board board)
        {
            List<ValueTuple<int, int>> LowestPosiibilities = new();
            int minPossibilities = Consts.BOARD_SIZE;
            //first loop, find the smallest amount of min Possibilities
            for (int i = 0; i < Consts.BOARD_SIZE; i++)
            {
                for (int j = 0; j < Consts.BOARD_SIZE; j++)
                {
                    if (board.cells[i, j].Possibilities.Count < minPossibilities && !board.cells[i, j].Isfilled)
                    {

                        minPossibilities = board.cells[i, j].Possibilities.Count;
                    }
                }
            }

            //second loop create a list of those who have it
            for (int i = 0; i < Consts.BOARD_SIZE; i++)
            {
                for (int j = 0; j < Consts.BOARD_SIZE; j++)
                {
                    if (board.cells[i, j].Possibilities.Count == minPossibilities && !board.cells[i, j].Isfilled)
                    {
                        LowestPosiibilities.Add(board.cells[i, j].Cords);
                    }
                }
            }
            return LowestPosiibilities;
        }
    }
}
