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
        public static int GetDegreeHueristic(Board board, ValueTuple<int, int> cords)
        {
            int count = 0;
            //return the number of empty cells the current cell has in its peers
            foreach (ValueTuple<int, int> peerCords in Board.rowPeers[cords])
            {
                if (!board.cells[peerCords.Item1, peerCords.Item2].Isfilled)
                {
                    count++;
                }

            }
            foreach (ValueTuple<int, int> peerCords in Board.colPeers[cords])
            {
                if (!board.cells[peerCords.Item1, peerCords.Item2].Isfilled)
                {
                    count++;
                }
            }
            foreach (ValueTuple<int, int> peerCords in Board.boxPeers[cords])
            {
                if (!board.cells[peerCords.Item1, peerCords.Item2].Isfilled)
                {
                    count++;
                }
            }
            return count;
        }
        public static List<ValueTuple<int, int>> GetMinPossibilityHueristic(Board board)
        {
            List<ValueTuple<int, int>> LowestPosiibilities = new();
            int minPossibilities = Consts.BOARD_WIDTH;
            //first loop, find the smallest amount of min possibilities
            for (int i = 0; i < Consts.BOARD_HEIGHT; i++)
            {
                for (int j = 0; j < Consts.BOARD_WIDTH; j++)
                {
                    if (board.cells[i, j].Possibilities.Count < minPossibilities && !board.cells[i, j].Isfilled)
                    {

                        minPossibilities = board.cells[i, j].Possibilities.Count;
                    }
                }
            }

            //second loop create a list of those who have it
            for (int i = 0; i < Consts.BOARD_HEIGHT; i++)
            {
                for (int j = 0; j < Consts.BOARD_WIDTH; j++)
                {
                    if (board.cells[i, j].Possibilities.Count == minPossibilities && !board.cells[i, j].Isfilled)
                    {
                        LowestPosiibilities.Add(board.cells[i, j].Cords);
                    }
                }
            }
            return LowestPosiibilities;
        }

        public static List<ValueTuple<int, int>> GetMaxPossibilityHueristic(Board board)
        {
            //return the cells with the maximum amount of possibilities in the board
            //not more than 25 tho because there should be way too much
            List<ValueTuple<int, int>> HighestPosiibilities = new();
            int maxPossibilities = 1;
            //first loop, find the smallest amount of max possibilities
            for (int i = 0; i < Consts.BOARD_HEIGHT; i++)
            {
                for (int j = 0; j < Consts.BOARD_WIDTH; j++)
                {
                    if (board.cells[i, j].Possibilities.Count > maxPossibilities && !board.cells[i, j].Isfilled)
                    {

                        maxPossibilities = board.cells[i, j].Possibilities.Count;
                    }
                }
            }

            //second loop create a list of those who have it
            for (int i = 0; i < Consts.BOARD_HEIGHT; i++)
            {
                for (int j = 0; j < Consts.BOARD_WIDTH; j++)
                {
                    if (board.cells[i, j].Possibilities.Count == maxPossibilities && !board.cells[i, j].Isfilled)
                    {
                        if (HighestPosiibilities.Count == 25)
                        {
                            return HighestPosiibilities;
                        }
                        HighestPosiibilities.Add(board.cells[i, j].Cords);
                    }
                }
            }
            return HighestPosiibilities;
        }


    }
}
