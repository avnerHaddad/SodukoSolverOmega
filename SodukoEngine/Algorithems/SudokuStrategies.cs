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
        public static void NakedSingles(Board board, ValueTuple<int, int> cords)
        {
            //check for hidden singles and set them vals
            if (board.cells[cords.Item1, cords.Item2].Possibilities.Count == 1 && !board.cells[cords.Item1, cords.Item2].Isfilled)
            {
                HelperFuncs.FixCellHidden(board, cords);
            }

        }


       //confirms the hidden suspects are all in the same cells, if yes than call HiddenTupples to eliminate possibilites
       //if they are also aligned in another group than func will call intersection removal to remove more possibilities

        public static void ConfirmHidden(Board board, List<char> FoundInGroup, ValueTuple<int, int> cords)
        {
            List<ValueTuple<int, int>> hiddenCells = HelperFuncs.AllCellsWithPossibility(board, Board.rowPeers[cords], FoundInGroup[0]);
            bool isInTheSameCell = true;
            //check if all peers connecting are in the same cell
            foreach (ValueTuple<int, int> cell in hiddenCells)
            {
                for (int i = 1; i < FoundInGroup.Count; i++)
                {
                    if (!board.cells[cell.Item1, cell.Item2].Possibilities.Contains(FoundInGroup[i]))
                    {
                        isInTheSameCell = false; break;
                    }
                }

            }
            if (isInTheSameCell)
            {
                //do the stuff
                //remove all other possibilities from all of them
                //ie set thier possibilities to - found in row!
                //enter all of these cells to the effectedQueue?
                foreach (ValueTuple<int, int> cell in hiddenCells)
                {
                    board.cells[cell.Item1, cell.Item2].Possibilities = FoundInGroup;
                    board.EffectedSet.Remove(cell);
                }
            }
        }

        //function check to see if cell might be a part of a HiddenPair.triple etc, if suspect calls the confoirm hidden
        public static void HasTuples(Board board, ValueTuple<int, int> cords, int amount)
        {
            int rowCount = 0, colCount = 0, boxCount = 0;
            List<char> FoundInRow = new List<char>();
            List<char> FoundInCol = new List<char>();
            List<char> FoundInBox = new List<char>();
            foreach (char possibility in board.cells[cords.Item1, cords.Item2].Possibilities)
            {
                if (HelperFuncs.PossibilityCountInPeers(board, Board.rowPeers[cords], possibility) == amount - 1)
                {
                    //found!
                    rowCount++;
                    FoundInRow.Add(possibility);
                }
                if (HelperFuncs.PossibilityCountInPeers(board, Board.colPeers[cords], possibility) == amount - 1)
                {
                    //hiddden amount found!
                    colCount++;
                    FoundInCol.Add(possibility);
                }
                if (HelperFuncs.PossibilityCountInPeers(board, Board.boxPeers[cords], possibility) == amount - 1)
                {
                    //hiddden amount found!
                    boxCount++;
                    FoundInBox.Add(possibility);
                }
            }
            if (rowCount == amount)
            {
                ConfirmHidden(board, FoundInRow,cords);
                
            }
            if (colCount == amount)
            {
                ConfirmHidden(board, FoundInCol,cords);

            }
            if (boxCount == amount)
            {
                ConfirmHidden(board, FoundInBox,cords);

            }
        }


        public static void NakedCells(Board board, ValueTuple<int, int> cords)
        {
            NakedSingles(board, cords);
            NakedCanidates(board, cords, 2);
            NakedCanidates(board, cords, 3);
            NakedCanidates(board, cords, 4);
        }

        public static void NakedCanidates(Board board, ValueTuple<int, int> cords, int amount)
        {
            if (board.Cells[cords.Item1,cords.Item2].Possibilities.Count != amount)
            {
                return;
                //dont bother checking, dont waste time
            }
            //go over all peers, if a 'amount' peers are a subList of this cells possibilities
            //this is a naked'amount'
            List<List<ValueTuple<int, int>>> peerGroups = new();
            peerGroups.Add(Board.rowPeers[cords]);
            peerGroups.Add(Board.colPeers[cords]);
            peerGroups.Add(Board.boxPeers[cords]);
            foreach(List<ValueTuple<int, int>> peerGroup in peerGroups)
            {
                List<ValueTuple<int, int>> subPossibilities = HelperFuncs.SubPossibilities(board, peerGroup, board.cells[cords.Item1, cords.Item2].Possibilities);
                if (subPossibilities.Count != amount - 1) { return; }
                List<ValueTuple<int, int>> NonTupled = peerGroup;
                NonTupled.Except(subPossibilities);
                foreach (ValueTuple<int, int> valueTuple in NonTupled)
                {
                    board.cells[valueTuple.Item1, valueTuple.Item2].Possibilities.Except(board.cells[cords.Item1, cords.Item2].Possibilities);
                    board.EffectedSet.Add(valueTuple);
                }
            }
            

        }
        public static void InterSectionRemoval()
        {
            return;
        }

    }
}
