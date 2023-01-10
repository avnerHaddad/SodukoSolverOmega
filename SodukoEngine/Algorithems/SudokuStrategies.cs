using SodukoSolverOmega.SodukoEngine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolverOmega.SodukoEngine.Algorithems
{
    internal static class SudokuStrategies
    {


        public static void HiddenSingles(Board board, ValueTuple<int, int> cell)
        {
            //check the cell row
            //check the cell box
            //check the cell col
            //for each group count the number of cells with the possibiity from cell possibilities?
            //if missiing llok for him?
            //worst case 3n^2
            foreach (char possibility in board.cells[cell.Item1, cell.Item2].Possibilities)
            {
                if (!HelperFuncs.ExsistInRowPeers(board, cell, possibility))
                {
                    HelperFuncs.FixCell(board,cell, possibility);
                    return;
                }
                if (!HelperFuncs.ExsistInColPeers(board, cell, possibility))
                {
                    HelperFuncs.FixCell(board,cell, possibility);
                    return;
                }
                if (!HelperFuncs.ExsistInBoxPeers(board, cell, possibility))
                {
                    HelperFuncs.FixCell(board, cell, possibility);
                    return;
                }
            }
        }

        //removes possibilities for pairs that are lonely with 2 possibilities in a group
        public static void HiddenPairs()
        {
            //if  2 cells in a group contain the same 2 possibilities that do not exsist anywhere else in the group
            //remove all of thier other possibilities
            //remove the 2 possibilities from the second group they are both in, ie if in same row remove from box etc

            //algorithem
            //scan the board
            //check for each cell if it ha

        }
        public static void NakedSingles(Board board, ValueTuple<int, int> cords)
        {
            //check for hidden singles and set them vals
            if (board.cells[cords.Item1, cords.Item2].Possibilities.Count == 1 && !board.cells[cords.Item1, cords.Item2].isfilled)
            {
                HelperFuncs.fixCellHidden(board, cords);
            }

        }
    }
}
